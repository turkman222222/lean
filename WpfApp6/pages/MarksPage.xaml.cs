using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class MarksPage : Page
    {
        public MarksPage()
        {
            InitializeComponent();
            LoadMarks();
        }

        private void LoadMarks()
        {
            MarksListView.ItemsSource = AppConnect.model2.Marks.ToList();
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmMane2.Navigate(new MarkEditPage());
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Marks selectedMark)
            {
                AppFrame.frmMane2.Navigate(new MarkEditPage(selectedMark));
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Marks selectedMark)
            {
                if (selectedMark.Carss.Any())
                {
                    MessageBox.Show("Нельзя удалить марку, так как есть автомобили этой марки!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MessageBox.Show($"Вы точно хотите удалить марку {selectedMark.name_marka}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        AppConnect.model2.Marks.Remove(selectedMark);
                        AppConnect.model2.SaveChanges();
                        LoadMarks();
                        MessageBox.Show("Марка удалена!", "Успех");
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
            var marks = AppConnect.model2.Marks.ToList();

            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                marks = marks.Where(m => m.name_marka.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            }

            MarksListView.ItemsSource = marks;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frmMane2.GoBack();
        }
    }
}