using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfKolekcjaZdjec.Plugins
{
    /// <summary>
    /// Plugin interface.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Executes this plugin.
        /// </summary>
        /// <returns>Status of operation.</returns>
        bool Execute();

        /// <summary>
        /// Gets or sets the plugin's host.
        /// </summary>
        /// <value>The host.</value>
        IHostPlugin Host { get; set; }
    }
}