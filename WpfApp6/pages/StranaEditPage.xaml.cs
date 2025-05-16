using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class StranaEditPage : Page
    {
        public strana CurrentStrana { get; set; }
        public string Title { get; set; }

        public StranaEditPage()
        {
            InitializeComponent();
            CurrentStrana = new strana();
            Title = "Добавление страны";
            DataContext = this;
        }

        public StranaEditPage(strana selectedStrana)
        {
            InitializeComponent();
            CurrentStrana = selectedStrana;
            Title = "Редактирование страны";
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentStrana.strana_name))
            {
                MessageBox.Show("Название страны не может быть пустым!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (CurrentStrana.id == 0)
                {
                    AppConnect.model2.strana.Add(CurrentStrana);
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
            if (CurrentStrana.id == 0 ||
                AppConnect.model2.Entry(CurrentStrana).State == System.Data.Entity.EntityState.Modified)
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