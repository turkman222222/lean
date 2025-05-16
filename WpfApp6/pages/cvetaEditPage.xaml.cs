using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    /// <summary>
    /// Логика взаимодействия для cvetaEditPage.xaml
    /// </summary>
 
    
        public partial class cvetaEditPage : Page
        {
            private AppDate.cveta _currentColor;

            public cvetaEditPage()
            {
                InitializeComponent();
                // Режим добавления нового цвета
                _currentColor = new AppDate.cveta();
                DataContext = _currentColor;
                Title = "Добавление нового цвета";
            }

            public cvetaEditPage(AppDate.cveta selectedColor)
            {
                InitializeComponent();
                // Режим редактирования существующего цвета
                _currentColor = selectedColor;
                DataContext = _currentColor;
                Title = "Редактирование цвета";
            }

            private void Button_Save_Click(object sender, RoutedEventArgs e)
            {
                // Валидация данных
                if (string.IsNullOrWhiteSpace(_currentColor.cvet_name))
                {
                    MessageBox.Show("Название цвета не может быть пустым!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    // Сохранение данных
                    if (_currentColor.id == 0) // Новый цвет
                    {
                        AppConnect.model2.cveta.Add(_currentColor);
                    }

                    AppConnect.model2.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены!", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // Возврат на предыдущую страницу
                    AppFrame.frmMane2.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void Button_Cancel_Click(object sender, RoutedEventArgs e)
            {
                // Подтверждение отмены при наличии изменений
                if (_currentColor.id == 0 ||
                    AppConnect.model2.Entry(_currentColor).State == System.Data.Entity.EntityState.Modified)
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

