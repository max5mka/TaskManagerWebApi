using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Models.Services;

namespace TaskManagerWebApi.Models
{
    public static class Extensions
    {
        public static IServiceCollection AddData(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IWorkItemService, WorkItemService>();
            return serviceCollection;
        }
    }
}
