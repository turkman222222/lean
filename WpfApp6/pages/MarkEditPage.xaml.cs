using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class MarkEditPage : Page
    {
        public Marks CurrentMark { get; set; }
        public string Title { get; set; }

        public MarkEditPage()
        {
            InitializeComponent();
            CurrentMark = new Marks();
            Title = "Добавление новой марки";
            DataContext = this;
        }

        public MarkEditPage(Marks selectedMark)
        {
            InitializeComponent();
            CurrentMark = selectedMark;
            Title = "Редактирование марки";
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentMark.name_marka))
            {
                MessageBox.Show("Название марки не может быть пустым!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (CurrentMark.id == 0)
                {
                    AppConnect.model2.Marks.Add(CurrentMark);
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
            if (CurrentMark.id == 0 ||
                AppConnect.model2.Entry(CurrentMark).State == System.Data.Entity.EntityState.Modified)
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