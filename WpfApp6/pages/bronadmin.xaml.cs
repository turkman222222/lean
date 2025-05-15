using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp6.AppDate;

namespace WpfApp6.pages
{
    public partial class bronadmin : Page
    {
        public bronadmin()
        {
            InitializeComponent();
            LoadUserFavorites();
            InitializeFilters();
        }

        private void LoadUserFavorites()
        {
            int currentUserId = AppConnect.id_userr;

            var userFavorites = AppConnect.model2.izbr
                .Where(f => f.user_id == currentUserId)
                .Select(f => f.Carss)
                .ToList();

            Prod.ItemsSource = userFavorites;
        }

        private void InitializeFilters()
        {
            filtr.Items.Add("цена");
            filtr.Items.Add("по возрастанию");
            filtr.Items.Add("по убыванию");
            filtr.SelectedIndex = 0;

            vidRecept.SelectedIndex = 0;
            vidRecept.Items.Add("Категория");
            foreach (var mark in AppConnect.model2.Marks)
            {
                vidRecept.Items.Add(mark.name_marka);
            }
        }

        private void DeleteFavorite_Click(object sender, RoutedEventArgs e)
        {
            if (Prod.SelectedItem is Carss selectedCar)
            {
                int currentUserId = AppConnect.id_userr;
                var favorite = AppConnect.model2.izbr
                    .FirstOrDefault(f => f.user_id == currentUserId && f.car_id == selectedCar.id);

                if (favorite != null)
                {
                    AppConnect.model2.izbr.Remove(favorite);
                    AppConnect.model2.SaveChanges();
                    LoadUserFavorites();
                    MessageBox.Show("Автомобиль удален из избранного");
                }
            }
           
        }

        private void ApplyFilters()
        {
            int currentUserId = AppConnect.id_userr;
            var query = AppConnect.model2.izbr
                .Where(f => f.user_id == currentUserId)
                .Select(f => f.Carss);

            if (vidRecept.SelectedIndex > 0)
            {
                query = query.Where(c => c.id_marki == vidRecept.SelectedIndex);
            }

            if (filtr.SelectedIndex > 0)
            {
                query = filtr.SelectedIndex == 1
                    ? query.OrderBy(c => c.price)
                    : query.OrderByDescending(c => c.price);
            }

            Prod.ItemsSource = query.ToList();
        }

        private void vidRecept_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void filtr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void txttxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txttxt.Text))
            {
                int currentUserId = AppConnect.id_userr;
                var filtered = AppConnect.model2.izbr
                    .Where(f => f.user_id == currentUserId)
                    .Select(f => f.Carss)
                    .Where(c => c.model.ToLower().Contains(txttxt.Text.ToLower()))
                    .ToList();

                Prod.ItemsSource = filtered;
            }
            else
            {
                ApplyFilters();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Prod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Обработка выбора элемента
        }
    }
}