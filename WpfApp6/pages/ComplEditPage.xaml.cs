using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class ComplEditPage : Page
    {
        public compl CurrentCompl { get; set; }
        public string Title { get; set; }

        public ComplEditPage()
        {
            InitializeComponent();
            CurrentCompl = new compl();
            Title = "Добавление новой комплектации";
            DataContext = this;
        }

        public ComplEditPage(compl selectedCompl)
        {
            InitializeComponent();
            CurrentCompl = selectedCompl;
            Title = "Редактирование комплектации";
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentCompl.kompl_name))
            {
                MessageBox.Show("Название комплектации не может быть пустым!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (CurrentCompl.id == 0)
                {
                    AppConnect.model2.compl.Add(CurrentCompl);
                }

                AppConnect.model2.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                AppFrame.frmMane2.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentCompl.id == 0 ||
                AppConnect.model2.Entry(CurrentCompl).State == System.Data.Entity.EntityState.Modified)
            {
                if (MessageBox.Show("Отменить изменения и вернуться?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    return;
                }
            }

            AppFrame.frmMane2.GoBack();
        }
    }
}