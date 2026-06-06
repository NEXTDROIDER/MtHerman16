$path = "C:\Users\Student\Documents\GitHub\MtHerman16"
$filter = "*.*"

cd $path
Write-Host "Repository monitoring started..." -ForegroundColor Green

$fsw = New-Object IO.FileSystemWatcher $path, $filter
$fsw.IncludeSubdirectories = $true
$fsw.EnableRaisingEvents = $true

$action = {
    $filePath = $Event.SourceEventArgs.FullPath
    # Ignore changes inside the .git folder
    if ($filePath -notmatch '\\\.git\\') {
        $time = Get-Date -Format "HH:mm:ss"
        Write-Host "[$time] Changed file: $filePath" -ForegroundColor Yellow

        # Git commands execution
        git add -A
        git commit -m "Auto-commit: file changed"
        # Uncomment the line below if you need auto-push to GitHub:
        # git push
    }
}

Register-ObjectEvent $fsw Changed -Action $action
Register-ObjectEvent $fsw Created -Action $action
Register-ObjectEvent $fsw Deleted -Action $action

while ($true) { Start-Sleep 1 }
