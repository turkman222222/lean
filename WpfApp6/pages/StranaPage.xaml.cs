using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class StranaPage : Page
    {
        public StranaPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            StranaListView.ItemsSource = AppConnect.model2.strana.ToList();
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmMane2.Navigate(new StranaEditPage());
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is strana selectedStrana)
            {
                AppFrame.frmMane2.Navigate(new StranaEditPage(selectedStrana));
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is strana selectedStrana)
            {
                if (selectedStrana.Carss.Any())
                {
                    MessageBox.Show("Нельзя удалить страну, так как есть автомобили этой страны!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MessageBox.Show($"Вы точно хотите удалить страну {selectedStrana.strana_name}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        AppConnect.model2.strana.Remove(selectedStrana);
                        AppConnect.model2.SaveChanges();
                        LoadData();
                        MessageBox.Show("Страна удалена!", "Успех");
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
            var stranas = AppConnect.model2.strana.ToList();

            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                stranas = stranas.Where(s => s.strana_name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            }

            StranaListView.ItemsSource = stranas;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmMane2.GoBack();
        }
    }
}