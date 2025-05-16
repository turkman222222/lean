using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class UserEditPage : Page
    {
        public user CurrentUser { get; set; }
        public string Title { get; set; }

        public UserEditPage()
        {
            InitializeComponent();
            CurrentUser = new user();
            Title = "Добавление пользователя";
            DataContext = this;
            LoadRoles();
        }

        public UserEditPage(user selectedUser)
        {
            InitializeComponent();
            CurrentUser = selectedUser;
            Title = "Редактирование пользователя";
            DataContext = this;
            LoadRoles();
        }

        private void LoadRoles()
        {
            cmbRoles.ItemsSource = AppConnect.model2.rol.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentUser.user_name) ||
                string.IsNullOrWhiteSpace(CurrentUser.login) ||
                string.IsNullOrWhiteSpace(CurrentUser.mail))
            {
                MessageBox.Show("Заполните все обязательные поля!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Если пароль был изменен
                if (!string.IsNullOrEmpty(txtPassword.Password))
                {
                    CurrentUser.password = txtPassword.Password;
                }

                if (CurrentUser.id_user == 0)
                    AppConnect.model2.user.Add(CurrentUser);

                AppConnect.model2.SaveChanges();
                MessageBox.Show("Данные сохранены!", "Успех");
                AppFrame.frmMane2.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Отменить изменения?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                AppFrame.frmMane2.GoBack();
            }
        }
    }
}