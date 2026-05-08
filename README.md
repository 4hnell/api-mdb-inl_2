## Inlämning del 2: Mormor Dagnys Bageri

#### Setup:

```bash
cat > appsettings.Development.json << EOF
{
  "ConnectionStrings": {
    "sqlite": "Data Source=mdb.db",
    "mysql": "MYSQL_CONNECTION_STRING"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Information"
    }
  }
}
EOF
```

```bash
cat > appsettings.json << EOF
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Information"
    }
  },
  "AllowedHosts": "*"
}
EOF
```

#### Run:

```bash
dotnet build

docker-compose up -d

cd api

dotnet run
```
