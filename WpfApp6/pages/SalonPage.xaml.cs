using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class SalonPage : Page
    {
        public SalonPage()
        {
            InitializeComponent();
            LoadSalons();
        }

        private void LoadSalons()
        {
            SalonListView.ItemsSource = AppConnect.model2.salonch.ToList();
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmMane2.Navigate(new SalonEditPage());
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is salonch selectedSalon)
            {
                AppFrame.frmMane2.Navigate(new SalonEditPage(selectedSalon));
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is salonch selectedSalon)
            {
                if (selectedSalon.Carss.Any())
                {
                    MessageBox.Show("Нельзя удалить салон, так как есть автомобили этого салона!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MessageBox.Show($"Вы точно хотите удалить салон {selectedSalon.salon}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        AppConnect.model2.salonch.Remove(selectedSalon);
                        AppConnect.model2.SaveChanges();
                        LoadSalons();
                        MessageBox.Show("Салон удален!", "Успех");
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
            var salons = AppConnect.model2.salonch.ToList();

            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                salons = salons.Where(s => s.salon.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            }

            SalonListView.ItemsSource = salons;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmMane2.GoBack();
        }
    }
}