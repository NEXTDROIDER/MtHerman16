$path = "C:\Users\Student\Documents\GitHub\MtHerman16"
$filter = "*.*"

cd $path
Write-Host "Слежение за репозиторием запущено..." -ForegroundColor Green

$fsw = New-Object IO.FileSystemWatcher $path, $filter
$fsw.IncludeSubdirectories = $true
$fsw.EnableRaisingEvents = $true

$action = {
    $filePath = $Event.SourceEventArgs.FullPath
    # Игнорируем изменения внутри самой папки .git
    if ($filePath -notmatch '\\\.git\\') {
        $time = Get-Date -Format "HH:mm:ss"
        Write-Host "[$time] Изменение в: $filePath" -ForegroundColor Yellow

        # Выполняем команды Git
        git add -A
        git commit -m "Авто-коммит: изменение файла"
        # Раскомментируйте строку ниже, если нужен авто-пуш на GitHub:
        # git push
    }
}

Register-ObjectEvent $fsw Changed -Action $action
Register-ObjectEvent $fsw Created -Action $action
Register-ObjectEvent $fsw Deleted -Action $action

while ($true) { Start-Sleep 1 }
