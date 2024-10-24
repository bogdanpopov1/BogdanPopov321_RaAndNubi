using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using RaAndNubi.Data.DBO;
using RaAndNubi.Data;
using System.Linq;
using System;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace RaAndNubi.Pages
{
    public partial class PetInfoPage : Page
    {
        private Pet _pet;
        private Person _currentPerson;
        private List<Content> _content = new List<Content>();
        private byte[] _img = new byte[0];
        private List<Content> _listView;

        private List<string> _sortingItems = new List<string>() { "По умолчанию", "Сначала новые", "Сначала старые" };
        public PetInfoPage(Pet pet, Person person)
        {
            InitializeComponent();
            _pet = pet;
            _currentPerson = person;
            LoadData();
            _listView = _content;
            Title.Content = _pet.Id == 1 ? "КОТИК РА" : "ПЕСИК НУБИ";

            SortCB.ItemsSource = _sortingItems;
            SortCB.SelectedItem = "По умолчанию";

            SelectPetCB.ItemsSource = DBManager.GetPets();
        }

        private void LoadData()
        {
            _content = DBManager.GetContent().Where(x => x.Id_pet == _pet.Id).ToList();
            ListView.ItemsSource = _content;
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(SearchTB.Text))
                _listView = _content.FindAll(Content => Content.Discription.ToLower().Contains(SearchTB.Text));
            else
                _listView = _content;

            Sort();
            ListView.Items.Refresh();
        }

        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }

        private void Sort()
        {
            switch (SortCB.SelectedItem.ToString())
            {
                case "По умолчанию":
                    ListView.ItemsSource = _listView;
                    break;
                case "Сначала новые":
                    ListView.ItemsSource = _listView.Where(x => x.Id_pet == _pet.Id).ToList().OrderByDescending(x => x.Date).ToList(); ;
                    break;
                case "Сначала старые":
                    ListView.ItemsSource = _listView.Where(x => x.Id_pet == _pet.Id).ToList().OrderBy(x => x.Date).ToList();
                    break;
            }
        }

        private void LoadImgBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a photo";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                var img = new BitmapImage(new Uri(op.FileName));
                _img = File2Byte(op.FileName);
                PetImg.Source = img;
                LoadImgBtn.Background = Brushes.Transparent;
                LoadImgBtn.BorderBrush = Brushes.Transparent;
            }
        }

        public Byte[] File2Byte(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                return File.ReadAllBytes(filePath);

            return null;
        }

        private void DeletePhoto_Click(object sender, RoutedEventArgs e)
        {
            DeleteImg();
        }

        private void DeleteImg()
        {
            _img = null;
            PetImg.Source = null;
            LoadImgBtn.Background = Brushes.LightGray;
            LoadImgBtn.BorderBrush = Brushes.Gray;
        }

        private string _initialText = "Описание к фотографии";

        private void NewPostDiscrTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NewPostDiscrTB.Text == _initialText)
            {
                NewPostDiscrTB.Text = string.Empty;
                NewPostDiscrTB.Foreground = Brushes.Black;
            }
        }

        private void NewPostDiscrTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewPostDiscrTB.Text))
            {
                NewPostDiscrTB.Text = _initialText;
                NewPostDiscrTB.Foreground = Brushes.LightGray;
            }
        }

        private void NewPostDiscrTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NewPostDiscrTB.Text.Length > 500)
            {
                NewPostDiscrTB.Text = NewPostDiscrTB.Text.Substring(0, 500);
                NewPostDiscrTB.CaretIndex = NewPostDiscrTB.Text.Length;
                MessageBox.Show("Достигнуто максимальное количество символов!", "Ошибка добавления описания", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (NewPostDiscrTB.Text == _initialText)
                NumberOfCharactersTB.Text = "0/500";
            else
                NumberOfCharactersTB.Text = NewPostDiscrTB.Text.Length + "/500";

            if (!string.IsNullOrWhiteSpace(NewPostDiscrTB.Text) && NewPostDiscrTB.Text != _initialText)
            {
                NewPostDiscrTB.Foreground = Brushes.Black;
            }
        }

        private void AddPostBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_img == null || SelectPetCB.SelectedItem == null)
            {
                MessageBox.Show("Добавьте фото и/или выберите животного!", "Ошибка публикации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Pet _selectedPet = SelectPetCB.SelectedItem as Pet;
            string discr = NewPostDiscrTB.Text == _initialText ? "" : NewPostDiscrTB.Text;
            Content newPost = new Content();
            newPost.Id_pet = _selectedPet.Id;
            newPost.Img = _img;
            newPost.Discription = discr;
            newPost.Id_person = _currentPerson.Id;
            newPost.Date = DateTime.Now;
            DBManager.AddContent(newPost);
            LoadData();
            _listView = _content;
            SearchTB.Text = "";
            SortCB.SelectedItem = "По умолчанию";
            ListView.Items.Refresh();
            ClearFields();
            MessageBox.Show("Опубликовано!", "Новая публикаия", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ClearFields()
        {
            _img = null;
            DeleteImg();
            LoadImgBtn.Background = Brushes.LightGray;
            LoadImgBtn.BorderBrush = Brushes.Gray;
            SelectPetCB.SelectedItem = null;
            NewPostDiscrTB.Text = _initialText;
            NewPostDiscrTB.Foreground = Brushes.LightGray;
        }
    }
}