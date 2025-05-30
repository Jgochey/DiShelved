# **DiShelved 💻**
<p align="center">
  <a href="#-technologies">Technologies</a> •
  <a href="#-getting-started">Getting Started</a> •
  <a href="#-api-endpoints">API Endpoints</a> •
  <a href="#collaborators">Collaborators</a> •
  <a href="#contribute">Contribute</a>
</p>

<p align="center">
DiShelved is a management app for Item Collections. Whether it's Board Games, Miniatures or Holiday Decorations - DiShelved makes it simple for Users to manage where their Items are stored.
</p>

## 💻 Technologies

- C#
- .NET
- Entity Framework Core
- Moq
- xUnit
- PostgreSQL
- pgAdmin
- Swagger
- Postman

## 🚀 Getting Started

Follow the steps below:

1. Clone the DiShelved repository using `git clone` to copy it to your local machine.
2. Open the project and install the required packages with the following commands:

   ```bash
   dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0
   dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0
   dotnet user-secrets init
   dotnet user-secrets set "DiShelvedDbConnectionString" "Host=localhost;Port=5432;Username=postgres;Password=;Database=DiShelved"

3. Run the following to apply migrations and populate your database:

   ```bash
   dotnet ef database update

---

## 📍 API Endpoints

Here are the main routes of the API:

### 🗂️ Categories

- `GET /Categories/:id`
- `DELETE /Categories/:id`
- `POST /Categories`
- `GET /Categories/User/:userid`
- `PUT /Categories/:id`

### 📦 Containers

- `GET /Containers/:id`
- `DELETE /Containers/:id`
- `POST /Containers`
- `GET /Containers/User/:id`
- `PUT /Containers/:id`

### 🧾 Items

- `GET /Items/:id`
- `DELETE /Items/:id`
- `POST /Items`
- `GET /Items/User/:id`
- `PUT /Items/:id`

### 🔗 ItemCategories

- `DELETE /ItemCategory/:itemId/:categoryId`
- `POST /ItemCategory/:itemId/:categoryId`

### 📍 Locations

- `GET /Locations/:id`
- `DELETE /Locations/:id`
- `POST /Locations`
- `GET /Locations/User/:id`
- `PUT /Locations/:id`

### 🗣️ Users

- `POST /Users`

---

## 📄 Documentation

[View the DiShelved Postman Documentation to learn more about the Endpoints above.](https://documenter.getpostman.com/view/36639418/2sB2qfAzEy?authuser=0)

---
