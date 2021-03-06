Set-StrictMode -Version Latest

Describe "Series.Apply() tests" {

    BeforeEach {
        $dataMap = New-DataMap
        $dataMap["a"] = [int[]](1, 2, 3, 4)
        $s = $dataMap["a"]
    }

    It "can call Apply()" {
        $s1 = $s.Apply("(x, i) => x * i")
        $s1.Values | Should -Be 0, 2, 6, 12

        $s1 = $s.Apply({ $value * $index})
        $s1.Values | Should -Be 0, 2, 6, 12
    }

    It "can call ApplyFill()" {
        $s.ApplyFill("(x, i) => x * i")
        $s.Values | Should -Be 0, 2, 6, 12
    }

    It "can refer to the container DataMap from func string" {
        $s1 = $s.Apply('(x, i) => x * (int)dataMap["a"][i]')
        $s1.Values | Should -Be 1, 4, 9, 16
    }

    It "can refer to the column object from func string" {
        $s1 = $s.Apply('(x, i) => x * (int)column[i]')
        $s1.Values | Should -Be 1, 4, 9, 16
    }
}

Describe "DataMap.Summarize() tests" {

    BeforeEach {
        $dataMap = New-DataMap
        $dataMap["cat"] = [string[]]("x", "y", "z", "x")
        $dataMap["a"] = [int[]](1, 2, 3, 4)
        $dataMap["b"] = [int[]](10, 20, 30, 40)
    }

    It "can accept script blocks" {

        $s = $dataMap.Summarize("cat", "a", [ordered]@{
            "max" = "Max"
            "min" = { $column.Min() }
        })

        $s.ColumnNames | Should -Be "cat", "max/a", "min/a"
        $s["cat"].Values | Should -Be "x", "y", "z"
        $s["max/a"].Values | Should -Be 4, 2, 3
        $s["min/a"].Values | Should -Be 1, 2, 3
    }
}
