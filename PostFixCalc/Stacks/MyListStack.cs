using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFixCalc
{
    /// <summary>
    /// The custom stack MyListStack
    /// </summary>
    /// <typeparam name="T">Generic type to fit any data type</typeparam>
    class MyListStack<T> : Stack
    {
        public double? item;
        private MyListStack<T> next;

        /// <summary>
        /// Adds a new item to the MyListStack
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value">The value that needs to be added</param>
        public void Add(int index, double? value)
        {
            // stop condition
            if (item == null)
            {
                item = value;
            }
            else if (next == null)
            {
                next = new MyListStack<T>();
                next.item = item;
                item = null;
                Add(0, value);
            }
            else
            {
                next.Add(0, item);
                item = value;
            }
        }

        // Documented in Stack.cs
        public override double? GetItem(int index)
        {
            if (index == 0)
            {
                return item;
            }
            else
            {
                if (next != null)
                {
                    return next.GetItem(index - 1);
                }
                else
                {
                    return null;
                }
            }
        }

        // Documented in Stack.cs
        public override void Delete(int index)
        {
            if (index == 0)
            {
                item = null;
            }
            else
            {
                if (next != null)
                {
                    next.Delete(index - 1);
                }
            }
            // Sort the Stack
            Sort();
        }

        /// <summary>
        /// Sorts the Stack so all the null pointers are moved to the end of the structure
        /// </summary>
        public void Sort()
        {
            if (next != null || item != null)
            {
                if (item == null)
                {
                    item = next.item;
                    next.item = null;
                    next.Sort();
                }
                else
                {
                    next.Sort();
                }
            }
        }

        // Documented in Stack.cs
        public override void Reset()
        {
            int index = 0;
            
            while (GetItem(0) != null)
            {
                Delete(0);
                index++;
            }
            FlushNext();
        }


        /// <summary>
        /// Makes all next variables equal to null
        /// </summary>
        private void FlushNext()
        {
            if (next != null)
            {
                next.FlushNext();
                next = null;
            }
        }

        /// <summary>
        /// Finalizes the MyListStack class
        /// </summary>
        ~MyListStack()
        {
            Clean clean = new Clean();
            clean.Dispose();
        }
    }
}
