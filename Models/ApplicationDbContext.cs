using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Models.Entities;

namespace TaskManagerWebApi.Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<WorkItem> WorkItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>().HasData(
                new WorkItem
                {
                    Id = 1,
                    Title = "Разработать архитектуру бэкенда",
                    Description = "Создать диаграмму компонентов и выбрать технологии (EF Core, JWT, Swagger)",
                    Status = "In Progress",
                    Priority = "P1"
                },
                new WorkItem
                {
                    Id = 2,
                    Title = "Настроить JWT-аутентификацию",
                    Description = "Реализовать регистрацию, логин, выдачу токенов и middleware для валидации",
                    Status = "In Progress",
                    Priority = "P1"
                },
                new WorkItem
                {
                    Id = 3,
                    Title = "Создать CRUD для WorkItems",
                    Description = "Написать контроллер, DTO, маппинг и репозиторий для управления рабочими элементами",
                    Status = "New",
                    Priority = "P2"
                },
                new WorkItem
                {
                    Id = 4,
                    Title = "Написать юнит-тесты для репозитория",
                    Description = "Покрыть тестами методы GetById, Add, Update, Delete с использованием Moq",
                    Status = "New",
                    Priority = "P2"
                },
                new WorkItem
                {
                    Id = 5,
                    Title = "Интегрировать Swagger/OpenAPI",
                    Description = "Настроить генерацию документации, добавить примеры запросов/ответов",
                    Status = "In Progress",
                    Priority = "P3"
                },
                new WorkItem
                {
                    Id = 6,
                    Title = "Настроить глобальную обработку ошибок",
                    Description = "Реализовать Middleware для перехвата исключений и возврата кастомных JSON-ошибок",
                    Status = "Pending",
                    Priority = "P2"
                },
                new WorkItem
                {
                    Id = 7,
                    Title = "Внедрить пагинацию и фильтрацию",
                    Description = "Добавить параметры PageNumber, PageSize, фильтр по статусу и категории",
                    Status = "New",
                    Priority = "P3"
                },
                new WorkItem
                {
                    Id = 8,
                    Title = "Подготовить Docker-контейнер",
                    Description = "Написать Dockerfile и docker-compose для запуска API вместе с SQL Server",
                    Status = "Pending",
                    Priority = "P4"
                },
                new WorkItem
                {
                    Id = 9,
                    Title = "Оптимизировать запросы к БД",
                    Description = "Добавить индексы, использовать AsNoTracking для чтения, избежать N+1",
                    Status = "Closed",
                    Priority = "P5"
                },
                new WorkItem
                {
                    Id = 10,
                    Title = "Написать README для проекта",
                    Description = "Описать цели, технологии, инструкцию по запуску и примеры API-запросов",
                    Status = "Closed",
                    Priority = "P5"
                }
            );
        }
    }
}
