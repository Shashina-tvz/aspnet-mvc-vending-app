param(
    [string]$jsonInput
)

# Definiraj log fajl
$logFile = ".github/hooks/agent_log.txt"

# Napravi timestamp za zapis (kad je hook stvarno pokrenut)
$now = Get-Date -Format "yyyy-MM-dd HH:mm:ss"

# Formiraj "pretty" log liniju
$logLine = "[$now] INPUT RECEIVED:`n$jsonInput`n------------------------`n"

# Zapiši u fajl
Add-Content -Path $logFile -Value $logLine