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
using System.ComponentModel.DataAnnotations;

namespace RegValidation
{
    // attribute class for gender validation
    public class Gender : ValidationAttribute
    {
        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {

            if (value.ToString() == "M" || value.ToString() == "F")
            {
                return System.ComponentModel.DataAnnotations.ValidationResult.Success;
            }

            return
                new System.ComponentModel.DataAnnotations.ValidationResult(String.Format("{0} is not a gender", value));
        }
    }

    public class UserData
    {
        public const int maxLen = 255;
        public const int maxLenExtended = 2000;
        public const int phoneLen = 10;


        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are accaptable for first name")]
        [MaxLength(maxLen, ErrorMessage = "Too long first name")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are accaptable for last name")]
        [MaxLength(maxLen, ErrorMessage = "Too long first name")]
        public string LastName { get; set; }


        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        [MaxLength(maxLen, ErrorMessage = "Too long email")]
        public string Email { get; set; }

        [Gender]
        public string Gender { get; set; }

        [MaxLength(maxLenExtended, ErrorMessage = "Too long info section")]
        public string Info { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    }

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void validate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var UserData = new UserData()
                {
                    FirstName = firstName.Text,
                    LastName = lastName.Text,
                    DateOfBirth = new DateTime(int.Parse(yearOfBirth.Text),
                        int.Parse(monthOfBirth.Text),
                        int.Parse(dayOfBirth.Text)),
                    Gender = gender.Text,
                    Email = eMail.Text,
                    PhoneNumber = phoneNum.Text,
                    Info = info.Text
                };

                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var context = new ValidationContext(UserData);

                if (!Validator.TryValidateObject(UserData, context, results, true))
                {
                    string msg = "";
                    foreach (var error in results)
                    {
                        msg += (error.ErrorMessage) + "\n";
                    }
                    MessageBox.Show(msg);
                }
                else
                {
                    MessageBox.Show("Validated successfully!");
                }
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            
        }
        

    }
}
