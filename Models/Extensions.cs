using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Models.Repositories;
using TaskManagerWebApi.Models.Services;

namespace TaskManagerWebApi.Models
{
    public static class Extensions
    {
        public static IServiceCollection AddData(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IWorkItemRepository, WorkItemRepository>();
            serviceCollection.AddScoped<IWorkItemService, WorkItemService>();
            return serviceCollection;
        }
    }
}
