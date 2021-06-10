# AirportBackend
Backend application for Airport.  
Application allows basic functionality for the users, and provide management capabilites for administrators and employees.  
Application uses Web API for communicaiton. Implementaiton of Swagger allows for creating easy to understand documentation.  
Frontend part of this application is presented in [this](https://github.com/PSI-projekt/AirportFrontend) project.
## Managing migrations
```bash
cd Airport.Infrastructure
dotnet ef --startup-project ../Airport.Api/ migrations add [name]
dotnet ef --startup-project ../Airport.Api/ migrations list
dotnet ef --startup-project ../Airport.Api/ database update [name/0]
dotnet ef --startup-project ../Airport.Api/ migrations remove
```
## Docker
```
docker-compose up -d
```
## Secrets
```
dotnet user-secrets init
dotnet user-secrets set "AppSettings:EncryptionKey" "TAf30yv4g15177S6EW6idxfE5YxyJiCX8Wf2c4nf9Aw="
dotnet user-secrets set "AppSettings:Token" "TAf30yv4g15177S6EW6idxfE5YxyJiCX8Wf2c4nf9Aw="
dotnet user-secrets set "EmailConfiguration:Sender" ""
dotnet user-secrets set "EmailConfiguration:SenderName" "noreply @ OpoleAirport"
dotnet user-secrets set "EmailConfiguration:SmtpServer" "smtp.gmail.com"
dotnet user-secrets set "EmailConfiguration:Port" 465
dotnet user-secrets set "EmailConfiguration:Username" ""
dotnet user-secrets set "EmailConfiguration:Password" ""
dotnet user-secrets set "CloudinarySettings:CloudName" "" 
dotnet user-secrets set "CloudinarySettings:ApiKey" ""
dotnet user-secrets set "CloudinarySettings:ApiSecret" ""
```
