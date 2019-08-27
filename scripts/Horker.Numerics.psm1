[Horker.MXNet.Operators.Operator]::LoadSymbolCreators()

exit

Set-StrictMode -Version Latest

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

$typesAndMethods = (
    ([Horker.Tensor.Tensors.TensorObject[double]], [Horker.Tensor.Interop.PowerShell.Methods.DoubleTensorMethods]),
    ([Horker.Tensor.Tensors.TensorObject[float]], [Horker.Tensor.Interop.PowerShell.Methods.FloatTensorMethods]),
    ([Horker.Tensor.Tensors.TensorObject[long]], [Horker.Tensor.Interop.PowerShell.Methods.LongTensorMethods]),
    ([Horker.Tensor.Tensors.TensorObject[int]], [Horker.Tensor.Interop.PowerShell.Methods.IntTensorMethods]),
    ([Horker.Tensor.Tensors.TensorObject[Int16]], [Horker.Tensor.Interop.PowerShell.Methods.ShortTensorMethods]),
    ([Horker.Tensor.Tensors.TensorObject[byte]], [Horker.Tensor.Interop.PowerShell.Methods.ByteTensorMethods]),
    ([Horker.Tensor.Tensors.TensorObject[sbyte]], [Horker.Tensor.Interop.PowerShell.Methods.SByteTensorMethods]),
    ([Horker.Tensor.Tensors.TensorObject[string]], [Horker.Tensor.Interop.PowerShell.Methods.StringTensorMethods]),
    ([Horker.Tensor.TensorMaps.DataFrame], [Horker.Tensor.Interop.PowerShell.Methods.DataFrameMethods]),
    ([Horker.Tensor.TensorMaps.DataMap], [Horker.Tensor.Interop.PowerShell.Methods.DataMapMethods])
)

foreach ($tm in $typesAndMethods) {
    Define-PowerShellMethods $tm[0] $tm[1]
}
