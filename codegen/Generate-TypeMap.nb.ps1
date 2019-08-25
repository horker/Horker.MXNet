#!Notebook v1

#!CommandLine
Set-StrictMode -Version Latest
#!Output
# 
#!CommandLine
cd ~\work\mxnet\codegen
#!Output
# 
#!CommandLine

#!Output
# 
#!CommandLine
#####################################################
# Load predefined prefix map
#####################################################

$p = import-csv -enc utf8 predefined_prefixes.csv
$predefinedOutParamMap = @{}
$p | group Name | foreach {
    $predefinedOutParamMap[$_.Name] = $_.Group
}
$predefinedOutParamMap
#!Output
# 
# Name                           Value                                                           
# ----                           -----                                                           
# MXGetGPUMemoryInformation64    {@{Name=MXGetGPUMemoryInformation64; ParamName=free_mem; Pref...
# MXFuncGetInfo                  {@{Name=MXFuncGetInfo; ParamName=name; Prefix=out}, @{Name=MX...
# MXFuncDescribe                 {@{Name=MXFuncDescribe; ParamName=num_use_vars; Prefix=out}, ...
# MXGetGPUMemoryInformation      {@{Name=MXGetGPUMemoryInformation; ParamName=free_mem; Prefix...
# MXSymbolGetAtomicSymbolName    {@{Name=MXSymbolGetAtomicSymbolName; ParamName=name; Prefix=o...
# MXImperativeInvokeEx           {@{Name=MXImperativeInvokeEx; ParamName=num_outputs; Prefix=r...
# MXDataIterGetIterInfo          {@{Name=MXDataIterGetIterInfo; ParamName=name; Prefix=out}, @...
# MXSymbolGetInputSymbols        {@{Name=MXSymbolGetInputSymbols; ParamName=inputs; Prefix=out}} 
# MXSymbolInferShapePartialEx64  {@{Name=MXSymbolInferShapePartialEx64; ParamName=complete; Pr...
# MXSymbolInferShapeEx64         {@{Name=MXSymbolInferShapeEx64; ParamName=complete; Prefix=out}}
# MXSymbolInferType              {@{Name=MXSymbolInferType; ParamName=complete; Prefix=out}}     
# MXSymbolGetAtomicSymbolInfo    {@{Name=MXSymbolGetAtomicSymbolInfo; ParamName=name; Prefix=o...
# MXSymbolInferShape64           {@{Name=MXSymbolInferShape64; ParamName=complete; Prefix=out}}  
# MXImperativeInvoke             {@{Name=MXImperativeInvoke; ParamName=num_outputs; Prefix=ref...
# MXSymbolGetNumOutputs          {@{Name=MXSymbolGetNumOutputs; ParamName=output_count; Prefix...
# MXSymbolInferTypePartial       {@{Name=MXSymbolInferTypePartial; ParamName=complete; Prefix=...
# MXDataIterGetPadNum            {@{Name=MXDataIterGetPadNum; ParamName=pad; Prefix=out}}        
# MXSymbolGetName                {@{Name=MXSymbolGetName; ParamName=success; Prefix=out}}        
# MXGenBackendSubgraph           {@{Name=MXGenBackendSubgraph; ParamName=ret_sym_handle; Prefi...
# MXSymbolGetAttr                {@{Name=MXSymbolGetAttr; ParamName=success; Prefix=out}}        
# MXSymbolCutSubgraph            {@{Name=MXSymbolCutSubgraph; ParamName=inputs; Prefix=out}}     
# MXSymbolInferShapeEx           {@{Name=MXSymbolInferShapeEx; ParamName=complete; Prefix=out}}  
# MXSymbolInferShapePartialEx    {@{Name=MXSymbolInferShapePartialEx; ParamName=complete; Pref...
# MXGenAtomicSymbolFromSymbol    {@{Name=MXGenAtomicSymbolFromSymbol; ParamName=ret_sym_handle...
# MXSymbolInferShapePartial      {@{Name=MXSymbolInferShapePartial; ParamName=complete; Prefix...
# MXSymbolInferShapePartial64    {@{Name=MXSymbolInferShapePartial64; ParamName=complete; Pref...
# 
# 
# 
#!CommandLine
###################################################################
# Fucntion to infer parameter prefixes (out/ref)
###################################################################

