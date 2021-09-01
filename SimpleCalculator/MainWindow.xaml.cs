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

namespace SimpleCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Calculator m_calc;
        public MainWindow()
        {
            InitializeComponent();
            TextboxTotalDisplay.Text = "0";
            TextboxValueDisplay.Text = "0";
            m_calc = new Calculator();
        }

        /**************************************
         * Handler for any button numeric or decimal button push; appends a number/decimal to the current internal value
         *************************************/
        private void onValueButtonPush(string numberToAppend)
        {
            string displayValue = TextboxTotalDisplay.Text;
            try
            {
                displayValue = m_calc.appendToValue(numberToAppend);
            }
            catch (Exception exc)
            {
                string messageBoxText = exc.Message;
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;

                result = MessageBox.Show(messageBoxText, caption, button, icon);
                displayValue = m_calc.getDisplayValue();
            }

            TextboxValueDisplay.Text = displayValue;
        }

        /**************************************
         * Handler for ButtonOne
         *************************************/
        private void OnPush_ButtonOne(object sender, RoutedEventArgs e)
        {
            onValueButtonPush("1");
        }

        /**************************************
         * Handler for ButtonTwo
         *************************************/
        private void OnPush_ButtonTwo(object sender, RoutedEventArgs e)
        {
            onValueButtonPush("2");
        }

        /**************************************
         * Handler for ButtonThree
         *************************************/
        private void OnPush_ButtonThree(object sender, RoutedEventArgs e)
        {
            onValueButtonPush("3");
        }

        /**************************************
         * Handler for ButtonFour
         *************************************/
        private void OnPush_ButtonFour(object sender, RoutedEventArgs e)
        {
            onValueButtonPush("4");
        }

        /**************************************
         * Handler for ButtonFive
         *************************************/
        private void OnPush_ButtonFive(object sender, RoutedEventArgs e)
        {
            onValueButtonPush("5");
        }

        /**************************************
         * Handler for ButtonSix
         *************************************/
        private void OnPush_ButtonSix(object sender, RoutedEventArgs e)
        {
            onValueButtonPush("6");
        }

        /**************************************
         * Handler for ButtonSeven
         *************************************/
        private void OnPush_ButtonSeven(object sender, RoutedEventArgs e)
        {
            onValueButtonPush("7");
        }

        /**************************************
         * Handler for ButtonEight
         *************************************/
        private void OnPush_ButtonEight(object sender, RoutedEventArgs e)
        {
            onValueButtonPush("8");
        }

        /**************************************
         * Handler for ButtonNine
         *************************************/
        private void OnPush_ButtonNine(object sender, RoutedEventArgs e)
        {
            onValueButtonPush("9");
        }

        /**************************************
         * Change value from positive to negative or vice versa
         *************************************/
        private void OnPush_ButtonPlusNegative(object sender, RoutedEventArgs e)
        {
            m_calc.toggleSign();
            displayData();
        }

        /**************************************
         * Handler for ButtonZero
         *************************************/
        private void OnPush_ButtonZero(object sender, RoutedEventArgs e)
        {
            onValueButtonPush("0");
        }

        /**************************************
         * Handler for ButtonDecimal
         *************************************/
        private void OnPush_ButtonDecimal(object sender, RoutedEventArgs e)
        {
            onValueButtonPush(".");
        }

        /**************************************
         * Add internal value to the total
         *************************************/
        private void OnPush_ButtonPlus(object sender, RoutedEventArgs e)
        {
            displayData(); m_calc.add();
            displayData();
        }

        /**************************************
         * Subtract internal value from the total
         *************************************/
        private void OnPush_ButtonMinus(object sender, RoutedEventArgs e)
        {
            displayData(); m_calc.subtract();
            displayData();
        }

        /**************************************
         * Multiply total by the current value
         *************************************/
        private void OnPush_ButtonMultiply(object sender, RoutedEventArgs e)
        {
            displayData(); m_calc.multiply();
            displayData();
        }

        /**************************************
        * Divide total by the current value
        *************************************/
        private void OnPush_ButtonDivide(object sender, RoutedEventArgs e)
        {
            m_calc.divide();
            displayData();
        }

        /**************************************
        * Clear internal total and value
        *************************************/
        private void OnPush_ButtonClear(object sender, RoutedEventArgs e)
        {
            m_calc.clear();
            displayData();
        }

        /**************************************
        * Clear internal value
        *************************************/
        private void OnPush_ButtonClearValue(object sender, RoutedEventArgs e)
        {
            m_calc.clearValue();
            displayData();
        }

        /**************************************
        * Handler for the equal button
        *************************************/
        private void OnPush_ButtonEquals(object sender, RoutedEventArgs e)
        {
            m_calc.equals();
            displayData();
        }

        /**************************************
        * Refresh the total, value, and operator on the screen
        *************************************/
        private void displayData()
        {
            TextboxTotalDisplay.Text = m_calc.getTotal().ToString();
            TextboxValueDisplay.Text = m_calc.getDisplayValue();
            LabelOperator.Content = m_calc.getSavedOperatorAsString();
        }
    }
}
