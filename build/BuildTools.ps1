############################################################
# Helper cmdlets for .build.ps1
############################################################

Set-StrictMode -Version Latest

function New-Folder2 {
  param(
    [string]$Path
  )

  try {
    $null = New-Item -Type Directory $Path -EA Stop
    Write-Host -ForegroundColor DarkCyan "$Path created"
  }
  catch {
    Write-Host -ForegroundColor DarkYellow $_
  }
}

function Copy-Item2 {
  param(
    [string]$Source,
    [string]$Dest
  )

  try {
    Copy-Item $Source $Dest -EA Stop
    Write-Host -ForegroundColor DarkCyan "Copy from $Source to $Dest done"
  }
  catch {
    Write-Host -ForegroundColor DarkYellow $_
  }
}

function Remove-Item2 {
    param(
        [string]$Path
    )

    Resolve-Path $PATH | foreach {
        try {
        Remove-Item $_ -EA Stop -Recurse -Force
        Write-Host -ForegroundColor DarkCyan "$_ removed"
        }
        catch {
        Write-Host -ForegroundColor DarkYellow $_
        }
    }
}

function Copy-ObjectFiles {
    param(
        [string[]]$SourceFiles,
        [string]$TargetPath
    )

    $ErrorActionPreference = "Continue"
    New-Folder2 $TargetPath

    Resolve-Path $SourceFiles | foreach {
        Copy-Item2 $_ $TargetPath
    }
}

############################################################
# Common tasks
############################################################

task Build {
    "Debug", "Release" | foreach {
        $build = $_
        Copy-ObjectFiles $ScriptFiles ($ModulePath -f $build)
        Copy-ObjectFiles ($ObjectFiles | foreach { $_-f $build }) ($ModulePath -f $build)
    }
}

task Test {
    Invoke-Pester $TestPath
}

task ImportDebug {
    Import-Module ($ModulePath -f "Debug") -Force
}

task Clean {
    "Debug", "Release" | foreach {
        Remove-Item2 ("$ModulePath\*" -f $_)
    }
}