function Infer-Out {
    param(
        $d
    )

    if (-not $d.ParamType.EndsWith("*")) {
        return ""
    }

    $pre = $predefinedOutParamMap[$d.Name]
    if ($null -ne $pre) {
        foreach ($p in $pre) {
            if ($p.ParamName -eq $d.ParamName) {
                return $p.Prefix
            }
        }
    }

    # Infer out/ref by heuristics

    if ($d.ParamName -eq "out" -or
        $d.ParamName.StartsWith("out_") -or
        $d.ParamDescription -match "returning" -or
        $d.ParamDescription -match "returns" -or
        $d.ParamDescription -match "returned" -or
        $d.ParamDescription.StartsWith("prev_")) {
        return "out"
    }

    return ""
}
#!Output
# 
#!CommandLine
###################################################################
# Fucntion to infer CLR types corresponding to types in the C API
###################################################################

$TypeMap = @{
    "const char*" = "string"
    "const char**" = "string[]"
    "char*" = "string"
    "char**" = "string[]"
    "const int*" = "int[]"
    "const int64_t*" = "int64_t[]"
    "const mx_int*" = "mx_int[]"
    "const mx_int64*" = "mx_int64[]"
    "const mx_uint*" = "mx_uint[]"
    "const mx_uint64*" = "mx_uint64[]"
    "NDArrayHandle*" = "NDArrayHandle[]"
}

function Infer-ClrType {
    param(
        [string]$Prefix,
        [string]$BaseType
    )

    $BaseType = $BaseType -Replace "\s+\*", "*"
    $BaseType = $BaseType -Replace "\s+\*", "*"

    if ([string]::IsNullOrEmpty($Prefix)) {
        $newType =  $TypeMap[$BaseType]
        if ($null -ne $newType) {
            return $newType
        }
    }

    if ($BaseType.EndsWith("*")) {
        $newType = "IntPtr"
    }
    else {
        $newType = $baseType -Replace "^const\s+", ""
    }

    $newType
}  
#!Output
# 
#!CommandLine
###################################################################
# Get inner text from XML node
###################################################################

function Get-InnerText {
    param(
        $node
    )

    if ($null -eq $node) {
        return ""
    }

    if ($node -is [string]) {
        return $node
    }

    $node.InnerText
}
#!Output
# 
#!CommandLine
###################################################################
# Get parameter descirption from XML node of function
###################################################################

