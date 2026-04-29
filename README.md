## Inlämning del 2: Mormor Dagnys Bageri

#### Setup:

```bash
cat > appsettings.Development.json << EOF
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Information"
    }
  },
  "ConnectionStrings": {
    "sqlite": "Data Source=mdb.db"
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
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
EOF
```

```bash
dotnet build
```

```bash
dotnet ef database update
```
