using System;
using System.Windows;

namespace MathsOperators
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Сalculate(object sender, RoutedEventArgs e)
        {
            try
            {
                Run();
            }
            catch (Exception exc)
            {
                result.Text = exc.Message;
            }

        }

        private void Run()
        {
                if (addition.IsChecked.GetValueOrDefault())
                {
                    Addition();
                }
                else if (subtraction.IsChecked.GetValueOrDefault())
                {
                    Subtraction();
                }
                else if (multiplication.IsChecked.GetValueOrDefault())
                {
                    Multiplication();
                }
                else if (division.IsChecked.GetValueOrDefault())
                {
                    Division();
                }
                else
                {
                    throw new InvalidOperationException("Operator is undefined!");
                }
        }

        private void Addition()
        {
            double left = double.Parse(leftBox.Text);
            double right = double.Parse(rightBox.Text);
            double result = left + right;
            this.result.Text = String.Format("{0:0.00}", result); 
        }

        private void Subtraction()
        {
            double left = double.Parse(leftBox.Text);
            double right = double.Parse(rightBox.Text);
            double result = left - right;

            this.result.Text = String.Format("{0:0.00}", result);
        }

        private void Multiplication()
        {
            double left = double.Parse(leftBox.Text);
            double right = double.Parse(rightBox.Text);
            double result = checked(left * right);

            this.result.Text = String.Format("{0:0.00}", result);
        }

        private void Division()
        {
            double left = double.Parse(leftBox.Text);
            double right = double.Parse(rightBox.Text);
            double result = left / right;

            this.result.Text = String.Format("{0:0.00}", result);
        }
    }

}
