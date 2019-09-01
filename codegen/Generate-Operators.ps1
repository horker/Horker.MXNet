Set-StrictMode -Version Latest

$source = "$PSScriptRoot\source\ops.json"
$opOutFile = "$PSScriptRoot\..\source\Horker.MXNet\Core\gen_Op.cs"
$ndArrayMethodsOutFile = "$PSScriptRoot\..\source\Horker.MXNet\Core\gen_NDArrayMethods.cs"

############################################################
# Convert-SnailCaseToCamelCase function
############################################################

$cultureInfo = [Threading.Thread]::CurrentThread.CurrentCulture
$textInfo = $cultureInfo.TextInfo

function Convert-SnailCaseToCamelCase {
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

function Convert-TypeName {
    param(
        [string]$ArgTypeInfos
    )

    if ($ArgTypeInfos.StartsWith("{")) {
        return "string"
    }

    if ($ArgTypeInfos.IndexOf("[]") -gt 0) {
        throw (New-Object ApplicationException "Array parameter is not supported: $ArgTypeInfos")
    }

    $name = ($ArgTypeInfos -split ",")[0]
    $TypeMap[$name]
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
    $desc = "/// <summary>`r`n        /// " + $desc + "`r`n        /// </summary>`r`n"

    $args = @()
    for ($i = 0; $i -lt $Op.NumArgs; ++$i) {
        $d = $Op.ArgDescriptions[$i]
        $d = ([regex]"\n").Replace($d, "`r`n        /// ")
        $args += "        /// <param name=`"$($Op.ArgNames[$i])`">$($d)</param>"
    }

    $desc + ($args -join "`r`n")
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
    if ($SkipFirst) {
        $first = 1
        $required = "this"
    }

    for ($i = $first; $i -lt $Op.NumArgs; ++$i) {
        $name = $Op.ArgNames[$i]
        if (-not $name.EndsWith("?")) {
            $typeInfo = $Op.ArgTypeInfos[$i]
            if ($typeInfo -match "Default") {
                $optional += Convert-SnailCaseToCamelCase $name
            }
            else {
                $required += Convert-SnailCaseToCamelCase $name
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
        if (-not $name.EndsWith("?") -and -not (Test-Input $typeInfo)) {
            if ($CamelCase) {
                Convert-SnailCaseToCamelCase $name
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

    "new[] { `"" + ($names -join "`", `"") + "`" }"
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
        if (-not $name.EndsWith("?") -and -not (Test-Input $typeInfo)) {
            Convert-SnailCaseToCamelCase $name
        }
    }
}

function Get-ArgumentsString {
    param(
        $Op
    )

    $args = @(Get-Arguments $Op)

    if ($args.Length -eq 0) {
        return "Empty"
    }

    $args = $args | foreach { "Convert($_)" }
    "new[] { " + ($args -join ", ") + " }"
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
        $results += Convert-SnailCaseToCamelCase $name
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
    "new[] { " + ($in -join ", ") + " }"
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

        $t = Convert-TypeName $type
        $param = $t + " " + (Convert-SnailCaseToCamelCase $name)
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

        if ($op.argTypeInfos -match "\[\]") {
            Write-Host "$($op.Name) contains array arguments"
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
using Horker.MXNet.Core;

namespace Horker.MXNet.Operators
{
    public class Op : OperatorsBase
    {
"@

    foreach ($op in $ops) {
        $opName = $op.Name
        Write-Host $opName

        $camelName = Convert-SnailCaseToCamelCase $op.Name
        $pascalName = Convert-SnailCaseToCamelCase $op.Name -Pascal
        try {
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
        $opName = $op.Name
        Write-Host $opName

        if ($op.ArgTypeInfos.Length -eq 0 -or -not (Test-Input -NoSymbol $op.ArgTypeInfos[0])) {
            Write-Host "$($op.Name) does not have NDArray or NDArrayOrSymbol as the first parameter"
            continue
        }

        $pascalName = Convert-SnailCaseToCamelCase $op.Name -Pascal
@"
        $(Get-DescriptionString $op)
        public NDArray $pascalName($(Get-SignatureString -SkipFirst $op))
        {
            return Op.$pascalName($((Get-ParamNames $op -SkipFirst -AddOutput) -join ", "));
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

#Generate-Op | Set-Content $opOutFile
Generate-NDArrayMethods | Set-Content $ndArrayMethodsOutFile