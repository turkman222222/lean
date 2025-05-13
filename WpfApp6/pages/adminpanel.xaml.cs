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
    /// Логика взаимодействия для adminpanel.xaml
    /// </summary>
    public partial class adminpanel : Page
    {
        public adminpanel()
        {
            InitializeComponent();
            InitializeComponent();
            InitializeComponent();
            Prod.ItemsSource = AppDate.AppConnect.model2.Carss.ToList();
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

        

       
        private void filtr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Prod.ItemsSource = Findmarka();
        }

        private void vidRecept_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Prod.ItemsSource = Findmarka();
        }

        private void txttxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Prod.ItemsSource = Findmarka();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Prod.SelectedItem is Carss selectedRecipe)
            {
                AddToFavorites(selectedRecipe.id);
                MessageBox.Show("Рецепт добавлен в избранное!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Addpage addpage = new Addpage();
            AppFrame.frmMane2.Navigate(addpage);
        }
        public void AddToFavorites(int id_user)
        {
            var newLikeRecipe = new izbr
            {
                user_id = AppConnect.id_userr,
                id = id_user,
            };

            AppConnect.model2.izbr.Add(newLikeRecipe);
            AppConnect.model2.SaveChanges();
        }
    }
}

