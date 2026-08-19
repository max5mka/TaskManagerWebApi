using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagerWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WorkItems",
                columns: new[] { "Id", "Description", "Priority", "Status", "Title" },
                values: new object[,]
                {
                    { 1, "Создать диаграмму компонентов и выбрать технологии (EF Core, JWT, Swagger)", "P1", "In Progress", "Разработать архитектуру бэкенда" },
                    { 2, "Реализовать регистрацию, логин, выдачу токенов и middleware для валидации", "P1", "In Progress", "Настроить JWT-аутентификацию" },
                    { 3, "Написать контроллер, DTO, маппинг и репозиторий для управления рабочими элементами", "P2", "New", "Создать CRUD для WorkItems" },
                    { 4, "Покрыть тестами методы GetById, Add, Update, Delete с использованием Moq", "P2", "New", "Написать юнит-тесты для репозитория" },
                    { 5, "Настроить генерацию документации, добавить примеры запросов/ответов", "P3", "In Progress", "Интегрировать Swagger/OpenAPI" },
                    { 6, "Реализовать Middleware для перехвата исключений и возврата кастомных JSON-ошибок", "P2", "Pending", "Настроить глобальную обработку ошибок" },
                    { 7, "Добавить параметры PageNumber, PageSize, фильтр по статусу и категории", "P3", "New", "Внедрить пагинацию и фильтрацию" },
                    { 8, "Написать Dockerfile и docker-compose для запуска API вместе с SQL Server", "P4", "Pending", "Подготовить Docker-контейнер" },
                    { 9, "Добавить индексы, использовать AsNoTracking для чтения, избежать N+1", "P5", "Closed", "Оптимизировать запросы к БД" },
                    { 10, "Описать цели, технологии, инструкцию по запуску и примеры API-запросов", "P5", "Closed", "Написать README для проекта" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkItems");
        }
    }
}
