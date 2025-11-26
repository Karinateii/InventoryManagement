# ⚠️ IMPORTANT: Required NuGet Packages

Before running the application, you need to install Serilog packages for logging.

## Quick Installation

Navigate to the main project directory and run:

```bash
cd InventoryManagement
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.File
dotnet add package Serilog.Sinks.Console
```

## Alternative: Restore All Packages

If packages are listed in the .csproj file:

```bash
dotnet restore
```

## Verify Installation

Check if packages are installed:

```bash
dotnet list package
```

You should see:
- Serilog.AspNetCore
- Serilog.Sinks.File  
- Serilog.Sinks.Console

## If Build Fails

1. Clean the solution:
   ```bash
   dotnet clean
   ```

2. Restore packages:
   ```bash
   dotnet restore
   ```

3. Rebuild:
   ```bash
   dotnet build
   ```

## Package Versions

Recommended versions for .NET 8.0:
- Serilog.AspNetCore: 8.0.0 or higher
- Serilog.Sinks.File: 5.0.0 or higher
- Serilog.Sinks.Console: 5.0.0 or higher

These will be automatically selected when using `dotnet add package`.
