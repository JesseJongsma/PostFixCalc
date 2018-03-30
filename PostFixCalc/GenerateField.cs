using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostFixCalc
{
    /// <summary>
    /// Creates the objects for the Calculator design
    /// </summary>
    class GenerateField
    {
        /// <summary>
        /// Creates a new textbox
        /// </summary>
        /// <param name="x">The X-axis of the starting point</param>
        /// <param name="y">The Y-axis of the starting point</param>
        /// <param name="width">The width of the textbox</param>
        /// <param name="fontSize">The FontSize of the textbox</param>
        /// <returns>A new textbox</returns>
        public TextBox GenerateTextBox(int x, int y, int width, int fontSize)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new System.Drawing.Point(x, y);
            textBox.Size = new System.Drawing.Size(width, 0);
            textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", fontSize, System.Drawing.FontStyle.Regular);
            textBox.ReadOnly = true;
            textBox.Focus();
            return textBox;
        }

        /// <summary>
        /// Generates a numpad for the calculator
        /// </summary>
        /// <param name="btnNames">Enter an array of button names</param>
        /// <returns>Returns a list of buttons</returns>
        public List<Button> GenerateNumpad(string[,] btnNames)
        {
            List<Button> buttons = new List<Button>();
            for (int x = 0; x < btnNames.GetLength(0); x++)
            {
                for (int y = 0; y < btnNames.GetLength(1); y++)
                {
                    Button newButton = new Button();
                    newButton.Name = btnNames[x, y];
                    newButton.Text = btnNames[x, y];
                    newButton.Location = new System.Drawing.Point(50 + 80 * y, 100 + 100 * x);
                    newButton.Size = new System.Drawing.Size(60, 60);
                    newButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16, System.Drawing.FontStyle.Regular);
                    buttons.Add(newButton);
                }
            }
            return buttons;
        }

        /// <summary>
        /// Generates a ListBox
        /// </summary>
        /// <param name="x">The X-axis of the starting point</param>
        /// <param name="y">The Y-axis of the starting point</param>
        /// <param name="width">The width of the box</param>
        /// <param name="height">The height of the box</param>
        /// <returns>Returns a new listbox</returns>
        public ListBox GenerateListBox(int x, int y, int width, int height)
        {
            ListBox listBox = new ListBox();
            listBox.Name = "StackList";
            listBox.Location = new System.Drawing.Point(x, y);
            listBox.Width = width;
            listBox.Height = height;
            return listBox;
        }

        /// <summary>
        /// Generate radiobuttons
        /// </summary>
        /// <param name="x">The X-axis of the starting point</param>
        /// <param name="y">The Y-axis of the starting point</param>
        /// <returns>Returns a list of radio buttons</returns>
        public List<RadioButton> GenerateRadioButtons(int x, int y)
        {
            List<RadioButton> radioButtons = new List<RadioButton>();
            string[] texts = new string[3] { "MyListStack" , "ArrayStack", "ListStack" };
            for(int i = 0; i < texts.Length; i++) 
            {
                RadioButton radio = new RadioButton();
                radio.Location = new System.Drawing.Point(x, y + i * 30);
                radio.Text = texts[i];

                if (i == 0)
                {
                    radio.Checked = true;
                }

                radioButtons.Add(radio);
            }
            return radioButtons;
        }

        /// <summary>
        /// Generates a button to append items to the listbox
        /// </summary>
        /// <param name="x">The X-axis of the starting point</param>
        /// <param name="y">The Y-axis of the starting point</param>
        /// <returns>Returns the enter button</returns>
        public Button GenerateEnterBtn(int x, int y)
        {
            Button enterButton = new Button();
            enterButton.Text = ">>";
            enterButton.Name = "Enter";
            enterButton.Location = new System.Drawing.Point(x, y);
            enterButton.Size = new System.Drawing.Size(40, 33);
            return enterButton;
        }

        /// <summary>
        /// Generates a button to reset the listbox
        /// </summary>
        /// <param name="x">The X-axis of the starting point</param>
        /// <param name="y">The Y-axis of the starting point</param>
        /// <returns>Returns the reset button</returns>
        public Button GenerateResetBtn(int x, int y)
        {
            Button resetBtn = new Button();
            resetBtn.Text = "Reset";
            resetBtn.Name = "Reset";
            resetBtn.Location = new System.Drawing.Point(x, y);
            return resetBtn;
        }
    }
}
