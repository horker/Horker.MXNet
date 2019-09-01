$ops = cat $PSScriptRoot\source\ops.json | convertfrom-json
$n = $ops.ArgNames
$t = $ops.ArgTypeInfos
$a=@(); for ($i = 0; $i -lt $n.length; ++$i) { $a += [pscustomobject]@{ Name=$n[$i]; TypeInfo=$t[$i]} }
$a | where { $_.TypeInfo.StartsWith("{") } | sort Name