# Contributing to Laboratory Inventory Management System

First off, thank you for considering contributing to LIMS! It's people like you that make this project better for everyone.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [How Can I Contribute?](#how-can-i-contribute)
- [Development Setup](#development-setup)
- [Coding Standards](#coding-standards)
- [Pull Request Process](#pull-request-process)
- [Reporting Bugs](#reporting-bugs)
- [Suggesting Enhancements](#suggesting-enhancements)

## Code of Conduct

This project and everyone participating in it is governed by our [Code of Conduct](CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code.

## How Can I Contribute?

### Reporting Bugs

Before creating bug reports, please check the issue list as you might find out that you don't need to create one. When you are creating a bug report, please include as many details as possible:

* **Use a clear and descriptive title**
* **Describe the exact steps to reproduce the problem**
* **Provide specific examples to demonstrate the steps**
* **Describe the behavior you observed and what behavior you expected**
* **Include screenshots if relevant**
* **Include your environment details** (OS, .NET version, browser, etc.)

### Suggesting Enhancements

Enhancement suggestions are tracked as GitHub issues. When creating an enhancement suggestion, please include:

* **Use a clear and descriptive title**
* **Provide a detailed description of the suggested enhancement**
* **Explain why this enhancement would be useful**
* **List some examples of how it would work**

### Pull Requests

1. Fork the repository and create your branch from `master`
2. If you've added code that should be tested, add tests
3. Ensure your code follows the coding standards
4. Make sure your code builds without errors or warnings
5. Update the documentation if needed
6. Write a clear commit message

## Development Setup

1. **Prerequisites**
   ```bash
   # Ensure you have .NET 8.0 SDK installed
   dotnet --version
   ```

2. **Clone your fork**
   ```bash
   git clone https://github.com/YOUR-USERNAME/InventoryManagement.git
   cd InventoryManagement
   ```

3. **Create a branch**
   ```bash
   git checkout -b feature/my-new-feature
   ```

4. **Install dependencies**
   ```bash
   dotnet restore
   ```

5. **Set up the database**
   ```bash
   cd InventoryManagement
   dotnet ef database update
   ```

6. **Run the application**
   ```bash
   dotnet run
   ```

## Coding Standards

### C# Code Style

- Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use meaningful names for variables, methods, and classes
- Keep methods small and focused on a single responsibility
- Add XML documentation comments for public APIs
- Use async/await for asynchronous operations
- Handle exceptions appropriately

**Example:**

```csharp
/// <summary>
/// Retrieves a lab supply by its unique identifier.
/// </summary>
/// <param name="id">The unique identifier of the lab supply.</param>
/// <returns>The lab supply if found; otherwise, null.</returns>
public async Task<LabSupply> GetLabSupplyByIdAsync(int id)
{
    return await _context.LabSupplies
        .Include(l => l.Supplier)
        .FirstOrDefaultAsync(l => l.SupplyID == id);
}
```

### JavaScript Code Style

- Use ES6+ features (const/let, arrow functions, template literals)
- Use meaningful variable names
- Add comments for complex logic
- Handle errors gracefully
- Use async/await for asynchronous operations

**Example:**

```javascript
/**
 * Loads lab supplies data and initializes the DataTable
 */
async function loadDataTable() {
    try {
        const response = await fetch('/admin/LabSupplies/getall');
        const data = await response.json();
        initializeDataTable(data);
    } catch (error) {
        console.error('Error loading data:', error);
        toastr.error('Failed to load lab supplies');
    }
}
```

### Database Migrations

- Use descriptive names for migrations
- Test migrations on a clean database before committing
- Never modify existing migrations that have been pushed to the repository

```bash
# Create a new migration
dotnet ef migrations add AddNewPropertyToLabSupply --startup-project ../InventoryManagement

# Update the database
dotnet ef database update --startup-project ../InventoryManagement
```

## Pull Request Process

1. **Update Documentation**: Ensure any new features are documented in the README.md
2. **Update Changelog**: Add your changes to the CHANGELOG.md file
3. **Test Thoroughly**: Test your changes in multiple scenarios
4. **Clean Commit History**: Squash commits if needed to maintain a clean history
5. **Write Clear PR Description**:
   - What changes were made
   - Why these changes were necessary
   - Any breaking changes
   - Screenshots (if UI changes)

### PR Template

```markdown
## Description
Brief description of what this PR does

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Testing
- [ ] I have tested these changes locally
- [ ] I have added tests that prove my fix is effective or that my feature works
- [ ] All existing tests pass

## Screenshots (if applicable)
Add screenshots here

## Checklist
- [ ] My code follows the style guidelines
- [ ] I have performed a self-review
- [ ] I have commented my code where necessary
- [ ] I have updated the documentation
- [ ] My changes generate no new warnings
```

## Git Commit Messages

* Use the present tense ("Add feature" not "Added feature")
* Use the imperative mood ("Move cursor to..." not "Moves cursor to...")
* Limit the first line to 72 characters or less
* Reference issues and pull requests after the first line

**Examples:**
```
Add supplier email validation

- Implement email validation in Supplier model
- Add server-side validation in controller
- Update view with client-side validation
- Fixes #123
```

## Branch Naming Conventions

* `feature/description` - for new features
* `bugfix/description` - for bug fixes
* `hotfix/description` - for urgent fixes
* `refactor/description` - for code refactoring
* `docs/description` - for documentation updates

## Questions?

Feel free to open an issue with your question, or contact the maintainers directly.

## Recognition

Contributors will be recognized in the project's README and release notes.

---

Thank you for contributing to LIMS! 🎉
