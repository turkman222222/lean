using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class userpanel : Page
    {
        public userpanel()
        {
            InitializeComponent();
            LoadCars();
            InitializeFilters();
        }

        private void LoadCars(bool showFavoritesOnly = false)
        {
            var cars = AppConnect.model2.Carss.AsQueryable();

            if (showFavoritesOnly)
            {
                // Получаем только избранные автомобили текущего пользователя
                var favoriteCarIds = AppConnect.model2.izbr
                    .Where(f => f.user_id == AppConnect.id_userr)
                    .Select(f => f.car_id)
                    .ToList();

                cars = cars.Where(c => favoriteCarIds.Contains(c.id));
            }

            Prods.ItemsSource = cars.ToList();
        }

        private void InitializeFilters()
        {
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

        private void ShowFavorites_Click(object sender, RoutedEventArgs e)
        {
            LoadCars(true); // Загружаем только избранное
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            LoadCars(); // Загружаем все автомобили
        }

        // Остальные методы остаются без изменений
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

        private Carss[] Findmarka()
        {
            var query = AppConnect.model2.Carss.AsQueryable();

            if (txttxt.Text != "")
            {
                query = query.Where(x => x.model.ToLower().Contains(txttxt.Text.ToLower()));
            }

            if (vidRecept.SelectedIndex > 0)
            {
                query = query.Where(x => x.id_marki == vidRecept.SelectedIndex);
            }

            if (filtr.SelectedIndex > 0)
            {
                query = filtr.SelectedIndex == 1
                    ? query.OrderBy(x => x.price)
                    : query.OrderByDescending(x => x.price);
            }

            return query.ToArray();
        }

        public void AddToFavorites(int id)
        {
            var existingFavorite = AppConnect.model2.izbr
                .FirstOrDefault(f => f.user_id == AppConnect.id_userr && f.car_id == id);

            if (existingFavorite == null)
            {
                var newFavorite = new izbr
                {
                    user_id = AppConnect.id_userr,
                    car_id = id,
                };
                AppConnect.model2.izbr.Add(newFavorite);
                AppConnect.model2.SaveChanges();
                MessageBox.Show("Машина добавлена в избранное!");
            }
            else
            {
                AppConnect.model2.izbr.Remove(existingFavorite);
                AppConnect.model2.SaveChanges();
                MessageBox.Show("Машина удалена из избранного!");
            }
        }

        private void AddToFavoritesItem_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Carss selectedCar)
            {
                AddToFavorites(selectedCar.id);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AppFrame.frmMane2.Navigate(new bronadmin());
        }
    }
}