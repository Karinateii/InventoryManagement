# Setup Instructions

## Prerequisites

Before running this project, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB, Express, or higher)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

## Step-by-Step Setup

### 1. Clone the Repository

```bash
git clone https://github.com/Karinateii/InventoryManagement.git
cd InventoryManagement
```

### 2. Install Required NuGet Packages

The project uses Serilog for logging. Ensure all packages are restored:

```bash
dotnet restore
```

**Key packages required**:
- `Serilog.AspNetCore` - For structured logging
- `Serilog.Sinks.File` - For file logging
- `Serilog.Sinks.Console` - For console logging
- `Microsoft.EntityFrameworkCore.SqlServer` - Database provider
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` - Identity support

To install Serilog packages manually if needed:
```bash
cd InventoryManagement
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.File
dotnet add package Serilog.Sinks.Console
```

### 3. Configure Database Connection

1. Copy `appsettings.example.json` to `appsettings.json`:
   ```bash
   cp InventoryManagement/appsettings.example.json InventoryManagement/appsettings.json
   ```

2. Update the connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(LocalDb)\\MSSQLLocalDb;Database=LIMS;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

   For other SQL Server instances, modify accordingly:
   - **SQL Server Express**: `Server=localhost\\SQLEXPRESS;Database=LIMS;...`
   - **Full SQL Server**: `Server=localhost;Database=LIMS;...`
   - **Azure SQL**: `Server=yourserver.database.windows.net;Database=LIMS;User Id=username;Password=password;`

### 4. Apply Database Migrations

Navigate to the main project directory and run:

```bash
cd InventoryManagement
dotnet ef database update
```

This will create the database and apply all migrations.

**Note**: If you encounter migration issues, ensure you're in the web project directory and that the connection string is correct.

### 5. Run the Application

```bash
dotnet run
```

Or use Visual Studio:
- Open `InventoryManagement.sln`
- Press `F5` or click the "Run" button

### 6. Access the Application

Open your browser and navigate to:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

### 7. Create an Admin User

On first run, register a new user:
1. Click "Register" in the top right
2. Fill in the registration form
3. After registration, you can manually add the Admin role in the database

**Manual Admin Role Assignment** (using SQL Server Management Studio or Azure Data Studio):

```sql
-- Find your user ID
SELECT Id, Email FROM AspNetUsers WHERE Email = 'your.email@example.com';

-- Insert Admin role if it doesn't exist
INSERT INTO AspNetRoles (Id, Name, NormalizedName)
VALUES (NEWID(), 'Admin', 'ADMIN');

-- Assign Admin role to user
INSERT INTO AspNetUserRoles (UserId, RoleId)
SELECT 
    (SELECT Id FROM AspNetUsers WHERE Email = 'your.email@example.com'),
    (SELECT Id FROM AspNetRoles WHERE Name = 'Admin');
```

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
cd InventoryManagement
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Issue: "Serilog not found"
**Solution**:
```bash
cd InventoryManagement
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.File
dotnet add package Serilog.Sinks.Console
```

### Issue: "Port already in use"
**Solution**: Change the port in `Properties/launchSettings.json` or kill the process using the port

## Development Workflow

### Running in Development Mode

```bash
dotnet run --environment Development
```

Development mode provides:
- Detailed error pages
- Hot reload
- Developer exception page
- Database error page

### Creating New Migrations

When you modify entity models:

```bash
cd InventoryManagement
dotnet ef migrations add YourMigrationName
dotnet ef database update
```

### Reverting Migrations

```bash
dotnet ef database update PreviousMigrationName
dotnet ef migrations remove
```

## Production Deployment

### 1. Publish the Application

```bash
dotnet publish -c Release -o ./publish
```

### 2. Configure Production Settings

Create `appsettings.Production.json` (not committed to git):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your-Production-Connection-String"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 3. Deploy to IIS

1. Install [.NET Hosting Bundle](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Copy published files to IIS directory
3. Create application pool targeting "No Managed Code"
4. Configure HTTPS certificate
5. Set appropriate permissions for application pool identity

### 4. Deploy to Azure App Service

```bash
# Using Azure CLI
az webapp up --name your-app-name --resource-group your-rg --runtime "DOTNETCORE:8.0"
```

Or use Visual Studio's "Publish" feature.

## Environment Variables

For production, consider using environment variables instead of `appsettings.json`:

```bash
# Windows
setx ConnectionStrings__DefaultConnection "your-connection-string"

# Linux/Mac
export ConnectionStrings__DefaultConnection="your-connection-string"
```

## Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/ef/core/)
- [Serilog Documentation](https://serilog.net/)
- [Bootstrap Documentation](https://getbootstrap.com/)

## Support

For issues or questions:
- Check the [CONTRIBUTING.md](CONTRIBUTING.md) guide
- Open an issue on GitHub
- Review the [ARCHITECTURE.md](ARCHITECTURE.md) for system details

---

Happy coding! 🚀
