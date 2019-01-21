using Base;
using System;

namespace Plugin2
{
    public class Plugin2 : IPluginBase
    {
        public void PrintData()
        {
            Console.WriteLine($"Plugin2: {typeof(Newtonsoft.Json.JsonConverter).AssemblyQualifiedName}");
        }
    }
}
