using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WpfKolekcjaZdjec.Plugins;

namespace WpfKolekcjaZdjec.Business
{
    /// <summary>
    /// Plugins utilities methods.
    /// </summary>
    public static class PluginsBusiness
    {
        /// <summary>
        /// Registered plugins array.
        /// </summary>
        private static IPlugin[] registeredPlugins;

        /// <summary>
        /// Gets the plugin.
        /// </summary>
        /// <param name="idx">The idx.</param>
        /// <returns>Registered plugin.</returns>
        public static IPlugin GetPlugin(int idx)
        {
            IPlugin returned = null;

            if (idx >= 0 && idx < registeredPlugins.Length)
            {
                returned = registeredPlugins[idx];
            }

            return returned;
        }

        /// <summary>
        /// Registers the plugins from directory.
        /// </summary>
        /// <param name="host">The host.</param>
        public static void RegisterPluginsFromDirectory(IHostPlugin host)
        {
            // Constructing plugins directory
            string directory = ConfigurationManager.AppSettings["PluginsDirectory"];
            if (string.IsNullOrWhiteSpace(directory))
            {
                directory = @"Plugins\";
            }

            string path = Path.Combine(Application.StartupPath, directory);
            string[] pluginFiles = Directory.GetFiles(path, "*.dll");

            // Constructing plugins array.
            registeredPlugins = new IPlugin[pluginFiles.Length];

            for (int i = 0; i < pluginFiles.Length; i++)
            {
                // Loading plugins.
                try
                {
                    Assembly assembly = Assembly.LoadFrom(pluginFiles[i]);

                    // Next we'll loop through all the Types found in the assembly.
                    foreach (Type pluginType in assembly.GetTypes())
                    {
                        if (pluginType.IsPublic) // Only look at public types.
                        {
                            if (!pluginType.IsAbstract)  // Only look at non-abstract types.
                            {
                                // Gets a type object of the interface we need the plugins to match.
                                Type typeInterface = pluginType.GetInterface("WpfKolekcjaZdjec.Plugins.IPlugin", true);

                                // Make sure the interface we want to use actually exists.
                                if (typeInterface != null)
                                {
                                    registeredPlugins[i] = (IPlugin)Activator.CreateInstance(assembly.GetType(pluginType.ToString()));
                                    registeredPlugins[i].Host = host;
                                }

                                typeInterface = null;
                            }
                        }
                    }

                    assembly = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Plugins Subsystem Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}