using namespace Horker.MXNet.Core

Set-StrictMode -Version Latest

Describe "NDArray operation tests" {

    It "can compute sin()" {
        [Context]::DefaultContext = [Context]::Cpu()

        $a = New-MxNDArray -Double 1, 2, 3
        $b = $a.Sin()

        $b.Shape | Should -Be "(3)"
        $b.ToArray() | Should -Be ([Math]::Sin(1)), ([Math]::Sin(2)), ([Math]::Sin(3))
    }
}
