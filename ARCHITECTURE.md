# Architecture Documentation

## Overview

The Laboratory Inventory Management System (LIMS) follows **Clean Architecture** principles with a focus on separation of concerns, maintainability, and testability. The application is structured into multiple projects, each with a specific responsibility.

## System Architecture

### High-Level Architecture Diagram

```
┌─────────────────────────────────────────────────────┐
│                     Client Layer                     │
│                  (Web Browser)                       │
│   HTML5 │ CSS3 │ Bootstrap │ jQuery │ DataTables   │
└─────────────────────┬───────────────────────────────┘
                      │ HTTPS
                      ↓
┌─────────────────────────────────────────────────────┐
│              Presentation Layer (MVC)                │
│         InventoryManagement Web Application          │
├─────────────────────────────────────────────────────┤
│  • Controllers (Admin, User, PurchaseOrders)        │
│  • Razor Views & Partial Views                      │
│  • ViewModels for complex data binding              │
│  • Static Files (CSS, JS, Images)                   │
│  • Authentication & Authorization (Identity)        │
└─────────────────────┬───────────────────────────────┘
                      │
                      ↓
┌─────────────────────────────────────────────────────┐
│                  Business Layer                      │
│              Inventory.Models Project                │
├─────────────────────────────────────────────────────┤
│  • Domain Models (Entities)                         │
│    - LabSupply, Supplier, PurchaseOrder            │
│  • ViewModels for UI data transfer                  │
│  • Data Annotations & Validation                    │
│  • Business Logic Properties                        │
└─────────────────────┬───────────────────────────────┘
                      │
                      ↓
┌─────────────────────────────────────────────────────┐
│               Data Access Layer                      │
│          Inventory.DataAccess Project                │
├─────────────────────────────────────────────────────┤
│  • AppDbContext (EF Core DbContext)                 │
│  • Repository Pattern Implementation                │
│    - Generic Repository                             │
│    - LabSupplyRepository, SupplierRepository        │
│  • Unit of Work Pattern                             │
│  • Database Migrations                              │
└─────────────────────┬───────────────────────────────┘
                      │
                      ↓
┌─────────────────────────────────────────────────────┐
│                  Utility Layer                       │
│            Inventory.Utility Project                 │
├─────────────────────────────────────────────────────┤
│  • Static Constants (SD)                            │
│  • Email Service Implementation                     │
│  • Helper Classes & Extension Methods              │
└─────────────────────┬───────────────────────────────┘
                      │
                      ↓
┌─────────────────────────────────────────────────────┐
│                  Database Layer                      │
│             SQL Server (LocalDB)                     │
├─────────────────────────────────────────────────────┤
│  • LabSupplies Table                                │
│  • Suppliers Table                                  │
│  • PurchaseOrders Table                             │
│  • AspNetUsers, AspNetRoles (Identity)             │
└──────────────────────────────────────────────────────┘
```

## Design Patterns

### 1. Repository Pattern

**Purpose**: Abstracts data access logic and provides a cleaner API for data operations.

**Implementation**:
- `IRepository<T>`: Generic interface for CRUD operations
- `Repository<T>`: Generic implementation using EF Core
- `ILabSupplyRepository`, `ISupplierRepository`: Entity-specific interfaces with custom operations
- `LabSupplyRepository`, `SupplierRepository`: Concrete implementations

**Benefits**:
- Decouples business logic from data access
- Easier unit testing through mocking
- Centralized data access logic
- Consistent API across different entities

### 2. Unit of Work Pattern

**Purpose**: Manages transactions across multiple repository operations.

**Implementation**:
- `IUnitOfWork`: Interface defining repository properties and Save methods
- `UnitOfWork`: Concrete implementation managing repository instances and DbContext

**Benefits**:
- Ensures data consistency across multiple operations
- Single point for database transaction management
- Reduces duplicate DbContext instances
- Simplified resource management

### 3. Dependency Injection (DI)

**Purpose**: Achieves loose coupling and improves testability.

**Implementation**:
```csharp
// Program.cs
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddDbContext<AppDbContext>(options => ...);
```

**Benefits**:
- Loose coupling between components
- Easy to swap implementations
- Simplified unit testing
- Better code maintainability

