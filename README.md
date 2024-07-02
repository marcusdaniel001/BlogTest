# Blog API

This project is a simple API for managing blog posts and comments, built with .NET 8 and Entity Framework Core. 
The API allows creating and listing blog posts, as well as adding comments to posts.

## Prerequisites

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

### Running migrations
At the root of the project, you need to run the following command to create the database and its structure
```bash
dotnet ef database update --project .\Infrastructure\ --startup-project .\BlogTest.Api\
```

### Test the api
You can test all methods through swagger
http://localhost:5000/Swagger

- List all posts: GET /api/posts
- Create a new post: POST /api/posts
```
Request body (JSON):
{
  "title": "Post XPTO",
  "content": "This is the content"
}
```

- Get a specific post by ID: GET /api/posts/{id}
- Add a comment to a post: POST /api/posts/{id}/comments
```
Request body (JSON):
{
  "text": "This is a comment."
}
```
You don't need to send any of the IDs, only when linking a comment to a post

### Running automated tests
```bash
dotnet test
```
