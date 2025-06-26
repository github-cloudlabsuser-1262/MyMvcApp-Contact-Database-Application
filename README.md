# MyMvcApp - Contact Database Application

## Overview
MyMvcApp is an ASP.NET Core MVC application designed to manage a contact database. It provides a user-friendly interface for creating, viewing, editing, and deleting contact information.

## Features
- List all contacts
- Add new contacts
- Edit existing contacts
- Delete contacts
- View contact details

## Project Structure
- **Controllers/**: Contains MVC controllers (e.g., `UserController.cs`) that handle HTTP requests and application logic.
- **Models/**: Contains data models (e.g., `User.cs`, `ErrorViewModel.cs`).
- **Views/**: Contains Razor views for rendering UI (e.g., `User/Index.cshtml`, `User/Create.cshtml`).
- **wwwroot/**: Static files (CSS, JS, images, libraries).
- **Program.cs**: Application entry point and configuration.
- **appsettings.json**: Application configuration settings.
- **Tests/**: Unit tests for controllers and models.

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Build and Run
1. **Restore dependencies:**
   ```bash
   dotnet restore
   ```
2. **Build the project:**
   ```bash
   dotnet build
   ```
3. **Run the application:**
   ```bash
   dotnet run
   ```
4. Open your browser and navigate to `https://localhost:5001` or the URL shown in the terminal.

### Running Tests
```bash
dotnet test
```

## Configuration
- **appsettings.json**: General configuration.
- **appsettings.Development.json**: Development-specific settings.

## Default Route
The default route is set to the `User` controller and `Index` action:
```
/{controller=User}/{action=Index}/{id?}
```

## License
This project is licensed under the MIT License.
