using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            UserListView.ItemsSource = AppConnect.model2.user.ToList();
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmMane2.Navigate(new UserEditPage());
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is user selectedUser)
            {
                AppFrame.frmMane2.Navigate(new UserEditPage(selectedUser));
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is user selectedUser)
            {
                if (selectedUser.bron.Any() || selectedUser.izbr.Any())
                {
                    MessageBox.Show("Нельзя удалить пользователя, так как есть связанные записи!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MessageBox.Show($"Вы точно хотите удалить пользователя {selectedUser.user_name}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        AppConnect.model2.user.Remove(selectedUser);
                        AppConnect.model2.SaveChanges();
                        LoadUsers();
                        MessageBox.Show("Пользователь удален!", "Успех");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка");
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var users = AppConnect.model2.user.ToList();

            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                users = users.Where(u =>
                    u.user_name.ToLower().Contains(txtSearch.Text.ToLower()) ||
                    u.mail.ToLower().Contains(txtSearch.Text.ToLower()) ||
                    u.login.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            }

            UserListView.ItemsSource = users;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmMane2.GoBack();
        }
    }
}