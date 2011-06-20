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

            Type objType = null;
            string args = string.Empty;

            for (int i = 0; i < pluginFiles.Length; i++)
            {
                // Loading plugins.
                try
                {
                    // TODO: Not working yet - exception :(
                    args = pluginFiles[i].Substring(pluginFiles[i].LastIndexOf("\\") + 1, pluginFiles[i].ToLowerInvariant().IndexOf(".dll") - pluginFiles[i].LastIndexOf("\\") + 3);
                    Assembly assembly = Assembly.Load(args);

                    if (assembly != null)
                    {
                        objType = assembly.GetType(args + ".PlugIn");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Plugins Subsystem Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Registering plugins.
                try
                {
                    if (objType != null)
                    {
                        registeredPlugins[i] = (IPlugin)Activator.CreateInstance(objType);
                        registeredPlugins[i].Host = host;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Plugins Loading Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}