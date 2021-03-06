Set-StrictMode -Version Latest

$source = "$PSScriptRoot\source\ops.json"
$opOutFile = "$PSScriptRoot\..\source\Horker.MXNet\Operators\gen_Op.cs"
$ndArrayMethodsOutFile = "$PSScriptRoot\..\source\Horker.MXNet\Core\gen_NDArrayMethods.cs"

############################################################
# ConvertTo-CamelCase function
############################################################

$cultureInfo = [Threading.Thread]::CurrentThread.CurrentCulture
$textInfo = $cultureInfo.TextInfo

function ConvertTo-CamelCase {
    param(
        [string]$Name,
        [switch]$Pascal
    )

    $builder = New-Object Text.StringBuilder

    $i = 0

    while ($Name[$i] -eq '_') {
        ++$i
    }

    if ($Pascal) {
        $null = $builder.Append([char]::ToUpper($Name[$i]))
    }
    else {
        $null = $builder.Append($Name[$i])
    }

    for (++$i; $i -lt $Name.Length; ++$i) {
        if ($Name[$i] -eq '_') {
            ++$i
            $null = $builder.Append([char]::ToUpper($Name[$i]))
        }
        else {
            $null = $builder.Append($Name[$i])
        }
    }

    $builder.ToString()
}

############################################################
# Test-Omittable function
############################################################

function Test-Omittable {
    param(
        [string]$ArgTypeInfos
    )

    $ArgTypeInfos -match "default\s*=\s*'?None'?"
}

############################################################
# Test-Input function
############################################################

function Test-Input {
    param(
        [string]$ArgTypeInfos,
        [switch]$NoSymbol
    )

    if ($NoSymbol) {
        $ArgTypeInfos -match "NDArray"
    }
    else {
        $ArgTypeInfos -match "NDArray|Symbol"
    }
}

############################################################
# Convert-Type function
############################################################

$TypeMap = @{
    "boolean" =  "bool"
    "boolean or None" =  "bool?"
    "double" =  "double"
    "double or None" =  "double?"
    "float" =  "double"
    "float or None" =  "double?"
    "int" =  "int"
    "int (non-negative)" =  "int"
    "int or None" =  "int?"
    "long (non-negative)" =  "long"
    "NDArray" =  "NDArray"
    "NDArray-or-Symbol" =  "NDArrayOrSymbol"
    "NDArray-or-Symbol[]" =  "NDArrayOrSymbol[]"
    "ptr" =  "IntPtr"
    "real_t" =  "double"
    "Shape or None" =  "NDShape"
    "Shape(tuple)" =  "NDShape"
    "string" =  "string"
    "Symbol" =  "Symbol"
    "Symbol or Symbol[]" =  "Symbol[]"
    "" = "double?" # experimental
}

$EnumMap = @{
    "act_type" = "ActType"
    "blank_label" = "BlankLabelType"
    "cudnn_tune" = "CuDNNTuneType"
    "dtype" = "DType"
    "format" ="FormatType"
    "forward_stype" = "SType"
    "in_format" = "FormatType"
    "layout" = "LayoutType"
    "multi_input_mode" = "MultiInputType"
    "normalization" = "NormalizationType"
    "out_dtype" = "DType"
    "out_format" = "FormatType"
    "out_type" = "DType"
    "pool_type" = "PoolType"
    "pooling_type_convention" = "PoolingTypeConvention"
    "ret_typ" = "RetType"
    "sample_type" = "SampleType"
    "sampler_type" = "SampleType"
    "stype" = "SType"
    "transform_type" = "TransformType"
}

function Convert-TypeName {
    param(
        [string]$OpName,
        [string]$ArgName,
        [string]$ArgType
    )

    if ($ArgType.StartsWith("{")) {
        if ($ArgName -eq "act_type" -and $OpName -eq "LeakyReLU") {
            return "LeakyReLUActType"
        }
        if ($EnumMap.ContainsKey($ArgName)) {
            return $EnumMap[$ArgName]
        }
        return "string"
    }

    if ($ArgType.IndexOf("[]") -gt 0) {
        throw (New-Object ApplicationException "Array parameter is not supported: $ArgTypeInfos")
    }

    $TypeMap[$ArgType]
}

