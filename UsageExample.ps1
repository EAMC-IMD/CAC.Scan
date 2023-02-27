[Reflection.Assembly]::LoadFile("ease\CACScan.dll")
$rawscan = ""
while ($rawscan -ne "EAMCExit") {
  $rawscan = Read-Host "Scan CAC Card"
  $cacscan = [CAC.Scan]::new($rawscan)
  $message = ""
  switch ($cacscan.ScanResult) {
    [CAC.ScanStatus]::NullInput { $message = "No input detected" }
    [CAC.ScanStatus]::InvalidScanData { $message = "A CAC barcode was detected, but errors were detected in scan.  Please try again." }
    [CAC.ScanStatus]::UnsupportedCard { $message = "This ID card is not supported for this application." }
    [CAC.ScanStatus]::UnknownDataFormat { $message = "An unknown barcode was detected" }
  }
  if ($message -ne "") {
    [System.Windows.Forms.MessageBox]::Show($message)
    continue
  } else {
    $edipi = $cacscan.Edipi
    #Do stuff
  }
}
