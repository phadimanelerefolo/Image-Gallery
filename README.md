# Image Gallery Web Application

This is a simple Image Gallery web application built using ASP.NET Core with image upload functionality. It also includes a RESTful API for basic image management operations.

## Getting Started

### Prerequisites

Before running the application, make sure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (at least version 6.0)
- [PostgreSQL](https://www.postgresql.org/) (for data storage)

### Clone the Repository

```bash
git clone https://github.com/phadimanelerefolo/Image-Gallery.git
cd image-gallery

### Database Setup

Update the connection string in appsettings.json with your database details.

### Apply Migrations

dotnet ef database update

### Run the Application

dotnet run --project Image.Gallery.WebAPP

The application will be accessible at https://localhost:7008 (or http://localhost:5150).