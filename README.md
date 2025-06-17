# MyApp - ASP.NET Core Web API with Entity Framework Core (SQLite)

This project demonstrates how to set up Entity Framework Core with a SQLite database in an ASP.NET Core Web API project.

## 📁 Project Structure

```
MyApp/
├── Controllers/
│   └── UserController.cs
├── Data/
│   └── UserContext.cs
├── DTOs/
│   ├── UserPostDTO.cs
│   └── UserGetDTO.cs
├── Entities/
│   └── UserEntity.cs
├── Migrations/
├── Program.cs
├── appsettings.json
```
## ⚙️ Setup Instructions

### 1. Prerequisites

- [.NET SDK 8+](https://dotnet.microsoft.com/download)
- EF Core CLI:
  ```bash
  dotnet tool install --global dotnet-ef
2. Install Required Packages
bash
Copy
Edit
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
3. Configure DbContext
File: Data/UserContext.cs

csharp
Copy
Edit
public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; }
}
4. Add Connection String
File: appsettings.json

json
Copy
Edit
"ConnectionStrings": {
  "DefaultConnection": "Data Source=users.db"
}
5. Register DbContext in Program.cs
csharp
Copy
Edit
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
6. Add Migrations and Update Database
bash
Copy
Edit
dotnet ef migrations add InitialCreate
dotnet ef database update
This will generate a Migrations folder and a SQLite database file named users.db.

🔥 API Endpoints
Method	Route	Description
POST	/User	Create a new user
GET	/User	Get all users
GET	/User/{id}	Get user by ID

🧪 Notes
DTOs are used to separate request/response models from database entities.

SQLite is lightweight and perfect for demos. For production, you can swap in PostgreSQL or SQL Server.

Swagger UI is enabled by default for testing your endpoints.

💡 Tips
You can remove or add a Repository layer depending on your architecture preference.

For logging, you can inject ILogger<T> into your controllers or services.

✅ Status
Project is working and database is connected with EF Core and SQLite.
