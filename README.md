# Product Proxy API (Based on RESTful Mock API)

This is a lightweight .NET 8 Web API that extends the functionality of the public mock API:  
👉 [REST API - Ready to use](https://restful-api.dev/)

## 📌 Purpose

This project acts as a proxy layer that interacts with the external mock API but only manages **products created through this proxy**. It ensures isolation by keeping track of product IDs created by this system.

To keep the solution simple and dependency-free, product IDs are stored **in memory**. No database is used.

⚠️ **Note**: All created product IDs are lost on application restart, requiring re-creation.

---

## ⚙️ Tech Stack & Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or Visual Studio Code

---

## 🧪 API Documentation

Swagger UI is available in development mode:
👉 [Open Swagger UI](https://localhost:7134/swagger)

---

## 🚀 Getting Started

1. Clone the repository:
```bash
git clone https://github.com/victoralmendro/TrueStoryCodeTask.git
cd your-repo-name
dotnet run
```
2. Run the project:
```bash
dotnet run
```
3. Open your browser and access the Swagger UI:
```bash
https://localhost:7134/swagger
```

---

## 🔍 Features
- Proxy for [https://restful-api.dev](https://restful-api.dev)
- In-memory tracking of created product IDs
- Product isolation: only returns and deletes products created through this API
- Add, Retrieve and Delete products

## 📈 Possible Improvements
- Persist product IDs using Redis or local storage
- Save error logs to a file
- Secure endpoints with API key authentication
- Add unit and integration test coverage