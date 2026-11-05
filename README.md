# TaskManagerWebApi
Demo project for skills demonstration

**About project:**
REST API for project and task management, built with ASP.NET Core.

**RUN GUIDE:**

`docker compose up -d --build` in root folder

You're up, frontend address by default is: localhost:8080/index.html

Backend swagger is avalable at localhost:7092/swagger/index.html

Default pre configured user: admin Admin123!

## Techhnology stack

*   **Framework:** .NET 9 (ASP.NET Core Web API)
*   **DB:** MS SQL Server, Entity Framework Core (Code First)
*   **Authentication:** JWT (JSON Web Tokens), roles (Admin, Owner, Developer, Observer)
*   **Testing:** xUnit, Moq, Testcontainers (integration tests with a real database in Docker)
*   **Containers:** Docker, Docker Compose
*   **CI/CD:** GitHub Actions (automated build, tests, and Docker image build)
*   **Documentation:** Swagger / OpenAPI
*   **Logging:** Serilog (output to console and database)
