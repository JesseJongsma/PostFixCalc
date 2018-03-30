using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostFixCalc
{
    public partial class Calculator : Form
    {
        // The instances of the stack classes
        private MyListStack<double> myListStack;
        private ListStack listStack;
        private ArrayStack arrayStack;

        // The list that will move listbox items to the Stacks on radioButtons_CheckedChanged() 
        private List<double?> listBoxValues;

        // Some form variables that are used throughout the class
        private ListBox listBox;
        private TextBox textBox;
        private Button resetButton;
        private Button enterButton;
        private List<RadioButton> radios = new List<RadioButton>();

        public Calculator()
        {
            InitializeComponent();

            myListStack = new MyListStack<double>();
            listStack = new ListStack();
            arrayStack = new ArrayStack();
            listBoxValues = new List<double?>();
        }

        /// <summary>
        /// Loads the components onto the Calculator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calculator_Load(object sender, EventArgs e)
        {
            // Initiate te GenerateField class to draw the fields
            GenerateField genField = new GenerateField();

            // Draw Textbox
            textBox = genField.GenerateTextBox(50, 50, 300, 16);
            Controls.Add(textBox);

            // The buttons for the calculator
            string[,] btnNames = new string[4, 4]
            {
                { "7", "8", "9", "/" },
                { "6", "5", "4", "*" },
                { "3", "2", "1", "-" },
                { "(-)", "0", ".", "+" }
            };
            foreach (Button btn in genField.GenerateNumpad(btnNames))
            {
                btn.Click += new EventHandler(Numpad_Click);

                // Draw Buttons
                Controls.Add(btn);
            }

            // Draw Listbox
            listBox = genField.GenerateListBox(400, 50, 550, 400);
            Controls.Add(listBox);

            // Draw radio buttons
            foreach (RadioButton radioButton in genField.GenerateRadioButtons(400, 460))
            {
                radioButton.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
                radios.Add(radioButton);
                Controls.Add(radioButton);
            }

            // Draw reset button
            resetButton = genField.GenerateResetBtn(875, 460);
            resetButton.Click += new EventHandler(resetButton_Clicked);
            Controls.Add(resetButton);

            // Draw reset button
            enterButton = genField.GenerateEnterBtn(355, 50);
            enterButton.Click += new EventHandler(enterButton_Clicked);
            Controls.Add(enterButton);
        }


        /// <summary>
        /// Resets the list box and updates it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetButton_Clicked(object sender, EventArgs e)
        {
            // MyListStack
            if (radios[0].Checked)
            {
                myListStack.Reset();
            }
            // ArrayStack
            else if (radios[1].Checked)
            {
                arrayStack.Reset();
            }
            // ListStack
            else
            {
                listStack.Reset();
            }
            listBoxValues = null;
            textBox.Focus();
            UpdateListBox();
        }

        /// <summary>
        /// Adds item to the list box and updates it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterButton_Clicked(object sender, EventArgs e)
        {
            UpdateListBox();
            textBox.Focus();
        }

        /// <summary>
        /// If the radiobuttons are changed, the previous Stack will be reset and the new will be filled with new data.
        /// This method will be called twice. The first time it will reset and the second time it will fill in the new data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            // if the listbox contains some data, it will be stored in listBoxValues
            if (listBox.Items.Count > 0)
            {
                listBoxValues = listBox.Items.Cast<double?>().ToList();
                listBoxValues.Reverse(); // reverse the list so it can be added directly to the Stacks
            }

            // Do nothing if the listbox is empty
            if (listBoxValues != null)
            {
                if (rb.Checked)
                {
                    foreach (double? value in listBoxValues)
                    {
                        if (rb.Text == "MyListStack")
                        {
                            myListStack.Add(0, value);
                        }
                        else if (rb.Text == "ArrayStack")
                        {
                            arrayStack.Add(0, value);
                        }
                        else
                        {
                            listStack.Add(0, value);
                        }
                    }
                }
                else
                {
                    if (rb.Text == "MyListStack")
                    {
                        myListStack.Reset();
                    }
                    else if (rb.Text == "ArrayStack")
                    {
                        arrayStack.Reset();
                    }
                    else
                    {
                        listStack.Reset();
                    }
                }
            }
            UpdateListBox();
        }

        /// <summary>
        /// Reacts to Buttons on the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Numpad_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text != "(-)")
            {
                InputValidation(Convert.ToChar(btn.Text));
            }
            else
            {
                InputValidation('_');
            }
        }

        /// <summary>
        /// Reacts to the keypresses on your keyboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyPress_Event(object sender, KeyPressEventArgs e)
        {
            InputValidation(e.KeyChar);
        }

        private void InputValidation(char keyChar)
        {
            // Check if the input is on top of the keyboard or numpad
            if (keyChar >= 48 && keyChar <= 57)
            {
                // Only add a 0 when another number has been added
                if (keyChar > 48 || textBox.Text != "")
                {
                    textBox.Text = textBox.Text + keyChar.ToString();
                }
            }

            // Only add a dot when a number has been entered and doesn't already contain a dot
            if (keyChar == '.' && textBox.Text != "" && !textBox.Text.Contains('.'))
            {
                textBox.Text = textBox.Text + keyChar.ToString();
            }

            // Only add a minus sign textbox doesn't already contain a minus sign and is empty
            if (keyChar == '_' && !textBox.Text.Contains('-') && textBox.Text == "")
            {
                textBox.Text = textBox.Text + '-'.ToString();
            }

            // Check when a arithmetic operation is pressed
            char[] allowedhars = new char[4] { '/', '*', '-', '+' };
            if (allowedhars.Contains(keyChar))
            {
                UpdateListBox(keyChar);
            }

            // Check if the backspace has been pressed
            if (keyChar == 8)
            {
                if (textBox.Text != "")
                {
                    int count = textBox.Text.Count();
                    textBox.Text = textBox.Text.Remove(count - 1, 1);
                }
            }

            // Check if the enter key has been pressed
            if (keyChar == (char)Keys.Enter)
            {
                UpdateListBox();
            }
        }

        /// <summary>
        /// Updates the listbox and adds data to list if textbox is not null
        /// </summary>
        private void UpdateListBox(char keyChar = '\0')
        {
            // MyListStack
            if (radios[0].Checked)
            {
                LoadInput(myListStack, keyChar);
            }
            // ArrayStack
            else if (radios[1].Checked)
            {
                LoadInput(arrayStack, keyChar);
            }
            // ListStack
            else
            {
                LoadInput(listStack, keyChar);
            }
            textBox.Text = "";
        }

        /// <summary>
        /// Adds items to the listbox and does calculations
        /// </summary>
        /// <param name="instance">Defines the class that this method will use</param>
        /// <param name="keyChar">The key of the equation like +, -, / or *. (Optional)</param>
        private void LoadInput(dynamic instance, char keyChar = '\0')
        {
            // check if keyChar hes been set '\0' is a null pointer in unicode
            if (keyChar != '\0')
            {
                // calculate the equation
                double? calc = instance.Operation(instance.item, keyChar);
                if (calc != null)
                {
                    instance.Delete(0);
                    instance.Add(0, calc);
                }
            }

            // add a new number to the list if the textbox is not empty
            if (textBox.Text.Length != 0)
            {
                instance.Add(0, Convert.ToDouble(textBox.Text));
            }
            // reloads the listbox
            listBox.DataSource = instance.GetList();
        }
    }
}
