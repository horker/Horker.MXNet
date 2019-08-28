Set-StrictMode -Version Latest

$source = "$PSScriptRoot\source\op.json"
$outOpFile = "$PSScriptRoot\..\source\Horker.MXNet\gen_NDArrayMethods.cs"

. "$PSScriptRoot\common.ps1"

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
    $result -join ", "
}

function Get-Arguments {
    param(
        [object[]]$Arguments
    )

    $result = @("this")
    foreach ($a in $Arguments) {
        $result += Convert-SnailCaseToCamelCase $a.Name
    }

    $result -join ", "
}

############################################################
# Generate FloatNDArrayMethods.cs
############################################################

$methodsHeader = @"
using System;
using System.Collections.Generic;
using Horker.MXNet.Operators;

namespace Horker.MXNet.Core
{
    public partial class NDArray : NDArrayOrSymbol
    {
"@

$methodsFooter = @"
    }
}
"@

$methodsTemplate = @"

        /// {0}
        public {1} {2}({3})
        {{
            return Op.{4}({5});
        }}
"@

function Get-NDArrayMethods {
    param(
        [object[]]$Ops,
        [string]$ClassNameFilter
    )

    $methodsHeader

    foreach ($op in $Ops) {

        if ($op.ClsName -ne $ClassNameFilter) {
            continue
        }

        $name = $op.Name

        Write-Host $name

        if ($op.Args.TypeName -contains "NdArrayOrSymbol[]" -or
            $op.Args.TypeName -contains "List<Symbol>") {
            Write-Host "$name uses NdArrayOrSymbol[] or List<Symbol>"
            continue
        }

        $a = $op.Args | where Name -ne "symbol_name"
        $a = Reorder-Params $a
        $a = @($a)

        if ($a.Length -eq 0 -or -not (Test-Input -NoSymbol $a[0].TypeName)) {
            Write-Host "$name does not have NDArray or NDArrayOrSymbol as the first parameter"
            continue
        }

        $pascalName = Convert-SnailCaseToCamelCase $op.Name -Pascal

        $returnType = "NDArray"

        # Skip the first parameter
        if ($a.Length -eq 1) {
            $signature = ""
            $arguments = "this"
        }
        else {
            $a = $a[1..($a.Length - 1)]

            $signature = Get-Signature $a
            $arguments = Get-Arguments $a
        }

        $methodsTemplate -f "doc", $returnType, $pascalName, $signature, $pascalName, $arguments
    }

    $methodsFooter
}

############################################################

$ops = Get-Content -Encoding utf8 $source | ConvertFrom-Json

Get-NDArrayMethods $ops "" | Set-Content -Encoding utf8 $outOpFile
