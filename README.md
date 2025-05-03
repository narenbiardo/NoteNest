# NoteNest

A full-stack application for managing personal notes with user authentication, archiving functionality, and note categorization.  
Built using **ASP.NET Core Web API** + **React**, with **Entity Framework Core** and **SQL Server Express LocalDB**.

> **Note:** When launching the React app at `http://localhost:3000/`, you can click on **"Create Account"** to register your own user account and start managing notes.

---

## Table of Contents

- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Installation & Setup](#installation--setup)
- [Database Migrations and Seeding](#database-migrations-and-seeding)
- [How to Run the Application](#how-to-run-the-application)
- [API Endpoints](#api-endpoints)

---

## Technologies Used

| Technology                 | Version     |
| -------------------------- | ----------- |
| .NET SDK                   | 8.0         |
| Entity Framework Core      | 9.0.4       |
| ASP.NET Core               | 8.0.16      |
| SQL Server Express LocalDB | 2019        |
| React                      | 19.1.0      |
| Node.js                    | 22.15.0 LTS |
| Axios                      | 1.9.0       |
| React Router               | 7.5.3       |

---

## Prerequisites

To run this project locally, make sure you have the following installed:

- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js 22.x](https://nodejs.org/en/download)
- [npm](https://www.npmjs.com/package/download)
- [SQL Server Express LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)

---

## Installation & Setup

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/ensolvers-notes.git
cd ensolvers-notes
```

### 2. Configure the Backend

Open backend/appsettings.json and ensure the database connection string is correct.
By default, it uses SQL Server LocalDB:

```bash
"DefaultConnection": "Server=(localdb)\\ENSOLVERSNOTESDB;Database=EnsolversChallenge;Trusted_Connection=True;"
```

Make sure LocalDB is installed and working on your system.

---

### 3. Configure the Frontend

Ensure the React frontend is set to proxy requests to the backend.
In frontend/package.json:

```bash
"proxy": "http://localhost:7200"
```

### Database Migrations and Seeding

Before running the app, apply the database migrations:

```bash
cd backend
dotnet ef database update
```

### How to Run the Application

Automatic Method (with script)

A simple bash script run.sh is provided for convenience. It:
-Applies EF migrations
-Starts the backend server
-Starts the frontend React app

```bash
chmod +x run.sh
./run.sh
```

Manual Method
Step 1: Start the Backend

```bash
cd backend
dotnet ef database update
dotnet run
```

This will start the ASP.NET Web API at <http://localhost:5000>
Step 2: Start the Frontend

Open a new terminal:

```bash
cd frontend
npm install
npm start
```

This will launch the React development server at <http://localhost:3000>

Now visit <http://localhost:3000/> in your browser.
From there, you can click the "Create Account" button to register your own user and start using the app.

### API Endpoints

| Method                                                                 | Endpoint                               | Description                   |
| :--------------------------------------------------------------------- | :------------------------------------- | :---------------------------- |
| ![POST](https://img.shields.io/badge/POST-blue?style=for-the-badge)    | `/user/login`                          | User login, returns JWT       |
| ![POST](https://img.shields.io/badge/POST-blue?style=for-the-badge)    | `/user/register`                       | Register a new user           |
| ![GET](https://img.shields.io/badge/GET-green?style=for-the-badge)     | `/user/notes`                          | Get all user’s notes          |
| ![GET](https://img.shields.io/badge/GET-green?style=for-the-badge)     | `/note/{noteId}`                       | Get a note                    |
| ![PUT](https://img.shields.io/badge/PUT-yellow?style=for-the-badge)    | `/note/{noteId}`                       | Update a note                 |
| ![DELETE](https://img.shields.io/badge/DELETE-red?style=for-the-badge) | `/note/{noteId}`                       | Delete a note                 |
| ![POST](https://img.shields.io/badge/POST-blue?style=for-the-badge)    | `/note/`                               | Create a new note             |
| ![GET](https://img.shields.io/badge/GET-green?style=for-the-badge)     | `/note/active`                         | Get all active user's notes   |
| ![GET](https://img.shields.io/badge/GET-green?style=for-the-badge)     | `/note/archived`                       | Get all archived user's notes |
| ![POST](https://img.shields.io/badge/POST-blue?style=for-the-badge)    | `/note/{noteId}/archive`               | Archive a note                |
| ![POST](https://img.shields.io/badge/POST-blue?style=for-the-badge)    | `/note/{noteId}/unarchive`             | Unarchive a note              |
| ![GET](https://img.shields.io/badge/GET-green?style=for-the-badge)     | `/note/{noteId}/categories`            | Get categories from a note    |
| ![POST](https://img.shields.io/badge/POST-blue?style=for-the-badge)    | `/note/{noteId}/category/{categoryId}` | Add a category to a note      |
| ![DELETE](https://img.shields.io/badge/DELETE-red?style=for-the-badge) | `/note/{noteId}`                       | Remove a category from a note |
| ![GET](https://img.shields.io/badge/GET-green?style=for-the-badge)     | `/category/{categoryId}`               | Get a category                |
| ![PUT](https://img.shields.io/badge/PUT-yellow?style=for-the-badge)    | `/category/{categoryId}`               | Update a category             |
| ![DELETE](https://img.shields.io/badge/DELETE-red?style=for-the-badge) | `/category/{categoryId}`               | Delete a category             |
| ![POST](https://img.shields.io/badge/POST-blue?style=for-the-badge)    | `/category`                            | Create a new category         |
| ![GET](https://img.shields.io/badge/GET-green?style=for-the-badge)     | `/note/{noteId}/categories`            | Get notes from a category     |

Swagger UI available at: [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)
