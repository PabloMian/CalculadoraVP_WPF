using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata;
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

namespace CalculadoraPrograVisual
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
        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                //MessageBox.Show("Haz dado un click ;D");
                string value = (string)button.Content;
                if (IsNumber(value))//Validar un número
                {
                    HandleNumbers(value);
                }
                else if (IsOperator(value))
                {
                    HandleOperators(value);
                }
                else if (value == "C")
                {
                    Screen.Clear();
                }
                else if (value == "=")
                {
                    HandleEquals(Screen.Text);
                }
                else if (value == "CE")
                {
                    Screen.Text = Screen.Text.Remove(Screen.Text.Length - 1);
                }


            }
            catch (Exception ex)
            {

                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        //Métodos auxiliares
        public bool IsNumber(string num)
        {
            /*if (double.TryParse(num, out _))
            {
                return true;
            }
            else
            {
                return false;
            }*/
            return double.TryParse(num, out _);

        }

        private void HandleNumbers(string value)
        {
            if (String.IsNullOrEmpty(Screen.Text))
            {
                Screen.Text = value;
            }
            else
            {
                Screen.Text += value;
            }
        }

        private bool IsOperator(string possibleOperator)
        {
            /*if(possibleOperator == "+" || possibleOperator == "-" || possibleOperator == "*" || possibleOperator == "/")
            {
                return true;
            }
            return false;*/

            return possibleOperator == "+" || possibleOperator == "-"
                || possibleOperator == "*" || possibleOperator == "/";
        }

        private void HandleOperators(string value)
        {
            if (!String.IsNullOrEmpty(Screen.Text) && !ContainsOtherOperators(Screen.Text))
            {
                Screen.Text += value;
            }
        }

        private bool ContainsOtherOperators(string screenContent)
        {
            return screenContent.Contains("+") || screenContent.Contains("-") || screenContent.Contains("*") || screenContent.Contains("/");
        }

        private void HandleEquals(string screenContent)
        {
            string op = FindOperator(screenContent);
            //Arreglar bien el tema de los números negativos. Estoes temporal.
            if (!String.IsNullOrEmpty(op))

                switch (op)
                {
                    case "+":
                        Screen.Text = Sum();
                        break;
                    case "-":
                        Screen.Text = Rest();
                        break;
                    case "*":
                        Screen.Text = Multi();
                        break;
                    case "/":
                        Screen.Text = Div();
                        break;
                }
        }
        private string FindOperator(string screenContent)
        {
            foreach (char c in screenContent)
            {
                if (IsOperator(c.ToString()))
                {
                    return c.ToString();
                }
            }
            return screenContent;
        }
        private string Sum()
        {
            string[] numbers = Screen.Text.Split("+");

            double.TryParse(numbers[0], out double n1);
            double.TryParse(numbers[1], out double n2);

            return Math.Round(n1 + n2, 12).ToString();
        }
        private string Rest()
        {
            string[] numbers = Screen.Text.Split("-");

            double.TryParse(numbers[0], out double n1);
            double.TryParse(numbers[1], out double n2);

            return Math.Round(n1 - n2, 12).ToString();
        }
        private string Multi()
        {
            string[] numbers = Screen.Text.Split("*");

            double.TryParse(numbers[0], out double n1);
            double.TryParse(numbers[1], out double n2);

            return Math.Round(n1 * n2, 12).ToString();
        }
        private string Div()
        {
            string[] numbers = Screen.Text.Split("/");

            double.TryParse(numbers[0], out double n1);
            double.TryParse(numbers[1], out double n2);

            return Math.Round(n1 / n2, 12).ToString();
        }
    }
}
