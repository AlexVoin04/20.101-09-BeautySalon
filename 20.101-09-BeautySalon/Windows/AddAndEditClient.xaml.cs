using _20._101_09_BeautySalon.Models;
using _20._101_09_BeautySalon.Pages;
using Microsoft.Win32;
using Mono.Cecil;
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
using System.Windows.Shapes;

namespace _20._101_09_BeautySalon.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddAndEditClient.xaml
    /// </summary>
    public partial class AddAndEditClient : Window
    {
        Client client = new Client();
        private Entities db;
        Client currentClient = new Client();
        private ClientPage clientPage;

        public AddAndEditClient(Client currentClient, Entities db, ClientPage clientPage)
        {
            InitializeComponent();
            this.clientPage = clientPage;
            this.db = db;
            if (currentClient != null)
            {
                this.client = currentClient;
                LViewTags.ItemsSource = client.TagList;
                tbID.Text = client.ID.ToString();
                this.Title = "Редактирование инфомрмации о клиенте";
            }
            else if (currentClient == null)
            {
                this.currentClient = currentClient;
                btnDeleteClient.IsEnabled = false;
                TagGrid.Visibility = Visibility.Collapsed;
                spID.Visibility = Visibility.Collapsed;
                this.Title = "Добавление инфомрмации о клиенте";
            }
            DataContext = client;
            cmbGender.ItemsSource = db.Gender.ToList();
        }

        private void AddAndEditClientWindow_Closed(object sender, EventArgs e)
        {

        }

        private void btnEnterImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var asmName = new AssemblyNameDefinition("DynamicAssembly", new Version(1, 0, 0, 0));
                var assembly = AssemblyDefinition.CreateAssembly(asmName, "<Module>", ModuleKind.Dll);
                OpenFileDialog GetImageDialog = new OpenFileDialog(); // Открытие диалогового окна
                string folderpath = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\ImageAndIcon\\agents\\";
                GetImageDialog.Title = "Выберите изображение";
                GetImageDialog.Filter = "Файлы изображений: (*.png,*.jpg,*.jpeg)| *.png;*.jpg;*.jpeg"; // Фильтр типов файлов
                GetImageDialog.InitialDirectory = folderpath;
                if (GetImageDialog.ShowDialog() == true)
                    client.PhotoPath = GetImageDialog.SafeFileName;//Добавление наименования файла в БД
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

        }

        private void tbFirstName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void tbLastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void tbPatronymic_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void tbEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbColorTag_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelTag_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