### 4. MVC (Model-View-Controller) Pattern

**Purpose**: Separates application into three interconnected components.

**Implementation**:
- **Models**: `Inventory.Models` project contains domain entities
- **Views**: Razor views in `Views` folders and `Areas`
- **Controllers**: Handle HTTP requests and coordinate between Model and View

**Benefits**:
- Clear separation of concerns
- Parallel development possible
- Easier maintenance and testing
- Reusable components

### 5. ViewModel Pattern

**Purpose**: Provides a specialized model for complex view requirements.

**Implementation**:
```csharp
public class LabSupplyVM
{
    public LabSupply LabSupply { get; set; }
    public IEnumerable<SelectListItem> SupplierList { get; set; }
}
```

**Benefits**:
- Separates domain models from presentation logic
- Combines data from multiple sources
- Reduces coupling between views and domain models

## Project Structure Details

### 1. InventoryManagement (Web Application)

**Responsibilities**:
- HTTP request handling
- User authentication and authorization
- View rendering
- Client-side resource serving

**Key Components**:
- **Areas**: Organize features into logical modules
  - `Admin`: Administrative functions (Lab Supplies, Suppliers)
  - `User`: User-facing features (Home, Dashboard)
  - `Identity`: Authentication pages (Login, Register)
- **Controllers**: Handle HTTP requests
- **Views**: Razor templates for HTML generation
- **wwwroot**: Static files (CSS, JavaScript, images)

### 2. Inventory.DataAccess

**Responsibilities**:
- Database context configuration
- Entity mappings
- Data access operations
- Migration management

**Key Components**:
- `AppDbContext`: EF Core context managing entity sets
- `Repository`: Generic and specific repository implementations
- `Migrations`: Database schema version control

### 3. Inventory.Models

**Responsibilities**:
- Domain entity definitions
- Business validation rules
- Data annotations
- ViewModels for UI

**Key Entities**:
- `LabSupply`: Laboratory supply inventory item
- `Supplier`: Supply vendor information
- `PurchaseOrder`: Order tracking
- `ApplicationUser`: Extended Identity user

### 4. Inventory.Utility

**Responsibilities**:
- Cross-cutting concerns
- Shared constants
- Helper services

**Key Components**:
- `SD`: Static constants (roles, statuses)
- `EmailSender`: Email service implementation

## Data Flow

### 1. Read Operation Flow

```
User Request → Controller → UnitOfWork → Repository → 
DbContext → Database → Entity → ViewModel → View → Response
```

**Example**: Viewing Lab Supplies List
1. User navigates to `/Admin/LabSupplies/Index`
2. `LabSuppliesController.Index()` is invoked
3. Controller calls `_unitOfWork.LabSupply.GetAllAsync(includeProperties: "Supplier")`
4. Repository queries database through EF Core
5. Results mapped to `LabSupply` entities
6. Controller passes data to View
7. Razor view renders HTML with DataTables
8. Response sent to browser

### 2. Write Operation Flow

```
User Submission → Controller → Model Validation → 
Repository → UnitOfWork.Save → DbContext → Database
```

**Example**: Creating a New Supplier
1. User submits form data
2. `SuppliersController.Create(Supplier obj)` receives data
3. Model validation checks data annotations
4. `_unitOfWork.Supplier.AddAsync(obj)` adds entity
5. `_unitOfWork.SaveAsync()` persists changes
6. Redirect to Index with success notification

## Security Architecture

### Authentication & Authorization

**Implementation**: ASP.NET Core Identity

**Features**:
- User registration and login
- Password hashing and storage
- Role-based access control
- Cookie-based authentication
- Account lockout protection

**Authorization Levels**:
```csharp
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class LabSuppliesController : Controller
```

### Data Protection

- **Connection Strings**: Stored in `appsettings.json` (excluded from source control in production)
- **HTTPS**: Enforced for all requests
- **CSRF Protection**: Anti-forgery tokens on forms
- **SQL Injection**: Protected via parameterized queries (EF Core)
- **XSS Protection**: Razor automatically encodes output

## Database Schema

### Entity Relationships

