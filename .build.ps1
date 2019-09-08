task . Compile, Build, ImportDebug, Test

Set-StrictMode -Version Latest

############################################################
# Settings
############################################################

$SOURCE_PATH = "$PSScriptRoot\source"
$SCRIPT_PATH = "$PSScriptRoot\scripts"

$MODULE_PATH = "$PSScriptRoot\module\Horker.MXNet"
$MODULE_PATH_DEBUG = "$PSScriptRoot\module\debug\Horker.MXNet"

$SOLUTION_FILE = "$PSScriptRoot\source\Horker.MXNet.sln"

$OBJECT_FILES = @(
    "Horker.MXNet.dll"
    "Horker.MXNet.pdb"
    "Horker.MXNet.PowerShell.dll"
    "Horker.MXNet.PowerShell.pdb"
    "Horker.Numerics.dll"
    "Horker.Numerics.pdb"
    "Horker.Numerics.PowerShell.dll"
    "Horker.Numerics.PowerShell.pdb"
)

#TODO
#$LIBRARY_PATH = "

#$TEMPLATE_INPUT_PATH = "$PSScriptRoot\templates"
#$TEMPLATE_OUTPUT_PATH = "$PSScriptRoot\source\Horker.PSCNTK\Generated files"

#$HELP_INPUT =  "$SOURCE_PATH\bin\Release\Horker.Math.dll"
#$HELP_INTERM = "$SOURCE_PATH\bin\Release\Horker.Data.dll-Help.xml"
#$HELP_OUTPUT = "$MODULE_PATH\Horker.Data.dll-Help.xml"
#$HELPGEN = "$PSScriptRoot\vendor\XmlDoc2CmdletDoc.0.2.10\tools\XmlDoc2CmdletDoc.exe"

############################################################
# Helper cmdlets
############################################################

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

############################################################
# Tasks
############################################################

task Compile {
  msbuild $SOLUTION_FILE /p:Configuration=Debug /p:Platform=x64 /nologo /v:minimal
  msbuild $SOLUTION_FILE /p:Configuration=Release /p:Platform=x64 /nologo /v:minimal
}

task Build {
  . {
    $ErrorActionPreference = "Continue"

    function Copy-ObjectFiles {
      param(
        [string]$targetPath,
        [string]$objectPath
      )

      New-Folder2 $targetPath

      Copy-Item2 "$SCRIPT_PATH\*" $targetPath
      $OBJECT_FILES | foreach {
        $path = Join-Path $objectPath $_
        Copy-Item2 $path $targetPath
      }
    }

    Copy-ObjectFiles $MODULE_PATH "$SOURCE_PATH\bin\x64\Release"
    Copy-ObjectFiles $MODULE_PATH_DEBUG "$SOURCE_PATH\bin\x64\Debug"
  }
}

#task BuildHelp `
#  -Inputs $HELP_INPUT `
#  -Outputs $HELP_OUTPUT `
#{
#  . $HELPGEN $HELP_INPUT
#
#  Copy-Item $HELP_INTERM $MODULE_PATH
#}

task Test Build, ImportDebug, {
  Invoke-Pester "$PSScriptRoot\tests"
}

task ImportDebug {
    Import-Module $MODULE_PATH_DEBUG -Force
}

task Clean {
  Remove-Item2 "$MODULE_PATH\*"
  Remove-Item2 "$MODULE_PATH_DEBUG\*"
}

task Pack {
    nuget.exe pack source\Horker.MXNet\Horker.MXNet.csproj -Prop Configuration=Release -Symbol -OutputDirectory nuget\
}
