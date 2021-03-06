using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCalculator
{
    public class Calculator
    {
        private Double m_total; //The total number; we are performing actions to this number (e.g. adding subtracting, etc to this number)
        private string m_value; //The number we are adding/multiplying/etc to m_total; 
                                //will get turned into a double; should be able to accommdate only 1 period in the string
        private Sign m_sign; //Whether or not m_value is currently positive or negative
        private Operator m_operator;

        public enum Operator
        {
            None = 0,
            Add = 1,
            Subtract =2,
            Multiply = 3,
            Divide = 4,
        }
        /**************************************
         * Indicates whether or not a number is positive or negative
         *************************************/
        public enum Sign
        {
            Plus=0,
            Minus=1
        }

        /**************************************
         * Sets up calculator object.  All values initialize at 0
         *************************************/
        public Calculator()
        {
            m_total = 0;
            m_value = "";
            m_sign = Sign.Plus;
            m_operator = Operator.None;
        }

        /**************************************
         * Getter for the calculated total
         *************************************/
        public double getTotal()
        {
            return m_total;
        }

        /**************************************
         * Getter for m_value as a double
         *************************************/
        public double getValue()
        {
            double val = 0;
            try
            {
                val = getDoubleValue();
            }
            catch ( Exception exc)
            {
                throw exc;
            }

            return val;
        }

        /**************************************
         * Getter for a string concatentation of sign and value; can be used for display purposes
         *************************************/
        public string getDisplayValue()
        {
            return getSignString() + m_value;
        }

        /**************************************
         * Clears internal total and value, and returns the current total
         * Return total value
         *************************************/
        public double clear()
        {
            m_total = 0;
            m_value = "";
            m_sign = Sign.Plus;

            return m_total;
        }

        /**************************************
         * Clears internal value, and returns the current value
         * Return total value
         *************************************/
        public double clearValue()
        {
            m_value = "";
            m_sign = Sign.Plus;

            return getDoubleValue();
        }

        /**************************************
         * Toggles negative or positive for value
         * Return: current value
         *************************************/
        public string toggleSign()
        {
            string valueModifier = "";
            if(m_sign == Sign.Plus)
            {
                m_sign = Sign.Minus;
                valueModifier = "-";
            }
            else if(m_sign == Sign.Minus)
            {
                m_sign = Sign.Plus;
            }

            return valueModifier + m_value;
        }

        /**************************************
         * Concatenates another number (or decimal) to the end of value
         * On error, throws FormatException or InvalidCastException
         * Return: current value
         *************************************/
        public string appendToValue(string appendVal)
        {
            string valToAdd = "";
            if(appendVal == ".")
            {
                //don't add a decimal if one already exists in the value
                //This shouldn't occur because we should disable the decimal button if they push it twice,
                //but add the logic just in case
                if (!m_value.Contains('.'))
                {
                    valToAdd = appendVal;
                }
                else
                {
                    throw new FormatException("<" + appendVal + "> cannot be appended because the value already contains a decimal.");
                }
            }
            else
            {
                if(!Int32.TryParse(appendVal, out int result))
                {
                    //only accept integer numbers, not strings.  Decimals handled higher up.
                    throw new InvalidCastException("<" + appendVal + "> is an invalid integer and cannot be added to the value.");
                }
                else
                {
                    valToAdd = appendVal;
                }

            }
            m_value += valToAdd;

            return getSignString() + m_value;
        }

        /**************************************
         * Converts value from a string to a double, if possible
         * On error, throws InvalidCastException
         * Return: m_value as a double
         *************************************/
        private double getDoubleValue()
        {
           if(m_value == "")
            {
                return 0;
            }

            if (!Double.TryParse(getSignString() + m_value, out double valDouble))
            {
                throw new InvalidCastException("<" + m_value + "> is an invalid double.  Operations cannot be performed!");
            }

            return valDouble;
        }

        /**************************************
         * Converts m_sign to - or emptystring for use in operational logic
         * Return: "-" if m_value's sign is negative, otherwise empty string
         *************************************/
        private string getSignString()
        {
            string sign = "";
            if (m_sign == Sign.Minus )
            {
                sign = "-";
            }

            return sign;
        }
        /**************************************
         * Perform the operator for total and value based on the operator saved in memory
         * return: the new total after the operation
         *************************************/
        public double performSavedOperation()
        {
            switch (m_operator)
            {
                case Operator.Add:
                {
                    m_total += getDoubleValue();
                    break;
                }
                case Operator.Subtract:
                {
                    m_total -= getDoubleValue();
                    break;
                }
                case Operator.Divide:
                {
                    m_total /= getDoubleValue();
                    break;
                }
                case Operator.Multiply:
                {
                    m_total *= getDoubleValue();
                    break;
                }
                default:
                {
                    break;
                }         

            }

            m_value = "";
            m_sign = Sign.Plus;
            m_operator = Operator.None;

            return m_total;
        }

        /**************************************
         * Saves the multiply operator and performs the previous operation
         * Returns: The new total after saved operation was performed
         *************************************/
        public double multiply()
        {
            try
            {
                if (m_operator == Operator.None)
                {
                    initializeTotalValue();
                }
                else
                {
                    performSavedOperation();  
                }

                m_operator = Operator.Multiply;
                m_value = "";
                m_sign = Sign.Plus;
            }
            catch (Exception exc)
            {
                //there is no custom text for overflow exceptions, but they should be thrown
                throw exc;
            }

            return m_total;
        }

        /**************************************
         * Saves the divide operator and performs the previous operation
         * Returns: The new total after saved operation was performed
         *************************************/
        public double divide()
        {
            try
            {
                if (m_operator == Operator.None)
                {
                    initializeTotalValue();
                }
                else
                {
                    performSavedOperation();
                }

                m_operator = Operator.Divide;
                m_value = "";
                m_sign = Sign.Plus;
            }
            catch (Exception exc)
            {
                //there is no custom text for overflow exceptions, but they should be thrown
                throw exc;
            }

            return m_total;
        }

        /**************************************
         * Saves the add operator and performs the previous operation
         * Returns: The new total after saved operation was performed
         *************************************/
        public double add()
        {
            try
            {
                if (m_operator == Operator.None)
                {
                    initializeTotalValue();
                }
                else
                {
                    performSavedOperation();
                }

                m_operator = Operator.Add;
                m_value = "";
                m_sign = Sign.Plus;
            }
            catch (Exception exc)
            {
                //there is no custom text for overflow exceptions, but they should be thrown
                throw exc;
            }

            return m_total;
        }

        /**************************************
         * Saves the subtract operator and performs the previous operation
         * Returns: The new total after saved operation was performed
         *************************************/
        public double subtract()
        {
            try
            {
                if (m_operator == Operator.None)
                {
                    initializeTotalValue();
                }
                else
                {
                    performSavedOperation();
                }

                m_operator = Operator.Subtract;
                m_value = "";
                m_sign = Sign.Plus;
            }
            catch (Exception exc)
            {
                //there is no custom text for overflow exceptions, but they should be thrown
                throw exc;
            }

            return m_total;
        }

        /**************************************
         * Subtracts the value from the total
         * Returns: The new total after the subtract operation is complete
         *************************************/
        public double equals()
        {
            try
            {
                performSavedOperation();  
            }
            catch (Exception exc)
            {
                //there is no custom text for overflow exceptions, but they should be thrown
                throw exc;
            }

            return m_total;
        }

        /**************************************
         * Used when no operator is saved in memory;
         * Set the total to the number that was just typed in
         *************************************/
        private void initializeTotalValue()
        {
            double value = getDoubleValue();
            if (value != 0)
            {
                m_total = value;
            }
        }

        /**************************************
         * Converts the saved operator to a string that can be displayed, logged, etc
         * Returns: String value of the saved operator
         *************************************/
        public string getSavedOperatorAsString()
        {
            string ret = "";
            switch (m_operator)
            {
                case Operator.Add:
                    {
                        ret = "+";
                        break;
                    }
                case Operator.Subtract:
                    {
                        ret = "-";
                        break;
                    }
                case Operator.Divide:
                    {
                        ret = "/";
                        break;
                    }
                case Operator.Multiply:
                    {
                        ret = "*";
                        break;
                    }
                default:
                    {
                        break;
                    }

            }
        
            return ret;
        }


    }

   
}