```
Supplier (1) ─────< (Many) LabSupply (1) ─────< (Many) PurchaseOrder
     │                           │
     │                           └─── ImageURL (File System)
     └─── Contact Information
```

### Key Tables

**LabSupplies**
- Primary Key: `SupplyID`
- Foreign Key: `SupplierID` → Suppliers
- Business Logic: `NeedsReorder` (computed property)

**Suppliers**
- Primary Key: `SupplierID`
- Navigation: `LabSupplies` collection

**PurchaseOrders**
- Primary Key: `OrderID`
- Foreign Key: `SupplyID` → LabSupplies

**Identity Tables**
- AspNetUsers, AspNetRoles, AspNetUserRoles, etc.

## Technology Stack

### Backend
- **Framework**: ASP.NET Core 8.0 MVC
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server / LocalDB
- **Authentication**: ASP.NET Core Identity
- **Language**: C# 12

### Frontend
- **UI Framework**: Bootstrap 5.3
- **JavaScript**: ES6+, jQuery 3.x
- **Components**: DataTables, SweetAlert2, Toastr
- **Icons**: Bootstrap Icons

### Development Tools
- **IDE**: Visual Studio 2022 / VS Code
- **Version Control**: Git
- **Package Manager**: NuGet

## Logging and Monitoring

**Implementation**: Serilog

**Features**:
- Structured logging
- File and console sinks
- Request logging middleware
- Minimum level configuration per source

**Configuration**:
```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
```

## Error Handling

### Global Exception Handling

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
```

### Controller-Level Handling

- Model validation errors
- Try-catch blocks for critical operations
- User-friendly error messages via TempData
- Logging of exceptions

## Performance Considerations

### Database Optimization

1. **Async Operations**: All database calls use async/await
2. **Eager Loading**: Include related entities when needed
3. **No-Tracking Queries**: For read-only operations
4. **Connection Resiliency**: Retry logic for transient failures

### Frontend Optimization

1. **DataTables**: Client-side processing for large datasets
2. **AJAX**: Asynchronous operations without page reload
3. **Minified Assets**: Production builds use minified CSS/JS
4. **Image Optimization**: Guidelines for uploaded images

## Scalability Considerations

### Horizontal Scaling

- Stateless application design
- Session state can be moved to distributed cache
- Load balancer compatible

### Vertical Scaling

- Async/await prevents thread blocking
- Connection pooling for database
- Efficient query patterns

## Testing Strategy

### Recommended Approach

1. **Unit Tests**: Test business logic in isolation
2. **Integration Tests**: Test repository and database operations
3. **Controller Tests**: Test MVC controller actions
4. **UI Tests**: Selenium/Playwright for end-to-end testing

### Testability Features

- Dependency injection for easy mocking
- Repository pattern abstracts data access
- Separation of concerns enables focused testing

## Future Enhancements

### Recommended Improvements

1. **API Layer**: Add RESTful API for mobile/external access
2. **Caching**: Implement distributed caching (Redis)
3. **Search**: Add full-text search capabilities
4. **Reporting**: Generate PDF/Excel reports
5. **Notifications**: Real-time alerts for low stock
6. **Audit Trail**: Track all data modifications
7. **Multi-tenancy**: Support multiple organizations
8. **Barcode Scanning**: Mobile app integration

## Development Best Practices

1. **Code Style**: Follow .editorconfig rules
2. **Naming Conventions**: PascalCase for public members
3. **Comments**: XML documentation for public APIs
4. **Git Workflow**: Feature branches, pull requests
5. **Code Reviews**: Required before merging
6. **Continuous Integration**: Automated builds and tests

## Deployment

### Development Environment

```bash
dotnet run --environment Development
```

### Production Deployment

1. Publish application: `dotnet publish -c Release`
2. Configure production database connection string
3. Run migrations: `dotnet ef database update`
4. Deploy to IIS / Azure App Service / Docker
5. Configure HTTPS certificate
6. Set up monitoring and logging

## Conclusion

This architecture provides a solid foundation for a maintainable, scalable, and testable web application. The clear separation of concerns, use of established design patterns, and adherence to SOLID principles make the codebase easy to understand, extend, and maintain.
