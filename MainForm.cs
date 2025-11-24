using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsQuizApp
{
    /// <summary>
    ///     Главная форма приложения с тестом из трёх вопросов.
    ///     Каждый вопрос расположен на отдельной вкладке (TabPage).
    /// </summary>
    public class MainForm : Form
    {
        private readonly TestResultProcessor _processor = new();

        private TabControl _tabControl = null!;

        // Вопрос 1 (RadioButton)
        private RadioButton _q1OptionInt = null!;
        private RadioButton _q1OptionString = null!;
        private RadioButton _q1OptionBool = null!;

        // Вопрос 2 (CheckBox)
        private CheckBox _q2CSharp = null!;
        private CheckBox _q2Java = null!;
        private CheckBox _q2Html = null!;
        private CheckBox _q2Sql = null!;

        // Вопрос 3 (TextBox)
        private TextBox _q3TextBox = null!;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Тест по основам программирования";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(800, 450);
            MaximizeBox = false;

            _tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            var tabQuestion1 = CreateQuestion1Tab();
            var tabQuestion2 = CreateQuestion2Tab();
            var tabQuestion3 = CreateQuestion3Tab();

            _tabControl.TabPages.Add(tabQuestion1);
            _tabControl.TabPages.Add(tabQuestion2);
            _tabControl.TabPages.Add(tabQuestion3);

            Controls.Add(_tabControl);
        }

        /// <summary>
        ///     Вкладка с вопросом 1 — одиночный выбор (RadioButton).
        /// </summary>
        private TabPage CreateQuestion1Tab()
        {
            var tabPage = new TabPage("Вопрос 1");

            var questionLabel = new Label
            {
                AutoSize = true,
                Location = new Point(20, 20),
                Text = "Какой тип данных в C# используется для целых чисел?"
            };

            _q1OptionInt = new RadioButton
            {
                AutoSize = true,
                Location = new Point(40, 60),
                Text = "int",
                Tag = "int"
            };

            _q1OptionString = new RadioButton
            {
                AutoSize = true,
                Location = new Point(40, 90),
                Text = "string",
                Tag = "string"
            };

            _q1OptionBool = new RadioButton
            {
                AutoSize = true,
                Location = new Point(40, 120),
                Text = "bool",
                Tag = "bool"
            };

            var confirmButton = new Button
            {
                Text = "Подтвердить ответ",
                Location = new Point(40, 170),
                AutoSize = true
            };
            confirmButton.Click += Question1ConfirmButton_Click;

            tabPage.Controls.Add(questionLabel);
            tabPage.Controls.Add(_q1OptionInt);
            tabPage.Controls.Add(_q1OptionString);
            tabPage.Controls.Add(_q1OptionBool);
            tabPage.Controls.Add(confirmButton);

            return tabPage;
        }

        /// <summary>
        ///     Вкладка с вопросом 2 — множественный выбор (CheckBox).
        /// </summary>
        private TabPage CreateQuestion2Tab()
        {
            var tabPage = new TabPage("Вопрос 2");

            var questionLabel = new Label
            {
                AutoSize = true,
                Location = new Point(20, 20),
                Text = "Какие из следующих вариантов являются языками программирования?"
            };

            _q2CSharp = new CheckBox
            {
                AutoSize = true,
                Location = new Point(40, 60),
                Text = "C#"
            };
            _q2Java = new CheckBox
            {
                AutoSize = true,
                Location = new Point(40, 90),
                Text = "Java"
            };
            _q2Html = new CheckBox
            {
                AutoSize = true,
                Location = new Point(40, 120),
                Text = "HTML"
            };
            _q2Sql = new CheckBox
            {
                AutoSize = true,
                Location = new Point(40, 150),
                Text = "SQL"
            };

            var hintLabel = new Label
            {
                AutoSize = true,
                Location = new Point(40, 185),
                ForeColor = Color.DimGray,
                Text = "Подсказка: правильный ответ может содержать несколько вариантов."
            };

            var confirmButton = new Button
            {
                Text = "Подтвердить ответ",
                Location = new Point(40, 220),
                AutoSize = true
            };
            confirmButton.Click += Question2ConfirmButton_Click;

            tabPage.Controls.Add(questionLabel);
            tabPage.Controls.Add(_q2CSharp);
            tabPage.Controls.Add(_q2Java);
            tabPage.Controls.Add(_q2Html);
            tabPage.Controls.Add(_q2Sql);
            tabPage.Controls.Add(hintLabel);
            tabPage.Controls.Add(confirmButton);

            return tabPage;
        }

        /// <summary>
        ///     Вкладка с вопросом 3 — текстовый ответ (TextBox).
        /// </summary>
        private TabPage CreateQuestion3Tab()
        {
            var tabPage = new TabPage("Вопрос 3");

            var questionLabel = new Label
            {
                AutoSize = true,
                Location = new Point(20, 20),
                Text = "Как называется парадигма программирования, основанная на объектах? (кратко)"
            };

            _q3TextBox = new TextBox
            {
                Location = new Point(40, 60),
                Width = 300
            };

            var hintLabel = new Label
            {
                AutoSize = true,
                Location = new Point(40, 90),
                ForeColor = Color.DimGray,
                Text = "Например: ООП"
            };

            var confirmButton = new Button
            {
                Text = "Подтвердить и показать отчёт",
                Location = new Point(40, 130),
                AutoSize = true
            };
            confirmButton.Click += Question3ConfirmButton_Click;

            tabPage.Controls.Add(questionLabel);
            tabPage.Controls.Add(_q3TextBox);
            tabPage.Controls.Add(hintLabel);
            tabPage.Controls.Add(confirmButton);

            return tabPage;
        }

        private void Question1ConfirmButton_Click(object? sender, EventArgs e)
        {
            string? selectedKey = null;

            if (_q1OptionInt.Checked)
                selectedKey = (string?)_q1OptionInt.Tag;
            else if (_q1OptionString.Checked)
                selectedKey = (string?)_q1OptionString.Tag;
            else if (_q1OptionBool.Checked)
                selectedKey = (string?)_q1OptionBool.Tag;

            _processor.SetQuestion1Answer(selectedKey);

            var message = _processor.Question1Correct switch
            {
                true => "Ответ на первый вопрос верный.",
                false => "Ответ на первый вопрос неверный.",
                null => "Вы не выбрали вариант ответа."
            };

            MessageBox.Show(this, message, "Вопрос 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Question2ConfirmButton_Click(object? sender, EventArgs e)
        {
            _processor.SetQuestion2Answer(
                _q2CSharp.Checked,
                _q2Java.Checked,
                _q2Html.Checked,
                _q2Sql.Checked);

            var message = _processor.Question2Correct switch
            {
                true => "Ответ на второй вопрос верный.",
                false => "Ответ на второй вопрос неверный.",
                null => "Не удалось определить ответ."
            };

            MessageBox.Show(this, message, "Вопрос 2", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Question3ConfirmButton_Click(object? sender, EventArgs e)
        {
            _processor.SetQuestion3Answer(_q3TextBox.Text);

            var message = _processor.Question3Correct switch
            {
                true => "Ответ на третий вопрос верный.\n\n",
                false => "Ответ на третий вопрос неверный.\n\n",
                null => "Вы не ввели ответ на третий вопрос.\n\n"
            };

            message += _processor.GetReport();

            MessageBox.Show(this, message, "Результат теста", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}


