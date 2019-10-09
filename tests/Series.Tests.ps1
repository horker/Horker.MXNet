Set-StrictMode -Version Latest

Describe "Series tests" {

    BeforeEach {
        $s = New-Object Horker.Numerics.DataMaps.Series (,([int[]](1, 2, 3, 4)))
    }

    It "can call Apply()" {
        $s1 = $s.Apply("(x, i) => x * i")
        $s1 | Should -Be 0, 2, 6, 12

        $s1 = $s.Apply({ $value * $index})
        $s1 | Should -Be 0, 2, 6, 12
    }

    It "can call ApplyFill()" {
        $s.ApplyFill("(x, i) => x * i")
        $s | Should -Be 0, 2, 6, 12
    }
}