############################################################
# Get-DescriptionString function
############################################################

function Get-DescriptionString {
    param(
        $Op
    )

    $desc = $Op.Description
    $desc = ([regex]"\n").Replace($desc, "`r`n        /// ")
    $desc = "/// <summary>`r`n        /// " + $desc + "`r`n        /// </summary>"

    $args = @()
    for ($i = 0; $i -lt $Op.NumArgs; ++$i) {
        $d = $Op.ArgDescriptions[$i]
        $d = ([regex]"\n").Replace($d, "`r`n        /// ")
        $args += "        /// <param name=`"$($Op.ArgNames[$i])`">$($d)</param>"
    }

    if ($Op.NumArgs -gt 0) {
        $desc + "`r`n" + ($args -join "`r`n")
    }
    else {
        $desc
    }
}

############################################################
# Get-ParamNames
############################################################

function Get-ParamNames {
    param(
        $Op,
        [switch]$Method,
        [switch]$AddOutput
    )

    $required = @()
    $optional = @()

    $first = 0
    if ($Method) {
        $first = 1
        $required += "this"
    }

    for ($i = $first; $i -lt $Op.NumArgs; ++$i) {
        $name = $Op.ArgNames[$i]
        if (-not $name.EndsWith("?")) {
            $typeInfo = $Op.ArgTypeInfos[$i]
            if ($typeInfo -match "Default") {
                $optional += ConvertTo-CamelCase $name
            }
            else {
                $required += ConvertTo-CamelCase $name
            }
        }
    }

    if ($AddOutput) {
        $optional += "output"
    }

    $required + $optional
}

############################################################
# Get-FixedParamNames
############################################################

function Get-FixedParamNames {
    param(
        $Op,
        [switch]$CamelCase
    )

    for ($i = 0; $i -lt $Op.NumArgs; ++$i) {
        $name = $Op.ArgNames[$i]
        $typeInfo = $Op.ArgTypeInfos[$i]
        if (-not $name.EndsWith("?") -and -not (Test-Input $typeInfo) -and -not (Test-Omittable $typeInfo)) {
            if ($CamelCase) {
                ConvertTo-CamelCase $name
            }
            else {
                $name
            }
        }
    }
}

function Get-FixedParamNamesString {
    param(
        $Op,
        [switch]$CamelCase
    )

    $names = @(Get-FixedParamNames $Op $CamelCase)

    if ($names.Length -eq 0) {
        return "Empty"
    }

    "new string[] { `"" + ($names -join "`", `"") + "`" }"
}

############################################################
# Get-Arguments function
############################################################

function Get-Arguments {
    param(
        $Op
    )

    for ($i = 0; $i -lt $Op.NumArgs; ++$i) {
        $name = $Op.ArgNames[$i]
        $typeInfo = $Op.ArgTypeInfos[$i]
        if (-not $name.EndsWith("?") -and -not (Test-Input $typeInfo) -and -not (Test-Omittable $typeInfo)) {
            ConvertTo-CamelCase $name
        }
    }
}

function Get-ArgumentsString {
    param(
        $Op
    )

    $args = @()
    for ($i = 0; $i -lt $Op.NumArgs; ++$i) {
        $name = $Op.ArgNames[$i]
        $typeInfo = $Op.ArgTypeInfos[$i]
        if (-not $name.EndsWith("?") -and -not (Test-Input $typeInfo) -and -not (Test-Omittable $typeInfo)) {
            $n = ConvertTo-CamelCase $name
            if (-not $Op.ArgTypeInfos[$i].StartsWith("{")) {
                $n = "Convert($n)"
            }
            $args += $n
        }
    }

    if ($args.Length -eq 0) {
        return "Empty"
    }

    "new string[] { " + ($args -join ", ") + " }"
}

function Get-SingleArgumentString {
    param(
        [string]$ArgName,
        [string]$ArgTypeInfos
    )

    $n = ConvertTo-CamelCase $ArgName
    if (-not $ArgTypeInfos.StartsWith("{")) {
        $n = "Convert($n)"
    }

    $n
}

############################################################
# Get-Inputs function
############################################################

function Get-Inputs {
    param(
        $Op
    )

    $results = @()
    for ($i = 0; $i -lt $Op.NumArgs; ++$i) {
        $typeInfo = $Op.ArgTypeInfos[$i]
        if (-not (Test-Input $typeInfo)) {
            continue
        }

        $name = $Op.ArgNames[$i]
        $results += ConvertTo-CamelCase $name
    }

    $results
}

function Get-InputsString {
    param(
        $Op,
        [switch]$Method
    )

    $in = @(Get-Inputs $Op)

    if ($in.Length -eq 0) {
        return "EmptyInput"
    }

    if ($Method) {
        $in = "this"
    }

    $in = $in | foreach { $_ + ".Handle" }
    "new IntPtr[] { " + ($in -join ", ") + " }"
}

############################################################
# Get-Signature function
############################################################

function Split-ArgTypeInfos {
    param(
        [string]$ArgTypeInfos
    )

    if ($ArgTypeInfos.StartsWith("{")) {
        if ($Op.ArgTypeInfos[$i] -match "^({[^}]+})\s*(,\s*([^,]+)\s*(,\s*([^,]+))?)?") {
            return $matches[1], $matches[3], $matches[5]
        }
        Write-Error "Invalid type info: $($Op.argTypeInfos[$i])"
    }
    $Op.ArgTypeInfos[$i] -split "\s*,\s*"
}

function Get-DefaultValueString {
    param(
        [string]$Type,
        [string]$DefaultValue
    )

    $DefaultValue = $DefaultValue -Replace "default\s*=\s*", ""

    if ($DefaultValue -match "None") {
        return "null"
    }

    if ($Type -eq "NDShape" -and ($DefaultValue -eq "[]" -or $DefaultValue -eq "[0 0]")) {
        return "null"
    }

    if ($Type.EndsWith("Type")) {
        return "null"
    }

    if ($Type -match "int|long|float|double") {
        return $DefaultValue -Replace "'"
    }

    if ($Type -match "bool")
    {
        if ($DefaultValue -match "0")
        {
            return "false"
        }
        else {
            return "true"
        }
    }

    if ($Type -match "string") {
        return $DefaultValue -Replace "'", "`""
    }

    $DefaultValue
}

function Get-SignatureString {
    param(
        $Op,
        [switch]$SkipFirst
    )

    $first = 0
    if ($SkipFirst) {
        ++$first
    }

    $required = @()
    $optional = @()
    for ($i = $first; $i -lt $Op.NumArgs; ++$i) {
        $name = $Op.ArgNames[$i]
        $type, $req, $defaultValue = Split-ArgTypeInfos $Op.ArgTypeInfos[$i]

        $t = Convert-TypeName $Op.Name $name $type
        $param = $t + " " + (ConvertTo-CamelCase $name)
        if ([string]::IsNullOrEmpty($defaultValue)) {
            $required += $param
        }
        else {
            $param += " = " + (Get-DefaultValueString $t $defaultValue)
            $optional += $param
        }
    }

    $optional += "NDArray output = null";

    ($required + $optional) -join ", "
}

############################################################
# Get-OperatorDefinitions function
############################################################

$Excludes = @(
    "MakeLoss"
)

function Get-OperatorDefinitions {
    $ops = Get-Content $source | ConvertFrom-Json

    foreach ($op in $ops) {
        $opName = $op.Name
        if ($Excludes -match $opName) {
            Write-Host "$opName is in Excludes list"
            continue
        }

        if ($opName -match "(^_cv)|(^_image_)|contrib") {
            Write-Host "$opName is contrib operator"
            continue
        }

        if ($op.argTypeInfos -match "(NDArray(OrSymbol)?|Symbol)\[\]") {
            Write-Host "$opName contains array arguments"
            continue
        }

        $op
    }
}

############################################################
# Generate-Op
############################################################

function Generate-Op {
    $ops = Get-OperatorDefinitions

@"
using System;
using System.Collections.Generic;
using Horker.MXNet.Core;

namespace Horker.MXNet.Operators
{
    public class Op : OperatorsBase
    {
"@

    foreach ($op in $ops) {
        $opName = $op.Name

        $camelName = ConvertTo-CamelCase $op.Name
        $pascalName = ConvertTo-CamelCase $op.Name -Pascal

        $hasOmittable = !!($op.argTypeInfos | where { Test-Omittable $_ })

        try {
            if ($hasOmittable) {
                # When there are omittable parameters.
@"
        private static string[] _$($camelName)ParamNames = $(Get-FixedParamNamesString $op);

        $(Get-DescriptionString $op)
        public static NDArray $pascalName($(Get-SignatureString $op))
        {
            var keys = new List<string>(_$($camelName)ParamNames);
            var values = new List<string>($(Get-ArgumentsString $op -CamelCase));

"@
                for ($i = 0; $i -lt $op.NumArgs; ++$i) {
                    if (Test-Omittable $op.ArgTypeInfos[$i]) {
@"
            if ($(ConvertTo-CamelCase $op.ArgNames[$i]) != null) {
                keys.Add("$($op.ArgNames[$i])");
                values.Add($(Get-SingleArgumentString $op.ArgNames[$i] $op.ArgTypeInfos[$i]));
            }

"@
                    }

                }
@"
            var result = Operator.Invoke(
                "$opName",
                keys.ToArray(),
                values.ToArray(),
                $(Get-InputsString $op),
                output
            );
            return result;
        }

"@
            }
            else {
                # When all parameters are not omittable; Generate a simpler code.
@"
        private static string[] _$($camelName)ParamNames = $(Get-FixedParamNamesString $op);

        $(Get-DescriptionString $op)
        public static NDArray $pascalName($(Get-SignatureString $op))
        {
            var result = Operator.Invoke(
                "$opName",
                _$($camelName)ParamNames,
                $(Get-ArgumentsString $op -CamelCase),
                $(Get-InputsString $op),
                output
            );
            return result;
        }

"@
            }
        }
        catch [ApplicationException] {
            write-host "Skip: $($_.Exception.Message)"
        }
    }

@"
    }
}
"@
}

############################################################
# Generate-NDArrayMethods
############################################################

function Generate-NDArrayMethods {
    $ops = Get-OperatorDefinitions

@"
using System;
using Horker.MXNet.Operators;

namespace Horker.MXNet.Core
{
    public partial class NDArray : NDArrayOrSymbol
    {
"@

    foreach ($op in $ops) {
        if ($op.ArgTypeInfos.Length -eq 0 -or -not (Test-Input -NoSymbol $op.ArgTypeInfos[0])) {
            Write-Host "$($op.Name) does not have NDArray or NDArrayOrSymbol as the first parameter"
            continue
        }

        $pascalName = ConvertTo-CamelCase $op.Name -Pascal
@"
        $(Get-DescriptionString $op)
        public NDArray $pascalName($(Get-SignatureString -SkipFirst $op))
        {
            return Op.$pascalName($((Get-ParamNames $op -Method -AddOutput) -join ", "));
        }

"@
    }

@"
    }
}
"@
}

############################################################
# Run
############################################################

Generate-Op | Set-Content $opOutFile
#Generate-NDArrayMethods | Set-Content $ndArrayMethodsOutFile
