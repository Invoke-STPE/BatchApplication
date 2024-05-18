using System.Reflection;
using BatchApplication.Core;

namespace BatchApplication;

public class BatchJobAssemblyManager
{
    public static Type[] GetBatchJobTypes()
    {
      IEnumerable<string> dllFiles = GetBatchJobDlls();

      var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

      var batchJobsTypes = dllFiles
      .AsParallel()
      .Select(dllFile => LoadAssembly(dllFile, loadedAssemblies))
      .Where(assembly => assembly != null)
      .SelectMany(assembly => ExtractBatchJobTypesFromAssembly(assembly!))
      .ToArray();

      return batchJobsTypes;
    }

    private static IEnumerable<Type> ExtractBatchJobTypesFromAssembly(Assembly assembly)
    {
      var batchJobInterface = typeof(IBatchJob);

      try {
        return assembly.GetTypes()
          .Where(type => batchJobInterface.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error getting types from assembly {assembly.FullName}: {ex.Message}");
        return [];
      }
    }

    private static Assembly? LoadAssembly(string dllFile, Assembly[] loadedAssemblies)
    {
      try
      {
        var assemlyName = AssemblyName.GetAssemblyName(dllFile);
        if (loadedAssemblies.Any(a => a.FullName == assemlyName.FullName)){
          return null;
        }

        return Assembly.LoadFrom(dllFile);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error loading assembly from {dllFile}: {ex.Message}");
        return null;
      }
    }

    private static IEnumerable<string> GetBatchJobDlls()
    {
      string enviromentCurrentDirectory = Environment.CurrentDirectory;

      string workingDirectory = Path.Combine(
        Directory.GetParent(Environment.CurrentDirectory)?.FullName ?? throw new InvalidOperationException("Parent directory not found."),
        "BatchJobs"
      );

      var dllFiles = Directory
      .GetFiles(workingDirectory, "*.dll", new EnumerationOptions() { RecurseSubdirectories = true })
      .Where(path => path.Contains("bin"));

      return dllFiles;
    }
}
