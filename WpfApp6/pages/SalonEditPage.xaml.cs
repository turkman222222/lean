using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class SalonEditPage : Page
    {
        public salonch CurrentSalon { get; set; }
        public string Title { get; set; }

        public SalonEditPage()
        {
            InitializeComponent();
            CurrentSalon = new salonch();
            Title = "Добавление нового салона";
            DataContext = this;
        }

        public SalonEditPage(salonch selectedSalon)
        {
            InitializeComponent();
            CurrentSalon = selectedSalon;
            Title = "Редактирование салона";
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentSalon.salon))
            {
                MessageBox.Show("Название салона не может быть пустым!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (CurrentSalon.id == 0)
                {
                    AppConnect.model2.salonch.Add(CurrentSalon);
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
            if (CurrentSalon.id == 0 ||
                AppConnect.model2.Entry(CurrentSalon).State == System.Data.Entity.EntityState.Modified)
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