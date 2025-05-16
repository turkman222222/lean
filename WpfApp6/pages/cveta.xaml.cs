using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class cveta : Page
    {
        public cveta()
        {
            InitializeComponent();
            Prods.ItemsSource = AppConnect.model2.cveta.ToList();

        }



        private void AddToFavoritesItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is AppDate.cveta selectedColor)
            {
                // Логика добавления цвета в избранное, если нужно
                MessageBox.Show($"Цвет {selectedColor.cvet_name} добавлен в избранное!");
            }
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is AppDate.cveta selectedColor)
            {
                // Переход на страницу редактирования цвета
                cvetaEditPage editPage = new cvetaEditPage(selectedColor);
                AppFrame.frmMane2.Navigate(editPage);
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is AppDate.cveta selectedColor)
            {
                if (MessageBox.Show($"Вы точно хотите удалить цвет {selectedColor.cvet_name}?",
                    "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Проверка на связанные автомобили
                        if (selectedColor.Carss.Any())
                        {
                            MessageBox.Show("Нельзя удалить цвет, так как есть автомобили с этим цветом!",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        AppConnect.model2.cveta.Remove(selectedColor);
                        AppConnect.model2.SaveChanges();
                        MessageBox.Show("Цвет удален!");
                        Prods.ItemsSource = AppConnect.model2.cveta.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                    }
                }
            }
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            // Переход на страницу добавления нового цвета
            cvetaEditPage addPage = new cvetaEditPage();
            AppFrame.frmMane2.Navigate(addPage);
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            // Возврат на предыдущую страницу
            AppFrame.frmMane2.GoBack();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var colors = AppConnect.model2.cveta.ToList();

            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                colors = colors.Where(x => x.cvet_name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            }

            Prods.ItemsSource = colors;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            putadmin putadmin = new putadmin();
            AppFrame.frmMane2.Navigate(putadmin);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            cvetaEditPage editPage = new cvetaEditPage();
            AppFrame.frmMane2.Navigate(editPage);
        }

        private void txtSearch_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            var colors = AppConnect.model2.cveta.ToList();

            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                colors = colors.Where(x => x.cvet_name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            }

            Prods.ItemsSource = colors;
        }
    }
}