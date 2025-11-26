# Setup

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB, Express, or higher)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

## Installation

```bash
git clone https://github.com/Karinateii/InventoryManagement.git
cd InventoryManagement
```

Install packages:

```bash
dotnet restore
```

Configure the database connection in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(LocalDb)\\MSSQLLocalDb;Database=LIMS;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Apply migrations:

```bash
cd InventoryManagement
dotnet ef database update
```

Run the app:

```bash
dotnet run
```

Access at: `https://localhost:5001`

## Troubleshooting

### Issue: "No database provider has been configured"
**Solution**: Ensure connection string is set in `appsettings.json`

### Issue: "A network-related or instance-specific error occurred"
**Solution**: 
- Verify SQL Server is running
- Check connection string is correct
- Ensure SQL Server LocalDB is installed

### Issue: "Build failed" or "Package not found"
**Solution**:
```bash
dotnet restore
dotnet clean
dotnet build
```

### Issue: "Unable to create migration"
**Solution**:
```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Creating Migrations

When you modify models:

```bash
dotnet ef migrations add YourMigrationName
dotnet ef database update
```

To revert:

```bash
dotnet ef database update PreviousMigrationName
dotnet ef migrations remove
```
