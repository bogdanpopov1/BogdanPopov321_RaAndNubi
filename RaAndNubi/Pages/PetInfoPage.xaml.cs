using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using RaAndNubi.Data.DBO;
using RaAndNubi.Data;
using System.Linq;
using System;

namespace RaAndNubi.Pages
{
    public partial class PetInfoPage : Page
    {
        private Pet _pet;
        private List<Content> _content = new List<Content>();
        private byte[] _img = new byte[0];
        private List<Content> _listView;

        private List<string> _sortingItems = new List<string>() { "По умолчанию", "Сначала новые", "Сначала старые" };

        public PetInfoPage(Pet pet)
        {
            InitializeComponent();
            _pet = pet;
            LoadData();
            _listView = _content;
            Title.Content = _pet.Id == 1 ? "КОТИК РА" : "ПЕСИК НУБИ";

            SortCB.ItemsSource = _sortingItems;
            SortCB.SelectedItem = "По умолчанию";
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
    }
}