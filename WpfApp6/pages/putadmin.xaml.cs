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
using WpfApp6.AppDate;

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
            RolPage RolPage = new RolPage();
            NavigationService.Navigate(RolPage);
        }

        private void btncar_Click_1(object sender, RoutedEventArgs e)
        {
            adminpanel cvetaff = new adminpanel();
            NavigationService.Navigate(cvetaff);

        }

        private void buttonbron_Click(object sender, RoutedEventArgs e)
        {
            bronadmin bronadmin = new bronadmin();
            NavigationService.Navigate(bronadmin);
        }

        private void btnsalon_Click(object sender, RoutedEventArgs e)
        {
            SalonPage salonnadmin = new SalonPage();
            NavigationService.Navigate(salonnadmin);
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Пустой метод, как в исходном коде
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            cveta cveta = new cveta();
            NavigationService.Navigate(cveta);

        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {

        }

        private void btnmarka_Click(object sender, RoutedEventArgs e)
        {
            MarksPage MarksPage = new MarksPage();
            NavigationService.Navigate(MarksPage);
        }

        private void Button_Click10(object sender, RoutedEventArgs e)
        {
            StranaPage StranaPage = new StranaPage();
            NavigationService.Navigate(StranaPage);
        }

        private void Button_Click11(object sender, RoutedEventArgs e)
        {
            UserPage UserPage = new UserPage();
            NavigationService.Navigate(UserPage);
        }
    }
}