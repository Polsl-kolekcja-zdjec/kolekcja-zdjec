using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace WpfKolekcjaZdjec.Business
{
    /// <summary>
    /// Help provider.
    /// </summary>
    public static class HelpProvider
    {
        /// <summary>
        /// Initializes the <see cref="HelpProvider"/> class.
        /// </summary>
        static HelpProvider()
        {
            CommandManager.RegisterClassCommandBinding(typeof(FrameworkElement), new CommandBinding(ApplicationCommands.Help,
                                                                                                    new ExecutedRoutedEventHandler(Executed),
                                                                                                    new CanExecuteRoutedEventHandler(CanExecute)));
        }

        /// <summary>
        /// Determines whether this instance can execute the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private static void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            FrameworkElement senderElement = sender as FrameworkElement;

            if (HelpProvider.GetHelpString(senderElement) != null)
            {
                e.CanExecute = true;
            }
        }

        /// <summary>
        /// Executeds the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private static void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Help.ShowHelp(null, @"Help\Help.chm");
        }

        /// <summary>
        /// Gets the help string.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string GetHelpString(DependencyObject obj)
        {
            return (string)obj.GetValue(HelpStringProperty);
        }

        /// <summary>
        /// Sets the help string.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetHelpString(DependencyObject obj, string value)
        {
            obj.SetValue(HelpStringProperty, value);
        }

        /// <summary>
        /// Help string property.
        /// </summary>
        public static readonly DependencyProperty HelpStringProperty = DependencyProperty.RegisterAttached("HelpString", typeof(string), typeof(HelpProvider));
    }
}