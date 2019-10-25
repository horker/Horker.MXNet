Set-StrictMode -Version Latest

[Horker.Numerics.LightGBM.UnmanagedDllLoader]::Load($PSScriptRoot)

# Define extension methods

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

#$typesAndMethods = (
#    ,([Horker.MXNet.Core.NDArray], [Horker.MXNet.PowerShell.NDArrayMethods])
#)
#
#foreach ($tm in $typesAndMethods) {
#    Define-PowerShellMethods $tm[0] $tm[1]
#}
#