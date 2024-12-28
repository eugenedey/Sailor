#Params for accepting source and destination
param(
[string]$Source = $(throw "param $Source required"), #Source of build deliver
[string]$Destination = $(throw "param $Destination required"), #Destination of build deliver
[string]$Tag = $(throw "param $Destination required"), #BuildNumber
[string]$MajorVersion = "1" #Major version of application, default to 1
)

$intscriptPath = split-path -parent $MyInvocation.MyCommand.Definition

write-host "Build location " $intscriptPath
### Build location D:\Jenkins\thosmd1601\thosmdmid2208\workspace\T18\ASPNetCore\Deliver_SailorWebSIte\scripts


$intscriptPath = $intscriptPath -replace '\\scripts',''
$Source = $intscriptPath + $Source

#If destination does not have a ending \, then add
IF($Destination.Substring($Destination.Length-1) -ne "\")
{
    $Destination = $Destination + "\"
}

#Version
$Version = "version="+$MajorVersion+"."+$Tag

#Determine build directory
$BuildDir = $MajorVersion + "." + $Tag


#Add build directory to be created as the destination
$Destination = $Destination + $BuildDir

#Check to see if directory is Empty, if it isn't 
if(Test-Path $Destination)
{
    $DestinationChk = Get-ChildItem $Destination

    if($DestinationChk.count -ne 0)
    {
        $(throw "$Destination is not empty")
    }
}

write-host "Copy WebSite content from $Source to $Destination"

#Copy all items from the source to the destination, exclue .svn and recurse through folder tree
# Copy-Item $Source.Insert($Source.Length,"*") -Destination $Destination -Recurse -Exclude "*.svn"
  Copy-Item $Source.Insert($Source.Length,"*") -Destination $Destination -Recurse
#>
$Version


