## Inlämning del 2: Mormor Dagnys Bageri

Min postman collection här i repot innehåller alla anrop jag gjort under arbetet med inlämingen.

#### Skapa appsettings-filerna:

```bash
cat > api/appsettings.Development.json << EOF
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
cat > api/appsettings.json << EOF
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

Min MySQL connection string var `Server=localhost;Port=3307;User Id=root;Password=password;Database=mdb_db;`.

#### För att köra:

```bash
dotnet build

docker-compose up -d

cd api

dotnet run
```

Det går även att köra med Sqlite genom att endast byta connection string i Program.cs och göra om migreringen.
