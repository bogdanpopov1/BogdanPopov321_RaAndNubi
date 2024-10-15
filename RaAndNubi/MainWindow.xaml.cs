using System;
using System.Linq;
using System.Windows;
using RaAndNubi.Data.DBO;
using RaAndNubi.Data;
using RaAndNubi.Pages;

namespace RaAndNubi
{
    public partial class MainWindow : Window
    {
        private Person _person;
        private Pet _pet;

        public MainWindow(Person person)
        {
            InitializeComponent();
            _person = person;

            if (person.Id == 1)
                _pet = DBManager.GetPets().FirstOrDefault(x => x.Id == 1);
            else
                _pet = DBManager.GetPets().FirstOrDefault(x => x.Id == 2);

            MainFrame.Navigate(new PetInfoPage(_pet));
        }
    }
}
