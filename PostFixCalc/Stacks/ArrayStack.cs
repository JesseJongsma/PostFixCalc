using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFixCalc
{
    /// <summary>
    /// The ArrayStack
    /// </summary>
    class ArrayStack : Stack
    {
        double?[] arrayStack = new double?[15];
        public double? item;

        /// <summary>
        /// Adds a new item to the arrayStack
        /// </summary>
        /// <param name="index">The index of the listStack</param>
        /// <param name="value">The value that needs to be added</param>
        public void Add(int index, double? value)
        {
            ArrayResize(index);

            // stop condition
            if (arrayStack[0] == null)
            {
                arrayStack[0] = value;
            }
            // if the index is not at the first value
            else if (index > 0)
            {
                arrayStack[index] = arrayStack[index - 1];
                arrayStack[index - 1] = null;
                Add(index - 1, value);
            }
            // if the user is trying to add a new value, but arrayStack[0] already has a value
            else
            {
                // Start anew but at the end of the array
                Add(CountArray(), value);
            }
            item = arrayStack[0];
        }

        /// <summary>
        /// Increment size of array if the array becomes too small
        /// </summary>
        /// <param name="index">The index that needs to be checked</param>
        private void ArrayResize(int index)
        {
            if (index >= arrayStack.Length)
            {
                // Copy arrayStack to a temporary array
                double?[] tempArrayStack = new double?[arrayStack.Length];
                arrayStack.CopyTo(tempArrayStack, 0);

                // Copy the items back
                arrayStack = new double?[arrayStack.Length + 15];
                tempArrayStack.CopyTo(arrayStack, 0);
            }
        }

        // Documented in Stack.cs
        public override double? GetItem(int index)
        {
            if (index < arrayStack.Count())
            {
                return arrayStack[index];
            }
            return null;
        }

        // Documented in Stack.cs
        public override void Delete(int index)
        {
            if (index <= CountArray())
            {
                arrayStack[index] = null;
            }
            // Sort the array
            Sort();
        }

        /// <summary>
        /// Sorts the Stack so all the null pointers are moved to the end of the array
        /// </summary>
        /// <param name="index">the index of the list</param>
        private void Sort(int index = 0)
        {
            if (arrayStack[index + 1] != null || arrayStack[index] != null)
            {
                if (arrayStack[index] == null)
                {
                    arrayStack[index] = arrayStack[index + 1];
                    arrayStack[index + 1] = null;
                    Sort(index + 1);
                }
                else
                {
                    Sort(index + 1);
                }
            }
        }

        /// <summary>
        /// Counts the values of the array that are not equal to null
        /// </summary>
        /// <returns>Returns number of not null objects</returns>
        private int CountArray()
        {
            int count = 0;
            foreach (double? value in arrayStack)
            {
                if (value != null)
                {
                    count++;
                }
            }
            return count;
        }

        public override void Reset()
        {
            arrayStack = new double?[15];
        }

        /// <summary>
        /// Finalizes the ArrayStack class
        /// </summary>
        ~ArrayStack()
        {
            Clean clean = new Clean();
            clean.Dispose();
        }
    }
}
