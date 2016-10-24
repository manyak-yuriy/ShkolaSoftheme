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

namespace WpfApplication1
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


        const string imageURIprefix = "http://www.psychicguild.com/images/starsign/clear/";
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime date = getDate(dateOfBirth.Text);
                string sign = getSign(date);
                int age = getAge(date);

                signBox.Content = sign;

                image.Source = new BitmapImage(new Uri(imageURIprefix + sign.ToLower() + ".png", UriKind.RelativeOrAbsolute));
                ageBox.Content = age;
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }    
        }

        private static DateTime getDate(string inputDate)
        {
            DateTime date;
            try
            {
                date = DateTime.ParseExact(inputDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid date format!\n");
                throw;
            }
            return date;
        }

        private static int getAge(DateTime date)
        {
            return DateTime.Now.Year - date.Year;
        }

        private static string getSign(DateTime date)
        {
            int month = date.Month;
            int day = date.Day;

            switch (month)
            {
                case 1:
                    if (day <= 19)
                        return "Capricorn";
                    else
                        return "Aquarius";

                case 2:
                    if (day <= 18)
                        return "Aquarius";
                    else
                        return "Pisces";
                case 3:
                    if (day <= 20)
                        return "Pisces";
                    else
                        return "Aries";
                case 4:
                    if (day <= 19)
                        return "Aries";
                    else
                        return "Taurus";
                case 5:
                    if (day <= 20)
                        return "Taurus";
                    else
                        return "Gemini";
                case 6:
                    if (day <= 20)
                        return "Gemini";
                    else
                        return "Cancer";
                case 7:
                    if (day <= 22)
                        return "Cancer";
                    else
                        return "Leo";
                case 8:
                    if (day <= 22)
                        return "Leo";
                    else
                        return "Virgo";
                case 9:
                    if (day <= 22)
                        return "Virgo";
                    else
                        return "Libra";
                case 10:
                    if (day <= 22)
                        return "Libra";
                    else
                        return "Scorpio";
                case 11:
                    if (day <= 21)
                        return "Scorpio";
                    else
                        return "Sagittarius";
                case 12:
                    if (day <= 21)
                        return "Sagittarius";
                    else
                        return "Capricorn";
                default:
                    return "";
            }

        }

    }
}
