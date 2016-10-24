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

namespace NumberPicker
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

        private void pickNumberClick(object sender, RoutedEventArgs e)
        {
            int numValue = 0;
            string message = "";

            try
            {
                numValue = int.Parse(number.Text);

                if (numValue < minNum || numValue > maxNum)
                    throw new ArgumentException("The number must be between 1 and 10");

                int randNum = generateRandNum();

                message = (numValue == randNum) ? "Success!" : "Wrong! The number was " + randNum.ToString();
            }
            catch(Exception exc)
            {
                message = exc.Message;
            }
            finally
            {
                MessageBox.Show(message);
            }
            
        }

        private const int minNum = 1;
        private const int maxNum = 10;

        Random rnd = new Random();
        private int generateRandNum()
        {
            int result = rnd.Next(minNum, maxNum + 1);
            return result;
        }
    }
}
