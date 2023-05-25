#!/usr/bin/env pwsh

[CmdletBinding()]
param ()

[version]$CurrentDate = [version]::new((Get-Date -Format yyyy), (Get-Date).DayOfYear, 0)
[version]$CurrentVersion = (gh release list --limit 1 --json tagName --jq '.[0].tagName')
[version]$IncrementVersion = [version]::new($CurrentVersion.Major, $CurrentVersion.Minor, $CurrentVersion.Build + 1)
[version]$TargetVersion = if ($CurrentDate -gt $IncrementVersion) { $CurrentDate } else { $IncrementVersion }

"version=${TargetVersion}" >> $env:GITHUB_OUTPUT