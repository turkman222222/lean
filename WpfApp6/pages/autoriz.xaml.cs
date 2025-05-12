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
    /// Логика взаимодействия для autoriz.xaml
    /// </summary>
    public partial class autoriz : Page
    {
        public autoriz()
        {
            InitializeComponent();
        }

        private void btnAutoriz_Click(object sender, RoutedEventArgs e)
        {
            var user_object = AppDate.AppConnect.model1.user.FirstOrDefault(x => x.login == txtLogin.Text && x.password == passBoxpassword.Password);

            if (user_object == null)
            {
                MessageBox.Show("Ошибка авторизации", "не знаем", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (user_object != null) 
            {
                MessageBox.Show("Привет " + user_object.user_name + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppConnect.id_user = user_object.rol_id;

                // Проверка роли пользователя
                if (user_object.rol_id == 1)
                {
                    // Переход на первое окно
                    userpanel firstWindow = new userpanel();
                    NavigationService.Navigate(firstWindow);
                }
                else if (user_object.rol_id == 2)
                {
                    // Переход на второе окно
                    adminpanel secondWindow = new adminpanel();
                    NavigationService.Navigate(secondWindow);
                }
                else
                {
                    // Обработка случая, если роль не соответствует ни одной из указанных
                    MessageBox.Show("Неизвестная роль пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            reg reg = new reg();
            NavigationService.Navigate(reg);
        }

        
    }
}
