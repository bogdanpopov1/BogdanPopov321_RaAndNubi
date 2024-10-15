using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using RaAndNubi.Data.DBO;
using RaAndNubi.Data;
using System.Linq;

namespace RaAndNubi.Pages
{
    public partial class PetInfoPage : Page
    {
        private Pet _pet;
        private List<Content> _content;
        public PetInfoPage(Pet pet)
        {
            InitializeComponent();
            _pet = pet;
            LoadData();
            Title.Content = _pet.Id == 1 ? "КОТИК РА" : "ПЕСИК НУБИ";
        }

        private void LoadData()
        {
            _content = DBManager.GetContent().Where(x => x.Id_pet == _pet.Id).ToList();
            RaListView.ItemsSource = _content;
        }
    }

    public class RaItem
    {
        public string Img { get; set; }
        public string Discription { get; set; }
    }
}