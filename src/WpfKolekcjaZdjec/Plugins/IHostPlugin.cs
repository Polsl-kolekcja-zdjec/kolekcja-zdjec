using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfKolekcjaZdjec.Plugins
{
    /// <summary>
    /// Interface for plugin's host.
    /// </summary>
    public interface IHostPlugin
    {
        /// <summary>
        /// Registers the specified plugin.
        /// </summary>
        /// <param name="plugin">The plugin interface implementation.</param>
        /// <returns>Status of this operation.</returns>
        bool Register(IPlugin plugin);
    }
}