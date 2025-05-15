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

namespace WpfApp6.pages
{
    /// <summary>
    /// Логика взаимодействия для putadmin.xaml
    /// </summary>
    public partial class putadmin : Page
    {
        public putadmin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bronadmin bronadmin  = new bronadmin();
            NavigationService.Navigate(bronadmin);
        }
      

        private void btncar_Click_1(object sender, RoutedEventArgs e)
        {
            adminpanel adminpanel = new adminpanel();
            NavigationService.Navigate(adminpanel);

        }

        private void buttonbron_Click(object sender, RoutedEventArgs e)
        {
            bronadmin bronadmin = new bronadmin();
            NavigationService.Navigate(bronadmin);

        }
    }
}
