# 🧪 Laboratory Inventory Management System (LIMS)

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4)](https://docs.microsoft.com/en-us/aspnet/core/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-512BD4)](https://docs.microsoft.com/en-us/ef/core/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A professional, full-stack web application for managing laboratory supplies, suppliers, and purchase orders. Built with ASP.NET Core MVC, Entity Framework Core, and modern web technologies, following industry-standard design patterns and best practices.

## 📋 Table of Contents

- [Features](#-features)
- [Technology Stack](#-technology-stack)
- [Architecture](#-architecture)
- [Getting Started](#-getting-started)
- [Project Structure](#-project-structure)
- [Database Schema](#-database-schema)
- [API Endpoints](#-api-endpoints)
- [Screenshots](#-screenshots)
- [Contributing](#-contributing)
- [License](#-license)

## ✨ Features

### Core Functionality
- **Inventory Management**: Track laboratory supplies with real-time quantity monitoring
- **Supplier Management**: Maintain comprehensive supplier information and contact details
- **Purchase Order System**: Create and manage purchase orders linked to specific supplies
- **Reorder Alerts**: Automatic alerts when supplies reach reorder points
- **Image Management**: Upload and manage product images for visual identification

### Security & Authentication
- **Identity Management**: Secure user authentication and authorization using ASP.NET Core Identity
- **Role-Based Access Control**: Admin and User roles with appropriate permissions
- **Protected Routes**: Area-based authorization for administrative functions

### User Experience
- **Responsive Design**: Mobile-first, responsive UI using Bootstrap 5
- **Interactive DataTables**: Advanced sorting, filtering, and pagination for large datasets
- **AJAX Operations**: Smooth, asynchronous data operations without page reloads
- **Sweet Alerts**: User-friendly confirmation dialogs and notifications
- **Real-time Validation**: Client-side and server-side validation for data integrity

## 🛠 Technology Stack

### Backend
- **Framework**: ASP.NET Core 8.0 MVC
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server (LocalDB for development)
- **Authentication**: ASP.NET Core Identity
- **Language**: C# 12

### Frontend
- **UI Framework**: Bootstrap 5.3
- **JavaScript Libraries**: 
  - jQuery 3.x
  - DataTables.js
  - SweetAlert2
  - Toastr
- **Icons**: Bootstrap Icons
- **CSS**: Custom CSS with Bootstrap theming

### Architecture & Patterns
- **Repository Pattern**: Abstraction layer for data access
- **Unit of Work Pattern**: Transaction management across multiple repositories
- **Dependency Injection**: Built-in ASP.NET Core DI container
- **Areas**: Organized by functional modules (Admin, Identity, User)
- **View Models**: Separation of concerns between domain models and presentation

## 🏗 Architecture

This project follows **Clean Architecture** principles with clear separation of concerns:

```
┌─────────────────────────────────────────────────────┐
│                  Presentation Layer                 │
│            (InventoryManagement - MVC)              │
│  Controllers │ Views │ ViewModels │ wwwroot        │
└─────────────────────┬───────────────────────────────┘
                      │
┌─────────────────────┴───────────────────────────────┐
│                  Business Layer                      │
│             (Inventory.Models)                       │
│    Domain Models │ ViewModels │ Validation          │
└─────────────────────┬───────────────────────────────┘
                      │
┌─────────────────────┴───────────────────────────────┐
│              Data Access Layer                       │
│           (Inventory.DataAccess)                     │
│  DbContext │ Repositories │ Unit of Work            │
└─────────────────────┬───────────────────────────────┘
                      │
┌─────────────────────┴───────────────────────────────┐
│                 Utility Layer                        │
│              (Inventory.Utility)                     │
│     Constants │ Email Service │ Helpers             │
└──────────────────────────────────────────────────────┘
```

### Design Patterns Implemented

1. **Repository Pattern**: Generic repository for CRUD operations
2. **Unit of Work Pattern**: Manages transactions across repositories
3. **Dependency Injection**: Loose coupling and testability
4. **MVC Pattern**: Separation of concerns in presentation layer
5. **ViewModels**: Data transfer objects for complex views

## 🚀 Getting Started

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
   
   Open your browser and navigate to: `https://localhost:5001` or `http://localhost:5000`

### Default Credentials

After running migrations, you may need to register a new user or seed an admin account.

## 📁 Project Structure

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

## 🗄 Database Schema

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

## 🔌 API Endpoints

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

## 📸 Screenshots

> **Note**: Add screenshots of your application here to showcase the UI and features.

### Dashboard
![Dashboard](docs/images/dashboard.png)

### Lab Supplies Management
![Lab Supplies](docs/images/lab-supplies.png)

### Supplier Management
![Suppliers](docs/images/suppliers.png)

## 🧪 Development

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

### Code Standards
- Follow Microsoft's C# Coding Conventions
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Keep methods focused and single-purpose
- Use async/await for I/O operations

## 🤝 Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct and the process for submitting pull requests.

### Development Workflow
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

**Karina**
- GitHub: [@Karinateii](https://github.com/Karinateii)
- LinkedIn: [Your LinkedIn Profile](https://linkedin.com/in/yourprofile)

## 🙏 Acknowledgments

- ASP.NET Core team for the excellent framework
- Bootstrap team for the responsive UI framework
- DataTables for the powerful table plugin
- The open-source community for inspiration and support

## 📞 Support

For questions or support, please:
- Open an issue on GitHub
- Contact via email: your.email@example.com

---

⭐ **If you find this project helpful, please consider giving it a star!**
