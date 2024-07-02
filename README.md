# Blog API

This project is a simple API for managing blog posts and comments, built with .NET 8 and Entity Framework Core. 
The API allows creating and listing blog posts, as well as adding comments to posts.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Project Setup

### Clone the Repository

Clone this repository to your local machine:

```bash
git clone https://github.com/your-username/blog-api.git
cd blog-api
```

### Build and Run with Docker Compose
In the project's root directory, run the following command to build and start the containers:
```bash
docker-compose up --build
```
This will:

Download the PostgreSQL image.
Build the API image.
Start both containers.
The API will be available at http://localhost:5000/Swagger and the PostgreSQL database will be accessible on port 5432.

### Test the api
You can test all methods through swagger

### Running Tests
```bash
dotnet test
```
