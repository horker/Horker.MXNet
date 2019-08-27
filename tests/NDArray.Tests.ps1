using namespace Horker.MXNet.Core

Set-StrictMode -Version Latest

Describe "NDArray operation tests" {

    It "can compute sin()" {
        [Context]::DefaultContext = [Context]::Cpu()

        $a = New-NDArray -DoubleValues 1, 2, 3
        $a.Sin()

        $c.Shape | Should -Be 3
        $c.ToArray() | Should -Be ([Math]::Sin(1)), ([Math]::Sin(2)), ([Math]::Sin(3))
    }
}
