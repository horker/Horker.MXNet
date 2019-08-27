Set-StrictMode -Version Latest

$source = "$PSScriptRoot\source\op.json"
$outOpFile = "$PSScriptRoot\..\source\Horker.Numerics\gen_NumericNDArrayMethods.cs"

. "$PSScriptRoot\common.ps1"

############################################################

function Replace-TypeName {
    param(
        [string]$TypeName
    )

    switch ($TypeName) {
        "NdArrayOrSymbol" {
            "NumericNDArray<T>"
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

    $result = @("_impl")
    foreach ($a in $Arguments) {
        if (Test-Input $a.TypeName) {
            $result += (Convert-SnailCaseToCamelCase $a.Name) + "._impl"
        }
        else {
            $result += Convert-SnailCaseToCamelCase $a.Name
        }
    }

    $result -join ", "
}

############################################################
# Generate FloatNDArrayMethods.cs
############################################################

$methodsHeader = @"
using System;
using System.Collections.Generic;
using Horker.MXNet.Core;
using Horker.MXNet.Operators;
using Horker.Numerics;

namespace Horker.Numerics
{
    public partial class NumericNDArray<T> : NDArray<T>
        where T: struct
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
            var impl = Op.{4}({5});
            return new NumericNDArray<T>(impl);
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

        $returnType = "NumericNDArray<T>"

        # Skip the first parameter
        if ($a.Length -eq 1) {
            $signature = ""
            $arguments = "_impl"
        }
        else {
            $a = $a[1..($a.Length - 1)]

            $signature = Get-Signature $a
            $arguments = Get-Arguments $a -Impl
        }

        $methodsTemplate -f "func", $returnType, $pascalName, $signature, $pascalName, $arguments
    }

    $methodsFooter
}

############################################################

$ops = Get-Content -Encoding utf8 $source | ConvertFrom-Json

Get-NDArrayMethods $ops "" | Set-Content -Encoding utf8 $outOpFile