function Get-ParamDescription {
    param(
        $node,
        [string]$paramName
    )

    $n = $node.SelectSingleNode("detaileddescription//parameteritem[//parametername='$paramName']/parameterdescription")
    Get-InnerText $n
}
#!Output
# 
#!CommandLine
$doc = [xml](cat -enc utf8 source\c__api_8h.xml)
$memberdefs = $doc.doxygen.SelectNodes("//memberdef[@kind=`"function`"]")
#!Output
# 
#!CommandLine
###################################################################
# Generate the parameter type map of the C API
###################################################################

$decls = new-object System.Collections.Generic.List[PSObject]

foreach ($m in $memberdefs) {

#    $m | fc

    $name = Get-InnerText $m.name
    $type = Infer-ClrType "" ((Get-InnerText $m.type) -Replace "^MXNET_DLL\s+", "")
    $briefDesc = Get-InnerText $m.briefdescription
    $detailedDesc = Get-InnerText $m.detaileddescription

    if ($null -eq $m.SelectSingleNode("param") -or @($m.param).Count -eq 0) {
        $d = [PSCustomObject]@{
            Name = $name
            ReturnType = $type
            BriefDescription = $briefDesc
            DetailedDescription = $detailedDesc
            ParamType = ""
            ParamName = ""
            ParamDescription = ""
            infer_prefix = ""
            infer_baseType = ""
            infer_clrType = ""
        }
        $decls.Add($d)
        continue
    }

    foreach ($p in $m.param) {
        $paramType = Get-InnerText $p.type
        $paramName = Get-InnerText $p.declname

        if ($paramName -eq "DEFAULT") {
            $words = ($paramType.Trim()) -split "\s+|(\*)"
            $paramType = $words[0..($words.Length - 2)] -join " "
            $paramName = $words[$words.Length - 1]
        }
        $paramType = $paramType -Replace "\s+\*", "*"
        $paramType = $paramType -Replace "\s+\*", "*"

        [string]$paramDesc = Get-ParamDescription $m $paramName

        $d = [pscustomobject]@{
            Name = $name
            ReturnType = $type
            BriefDescription = $briefDesc
            DetailedDescription = $detailedDesc
            ParamType = $paramType
            ParamName = $paramName
            ParamDescription = $paramDesc
        }

#        $d

        $prefix = Infer-Out $d

        if (-not [string]::IsNullOrEmpty($prefix)) {
            $baseType = $d.ParamType -Replace "\*$", ""
        }
        else {
            $baseType = $d.ParamType
        }

        $clrType = Infer-ClrType $prefix $baseType

        $d | Add-Member infer_prefix $prefix
        $d | Add-Member infer_baseType $baseType
        $d | Add-Member infer_clrType $clrType

        $decls.Add($d)
    }
}

# Display the first 5 results
$decls | select -f 5 | fl
#!Output
# 
# 
# Name                : MXGetLastError
# ReturnType          : string
# BriefDescription    : return str message of the last error all function in this file will retur
#                       n 0 when success and -1 when an error occured, MXGetLastError can be call
#                       ed to retrieve the error 
# DetailedDescription : this function is threadsafe and can be called by different thread error i
#                       nfo 
# ParamType           : 
# ParamName           : 
# ParamDescription    : 
# infer_prefix        : 
# infer_baseType      : 
# infer_clrType       : 
# 
# Name                : MXLoadLib
# ReturnType          : int
# BriefDescription    : Load library dynamically. 
# DetailedDescription : pathto the library .so file 0 when success, -1 when failure happens. 
# ParamType           : const char*
# ParamName           : path
# ParamDescription    : to the library .so file 
# infer_prefix        : 
# infer_baseType      : const char*
# infer_clrType       : string
# 
# Name                : MXLibInfoFeatures
# ReturnType          : int
# BriefDescription    : Get list of features supported on the runtime. 
# DetailedDescription : libFeaturepointer to array of LibFeaturesizeof the array 0 when success, 
#                       -1 when failure happens. 
# ParamType           : const struct LibFeature**
# ParamName           : libFeature
# ParamDescription    : pointer to array of LibFeature
# infer_prefix        : 
# infer_baseType      : const struct LibFeature**
# infer_clrType       : IntPtr
# 
# Name                : MXLibInfoFeatures
# ReturnType          : int
# BriefDescription    : Get list of features supported on the runtime. 
# DetailedDescription : libFeaturepointer to array of LibFeaturesizeof the array 0 when success, 
#                       -1 when failure happens. 
# ParamType           : size_t*
# ParamName           : size
# ParamDescription    : pointer to array of LibFeature
# infer_prefix        : 
# infer_baseType      : size_t*
# infer_clrType       : IntPtr
# 
# Name                : MXRandomSeed
# ReturnType          : int
# BriefDescription    : Seed all global random number generators in mxnet. 
# DetailedDescription : seedthe random number seed. 0 when success, -1 when failure happens. 
# ParamType           : int
# ParamName           : seed
# ParamDescription    : the random number seed. 
# infer_prefix        : 
# infer_baseType      : int
# infer_clrType       : int
# 
# 
# 
# 
#!CommandLine
###################################################################
# Save C API type map to file
###################################################################

$decls | Export-Csv -enc utf8 -NoTypeInfo api_type_map.csv
start api_type_map.csv
#!Output
# 
#!CommandLine
#!Output
# 
