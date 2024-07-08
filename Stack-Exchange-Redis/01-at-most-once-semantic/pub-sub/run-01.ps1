
. "..\..\00-common\ps1\common.ps1"

function RunPublisher($i){
	$p = (Start-Process $publisher -ArgumentList $i -WorkingDirectory  $publisherPath -PassThru -WindowStyle Normal )
	# Write-Host $p.Id
	SetWindowPosition $p 0 $i $COLS $ROWS 
	return $p
}

function RunSubscriber($i){
	$q += (Start-Process $subscriber -ArgumentList $i -WorkingDirectory  $subscriberPath -PassThru )
	# Write-Host $p.Id
	$x = 1 + ($i % $XUBS)
	$y = [System.Math]::Floor($i / $XUBS)
	## Write-Host $x $y
	SetWindowPosition $q $x $y $COLS $ROWS 
	return $q
}


# 01-at-most-once-semantic
$path = (Get-Location).Path
$xpath = "$path\01"

$publisherProject="$xpath-01-publisher\01-01-publisher.csproj"
$publisherPath ="$xpath-01-publisher\bin\Debug\net8.0"
$publisher ="$xpath-01-publisher\bin\Debug\net8.0\01-01-publisher.exe"

dotnet build -c Debug $publisherProject 

$subscriberProject= "$xpath-02-subscriber\01-02-subscriber.csproj"
$subscriberPath = "$xpath-02-subscriber\bin\Debug\net8.0"
$subscriber = "$xpath-02-subscriber\bin\Debug\net8.0\01-02-subscriber.exe"

dotnet build -c Debug $subscriberProject 

$COLS = 4;
$ROWS = 4;
$PUBS = 2; 
$SUBS = 4; $XUBS = 2;

## use array imn $q to store the process objects


$q = @()
$p = @()

try{


	for($i=0; $i -lt $SUBS; $i++){
		$q += RunSubscriber $i
	}
	
	for($i=0; $i -lt $PUBS; $i++){
		$p += RunPublisher $i
	}

	do{
	
		## revisamos si se ha caido algun q[i]
		for($i=0; $i -lt $SUBS; $i++){
			if($q[$i].HasExited){
				Write-Host "Subscriber $i has exited"
				$q[$i] = RunSubscriber $i
			}
		}

		## revisamos si se ha caido algun p[i]
		for($i=0; $i -lt $PUBS; $i++){
			if($p[$i].HasExited){
				$p[$i] = RunPublisher $i
			}
		}


		Start-Sleep -Seconds 1

		Write-Host "Press [ESC] to exit"

		if ([Console]::KeyAvailable) {
			$keyInfo = [Console]::ReadKey($true)

			# Verifica si la tecla presionada es Escape y sale del bucle
			if ($keyInfo.Key -eq [ConsoleKey]::Escape) {
				break
			}
		}

	}while($true)

}finally{
	$p | ForEach-Object { $_.Kill() }
	$q | ForEach-Object { $_.Kill() }
}