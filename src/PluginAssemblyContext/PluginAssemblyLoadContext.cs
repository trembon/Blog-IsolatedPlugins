using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace TEST_AssemblyContext
{
    public class PluginAssemblyLoadContext : AssemblyLoadContext
    {
        private List<Assembly> loadedAssemblies;
        private Dictionary<string, Assembly> sharedAssemblies;

        private string path;

        public PluginAssemblyLoadContext(string path, params Type[] sharedTypes)
        {
            this.path = path;

            this.loadedAssemblies = new List<Assembly>();
            this.sharedAssemblies = new Dictionary<string, Assembly>();

            foreach (Type sharedType in sharedTypes)
                sharedAssemblies[Path.GetFileName(sharedType.Assembly.Location)] = sharedType.Assembly;
        }

        public void Initialize()
        {
            foreach (string dll in Directory.EnumerateFiles(path, "*.dll"))
            {
                if (sharedAssemblies.ContainsKey(Path.GetFileName(dll)))
                    continue;
                
                loadedAssemblies.Add(this.LoadFromAssemblyPath(dll));
            }
        }

        public IEnumerable<T> GetImplementations<T>()
        {
            return loadedAssemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(T).IsAssignableFrom(t))
                .Select(t => Activator.CreateInstance(t))
                .Cast<T>();
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            string filename = $"{assemblyName.Name}.dll";
            if (sharedAssemblies.ContainsKey(filename))
                return sharedAssemblies[filename];

            return Assembly.Load(assemblyName);
        }
    }
}
