$easyimgur_folder = (Get-Item (Split-Path $script:MyInvocation.MyCommand.Path)).parent.FullName
$destination_folder = $easyimgur_folder + "\dist\"
$source_folder = $easyimgur_folder + "\EasyImgur\bin\"

$files = "Release\EasyImgur.exe","Release\Newtonsoft.Json.dll","Debug\EasyImgur.exe","Debug\Newtonsoft.Json.dll"

ForEach ($file in $files)
{
	$full_destination_path = $destination_folder + $file
	$full_source_path = $source_folder + $file
	
	$destination_timestamp 	= Get-Date -Date "1970-01-01 00:00:00Z"
	$source_timestamp 		= Get-Date -Date "1970-01-01 00:00:00Z"
	if (Test-Path $full_destination_path)
	{
		$destination_timestamp 	= [datetime](Get-ItemProperty -Path $full_destination_path -Name LastWriteTime).lastwritetime
	}
	if (Test-Path $full_source_path)
	{
		$source_timestamp 		= [datetime](Get-ItemProperty -Path $full_source_path -Name LastWriteTime).lastwritetime
	}
	else
	{
		Write-Error ("Source file does not exist: " + $file)
		continue
	}
	
	if ($source_timestamp -gt $destination_timestamp)
	{
		Write-Host ("Detected newer source, copied file: " + $file)
		Copy-Item $full_source_path $full_destination_path
	}
	else
	{
		Write-Warning (	"Source is not newer than destination, file not copied (did you forget to recompile?): `n`t" + 
						$file + 
						"`n`tSource: [" + $source_timestamp + "]`tDestination: [" + $destination_timestamp + "]")
	}
	
	Write-Host "======================="
}

Write-Host "Press any key to continue ..."
$x = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")