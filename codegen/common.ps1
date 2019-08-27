Set-StrictMode -Version Latest

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

function Test-Input {
    param(
        [string]$TypeName,
        [switch]$NoSymbol
    )

    if ($NoSymbol) {
        $TypeName -eq "NdArrayOrSymbol" -or $TypeName -eq "NdArray"
    }
    else {
        $TypeName -eq "NdArrayOrSymbol" -or $TypeName -eq "NdArray" -or $TypeName -eq "Symbol"
    }
}

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
