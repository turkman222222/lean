using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class ComplPage : Page
    {
        public ComplPage()
        {
            InitializeComponent();
            LoadCompl();
        }

        private void LoadCompl()
        {
            ComplListView.ItemsSource = AppConnect.model2.compl.ToList();
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmMane2.Navigate(new ComplEditPage());
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is compl selectedCompl)
            {
                AppFrame.frmMane2.Navigate(new ComplEditPage(selectedCompl));
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is compl selectedCompl)
            {
                if (selectedCompl.Carss.Any())
                {
                    MessageBox.Show("Нельзя удалить комплектацию, так как есть автомобили с этой комплектацией!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MessageBox.Show($"Вы точно хотите удалить комплектацию {selectedCompl.kompl_name}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        AppConnect.model2.compl.Remove(selectedCompl);
                        AppConnect.model2.SaveChanges();
                        LoadCompl();
                        MessageBox.Show("Комплектация удалена!", "Успех");
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
            var complList = AppConnect.model2.compl.ToList();

            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                complList = complList.Where(c => c.kompl_name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            }

            ComplListView.ItemsSource = complList;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmMane2.GoBack();
        }
    }
}