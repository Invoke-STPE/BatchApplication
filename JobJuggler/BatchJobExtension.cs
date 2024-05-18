using System.Reflection;
using JobJuggler.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace JobJuggler;

public static class BatchJobExtension
{
    public static IServiceCollection RegisterBatchJobs(this IServiceCollection services)
    {
        var batchJobAttribute = typeof(BatchJobAttribute);

        // Get the path of the executing assembly
        // var path = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);

        // // Load all assemblies from the executing assembly's directory
        // var assemblies = Directory.GetFiles(path!, "*.dll")
        //                         .Select(Assembly.LoadFrom)
        //                         .ToArray();

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        // Find all types with the BatchJobAttribute
        var types = assemblies.SelectMany(a => a.GetTypes())
                            .Where(p => p.IsDefined(batchJobAttribute, false) && !p.IsInterface)
                            .Select(s => (Service: s.GetInterfaces().FirstOrDefault(), Implementation: s))
                            .Where(t => t.Service != null);

        
        foreach (var (Service, Implementation) in types)
        {
            services.AddTransient(Service, Implementation);
        }

        return services;
    }
}