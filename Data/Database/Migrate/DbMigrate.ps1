
[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.SMO") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.SmoExtended") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.ConnectionInfo") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.SmoEnum") | Out-Null
Import-Module SQLPS
 
$serverName = "localhost\SQLEXPRESS"
$DBNewPath = "d:\Development\DataBase"
$server = New-Object ("Microsoft.SqlServer.Management.Smo.Server") $serverName
$dbs = $server.Databases
$databaseName = "QA"
$BackupDir = "\\EzeCloud\Backups\Database"
  
$LastBackup = gci $BackupDir | sort LastWriteTime | Select -last 1
$TargetFile = "C:\tmp\$LastBackup"


#######################################Delete Database ######################################################
echo "Getting Database $databaseName"
$db = $server.databases[$databaseName]
if ($db) {
  echo "Killing Processes"
  $server.KillAllprocesses($databaseName)
  echo "Dropping Database"
  $db.Drop()
} else {
  echo "Didn't find database [$databaseName]"
}

#####################################Get File System ########################################################
echo "Most Recent Backup $LastBackup"
echo "Copying file $BackupDir\$LastBackup to $TargetFile"
Copy-Item -Path "$BackupDir\$LastBackup" -Destination $TargetFile

$cmd = "restore filelistonly from disk='$TargetFile'" 
echo "Gettin the Logical files $cmd"
$dt = Invoke-Sqlcmd $cmd -ServerInstance $serverName 

$dataFileLogicalName = ""
$logFileLogicalName = ""

foreach ($r in $dt)
{
  if ($r.Type -eq "L")
  {
    $logFileLogicalName = $r.LogicalName
  }
  if ($r.Type -eq "D")
  {
    $dataFileLogicalName = $r.LogicalName
  }
}
write-host "data=$dataFileLogicalName  log=$logFileLogicalName"

###################################### Restore Database #####################################################

$RelocateData = New-Object Microsoft.SqlServer.Management.Smo.RelocateFile($dataFileLogicalName, "$DBNewPath\$databaseName.mdf")
$RelocateLog = New-Object Microsoft.SqlServer.Management.Smo.RelocateFile($logFileLogicalName, "$DBNewPath\$databaseName.ldf")

echo "Restoring Database with $TargetFile"

Restore-SqlDatabase -ServerInstance $serverName -Database $databaseName -BackupFile $TargetFile -ReplaceDatabase -NoRecovery -RelocateFile @($RelocateData, $RelocateLog) -Verbose
Invoke-Sqlcmd "RESTORE DATABASE $databaseName with RECOVERY" -ServerInstance $serverName 
Invoke-Sqlcmd "use qa;drop user idcasca;" -ServerInstance $serverName 
Invoke-Sqlcmd "use qa;create user idcasca for Login idcasca with DEFAULT_SCHEMA = QA; " -ServerInstance $serverName 
Invoke-Sqlcmd "use qa;Exec sp_addrolemember 'db_datareader', 'idcasca'" -ServerInstance $serverName 
Invoke-Sqlcmd "use qa;Exec sp_addrolemember 'db_datawriter', 'idcasca'" -ServerInstance $serverName

Invoke-Sqlcmd "use qa;grant execute on AddClientRole to idcasca" -ServerInstance $serverName
Invoke-Sqlcmd "use qa;grant execute on CleanQA to idcasca" -ServerInstance $serverName
Invoke-Sqlcmd "use qa;grant execute on ConvertToTrip to idcasca" -ServerInstance $serverName
Invoke-Sqlcmd "use qa;grant execute on DeleteQuoteGroup to idcasca" -ServerInstance $serverName
Invoke-Sqlcmd "use qa;grant execute on DeleteQuoteRequestData to idcasca" -ServerInstance $serverName



remove-item -Path $TargetFile

#################################### Execute script to Clean QA DB ##############################################
Invoke-Sqlcmd "Exec CleanQA"  -ServerInstance $serverName 

################################### Execute script to Migrate the DB##############################################
Invoke-Sqlcmd -InputFile \\Horse\wwwroot\DbMigrate\DbMigrate.sql -ServerInstance $serverName 

