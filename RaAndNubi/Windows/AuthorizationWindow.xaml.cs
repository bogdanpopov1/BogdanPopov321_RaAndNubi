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
        public AuthorizationWindow()
        {
            InitializeComponent();
            _people = DBManager.GetPeople();
            LoginCB.ItemsSource = _people;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
