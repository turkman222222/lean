using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using Path = System.IO.Path;

namespace WpfApp6.pages
{
    /// <summary>
    /// Логика взаимодействия для carsadd.xaml
    /// </summary>
    public partial class carsadd : Page
    {
        private Carss _currentCar = new Carss();
        public carsadd()
        {
            InitializeComponent();
            Loadmark();
            Loadcveta();
            Loadsalona();
            Loadkompl();
            Loadatrana();
            DataContext = _currentCar;


        }
        public carsadd(Carss selectedRecipe)
        {
            InitializeComponent(); // THIS IS CRUCIAL - UNCOMMENT THIS LINE
            if (_currentCar != null)
                this._currentCar = selectedRecipe;
            DataContext = _currentCar;

            Loadmark();
            Loadcveta();
            Loadsalona();
            Loadkompl();
            Loadatrana();



        }
        private void Loadmark()
        {
            if (txtmark == null) return;

            txtmark.Items.Clear();
            var authors = AppConnect.model2.Marks;
            txtmark.Items.Add("марки");
            foreach (var auth in authors)
            {
                txtmark.Items.Add(auth.name_marka);
            }
        }
        private void Loadatrana()
        {
            var stranaf = AppConnect.model2.strana;
            txtstrana.Items.Add("страны");
            foreach (var authggg in stranaf)
            {
                txtstrana.Items.Add(authggg.strana_name);
            }
            //txtAuthorName.ItemsSource = authors;
            //txtAuthorName.DisplayMemberPath = "Authorname";
        }
        private void Loadkompl()
        {
            var kompl = AppConnect.model2.compl;
            txtkompl.Items.Add("комплектации");
            foreach (var authff in kompl )
            {
                txtkompl.Items.Add(authff.kompl_name);
            }
            //txtAuthorName.ItemsSource = authors;
            //txtAuthorName.DisplayMemberPath = "Authorname";
        }
        private void Loadcveta()
        {
            var cveta = AppConnect.model2.cveta;
            txtcvet.Items.Add("цвета");
            foreach (var auths in cveta)
            {
                txtcvet.Items.Add(auths.cvet_name);
            }
            //txtAuthorName.ItemsSource = authors;
            //txtAuthorName.DisplayMemberPath = "Authorname";
        }
        private void Loadsalona()
        {
            var authorss = AppConnect.model2.salonch;
            txtsalon.Items.Add("салон");
            foreach (var authss in authorss)
            {
                txtsalon.Items.Add(authss.salon);
            }
            //txtAuthorName.ItemsSource = authors;
            //txtAuthorName.DisplayMemberPath = "Authorname";
        }
        //private void LoadCategories()
        //{
        //    var categories = AppConnect.model1.Categories;
        //    txtCategoriesName.Items.Add("Категория");
        //    foreach (var auth in categories)
        //    {
        //        txtCategoriesName.Items.Add(auth.CategoryName);
        //    }
        //    //txtCategoriesName.ItemsSource = categories;
        //    //txtCategoriesName.DisplayMemberPath = "CategoryName";
        //}
        private void text03_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public string SaveImg(FileStream img)
        {
            // Папка img в корне проекта (если её нет — создаём)
            string imgFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img");

            if (!Directory.Exists(imgFolderPath))
            {
                Directory.CreateDirectory(imgFolderPath);
            }

            // Формируем путь для сохранения (только имя файла без полного пути)
            string fileName = Path.GetFileName(img.Name);
            string destinationPath = Path.Combine(imgFolderPath, fileName);

            try
            {
                // Копируем файл
                using (var outputStream = new FileStream(destinationPath, FileMode.Create))
                {
                    img.Seek(0, SeekOrigin.Begin);  // Перемещаемся в начало потока
                    img.CopyTo(outputStream);
                }

                // Возвращаем относительный путь (например, "img/photo.jpg")
                return Path.Combine("img", fileName);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
{
    var dialog = new OpenFileDialog
    {
        Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg",
        Title = "Выберите изображение",
        Multiselect = false
    };

    if (dialog.ShowDialog() == true)
    {
        try
        {
            // Ограничение размера файла (например, 5MB)
            FileInfo fileInfo = new FileInfo(dialog.FileName);
            if (fileInfo.Length > 5 * 1024 * 1024)
            {
                MessageBox.Show("Размер файла не должен превышать 5MB", "Ошибка", 
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Чтение файла
            byte[] imageData;
            using (FileStream fileStream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
            {
                imageData = new byte[fileStream.Length];
                fileStream.Read(imageData, 0, (int)fileStream.Length);
            }

            // Сохранение в объект
            _currentCar.image = imageData;

            // Предпросмотр изображения (опционально)
            if (imageData != null && imageData.Length > 0)
            {
                var bitmapImage = new BitmapImage();
                using (var memStream = new MemoryStream(imageData))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = memStream;
                    bitmapImage.EndInit();
                }
                // imgPreview - это Image control в XAML
            }

            MessageBox.Show("Изображение успешно загружено!", "Успех", 
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при загрузке изображения:\n{ex.Message}", "Ошибка", 
                          MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

        private void txtCategoriesName_Копировать4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentCar == null)
                {
                    MessageBox.Show("Ошибка: текущий автомобиль не инициализирован");
                    return;
                }

                // Получаем ID связанных сущностей с проверкой на null
                var mark = AppConnect.model2.Marks.FirstOrDefault(x => x.name_marka == txtmark.Text);
                if (mark == null)
                {
                    MessageBox.Show("Марка не найдена!");
                    return;
                }
                _currentCar.id_marki = mark.id;

                var country = AppConnect.model2.strana.FirstOrDefault(x => x.strana_name == txtstrana.Text);
                if (country == null)
                {
                    MessageBox.Show("Страна не найдена!");
                    return;
                }
                _currentCar.id_str = country.id;

                var color = AppConnect.model2.cveta.FirstOrDefault(x => x.cvet_name == txtcvet.Text);
                if (color == null)
                {
                    MessageBox.Show("Цвет не найден!");
                    return;
                }
                _currentCar.id_cvet = color.id;

                var complectation = AppConnect.model2.compl.FirstOrDefault(x => x.kompl_name == txtkompl.Text);
                if (complectation == null)
                {
                    MessageBox.Show("Комплектация не найдена!");
                    return;
                }
                _currentCar.id_kompl = complectation.id;

                var salon = AppConnect.model2.salonch.FirstOrDefault(x => x.salon == txtsalon.Text);
                if (salon == null)
                {
                    MessageBox.Show("Салон не найден!");
                    return;
                }
                _currentCar.id_salona = salon.id;

                // Добавляем новую запись только если id == 0
                if (_currentCar.id == 0)
                {
                    AppConnect.model2.Carss.Add(_currentCar);
                    MessageBox.Show("Данные добавлены.");
                }
                else
                {
                    MessageBox.Show("Данные обновлены.");
                }

                // Сохраняем изменения
                AppConnect.model2.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}\n\nInner Exception: {ex.InnerException?.Message}");
            }
        }

        private void txtmark_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentCar == null)
                {
                    MessageBox.Show("Ошибка: текущий автомобиль не инициализирован");
                    return;
                }

                // Получаем ID связанных сущностей с проверкой на null
                var mark = AppConnect.model2.Marks.FirstOrDefault(x => x.name_marka == txtmark.Text);
                if (mark == null)
                {
                    MessageBox.Show("Марка не найдена!");
                    return;
                }
                _currentCar.id_marki = mark.id;

                var country = AppConnect.model2.strana.FirstOrDefault(x => x.strana_name == txtstrana.Text);
                if (country == null)
                {
                    MessageBox.Show("Страна не найдена!");
                    return;
                }
                _currentCar.id_str = country.id;

                var color = AppConnect.model2.cveta.FirstOrDefault(x => x.cvet_name == txtcvet.Text);
                if (color == null)
                {
                    MessageBox.Show("Цвет не найден!");
                    return;
                }
                _currentCar.id_cvet = color.id;

                var complectation = AppConnect.model2.compl.FirstOrDefault(x => x.kompl_name == txtkompl.Text);
                if (complectation == null)
                {
                    MessageBox.Show("Комплектация не найдена!");
                    return;
                }
                _currentCar.id_kompl = complectation.id;

                var salon = AppConnect.model2.salonch.FirstOrDefault(x => x.salon == txtsalon.Text);
                if (salon == null)
                {
                    MessageBox.Show("Салон не найден!");
                    return;
                }
                _currentCar.id_salona = salon.id;

                // Добавляем новую запись только если id == 0
                if (_currentCar.id == 0)
                {
                    AppConnect.model2.Carss.Add(_currentCar);
                    MessageBox.Show("Данные добавлены.");
                }
                else
                {
                    MessageBox.Show("Данные обновлены.");
                }

                // Сохраняем изменения
                AppConnect.model2.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}\n\nInner Exception: {ex.InnerException?.Message}");
            }
        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    adminpanel adm = new adminpanel();
        //    NavigationService.Navigate(adm);
        //}
    }
}
