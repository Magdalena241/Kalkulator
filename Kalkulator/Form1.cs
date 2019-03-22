using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kalkulator
{
    public partial class Form1 : Form

    {
        private string firstNumber = "";
        private string secondNumber = "";
        private string mathSign = "";
        private string answer = "";
        public Form1()
        {
            InitializeComponent();
            string separator = CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
            separatorButton.Text = separator;
            label1.Focus();
            
        }

        private void number_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string temp = button.Text;
            writeNumber(temp);
           
        }

        private void updateScreen()
        {
            screenBox.Text =firstNumber + "\r\n" + mathSign + "\r\n" + secondNumber;
            if (answer != "")
            {
                screenBox.Text += "\r\n" + answer;
            }
        }


        private void mathOperation_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            setSign(button.Text);
            label1.Focus();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            clearScreen();
            label1.Focus();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            backspace();
            label1.Focus();
        }

        private void equalButton_Click(object sender, EventArgs e)
        {
            calculate();
            label1.Focus();
        }

        private void separatorButton_Click(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            string temp = button.Text;
            if(mathSign !="")
            {
                if(secondNumber == "")
                {
                    secondNumber = "0" + temp;
                }
                else
                {
                   if(secondNumber.Contains(temp)==false)
                    {
                        secondNumber += temp;
                    }
                }
            }
            else
            {
                if (firstNumber == "")
                {
                    firstNumber = "0" + temp;
                }
                else
                {
                    if (firstNumber.Contains(temp) == false)
                    {
                        firstNumber += temp;
                    }
                }
            }
            updateScreen();
            label1.Focus();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int enter = (char)Keys.Enter;
            int back = (char)Keys.Back;
            if(e.KeyChar >= 48 && e.KeyChar<= 57)
            {
                writeNumber(e.KeyChar.ToString());
            }
            else if (e.KeyChar =='/' || e.KeyChar == '+' || e.KeyChar == '-' || e.KeyChar == 42)
            {
                setSign(e.KeyChar.ToString());
            }
            else if (e.KeyChar == 13 )
            {
                calculate();
            } 
            else if (e.KeyChar == 46)
            {
                clearScreen();
            }
            else if (e.KeyChar == back)
            {
                backspace();
            }
            e.Handled = true;
        }


        private void writeNumber(string temp)
        {
            if (answer != "")
            {
                clearButton_Click(null, null);
            }
            if (mathSign != "")
            {
                if (temp == "0" && secondNumber == "0")
                    return;
                if (secondNumber == "0")
                    secondNumber = "";
                if (secondNumber.Length < 20)
                    secondNumber += temp;
            }
            else
            {
                if (temp == "0" && firstNumber == "0")
                    return;
                if (firstNumber == "0")
                    firstNumber = "";
                if (firstNumber.Length < 20)
                    firstNumber += temp;
            }
            updateScreen();
        }

        private void clearScreen()
        {
            firstNumber = "";
            secondNumber = "";
            mathSign = "";
            answer = "";
            updateScreen();
        }

        private void setSign(string temp)
        {
            if (firstNumber == "")
                firstNumber = "0";
            if (answer != "")
                return;
            if (firstNumber[firstNumber.Length - 1] == separatorButton.Text[0])
                firstNumber = firstNumber.Substring(0, firstNumber.Length - 1);
            mathSign = temp;
            updateScreen();
        }

        private void backspace()
        {
            if (answer != "")
                return;
            if (mathSign != "")
            {
                if (secondNumber != "")
                {
                    int n = secondNumber.Length;
                    secondNumber = secondNumber.Substring(0, n - 1);
                }
            }
            else
            {
                if (firstNumber != "")
                {
                    int n = firstNumber.Length;
                    firstNumber = firstNumber.Substring(0, n - 1);
                }
            }
            updateScreen();
        }

        private void calculate()
        {
            if (mathSign == "")
                return;
            if (secondNumber == "")
                secondNumber = "0";
            if (secondNumber[secondNumber.Length - 1] == separatorButton.Text[0])
                secondNumber = secondNumber.Substring(0, secondNumber.Length - 1);
            double a = Double.Parse(firstNumber);
            double b = Double.Parse(secondNumber);
            double c = 0;
            if (mathSign == "+")
            {
                c = a + b;
            }
            else if (mathSign == "-")
            {
                c = a - b;
            }
            else if (mathSign == "*")
            {
                c = a * b;
            }
            else if (mathSign == "/")
            {
                if (b != 0)
                    c = a / b;
                else
                    answer = "nie dzielimy przez 0";
            }
            if (answer == "")
                answer = "=" + c.ToString();
            updateScreen();
        }
    }
}