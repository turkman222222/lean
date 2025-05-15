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
            //InitializeComponent();
            //if (_currentCar != null)
            //    this._currentCar = currentCar;
            DataContext = _currentCar;

            Loadmark();
            Loadcveta();
            Loadsalona();
            Loadkompl();
            Loadatrana();

           

        }
        private void Loadmark()
        {
            var authors = AppConnect.model2.Marks;
            txtmark.Items.Add("марки");
            foreach (var auth in authors)
            {
                txtmark.Items.Add(auth.name_marka);
            }

            //txtmark.ItemsSource = authors;
            //txtmark.DisplayMemberPath = "name_marka";
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
                Filter = "Image files (*.png;*.jpg)|*.png;*.jpg",
                Title = "Выберите изображение"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    byte[] imageData;
                    using (FileStream fileStream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            fileStream.CopyTo(memoryStream);
                            imageData = memoryStream.ToArray(); // Преобразуем в массив байтов
                        }
                    }

                    // Сохраняем массив байтов в объект Carss
                    _currentCar.image = imageData; // Используем новое имя свойства

                    MessageBox.Show("Изображение успешно добавлено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtCategoriesName_Копировать4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{

            //    if (_currentCar.id == 0)
            //    {
            //        _currentCar.id_marki = AppConnect.model2.Marks.FirstOrDefault(x => x.name_marka  == txtmark.Text).id;
                   
            //        _currentCar.id_str = AppConnect.model2.strana.FirstOrDefault(x => x.strana_name == txtstrana.Text).id;
            //        _currentCar.id_cvet = AppConnect.model2.cveta.FirstOrDefault(x => x.cvet_name == txtcvet.Text).id;
            //        _currentCar.id_kompl = AppConnect.model2.compl.FirstOrDefault(x => x.kompl_name  == txtkompl.Text).id;
            //        _currentCar.id_salona = AppConnect.model2.salonch.FirstOrDefault(x => x.salon == txtsalon.Text).id;


            //        AppConnect.model2.Carss.Add(_currentCar);
            //    }

            //    AppConnect.model2.SaveChanges();
            //    //this.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void txtmark_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
