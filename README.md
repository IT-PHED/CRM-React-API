# Finance Management API

![.NET Version](https://img.shields.io/badge/.NET9.0-%23512bd4) 
![License](https://img.shields.io/badge/license-MIT-green)

A modern RESTful API for managing Customer complaints, approvals, and tracking, built with .NET 9 Minimal APIs.

## ✨ Features

- **Customer Complaint**
  - Submit Customer Complaint form
  - View Customer Complaint
  - Approve/reject Customer Complaint

## 🚀 Quick Start

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [MSSQL DB](DB Admin will provide this credentials)

### Installation
```bash
# Clone repository
git clone https://github.com/IT-PHED/HCM-API.git
cd Finance_API

# Configure appsettings
cp appsettings.json appsettings.Development.json

# Run migrations
dotnet ef database update

# Start application
dotnet run
'

### Installation
  - Interactive Swagger docs available at:
    https://localhost:5001/swagger