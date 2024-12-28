<#Params for accepting source and destination#>
param(
[string]$Source = $(throw "param $Source required"),
[string[]]$Destination = $(throw "param $Destination required"),
[string]$Tag = $(throw "param $Destination required")
)

#If source does not have an ending \, then add
IF($Source.Substring($Source.Length-1) -ne "\")
{
    $Source = $Source + "\"
}

#Take in source and add tag location
$Source = $Source + $Tag + "\*"

#Make sure source directory exists, error if not
IF(-Not (Test-Path $Source))
{
    $(throw "$Source not found")
}

write-host "Deploying content from $Source to $Destination"

write-host "Stopping website.."

$apppool = "SailorTheCat"
Invoke-Command -ComputerName "dhomidiis1601" -ScriptBlock {param($apppool) C:\Windows\System32\inetsrv\appcmd.exe stop apppool /APPPOOL.NAME:$apppool } -ArgumentList $apppool
start-sleep -s 3

 $DestRem = $Destination.Trim()
 $DestRem += "\*"
     #Remove-Item -Path $Destination -Force
$DestRem = $DestRem -replace ('\d$','\D`$')

    #Remove subfolder from remote node

write-host "Cleaning up $DestRem"

$path = $Destination.Trim()

# Delete old files 
Get-ChildItem -Path $path -Recurse -Force | Where-Object { !$_.PSIsContainer } | Remove-Item -Force

# Delete any empty directories left behind after deleting the old files.
Get-ChildItem -Path $path -Recurse -Force | Where-Object { $_.PSIsContainer -and (Get-ChildItem -Path $_.FullName -Recurse -Force | Where-Object { !$_.PSIsContainer }) -eq $null } | Remove-Item -Force -Recurse

start-sleep -s 3

     $DestRem = $Destination.Trim()
     $DestRem += "\"
     write-host "Copy items from $Source to $DestRem"
      Copy-Item -Path $Source -Destination $path -Recurse -Force
      start-sleep -s 3
write-host "Starting website.."
Invoke-Command -ComputerName "dhomidiis1601" -ScriptBlock {param($apppool) C:\Windows\System32\inetsrv\appcmd.exe start apppool /APPPOOL.NAME:$apppool } -ArgumentList $apppool
start-sleep -s 3

Write-host ""version=""$Tag""



