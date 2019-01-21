using Base;
using System;

namespace Plugin1
{
    public class Plugin1 : IPluginBase
    {
        public void PrintData()
        {
            Console.WriteLine($"Plugin1: {typeof(Newtonsoft.Json.JsonConverter).AssemblyQualifiedName}");
        }
    }
}
