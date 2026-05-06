## Inlämning del 2: Mormor Dagnys Bageri

#### Setup:

```bash
cat > appsettings.Development.json << EOF
{
  "ConnectionStrings": {
    "sqlite": "Data Source=mdb.db",
    "mysql": "Server=localhost;Port=3306;User Id=root;Password=password;Database=mdb_db;"
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

```bash
dotnet build
```

```bash
dotnet ef database update
```
