$path = "C:\Users\Student\Documents\MTHermanTF2 16\MTHermanTF216"
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

        # 1. Фиксируем локальные изменения
        git add -A
        # Проверяем, есть ли что коммитить, чтобы не плодить пустые коммиты
        $status = git status --porcelain
        if ($status) {
            git commit -m "Auto-commit: file changed"
            Write-Host "[$time] Local commit created." -ForegroundColor Cyan
        }

        # 2. Проверяем обновления на GitHub
        Write-Host "[$time] Checking remote repository (git fetch)..." -ForegroundColor Gray
        git fetch origin

        # Получаем статус локальной ветки относительно удаленной
        $localStatus = git status -sb

        # 3. Безопасно отправляем или стягиваем изменения
        if ($localStatus -match "behind") {
            Write-Host "[$time] Remote changes detected! Pulling first..." -ForegroundColor Magenta
            git pull --rebase origin main
            Write-Host "[$time] Pushing your changes to GitHub..." -ForegroundColor Green
            git push origin main
        }
        else {
            Write-Host "[$time] Pushing changes to GitHub..." -ForegroundColor Green
            git push origin
        }
    }
}

# Сброс старых подписок, чтобы избежать дублирования событий при перезапуске
Get-EventSubscriber | Unregister-Event -ErrorAction SilentlyContinue

Register-ObjectEvent $fsw Changed -Action $action
Register-ObjectEvent $fsw Created -Action $action
Register-ObjectEvent $fsw Deleted -Action $action

while ($true) { Start-Sleep 1 }
