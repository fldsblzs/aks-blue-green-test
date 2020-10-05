
while ($true) {
    
    $response = Invoke-WebRequest -Uri "http://10.0.1.243/version" -UseBasicParsing
    Write-Output $response.Content

    Start-Sleep -Seconds 1
}


