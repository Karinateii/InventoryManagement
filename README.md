# Laboratory Inventory Management System

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Web application for managing laboratory supplies, suppliers, and purchase orders. Built with ASP.NET Core MVC and Entity Framework Core.

## Features

- Track lab supplies with quantity monitoring and reorder alerts
- Manage supplier information and contact details  
- Create and track purchase orders with partial fulfillment support
- Upload product images
- Role-based access control (Admin/User roles)
- Responsive Bootstrap UI with DataTables
- AJAX operations and form validation

## Tech Stack

**Backend:** ASP.NET Core 8.0 MVC, Entity Framework Core, SQL Server, ASP.NET Core Identity

**Frontend:** Bootstrap 5, jQuery, DataTables, SweetAlert2

**Patterns:** Repository Pattern, Unit of Work, Dependency Injection

## Architecture

Layered architecture with clear separation:

```
Presentation → Models → Data Access → Database
```

See [ARCHITECTURE.md](ARCHITECTURE.md) for details.

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or higher)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/downloads)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Karinateii/InventoryManagement.git
   cd InventoryManagement
   ```

2. **Configure the database connection**
   
   Update the connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(LocalDb)\\MSSQLLocalDb;Database=LIMS;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

3. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

4. **Apply database migrations**
   ```bash
   cd InventoryManagement
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the application**
   
   Navigate to: `https://localhost:5001`

## Project Structure

```
InventoryManagement/
├── Inventory.DataAccess/          # Data access layer
│   ├── Data/
│   │   └── AppDbContext.cs        # EF Core DbContext
│   ├── Migrations/                # EF Core migrations
│   └── Repository/                # Repository implementations
│       ├── IRepository/           # Repository interfaces
│       ├── Repository.cs          # Generic repository
│       ├── LabSupplyRepository.cs
│       ├── SupplierRepository.cs
│       └── UnitOfWork.cs
│
├── Inventory.Models/              # Domain models and ViewModels
│   ├── Models/
│   │   ├── LabSupply.cs
│   │   ├── Supplier.cs
│   │   ├── PurchaseOrder.cs
│   │   └── ErrorViewModel.cs
│   ├── ViewModels/
│   │   └── LabSupplyVM.cs
│   └── ApplicationUser.cs         # Extended Identity user
│
├── Inventory.Utility/             # Cross-cutting concerns
│   ├── SD.cs                      # Static constants
│   └── EmailSender.cs             # Email service
│
└── InventoryManagement/           # Main web application
    ├── Areas/
    │   ├── Admin/                 # Admin area
    │   │   ├── Controllers/
    │   │   └── Views/
    │   ├── Identity/              # Identity scaffolded pages
    │   └── User/                  # User area
    ├── Controllers/               # Root controllers
    ├── Views/                     # Razor views
    ├── wwwroot/                   # Static files
    │   ├── css/
    │   ├── js/
    │   └── images/
    ├── appsettings.json
    └── Program.cs                 # Application entry point
```

## Database Schema

### Core Entities

**LabSupply**
- `SupplyID` (PK)
- `SupplyName`
- `QuantityOnHand`
- `ReorderPoint`
- `ImageURL`
- `SupplierID` (FK → Supplier)

**Supplier**
- `SupplierID` (PK)
- `SupplierName`
- `ContactPerson`
- `ContactEmail`

**PurchaseOrder**
- `OrderID` (PK)
- `SupplyID` (FK → LabSupply)
- `OrderDate`
- `QuantityOrdered`
- `OrderStatus`

### Relationships
- One-to-Many: Supplier → LabSupplies
- One-to-Many: LabSupply → PurchaseOrders

## Routes

### Lab Supplies
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/Admin/LabSupplies/Index` | List all supplies |
| GET | `/Admin/LabSupplies/Upsert/{id?}` | Create/Edit form |
| POST | `/Admin/LabSupplies/Upsert` | Create/Update supply |
| GET | `/Admin/LabSupplies/GetAll` | JSON data for DataTables |
| DELETE | `/Admin/LabSupplies/Delete/{id}` | Delete supply (AJAX) |

### Suppliers
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/Admin/Suppliers/Index` | List all suppliers |
| GET | `/Admin/Suppliers/Create` | Create form |
| POST | `/Admin/Suppliers/Create` | Create supplier |
| GET | `/Admin/Suppliers/Edit/{id}` | Edit form |
| POST | `/Admin/Suppliers/Edit` | Update supplier |
| GET | `/Admin/Suppliers/Delete/{id}` | Delete confirmation |
| POST | `/Admin/Suppliers/Delete` | Delete supplier |

### Purchase Orders
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/PurchaseOrders/Index` | List all orders |
| GET | `/PurchaseOrders/Create` | Create form |
| POST | `/PurchaseOrders/Create` | Create order |
| GET | `/PurchaseOrders/Edit/{id}` | Edit form |
| POST | `/PurchaseOrders/Edit` | Update order |
| GET | `/PurchaseOrders/Delete/{id}` | Delete confirmation |
| POST | `/PurchaseOrders/Delete` | Delete order |

## Development

### Building the Project
```bash
dotnet build
```

### Running Tests
```bash
dotnet test
```

### Creating a New Migration
```bash
cd Inventory.DataAccess
dotnet ef migrations add MigrationName --startup-project ../InventoryManagement
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Author

[@Karinateii](https://github.com/Karinateii)
