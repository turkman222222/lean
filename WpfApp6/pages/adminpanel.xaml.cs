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
    public partial class adminpanel : Page
    {
        public adminpanel()
        {
            InitializeComponent();
            Prods.ItemsSource = AppConnect.model2.Carss.ToList();
            filtr.Items.Add("цена");
            filtr.Items.Add("по возрастанию");
            filtr.Items.Add("по убыванию");
            filtr.SelectedIndex = 0;

            vidRecept.SelectedIndex = 0;
            var marki = AppConnect.model2.Marks;
            vidRecept.Items.Add("Категория");
            foreach (var item in marki)
            {
                vidRecept.Items.Add(item.name_marka);
            }
        }

        private void AddToFavoritesItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Carss selectedCar)
            {
                AddToFavorites(selectedCar.id);
                MessageBox.Show("Машина добавлена в избранное!");
            }
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Carss selectedCar)
            {
                carsadd editPage = new carsadd(selectedCar);
                AppFrame.frmMane2.Navigate(editPage);
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Carss selectedCar)
            {
                if (MessageBox.Show($"Вы точно хотите удалить автомобиль {selectedCar.model}?",
                    "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        AppConnect.model2.Carss.Remove(selectedCar);
                        AppConnect.model2.SaveChanges();
                        MessageBox.Show("Автомобиль удален!");
                        Prods.ItemsSource = AppConnect.model2.Carss.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        // Остальной код остается без изменений
        private void filtr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Prods.ItemsSource = Findmarka();
        }

        private void vidRecept_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Prods.ItemsSource = Findmarka();
        }

        private void txttxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Prods.ItemsSource = Findmarka();
        }

        Carss[] Findmarka()
        {
            var model = AppConnect.model2.Carss.ToList();
            var suchmodel = model;
            if (txttxt.Text != "" && model.Count > 0)
            {
                model = model.Where(x => x.model.ToLower().Contains(txttxt.Text.ToLower())).ToList();
            }
            if (vidRecept.SelectedIndex > 0)
            {
                model = model.Where(x => x.id_marki == vidRecept.SelectedIndex).ToList();
            }
            if (filtr.SelectedIndex > 0)
            {
                switch (filtr.SelectedIndex)
                {
                    case 1:
                        model = model.OrderBy(x => x.price).ToList(); break;
                    case 2:
                        model = model.OrderByDescending(x => x.price).ToList();
                        break;
                }
            }
            return model.ToArray();
        }

        public void AddToFavorites(int id)
        {
            var newLikeRecipe = new izbr
            {
                user_id = AppConnect.id_userr,
                car_id = id,
            };

            AppConnect.model2.izbr.Add(newLikeRecipe);
            AppConnect.model2.SaveChanges();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (Prods.SelectedItem is Carss selectedRecipe)
            {
                AddToFavorites(selectedRecipe.id);
                MessageBox.Show("машина добавлена в избранное!");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            carsadd addpagedd = new carsadd();
            AppFrame.frmMane2.Navigate(addpagedd);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedItems = Prods.SelectedItems.Cast<Carss>().ToList();

            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите элементы для удаления.");
                return;
            }

            if (MessageBox.Show($"Вы точно хотите удалить следующие {selectedItems.Count} элементов?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AppConnect.model2.Carss.RemoveRange(selectedItems);
                    AppConnect.model2.SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    Prods.ItemsSource = AppConnect.model2.Carss.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Prods.SelectedItem is Carss selectedRecipe)
            {
                carsadd editPage = new carsadd(selectedRecipe);
                if (Prods.SelectedItem != null)
                {
                    AppFrame.frmMane2.Navigate(editPage);
                }
                else
                {
                    MessageBox.Show("Выберите автомобиль для редактирования!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            putadmin addpagedddd = new putadmin();
            AppFrame.frmMane2.Navigate(addpagedddd);
        }

        private void Prods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ваш код обработки изменения выбора
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bronadmin addpageddddff = new bronadmin();
            AppFrame.frmMane2.Navigate(addpageddddff);
        }
    }
}