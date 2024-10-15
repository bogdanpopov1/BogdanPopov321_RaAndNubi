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
using RaAndNubi.Data.DBO;
using RaAndNubi.Data;

namespace RaAndNubi.Windows
{
    public partial class AuthorizationWindow : Window
    {
        private List<Person> _people;
        private Person _selectedPerson;
        public AuthorizationWindow()
        {
            InitializeComponent();
            _people = DBManager.GetPeople();
            LoginCB.ItemsSource = _people;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LoginCB.SelectedItem != null && _selectedPerson.Password.ToString() == PasswordPB.Password.Trim())
            {
                new MainWindow(_selectedPerson).Show();
                this.Close();
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
