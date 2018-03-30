using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PostFixCalc
{
    public partial class Calculator : Form
    {
        // The list that will move listbox items to the Stacks on radioButtons_CheckedChanged() 
        protected List<double?> listBoxValues = new List<double?>();

        // Some form variables that are used throughout the class
        protected TextBox textBox;
        protected ListBox listBox;
        protected List<RadioButton> radios = new List<RadioButton>();
        private CalculatorLogic calculatorLogic = new CalculatorLogic();
        GenerateField genField = new GenerateField();

        public Calculator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the components onto the Calculator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calculator_Load(object sender, EventArgs e)
        {
            LoadTextBox();
            LoadListBox();
            LoadNumpad();
            LoadRadios();
            loadEnterResetBtns();
        }

        /// <summary>
        /// Load a textbox onto the form
        /// </summary>
        private void LoadTextBox()
        {
            textBox = genField.GenerateTextBox(50, 50, 300, 16);
            calculatorLogic.textBox = textBox;
            Controls.Add(textBox);
        }

        /// <summary>
        /// Load a numpad onto the form
        /// </summary>
        private void LoadNumpad()
        {
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
                btn.Click += (calculatorLogic.numpad_Click);

                // Draw Buttons
                Controls.Add(btn);
            }
        }

        /// <summary>
        /// Load a listbox onto the form
        /// </summary>
        private void LoadListBox()
        {
            // Draw Listbox
            listBox = genField.GenerateListBox(400, 50, 550, 400);
            calculatorLogic.listBox = listBox;
            Controls.Add(listBox);
        }

        /// <summary>
        /// Load some radiobuttons onto the form
        /// </summary>
        private void LoadRadios()
        {
            // Draw radio buttons
            foreach (RadioButton radioButton in genField.GenerateRadioButtons(400, 460))
            {
                radioButton.CheckedChanged += new EventHandler(calculatorLogic.radioButtons_CheckedChanged);
                radios.Add(radioButton);
                Controls.Add(radioButton);
            }
            calculatorLogic.radios = radios;
        }

        /// <summary>
        /// Load enter and reset buttons onto the form
        /// </summary>
        private void loadEnterResetBtns()
        {
            // Draw reset button
            Button resetButton = genField.GenerateResetBtn(875, 460);
            resetButton.Click += new EventHandler(calculatorLogic.resetButton_Clicked);
            Controls.Add(resetButton);

            // Draw reset button
            Button enterButton = genField.GenerateEnterBtn(355, 50);
            enterButton.Click += new EventHandler(calculatorLogic.enterButton_Clicked);
            Controls.Add(enterButton);
        }

        /// <summary>
        /// Reacts to the keypresses on your keyboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyPress_Event(object sender, KeyPressEventArgs e)
        {
            calculatorLogic.InputValidation(e.KeyChar);
        }
    }
}
