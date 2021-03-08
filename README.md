# AirportBackend

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
