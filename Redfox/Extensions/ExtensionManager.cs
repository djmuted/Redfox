using NLog;
using Redfox.Extensions.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Linq;
using Redfox.Messages;

namespace Redfox.Extensions
{
    internal class ExtensionManager
    {
        internal Dictionary<string, Type> extensions;
        internal Dictionary<string, List<Type>> extensionHandlers;

        public ExtensionManager()
        {
            this.extensions = new Dictionary<string, Type>();
            this.extensionHandlers = new Dictionary<string, List<Type>>();
        }
        public void Rescan()
        {
            LogManager.GetCurrentClassLogger().Info($"Scanning for extensions...");
            DirectoryInfo folder = new DirectoryInfo("Extensions");
            FileInfo[] files = folder.GetFiles("*.dll");
            foreach (var file in files)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                LogManager.GetCurrentClassLogger().Info($"Found extension assembly: {file.Name}, version: {assembly.GetName().Version}");
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(RedfoxExtension)))
                    {
                        RedfoxExtension extension = Activator.CreateInstance(type) as RedfoxExtension;
                        AddExtension(extension, type, assembly);
                    }
                }
            }
            if (files.Length == 0)
            {
                LogManager.GetCurrentClassLogger().Info($"No extensions found!");
            }
        }
        public void AddExtension(RedfoxExtension extension, Type extensionType, Assembly assembly)
        {
            this.extensions.Add(extension.ExtensionName, extensionType);
            var types = assembly.GetTypes().Where(p => typeof(IZoneRequestMessage).IsAssignableFrom(p) && !p.IsAbstract).ToList();
            this.extensionHandlers.Add(extension.ExtensionName, types);
            LogManager.GetCurrentClassLogger().Info($"Registered a new extension: {extension.ExtensionName}");
        }
    }
}
