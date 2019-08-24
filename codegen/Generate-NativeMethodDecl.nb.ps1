#!Notebook v1

#!CommandLine
Set-StrictMode -Version Latest
#!Output
# 
#!CommandLine
cd $HOME\work\mxnet
#!Output
# 
#!CommandLine
$xml = [xml](cat codegen\source\c__api_8h.xml)
#!Output
# 
#!CommandLine
$memberdefs = $xml.doxygen.SelectNodes("//memberdef[@kind=`"function`"]")
#!Output
# 
#!CommandLine
$Indent = "    "
$Indent2 = $Indent * 2
$Indent3 = $Indent * 3
$Indent4 = $Indent * 4

$HeaderBase = @"
using System;
using System.Runtime.InteropServices;

using AtomicSymbolCreator = System.IntPtr;
using DataIterCreator = System.IntPtr;
using DataIterHandle = System.IntPtr;
using ExecutorHandle = System.IntPtr;
using FunctionHandle = System.IntPtr;
using NDArrayHandle = System.IntPtr;
using ProfileHandle = System.IntPtr;
using SymbolHandle = System.IntPtr;
using size_t = System.Int64;
using int64_t = System.Int64;
using uint64_t = System.Int64;
using mx_int = System.Int32;
using mx_uint = System.Int32;
using mx_int64 = System.Int64;
using mx_uint64 = System.Int64;
using mx_float = System.Single;
using ExecutorMonitorCallback = System.IntPtr;

namespace Horker.MXNet.Core
{
    public static class <ClassName>
    {
"@

$FooterBase = @"
    }
}
"@

$DeclarationHeader = $HeaderBase -Replace "<ClassName>", "CApiDeclaration"
$DeclarationFooter = $FooterBase

$WrapperHeader = $HeaderBase -Replace "<ClassName>", "CApi"
$WrapperFooter = $FooterBase

$DeclarationTemplate = @"
        {5}
        [DllImport("libmxnet.dll", EntryPoint = "{0}")]
        public static extern {1} {2}( // {3}
{4}        );

"@

$WrapperTemplate = @"
        public static void {0}(
{2}        )
        {{
            var resultCode = CApiDeclaration.{1}({3});
            if (resultCode != 0)
            {{
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }}
        }}

"@

$TypeMap = @{
    "const char *" = "string"
    "const char*" = "string"
    "const char **" = "string[]"
    "const char**" = "string[]"
    "char *" = "string"
    "char*" = "string"
    "char **" = "string[]"
    "char**" = "string[]"
    "int" = "int"
    "AtomicSymbolCreator" = "AtomicSymbolCreator"
    "DataIterCreator" = "DataIterCreator"
    "DataIterHandle" = "DataIterHandle"
    "ExecutorHandle" = "ExecutorHandle"
    "FunctionHandle" = "FunctionHandle"
    "NDArrayHandle" = "NDArrayHandle"
    "ProfileHandle" = "ProfileHandle"
    "SymbolHandle" = "SymbolHandle"
    "size_t" = "size_t"
    "uint64_t" = "uint64_t"
    "mx_uint" = "mx_uint"
    "mx_float" = "mx_float"
    "const int *" = "int[]"
    "const int*" = "int[]"
    "const int64_t *" = "int64_t[]"
    "const int64_t*" = "int64_t[]"
    "const mx_int *" = "mx_int[]"
    "const mx_int*" = "mx_int[]"
    "const mx_int64 *" = "mx_int64[]"
    "const mx_int64*" = "mx_int64[]"
    "const mx_uint *" = "mx_uint[]"
    "const mx_uint*" = "mx_uint[]"
    "const mx_uint64 *" = "mx_uint64[]"
    "const mx_uint64*" = "mx_uint64[]"
}

function Get-InnerText {
    param(
        $m
    )
    if ($m -is [Xml.XmlElement]) {
        $m.InnerText
    }
    else {
        $m.ToString()
    }
}

function Get-ParamTypeAndName {
    param(
        [Xml.XmlElement]$e
    )
    if ($e.declname -eq "DEFAULT") {
        $null = (get-innertext $e.type) -match "(.*[^a-zaA-Z0-9_])([a-zA-Z0-9_]+)"
        $type = $matches[1]
        $name = $matches[2]
    }
    else {
        $type = Get-InnerText $e.type
        $name = Get-InnerText $e.declname
    }

    $type = $type.Trim()
    $name = $name.Trim()
    if ($name -eq "out") {
        $name = "@out"
    }

    $type, $name
}

function Get-ParameterPrefix {
    param(
        [string]$Type,
        [string]$Name
    )

    if ($Name -eq "") {
        # Not a parameter, but a return value
        return ""
    }

    if ($Type -eq "void *") {
        return ""
    }

    if ($Name -match "outputs") {
        return "ref "
    }

    if ($Name.StartsWith("param_") -or $Name -match "input") {
        return ""
    }

    if ($Type.EndsWith("**") -or
        ($Type.EndsWith("*") -and -not $Type.StartsWith("const "))) {
        return "out "
    }

    return ""
}
    
function Replace-ParamType {
    param(
        [string]$Type,
        [string]$Name
    )

    $prefix = Get-ParameterPrefix $Type $Name
    if (-not [string]::IsNullOrEmpty($prefix)) {
        $Type = ($Type -replace "\s*\*$", "")
    }

    if ($Type -eq "NDArrayHandle *" -and $prefix -eq "ref ") {
        # Needs pinning
        $newType = "IntPtr"
    }
    if ($Type.EndsWith("*") -and $prefix -eq "out " -and $Type -notmatch "char") {
        # Needs to consider the array size
        $newType = "IntPtr"
    }
    else {
        $newType = $TypeMap[$Type]
        if ($null -eq $newType) {
            $newType = "IntPtr"
        }
    }

    return $prefix + $newType
}

function Get-ParamList {
    param(
        [Xml.XmlElement]$m
    )

    $params = $m.SelectNodes("param")

    if ($null -eq $params) {
        return ""
    }

    if ($params -is [Xml.XmlElement]) {
        $params = @($params)
    }

    $b = New-Object Text.StringBuilder
    for ($i = 0; $i -lt $params.Count; ++$i) {
        $p = $params[$i]
        $type, $name = Get-ParamTypeAndName $p
        $newType = Replace-ParamType $type $name
        if ($name -eq "out") {
            $name = "@out"
        }

        if ($i -lt $params.Count - 1) {
            $name += ","
        }

        [void]$b.AppendFormat("{0}{1,-20} {2,-20}  // {3}`r`n", $Indent3, $newType, $name, $type)
    }
    $b.ToString()
}

function Get-ArgumentList {
    param(
        [Xml.XmlElement]$m
    )

    $params = $m.SelectNodes("param")
    ($params | foreach {
        $type, $name = Get-ParamTypeAndName $_
        $prefix = Get-ParameterPrefix $type $name
        $prefix + $name
    }) -join ", "
}    

function Get-Declaration {
    param(
        [Xml.XmlElement]$m
    )

    $name = $m.name
    $returnType = $m.type.innertext.replace("MXNET_DLL ", "").Trim()
    $newReturnType = Replace-ParamType $returnType ""
    $params = Get-ParamList $m
    $DeclarationTemplate -f $name, $newReturnType, $name, $returnType, $params
}

function Get-DeclarationFile {

    $DeclarationHeader

    foreach ($m in $memberdefs) {
        Get-Declaration $m
    }

    $DeclarationFooter
}

function Get-WrapperMethod {
    param(
        [Xml.XmlElement]$m
    )

    $name = $m.name
    $params = Get-ParamList $m
    $args = Get-ArgumentList $m
    $WrapperTemplate -f $name, $name, $params, $args
}

function Get-WrapperFile {

    $WrapperHeader

    foreach ($m in $memberdefs) {
        $returnType = $m.Type.InnerText.Replace("MXNET_DLL ", "")
        if ($returnType -ne "int") {
            continue
        }

        get-WrapperMethod $m
    }

    $WrapperFooter
}
#!Output
# 
#!CommandLine
Get-DeclarationFile | set-content "source\Horker.MXNet.Core\Generated\CApiDeclaration.cs"
#!Output
# 
#!CommandLine
Get-WrapperFile | Set-Content "source\Horker.MXNet.Core\Generated\CApi.cs"
#!Output
# 
#!CommandLine
#!Output
# 
