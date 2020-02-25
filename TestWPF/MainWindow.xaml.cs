using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.Storage;
using Windows.System;
using Windows.System.UserProfile;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            IReadOnlyList<User> users = await User.FindAllAsync();
            var current = users.Where(p => p.AuthenticationStatus == UserAuthenticationStatus.LocallyAuthenticated &&
                                        p.Type == UserType.LocalUser).FirstOrDefault();

            // user may have username
            var data = await current.GetPropertyAsync(KnownUserProperties.AccountName);
            string displayName = (string)data;

            //or may be authinticated using hotmail 
            if (String.IsNullOrEmpty(displayName))
            {

                string a = (string)await current.GetPropertyAsync(KnownUserProperties.FirstName);
                string b = (string)await current.GetPropertyAsync(KnownUserProperties.LastName);
                displayName = string.Format("{0} {1}", a, b);
            }

            MyTextBlock.Text = displayName;
        }
    }
}
