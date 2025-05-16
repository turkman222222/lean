using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class RolPage : Page
    {
        public RolPage()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void LoadRoles()
        {
            RolListView.ItemsSource = AppConnect.model2.rol.ToList();
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmMane2.Navigate(new RolEditPage());
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is rol selectedRol)
            {
                AppFrame.frmMane2.Navigate(new RolEditPage(selectedRol));
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is rol selectedRol)
            {
                if (selectedRol.user.Any())
                {
                    MessageBox.Show("Нельзя удалить роль, так как есть пользователи с этой ролью!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MessageBox.Show($"Вы точно хотите удалить роль {selectedRol.rol_name}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        AppConnect.model2.rol.Remove(selectedRol);
                        AppConnect.model2.SaveChanges();
                        LoadRoles();
                        MessageBox.Show("Роль удалена!", "Успех");
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
            var roles = AppConnect.model2.rol.ToList();

            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                roles = roles.Where(r => r.rol_name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            }

            RolListView.ItemsSource = roles;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmMane2.GoBack();
        }
    }
}