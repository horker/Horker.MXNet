Set-StrictMode -Version Latest

$source = "$PSScriptRoot\source\op.json"
$outFile = "$PSScriptRoot\..\source\Horker.MXNet\Generated\Op.cs"

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
# Get-Description function
############################################################

function Get-Description {
    param(
        [object]$Op
    )

    $desc = $op.Description
    $desc = ([regex]"\n").Replace($desc, "`r`n        /// ")
    $desc = "/// <summary>`r`n        /// " + $desc + "`r`n        /// </summary>`r`n"

    $args = @()
    foreach ($a in $Op.args) {
        $d = $a.Description
        $d = ([regex]"\n").Replace($d, "`r`n        /// ")
        $args += "        /// <param name=`"$($a.Name)`">$($d)</param>"
    }

    $desc + ($args -join "`r`n")
}

############################################################
# Test-Input function
############################################################

function Test-Input {
    param(
        [string]$TypeName
    )

    $TypeName -eq "NdArrayOrSymbol" -or $TypeName -eq "NdArray" -or $TypeName -eq "Symbol"
}

############################################################
# Reorder-Params function
############################################################

function Reorder-Params {
    param(
        [object[]]$Arguments
    )

    $result = @()
    foreach ($a in $Arguments) {
        if (-not $a.HasDefault) {
            $result += $a
        }
    }
    foreach ($a in $Arguments) {
        if ($a.HasDefault) {
            $result += $a
        }
    }

    $result
}

############################################################
# Replace-TypeName function
############################################################

function Replace-TypeName {
    param(
        [string]$TypeName
    )

    switch ($TypeName) {
        "NdArrayOrSymbol" {
            "NDArrayOrSymbol"
        }
        default {
            $TypeName
        }
    }
}

############################################################
# Get-Signature function
############################################################

function Get-Signature {
    param(
        [object[]]$Arguments
    )

    $result = @()
    foreach ($a in $Arguments) {
        $t = Replace-TypeName $a.TypeName
        $param = $t + " " + (Convert-SnailCaseToCamelCase $a.Name)
        if ($a.HasDefault) {
            $param += " = " + $a.DefaultString
        }
        $result += $param
    }

    $result += "NDArray output = null";

    $result -join ", "
}

############################################################
# Get-ParamNames function
############################################################

function Get-ParamNames {
    param(
        [object[]]$Arguments
    )

    if ($null -eq $Arguments -or $Arguments.Length -eq 0) {
        return "_empty"
    }

    $result = @()
    foreach ($a in $Arguments) {
        if (Test-Input $a.TypeName) {
            continue
        }
        $result += $a.Name
    }

    if ($result.Length -eq 0) {
        "_empty"
    }
    else {
        "new[] { `"" + ($result -join "`", `"") + "`" }"
    }
}

############################################################
# Get-Arguments function
############################################################

function Get-Arguments {
    param(
        [object[]]$Arguments
    )

    if ($null -eq $Arguments -or $Arguments.Length -eq 0) {
        return "_empty"
    }

    $result = @()
    foreach ($a in $Arguments) {
        if (Test-Input $a.TypeName) {
            continue
        }
        if ($a.IsEnum) {
            $result += "Convert((int)" + (Convert-SnailCaseToCamelCase $a.Name) + ")"
        }
        else {
            $result += "Convert(" + (Convert-SnailCaseToCamelCase $a.Name) + ")"
        }
    }

    if ($result.Length -eq 0) {
        "_empty"
    }
    else {
        "new[] { " + ($result -join ", ") + " }"
    }
}

############################################################
# Get-Inputs function
############################################################

function Get-Inputs {
    param(
        [object[]]$Arguments
    )

    if ($null -eq $Arguments -or $Arguments.Length -eq 0) {
        return "_emptyInput"
    }

    $result = @()
    foreach ($a in $Arguments) {
        if (-not (Test-Input $a.TypeName)) {
            continue
        }
        $result += Convert-SnailCaseToCamelCase $a.Name
    }

    if ($result.Length -eq 0) {
        "_emptyInput"
    }
    else {
        "new[] { " + ($result -join ", ") + " }"
    }
}

############################################################
# Template
############################################################

$header = @"
using System;
using System.Collections.Generic;
using Horker.MXNet.Core;

namespace Horker.MXNet.Operators
{
    public partial class Op : OperatorsBase
    {
"@

$additionalHeader = @"
    public class {0} : OperatorsBase
    {
"@

$footer = @"
    }
}
"@

$additionalFooter = @"
    }
"@

$template = @"

        private static string[] _{0}ParamNames = {1};

        {2}
        public static {3} {4}({5})
        {{
            var result = Operator.Invoke(
                "{6}",
                _{7}ParamNames,
                {8},
                {9},
                output);
            return {10};
        }}
"@

$Excludes = @("MakeLoss")

############################################################
# Main function
############################################################

function Get-ClassDefinition {
    param(
        [object[]]$Ops,
        [string]$ClassNameFilter
    )

    $header

    foreach ($op in $Ops) {

        if ($op.ClsName -ne $ClassNameFilter) {
            continue
        }

        $name = $op.Name

        if ($Excludes -eq $op.Name) {
            Write-Host "$name is included in exclude list"
            continue
        }

        $pascalName = Convert-SnailCaseToCamelCase $op.Name -Pascal
        $camelName = Convert-SnailCaseToCamelCase $op.Name

        $returnType = "NDArray"

        if ($op.Args.TypeName -contains "NdArrayOrSymbol[]" -or
            $op.Args.TypeName -contains "List<Symbol>") {
            Write-Host "$name uses NdArrayOrSymbol[] or List<Symbol>"
            continue
        }

        $a = $op.Args | where Name -ne "symbol_name"

        $desc = Get-Description $op

        $a = Reorder-Params $a

        $signature = Get-Signature $a
        $paramNames = Get-ParamNames $a
        $arguments = Get-Arguments $a
        $inputs = Get-Inputs $a

        $result= "result"

        $template -f $camelName, $paramNames,
            $desc,
            $returnType,
            $pascalName, $signature,
            $name, $camelName, $arguments, $inputs, $result
    }

    $footer
}

############################################################
# Run
############################################################


$ops = Get-Content -Encoding utf8 $source | ConvertFrom-Json

Get-ClassDefinition $ops "" | Set-Content -Encoding utf8 $outFile
