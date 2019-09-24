rm ~\localpsrepo\Horker.MXNet.*.nupkg
Publish-Module -path .\module\Horker.MXNet\ -Repository LocalPSrepo -NuGetApiKey any
