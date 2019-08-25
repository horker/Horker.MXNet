using namespace Horker.MXNet.Core

Set-StrictMode -Version Latest

Describe "GPU tests" {

    It "can pube created from an array" {
        [Context]::DefaultContext = [Context]::Gpu(0)
#        [Context]::DefaultContext = [Context]::Cpu()

        $a = New-NDArray -DoubleValues 1, 2, 3
        $b = New-NDArray -DoubleValues 4, 5, 6

        $c = $a + $b

        $c.Shape | Should -Be 3
        $c.ToArray() | Should -Be 5, 7, 9

        [Context]::DefaultContext = [Context]::Cpu()
    }
}
