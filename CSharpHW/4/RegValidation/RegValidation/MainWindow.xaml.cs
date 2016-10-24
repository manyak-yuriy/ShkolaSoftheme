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

namespace RegValidation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void validate_Click(object sender, RoutedEventArgs e)
        {
            if (firstNameValid() && lastNameValid() && birthDateValid()
                && genderValid() && emailValid() && phoneValid() && infoValid())
                MessageBox.Show("Form data is correct!");
        }


        const int maxLen = 255;
        const int maxLenExtended = 2000;
        const int phoneLen = 10;
        private bool firstNameValid()
        {
            if (firstName.Text.Length >= maxLen)
            {
                MessageBox.Show("First name is too long");
                return false;
            }
                

            foreach (char symbol in firstName.Text)
                if (!Char.IsLetter(symbol))
                {
                    MessageBox.Show("First name must contain only letters");
                    return false;
                }

            return true;
        }

        private bool lastNameValid()
        {
            if (lastName.Text.Length >= maxLen)
            {
                MessageBox.Show("Last name is too long");
                return false;
            }
                

            foreach (char symbol in lastName.Text)
                if (!Char.IsLetter(symbol))
                {
                    MessageBox.Show("Last name must contain only letters");
                    return false;
                }

            return true;
        }

        private bool birthDateValid()
        {
            int day, month, year;

            try
            {
                day = int.Parse(dayOfBirth.Text);
                month = int.Parse(monthOfBirth.Text);
                year = int.Parse(yearOfBirth.Text);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
                return false;
            }

            if (day < 1 || day > 31)
            {
                MessageBox.Show("Invalid day!");
                return false;
            }

            if (month < 1 || month > 12)
            {
                MessageBox.Show("Invalid month!");
                return false;
            }
                
            if (year <= 1900 || year >= DateTime.Now.Year)
            {
                MessageBox.Show("The year of birth should be between 1900 and the current year!");
                return false;
            }

            return true;
        }

        private bool genderValid()
        {
            string genderStr = gender.Text;

            if (genderStr != "male" && genderStr != "female")
            {
                MessageBox.Show("No such gender is defined!");
                return false;
            }
            return true;
        }

        private bool emailValid()
        {
            string email = eMail.Text;
            if (!email.Contains("@"))
            {
                MessageBox.Show("email must contain @ sign!");
                return false;
            }

            if (email.Length >= maxLen)
            {
                MessageBox.Show("email is too long");
                return false;
            }

            return true;
        }

        private bool phoneValid()
        {
            foreach (char symbol in phoneNum.Text)
                if (!Char.IsDigit(symbol))
                {
                    MessageBox.Show("Phone number must contain only digits!");
                    return false;
                }

            if (phoneNum.Text.Length != phoneLen)
            {
                MessageBox.Show("Phone number must be 10 digits long!");
                return false;
            }

            return true;
        }

        private bool infoValid()
        {
            if (info.Text.Length >= maxLenExtended)
            {
                MessageBox.Show("Additional info is too long!");
                return false;
            }

            return true;
        }

    }
}
