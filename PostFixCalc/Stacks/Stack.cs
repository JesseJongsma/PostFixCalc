using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFixCalc
{
    /// <summary>
    /// The abstract class Stack
    /// </summary>
    abstract class Stack : Clean
    {
        /// <summary>
        /// Gets the value of the given index
        /// </summary>
        /// <param name="index">the index of the stack item</param>
        /// <returns>double or null</returns>
        public abstract double? GetItem(int index);

        /// <summary>
        /// Deletes the value of the given index
        /// </summary>
        /// <param name="index">the index of the stack item</param>
        public abstract void Delete(int index);

        /// <summary>
        /// Used to reset all the items in the list
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Check if the user wants to add, subtract, multiply or divide
        /// </summary>
        /// <param name="item">The first number of the calculation</param>
        /// <param name="operation">add(+), subtract(-), multiply(*) or divide(/)</param>
        /// <returns>double?</returns>
        public double? Operation(double? item, char operation)
        {
            double? calc;
            if (item != null && GetItem(1) != null)
            {
                switch (operation)
                {
                    case ('/'):
                        calc = item / GetItem(1);
                        break;
                    case ('*'):
                        calc = item * GetItem(1);
                        break;
                    case ('-'):
                        calc = item - GetItem(1);
                        break;
                    case ('+'):
                        calc = item + GetItem(1);
                        break;
                    default:
                        return null;
                }
                Delete(1);
                return calc;
            }
            return null;
        }

        /// <summary>
        /// Returns a list to show in the listbox.
        /// </summary>
        /// <returns>Returns a list with all the items of the stack.</returns>
        public List<double?> GetList()
        {
            List<double?> resultList = new List<double?>();

            int i = 0;
            while (GetItem(i) != null)
            {
                resultList.Add(GetItem(i));
                i++;
            }
            return resultList;
        }
    }
}
