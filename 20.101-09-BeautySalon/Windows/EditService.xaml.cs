using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;
using _20._101_09_BeautySalon.Classes;
using _20._101_09_BeautySalon.Models;
using _20._101_09_BeautySalon.Pages;
using Microsoft.Win32;
using Mono.Cecil;

namespace _20._101_09_BeautySalon.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditService.xaml
    /// </summary>
    
    public partial class EditService : Window
    {
        Service service = new Service();
        Entities db;
        ClientServiceWindow clientServiceWindow;
        public EditService(Service service, Entities db, ClientServiceWindow clientServiceWindow)
        {
            InitializeComponent();
            this.service = service;
            this.db = db;
            this.clientServiceWindow = clientServiceWindow;
            DataContext = service;
        }

        private void btnEnterImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var asmName = new AssemblyNameDefinition("DynamicAssembly", new Version(1, 0, 0, 0));
                var assembly = AssemblyDefinition.CreateAssembly(asmName, "<Module>", ModuleKind.Dll);
                OpenFileDialog GetImageDialog = new OpenFileDialog(); // Открытие диалогового окна
                string folderpath = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\ImagesAndIcons\\Услуги салона красоты\\";
                GetImageDialog.Title = "Выберите изображение";
                GetImageDialog.Filter = "Файлы изображений: (*.png,*.jpg,*.jpeg)| *.png;*.jpg;*.jpeg"; // Фильтр типов файлов
                GetImageDialog.InitialDirectory = folderpath;
                if (GetImageDialog.ShowDialog() == true)
                    service.MainImagePath = GetImageDialog.SafeFileName;//Добавление наименования файла в БД
                var sourse = new BitmapImage(new Uri(GetImageDialog.FileName));
                imgPhoto.Source = sourse;
                /*
                string filePath = folderpath + System.IO.Path.GetFileName(GetImageDialog.FileName);
                File.Copy(GetImageDialog.FileName, filePath, true);
                byte[] imageData = File.ReadAllBytes(filePath);
                var imageResource = new EmbeddedResource(GetImageDialog.FileName, ManifestResourceAttributes.Private, imageData);
                assembly.MainModule.Resources.Add(imageResource);*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string defaultImagePath = (string)FindResource("defaultImage"); // Получаем путь к изображению из ресурсов
                if (!string.IsNullOrEmpty(defaultImagePath))
                {
                    BitmapImage defaultImage = new BitmapImage(new Uri(defaultImagePath)); // Создаем BitmapImage из пути
                    imgPhoto.Source = defaultImage; // Устанавливаем изображение по умолчанию
                }
                service.MainImagePath = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Errors(bool expression, StringBuilder errors, string message)
        {
            if (expression)
            {
                errors.AppendLine(message);
            }
        }

        private void btnRefrSer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                if (int.TryParse(TbDiscountServ.Text, out int discount))
                {
                    Errors(discount > 100, errors, "Скидка не может быть больше 100%!");
                }
                Errors(TbTitleServ.Text == ""
                    || TbDurationInSecondsServ.Text == ""
                    || TbCostServ.Text == "", errors, "Не заполнена важная ифнормация!");
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
                if (TbDiscountServ.Text == "") service.Discount = null;
                SaveInDB("Обновление информации о сервисе завершено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void SaveInDB(string text)
        {
            try
            {
                db.SaveChanges();
                this.Close();
                MessageBox.Show(text, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelSer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.Service.Remove(service);
                SaveInDB("Удаление информации о сервесе завешено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void TbTitleServ_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (TbTitleServ.Text.Length >= 50 && !e.Text.Equals("\b"))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = !ValidatorExtensions.IsValidTitle(e.Text);
            }
            
        }

        private void TbDurationInSecondsServ_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (TbDurationInSecondsServ.Text.Length >= 9 && !e.Text.Equals("\b"))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = !ValidatorExtensions.IsValidCost(e.Text);
            }
        }

        private void TbCostServ_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //if (TbCostServ.Text.Length >= 11 && !e.Text.Equals("\b"))
            //{
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Handled = !ValidatorExtensions.IsValidCost(e.Text);
            //}
            if (TbCostServ.Text.Contains(".") && e.Text == ".")
            {
                // Запретить ввод более одной точки.
                e.Handled = true;
            }
            else if (TbCostServ.Text.Length >= 11 && !e.Text.Equals("\b"))
            {
                // Запретить ввод более 11 символов (если нужно).
                e.Handled = true;
            }
            else
            {
                // Проверить, что новый текст валиден.
                string newText = TbCostServ.Text.Insert(TbCostServ.CaretIndex, e.Text);
                e.Handled = !ValidatorExtensions.IsValidCost(newText);
            }
        }

        private void TbDiscountServ_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //if (TbDiscountServ.Text.Length >= 3 && !e.Text.Equals("\b"))
            //{
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Handled = !ValidatorExtensions.IsValidDiscount(e.Text);
            //}
            if (TbDiscountServ.Text.Length >= 3 && !e.Text.Equals("\b"))
            {
                e.Handled = true;
            }
            else
            {
                string newText = TbDiscountServ.Text.Insert(TbDiscountServ.CaretIndex, e.Text);

                // Проверьте, что новый текст является допустимым числом и меньше или равен 100.
                if (!ValidatorExtensions.IsValidDiscount(newText) || (int.TryParse(newText, out int discount) && discount > 100))
                {
                    e.Handled = true;
                }
            }
        }

        private void TbDescriptionServ_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (TbDescriptionServ.Text.Length >= 200 && !e.Text.Equals("\b"))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = !ValidatorExtensions.IsValidTitle(e.Text);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                if (clientServiceWindow != null)
                {
                    clientServiceWindow.Load(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
