using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class RolEditPage : Page
    {
        public rol CurrentRol { get; set; }
        public string Title { get; set; }

        public RolEditPage()
        {
            InitializeComponent();
            CurrentRol = new rol();
            Title = "Добавление роли";
            DataContext = this;
        }

        public RolEditPage(rol selectedRol)
        {
            InitializeComponent();
            CurrentRol = selectedRol;
            Title = "Редактирование роли";
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentRol.rol_name))
            {
                MessageBox.Show("Название роли не может быть пустым!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (CurrentRol.id == 0)
                    AppConnect.model2.rol.Add(CurrentRol);

                AppConnect.model2.SaveChanges();
                MessageBox.Show("Данные сохранены!", "Успех");
                AppFrame.frmMane2.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Отменить изменения?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                AppFrame.frmMane2.GoBack();
            }
        }
    }
}