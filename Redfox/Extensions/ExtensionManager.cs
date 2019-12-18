using NLog;
using Redfox.Extensions.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Loader;
using System.Text;

namespace Redfox.Extensions
{
    internal class ExtensionManager
    {
        internal Dictionary<string, Type> extensions;

        public ExtensionManager()
        {
            this.extensions = new Dictionary<string, Type>();
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
                        AddExtension(extension, type);
                    }
                }
            }
            if (files.Length == 0)
            {
                LogManager.GetCurrentClassLogger().Info($"No extensions found!");
            }
        }
        public void AddExtension(RedfoxExtension extension, Type extensionType)
        {
            this.extensions.Add(extension.ExtensionName, extensionType);
            LogManager.GetCurrentClassLogger().Info($"Registered a new extension: {extension.ExtensionName}");
        }
    }
}
