using System.Windows;
using System.Windows.Controls;
using WpfKolekcjaZdjec.Plugins;

namespace SamplePlugin
{
    /// <summary>
    /// Interaction logic for SampleForm.xaml.
    /// </summary>
    public partial class SampleForm : Window, IPlugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleForm"/> class.
        /// </summary>
        public SampleForm()
        {
            InitializeComponent();

            _name = "SampleFormPlugin";
        }

        /// <summary>
        /// Sample name.
        /// </summary>
        private string _name;

        /// <summary>
        /// Host plugin.
        /// </summary>
        private IHostPlugin _host;

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
            this.Show();
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