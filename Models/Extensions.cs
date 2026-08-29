using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Models.Services;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Models
{
    public static class Extensions
    {
        public static IServiceCollection AddData(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IWorkItemService, WorkItemService>();
            serviceCollection.AddScoped<IProjectService, ProjectService>();
            return serviceCollection;
        }
    }
}
