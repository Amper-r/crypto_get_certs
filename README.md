# Crypto get certs

{
  "name": "com.certs.native",
  "description": "Example host for native messaging",
  "path": "C:\\Users\\User\\Desktop\\WebServer\\WebServerCert4\\bin\\Debug\\netcoreapp3.1\\WebServerCert4.exe",
  "type": "stdio",
  "allowed_origins": [
    "chrome-extension:\/\/fikdbjmjhoendeooednecckjifgeimmf\/",
	"chrome-extension:\/\/ogjddnbgmiidhadmmmjinegkiknkicoc\/"
  ]
}


reg add HKCU\SOFTWARE\Google\Chrome\NativeMessagingHosts\com.certs.native /d C:\Users\User\AppData\Roaming\CryptoCerts\host_manifest.json /f