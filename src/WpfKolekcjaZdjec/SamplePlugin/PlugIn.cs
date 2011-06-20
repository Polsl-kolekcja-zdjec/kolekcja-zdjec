using System.Windows;
using WpfKolekcjaZdjec.Plugins;

namespace SamplePlugin
{
    /// <summary>
    /// Sample plugin implementation.
    /// </summary>
    public class PlugIn : IPlugin
    {
        /// <summary>
        /// Sample name.
        /// </summary>
        private string _name;

        /// <summary>
        /// Host plugin.
        /// </summary>
        private IHostPlugin _host;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlugIn"/> class.
        /// </summary>
        public PlugIn()
        {
            _name = "SampleNameForPlugin";
        }
        
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Sample execution.
        /// </summary>
        /// <returns>
        /// Status of operation.
        /// </returns>
        public bool Execute()
        {
            MessageBox.Show("Hello from plugin!");
            return true;
        }

        /// <summary>
        /// Gets or sets the plugin's host.
        /// </summary>
        /// <value>The host.</value>
        public IHostPlugin Host
        {
            get
            {
                return _host;
            }

            set
            {
                _host = value;
                _host.Register(this);
            }
        }
    }
}