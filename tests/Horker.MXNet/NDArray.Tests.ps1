using namespace MXNet

Set-StrictMode -Version Latest

Describe "NDArray creation" {

    It "can be created from an array" {
        $a = New-MxNDArray 1, 2, 3
        $a.DataType | Should -Be "Float32"
        $a.Shape | Should -Be "(3)"
        $a.ToArray() | Should -Be ([float]1), ([float]2), ([float]3)
    }

    It "can be created from a jagged array" {
        $a = New-MxNDArray (1, 2, 3), (4, 5, 6)
        $a.DataType | Should -Be "Float32"
        $a.Shape | Should -Be "(2,3)"
        $a.ToArray() | Should -Be ([float[]](1, 2, 3, 4, 5, 6))
    }
}

Describe "NDArray operations" {

    It "can compute sin()" {
        [Context]::CurrentContext = [Context]::Cpu()

        $a = New-MxNDArray 1, 2, 3 -DType ([DType]::Float64)
        $b = $a.Sin()

        $b.Shape | Should -Be "(3)"
        $b.ToArray() | Should -Be ([Math]::Sin(1)), ([Math]::Sin(2)), ([Math]::Sin(3))
    }
}
