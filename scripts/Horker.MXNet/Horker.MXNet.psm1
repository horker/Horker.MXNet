Set-StrictMode -Version 6

############################################################
# Load native DLLs
############################################################

$RuntimeDlls = @(
    # MKL
    "libiomp5md.dll"
    "mkldnn.dll"
    "mklml.dll"

    # OpenBLAS
    "libgcc_s_seh-1.dll"
    "libgfortran-3.dll"
    "libopenblas.dll"
    "libquadmath-0.dll"

    # MXNet
    "libmxnet.dll"

    # OpenCV
    "opencv_videoio_ffmpeg430_64.dll"
    "OpenCvSharpExtern.dll"
)

$script:win32api = Add-Type -Passthru `
    -Name ("Win32Api_" + ([Guid]::NewGuid().ToString() -replace "-", "_")) `
    -MemberDefinition @"
[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
public static extern IntPtr LoadLibrary(string dllToLoad);
"@

function Load-NativeDlls([string]$BasePath, [string[]]$Dlls) {
    foreach ($d in $Dlls) {
        $path = Join-Path $BasePath $d
        $result = $win32api::LoadLibrary($path)
        if ($result -eq [IntPtr]::Zero) {
            throw "Failed to load: $d"
        }
    }
}

Load-NativeDlls "$PSScriptRoot\runtime" $RuntimeDlls

############################################################
# Define PowerShell methods
############################################################

function script:Define-PowerShellMethods {
    param(
        [Type]$Class,
        [Type]$MethodDefined
    )

    $flags = [Reflection.BindingFlags]::Static -bor [Reflection.BindingFlags]::Public
    foreach ($mi in $MethodDefined.GetMethods($flags)) {
        Update-TypeData -TypeName $Class -MemberName $mi.Name -MemberType CodeMethod -Value $mi -Force
    }
}

$classes = @(
    [Horker.MxNet.PowerShell.NDArrayMethods]
    [Horker.MxNet.PowerShell.NDArrayIterMethods]
)

foreach ($c in $classes) {
    Define-PowerShellMethods $c::TargetType $c
}