using RaAndNubi.Data;
using RaAndNubi.Data.DBO;
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

namespace RaAndNubi.Pages
{
    public partial class AuthorizationPage : Page
    {
        private List<Person> _people;
        private Person _selectedPerson;
        private Pet _pet;
        public AuthorizationPage()
        {
            InitializeComponent();
            InitializeComponent();
            _people = DBManager.GetPeople();
            LoginCB.ItemsSource = _people;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LoginCB.SelectedItem != null && _selectedPerson.Password.ToString() == PasswordPB.Password.Trim())
            {
                if (_selectedPerson.Id == 1)
                    _pet = DBManager.GetPets().FirstOrDefault(x => x.Id == 1);
                else
                    _pet = DBManager.GetPets().FirstOrDefault(x => x.Id == 2);

                NavigationService.Navigate(new PetInfoPage(_pet, _selectedPerson));
            }
            else
                MessageBox.Show("Выберите пользователя или проверьте пароль!", "Ошибка заполнения", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void LoginCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPerson = (Person)LoginCB.SelectedItem;
        }
    }
}
