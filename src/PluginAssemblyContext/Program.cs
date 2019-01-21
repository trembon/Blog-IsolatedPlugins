using Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TEST_AssemblyContext
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] plugins = new string[]
            {
                @"...",
                @"..."
            };

            Type pluginType = typeof(IPluginBase);

            List<PluginAssemblyLoadContext> contexts = new List<PluginAssemblyLoadContext>();
            foreach(string pluginPath in plugins)
            {
                PluginAssemblyLoadContext context = new PluginAssemblyLoadContext(pluginPath, pluginType);
                context.Initialize();

                contexts.Add(context);
            }

            foreach (var context in contexts)
                foreach (var plugin in context.GetImplementations<IPluginBase>())
                    plugin.PrintData();

            Console.ReadLine();
        }
    }
}
