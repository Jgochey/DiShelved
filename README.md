# **DiShelved 💻**
<p align="center">
  <a href="#-features">Features</a> •
  <a href="#-user-description">User Description</a> •
  <a href="#-technologies">Technologies</a> •
  <a href="#-getting-started">Getting Started</a> •
  <a href="#-api-endpoints">API Endpoints</a> •
  <a href="#-documentation">Documentation</a> •
  <a href="#-contributors">Contributors</a>
</p>

<p align="center">
DiShelved is a management app for Item Collections. Whether it's Board Games, Miniatures or Holiday Decorations - DiShelved makes it simple for Users to manage where their Items are stored.
</p>

## ✨ Features
- Sign-in to save your own collection data
- List new Locations to display where your items are being kept
- Place new Containers in each Location to hold items in
- Add Items to your Containers! Provide Names, Descriptions and Images for each item to keep track of them
- You may also indicate the quantity of an Item(s) and whether or not it is missing any pieces

## 😊 User Description
DiShelved is designed with Item Collectors in mind. A key user would be someone with a large collection of Items such as games or collectables that needs managing.
This is why the Items will indicate whether they are complete or if there are more than one in the collection.
Though the app is built with Collectables in mind, it could also be used by anyone looking to keep track of their belongings, such as furniture, books and more.


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
[See a 1-minute overview of the API Endpoints on Postman.](https://www.loom.com/share/79097f102a1649b9bdb03ba98acacd0d?sid=e7871ba8-5b4d-4055-aba6-9aef7b6e0ec7)
[See the ERD on dbdiagram to understand the relationships between entities.](https://dbdiagram.io/d/DiShelved-68227fd95b2fc4582f4a2b7a)
[View the DiShelved Project board for progress on the latest updates.](https://github.com/users/Jgochey/projects/11)

---

## 👨‍🔬 Contributors
[Josh Gochey](https://github.com/Jgochey)
