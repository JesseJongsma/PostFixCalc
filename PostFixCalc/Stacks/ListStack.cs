using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFixCalc
{
    /// <summary>
    /// The ListStack
    /// </summary>
    class ListStack : Stack
    {
        private List<double?> listStack = new List<double?>() { null } ;
        public double? item;

        /// <summary>
        /// Adds a new item to the listStack
        /// </summary>
        /// <param name="index">The index of the listStack</param>
        /// <param name="value">The value that needs to be added</param>
        public void Add(int index, double? value)
        {
            // stop condition
            if (listStack.First() == null)
            {
                listStack[0] = value;
            }
            // if the index is not at the first value
            else if (index > 0)
            {
                listStack[index] = listStack[index - 1];
                listStack[index - 1] = null;
                Add(index - 1, value);
            }
            else
            {
                listStack.Add(null);
                Add(listStack.Count - 1, value);
            }
            item = listStack[0];
        }

        // Documented in Stack.cs
        public override double? GetItem(int index)
        {
            if (index < listStack.Count)
            {
                return listStack[index];
            }
            return null;
        }

        // Documented in Stack.cs
        public override void Delete(int index)
        {
            if (index <= listStack.Count -1)
            {
                listStack[index] = null;
            }
        }

        // Documented in Stack.cs
        public override void Reset()
        {
            listStack.Clear();
            
            // add null to hold the first value upon adding
            listStack.Add(null);
        }

        /// <summary>
        /// Finalizes the class
        /// </summary>
        ~ListStack()
        {
            Clean clean = new Clean();
            clean.Dispose();
        }
    }
}
