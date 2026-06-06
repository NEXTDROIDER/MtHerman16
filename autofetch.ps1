$path = "C:\Users\Student\Documents\GitHub\MtHerman16"
$intervalSeconds = 5

cd $path
Clear-Host
Write-Host "=== GITHUB AUTO-SYNC STARTED ===" -ForegroundColor Cyan
Write-Host "Checking for remote updates every $intervalSeconds seconds..." -ForegroundColor Gray
Write-Host "Project folder: $path`n" -ForegroundColor Gray

while ($true) {
    $time = Get-Date -Format "HH:mm:ss"
    
    # 1. Fetch the latest changes from the cloud
    git fetch origin 2>$null
    
    # 2. Check if your branch is behind the remote branch
    $status = git status -sb
    
    if ($status -match "behind") {
        Write-Host "[$time] New updates detected from your friend! Pulling files..." -ForegroundColor Yellow
        
        # 3. Safely download changes.
        # If you have unsaved edits, stash will temporarily hide them,
        # perform the pull, and then restore your local work on top.
        git stash 2>$null
        git pull origin --rebase
        git stash pop 2>$null
        
        Write-Host "[$time] Project successfully updated. Unity will now reload assets.`n" -ForegroundColor Green
    }
    
    # Wait before the next check
    Start-Sleep -Seconds $intervalSeconds
}
