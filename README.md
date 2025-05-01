# Ensolvers Notes App – Full-Stack Technical Challenge

A full-stack application for managing personal notes with user authentication, archiving functionality, and note categorization.  
Built using **ASP.NET Core 8 Web API** + **React 19.1.0**, with **Entity Framework Core 9.0.4** and **SQL Server LocalDB**.

> **Note:** When launching the React app at `http://localhost:3000/`, you can click the **"Create Account"** button to register your own user account and start managing notes.

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

| Technology       | Version    |
| ---------------- | ---------- |
| .NET SDK         | 8.0        |
| Entity Framework | Core 9.0.4 |
| ASP.NET Core     | Web API    |
| SQL Server       | LocalDB    |
| React            | 19.1.0     |
| Node.js          | 18.x LTS   |
| React Bootstrap  | 2.10.9     |
| Axios            | 1.9.0      |
| React Router     | 7.5.3      |

---

## Prerequisites

To run this project locally, make sure you have the following installed:

- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download)
- [Node.js 18.x](https://nodejs.org/)
- [npm](https://www.npmjs.com/)
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
dotnet ef database update # Apply migrations if not done yet
dotnet run
```

This will start the ASP.NET Web API at <http://localhost:5000>
Step 2: Start the Frontend

Open a new terminal:

```bash
cd frontend
npm install # Only required the first time
npm start
```

This will launch the React development server at <http://localhost:3000>

Now visit <http://localhost:3000/> in your browser.
From there, you can click the "Create Account" button to register your own user and start using the app.

### API Endpoints

Method Endpoint Description
POST /api/auth/login User login, returns JWT
POST /api/auth/register Register a new user
GET /api/notes Get active notes
GET /api/notes/archived Get archived notes
POST /api/notes Create a new note
PUT /api/notes/{id} Update a note
DELETE /api/notes/{id} Delete a note
POST /api/notes/{id}/archive Archive/unarchive a note
GET /api/categories Get list of all categories
POST /api/categories Create a new category

Swagger UI available at: <http://localhost:5000/swagger/index.html>
