using namespace MXNet

Set-StrictMode -Version Latest

Describe "NDArray with GPUs" {

    It "can be created from an array" {
        [Context]::CurrentContext = [Context]::Gpu(0)
#        [Context]::CurrentContext = [Context]::Cpu()

        $a = New-MxNDArray 1, 2, 3
        $b = New-MxNDArray 4, 5, 6

        $c = $a + $b

        $c.Shape | Should -Be "(3)"
        $c.ToArray() | Should -Be 5, 7, 9

        [Context]::CurrentContext = [Context]::Cpu()
    }
}
