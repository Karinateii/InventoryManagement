# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0] - 2025-11-26

### Added
- Comprehensive README.md with project overview, features, and setup instructions
- CONTRIBUTING.md with contribution guidelines and coding standards
- CODE_OF_CONDUCT.md for community standards
- ARCHITECTURE.md with detailed system architecture documentation
- LICENSE file (MIT License)
- .editorconfig for consistent coding standards across IDEs
- appsettings.example.json template for secure configuration
- XML documentation comments throughout codebase
- Structured logging using Serilog with file and console output
- Async/await support across repository layer
- Repository pattern enhancements with better error handling
- Database retry logic for resilient connections
- Enhanced .gitignore with application-specific exclusions

### Changed
- **BREAKING**: Repository methods now async (`GetAsync`, `GetAllAsync`, `AddAsync`)
- **BREAKING**: UnitOfWork now includes `SaveAsync()` method
- Improved Program.cs with structured logging and comprehensive Identity configuration
- Enhanced LabSupply model with better validation and computed properties
- Improved Supplier model with stricter validation rules
- Enhanced PurchaseOrder model with proper data annotations
- Refactored LabSupplyRepository with `Update` method (capitalized)
- Refactored SupplierRepository with additional `GetByEmailAsync` method
- Improved JavaScript in supply.js with ES6+ features, error handling, and JSDoc
- Updated DbContext with relationship configurations and indexes
- Repository pattern now includes null checks and proper exception handling

### Improved
- Code quality with consistent naming conventions
- Error handling throughout the application
- Security with HTTPS enforcement and anti-forgery tokens
- Performance with database connection pooling and async operations
- Maintainability with clear separation of concerns
- Documentation with comprehensive inline comments

### Security
- Protected sensitive configuration data (appsettings.json excluded from git)
- Enhanced password requirements (8+ characters, mixed case, numbers, symbols)
- Account lockout after 5 failed attempts
- Secure cookie configuration with sliding expiration

## [1.0.0] - 2024-01-08

### Added
- Initial project setup with ASP.NET Core MVC
- Entity Framework Core with SQL Server
- ASP.NET Core Identity for authentication
- Basic CRUD operations for Lab Supplies
- Basic CRUD operations for Suppliers
- Basic CRUD operations for Purchase Orders
- Bootstrap UI framework
- DataTables for data display
- Image upload functionality for lab supplies
- Repository pattern implementation
- Unit of Work pattern
- Area-based routing (Admin, User, Identity)

### Features
- User registration and login
- Lab supply inventory management
- Supplier contact management
- Purchase order tracking
- Reorder point monitoring
- Responsive design

---

## Version History

- **2.0.0** - Professional refactoring with best practices (2025-11-26)
- **1.0.0** - Initial working version (2024-01-08)
