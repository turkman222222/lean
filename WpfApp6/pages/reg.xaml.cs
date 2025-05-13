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
    /// Логика взаимодействия для reg.xaml
    /// </summary>
    public partial class reg : Page
    {
        public reg()
        {
            InitializeComponent();
        }

        private void reg55_Click(object sender, RoutedEventArgs e)
        {
            if (AppDate.AppConnect.model2.user.Count(x => x.login == LoginBox.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже есть!", "Уведомление",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                user userObjj = new user()
                {

                    mail = mailBox.Text,
                    user_name = user_nameBox.Text,
                    login = LoginBox.Text,
                    password = PasswordBox.Password,
                    rol_id = 1

                };
                AppDate.AppConnect.model2.user.Add(userObjj);
                AppDate.AppConnect.model2.SaveChanges();
                MessageBox.Show("Данные успешно добавлены!", "Уведомление",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                

            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
    }

