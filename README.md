# 🛠️ MaintenanceChronicleApi

**MaintenanceChronicleApi** is a backend REST API built with **ASP.NET Core** using a **CQS (Command Query Separation)** architecture and backed by a **PostgreSQL** database. It is designed to handle tasks and workflows associated with maintenance tracking and management.

---

## 📦 Tech Stack

- **ASP.NET Core Web API**
- **PostgreSQL**
- **CQS Architecture**
- **JWT Authentication**
- **SMTP Integration (for email workflows)**

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/maintenanceChronicleApi.git
cd maintenanceChronicleApi
```

### 2. Configure `appsettings.json`

Before running the project, ensure you fill in the necessary fields in the `appsettings.json` configuration file.

Here is a sample structure:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DbConnection": "your_postgresql_connection_string"
  },
  //Your info about the UI
  "EnvironmentOptions": {
    "FrontendHostUrl": "http://localhost:4200",
    "FrontendConfirmUrl": "/auth/email-confirm/[Email]/[ConfToken]",
    "FrontendPasswordResetUrl": "/auth/password-reset/[Email]/[PasswordToken]",
    "FrontendPasswordCreateUrl": "/auth/create-password/[Email]/[ConfToken]/[PasswordToken]",
    "SenderEmail": "info@mach.com",
    "SenderName": "Maintenance Chronicle"
  },
  "SmtpOptions": {
    "Host": "your_smtp_host",
    "Port": your_smtp_port,
    "Username": "your_smtp_username",
    "Password": "your_smtp_password"
  },
  "JwtOptions": {
    "SecretKey": "your_very_long_secret_key",
    "Issuer": "url_of_app",
    "Audience": "url_of_ui",
    "AccessTokenExpirationInMinutes": 30,
    "RefreshTokenExpirationInDays": 14
  }
}
```

> ⚠️ Replace all placeholder values with your own credentials.

---

## 🗃️ Database Setup

Ensure you have a running PostgreSQL instance. Update the `DbConnection` string in `appsettings.json` accordingly.

Run the migrations to create the database schema:

```bash
dotnet ef database update
```

---

## 🏃‍♂️ Running the Project

Use the .NET CLI to run the API locally:

```bash
dotnet run
```

The API will start on the default port (usually `https://localhost:5209`).

---

## 🧱 Architectural Overview

This project follows the **CQS (Command Query Separation)** pattern:

- **Commands**: Used for write operations (e.g., create/update/delete).
- **Queries**: Used for read operations (e.g., fetch by ID, list).

This separation promotes cleaner logic, better testability, and a more maintainable codebase.

---

## ✉️ Email Functionality

The API includes SMTP integration for:

- Email confirmation
- Password reset
- Password creation

Configure the `SmtpOptions` and `EnvironmentOptions` in `appsettings.json` to match your email provider and frontend URLs.

---

## 🔐 Authentication

Authentication is handled via **JWT (JSON Web Tokens)**. Ensure the `JwtOptions.SecretKey` is secure and sufficiently long.

