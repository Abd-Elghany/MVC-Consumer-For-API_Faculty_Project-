# MVC_Consumer_For_API_Faculty_Project

An **ASP.NET Core MVC** web application that acts as a **consumer client** for the `API_Faculty_Project` (a RESTful API managing Departments, Instructors, and Course Resources).

This project provides a user-friendly interface to interact with the backend API, displaying data and allowing CRUD operations where authorized.

---

## 🧩 Features

- Integration with `API_Faculty_Project` using HttpClient
- User login via JWT and token-based session
- Role-based UI controls (e.g., Admin vs Instructor)
- CRUD operations for:
  - Departments
  - Instructors
  - Course Resources
- Error handling and token refresh logic
- Bootstrap UI with responsive layout

---

## 🏗️ Project Structure

MVC_Consumer_For_API_Faculty_Project/
│
├── Controllers/ # MVC controllers (e.g., DepartmentController)
│
├── Models/ # ViewModels & DTOs matching API structure
│
├── Services/ # API service classes using HttpClient
│
├── Views/ # Razor views (.cshtml)
│
│ ├── Shared/ # Layout, partials
│
│ ├── Department/ # Department views
│
│ ├── Instructor/ # Instructor views
│ └── Course/ # Course resource views
│
├── wwwroot/ # Static assets (CSS, JS, etc.)
│
├── appsettings.json # App config
│
├── Program.cs # App bootstrap
│
├── Startup.cs (if used) # Middleware/services (older versions)
│
└── README.md # Project documentation
---

## 🔐 Authentication

This MVC app uses **JWT Bearer Token Authentication**:

- Users log in via `/Account/Login`
- On successful login, the token is stored in the session
- Token is added to every outgoing HttpClient request
- Unauthorized users are redirected to login

---

## 🔗 API Communication

The app consumes endpoints from:

http://localhost:5186/api/


> ✅ Make sure the **API_Faculty_Project** is running before launching the MVC app.

All API calls are made through typed `HttpClient` wrappers in the `Services/` directory, which handle:

- Token injection
- Error checking
- Serialization

---

## ⚙️ How to Run

### 1. Prerequisites

- [.NET 6 SDK or later](https://dotnet.microsoft.com/)
- Visual Studio 2022+ or VS Code

### 2. Clone the Repository

git clone https://github.com/your-username/MVC_Consumer_For_API_Faculty_Project.git
cd MVC_Consumer_For_API_Faculty_Project

3. Configure API Base URL
In appsettings.json, set the backend API base URL:
"ApiSettings": {
  "BaseUrl": "https://localhost:5001/api/"
}

4. Run the Application
dotnet run

🖼️ UI Pages
URL	Description
/Account/Login	Login form to authenticate user
/Department/Index	List all departments
/Instructor/Index	List all instructors
/Course/Index	List all course resources
/Department/Create	Create new department (Admin only)
/Instructor/Create	Create new instructor (Admin only)

🚧 To-Do / Ideas
🔄 Token refresh on expiration
🔒 JWT stored securely (not in plain session)
📁 Upload documents with course resources

📝 License
This project is licensed under the MIT License.

🙌 Contributing
Contributions are welcome! Please fork the repository and open a pull request with suggested changes.

### Would you like to:

- Include sample screenshots of the UI?
- Add Docker support instructions?
- Include Swagger usage for development/debugging?

Let me know if you want this turned into an actual `.md` file!
