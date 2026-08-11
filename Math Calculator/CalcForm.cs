using Own_Language_Course.Parsing;
using ToolTip = System.Windows.Forms.ToolTip;

namespace Math_Calculator
{
    public partial class CalcForm : Form
    {
        private readonly Stack<(string, string)> stack1 = [];
        private readonly Stack<(string, string)> stack2 = [];
        private readonly List<(string, string)> history = [];

        public static HistoryForm historyForm;
        public static bool IsFirstStack = true;

        private bool HistoryOpen
            => historyForm != null && !historyForm.IsDisposed;

        public CalcForm()
        {
            stack1.Push(("", ""));
            InitializeComponent();

            var tooltip1 = new ToolTip();
            var tooltip2 = new ToolTip();
            var tooltip3 = new ToolTip();
            var tooltip4 = new ToolTip();
            var tooltip5 = new ToolTip();
            var tooltip6 = new ToolTip();

            prevCalcButton.Enabled = false;
            nextCalcButton.Enabled = false;

            tooltip1.SetToolTip(helpButton, "Информация о математических функциях,\nдоступных в калькуляторе");
            tooltip2.SetToolTip(nextCalcButton, "Повторить вычисление");
            tooltip3.SetToolTip(prevCalcButton, "Отменить вычисление");
            tooltip4.SetToolTip(clearHistoryButton, "Очистить историю вычислений");
            tooltip5.SetToolTip(clearInputButton, "Очистить ввод");
            tooltip6.SetToolTip(historyButton, "История вычислений");
        }
        //ПОЛУЧЕНИЕ РЕЗУЛЬТАТА ВЫЧИСЛЕНИЙ
        private void equalButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inputTextBox.Text))
                    throw new Exception("Введите арифметическое выражение.");

                string input =
                    $"result = {inputTextBox.Text}";
                var tokens = new Lexer(input).Tokenize();

                var parser = new Parser(tokens);
                var program = parser.Parse();
                program.Execute();
                var result = parser.GetValue("result").ToString();
                outputTextBox.Text = result;

                stack1.Push((inputTextBox.Text, result));

                history.Add((inputTextBox.Text, result));

                if (HistoryOpen)
                {
                    historyForm.AddRow(inputTextBox.Text, result);
                    historyForm.UpdateView();
                }

                ChangeButtonsState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }
        //ЗАКРЫТИЕ ОКНА ФОРМЫ
        private void closeButton_Click(object sender, EventArgs e) => Close();
        //ОТКРЫТИЕ ОКНА ПОМОЩИ
        private void helpButton_Click(object sender, EventArgs e)
        {
            var form = new HelpForm();
            form.Show();
        }
        //ОЧИСТКА ВВОДА
        private void clearInputButton_Click(object sender, EventArgs e)
        {
            inputTextBox.Text = string.Empty;
            outputTextBox.Text = string.Empty;
        }
        //ОТМЕНА / ВОЗВРАТ ДЕЙСТВИЙ (СВЯЗАННЫХ С ВВОДОМ ИНФОРМАЦИИ)
        private void nextCalcButton_Click(object sender, EventArgs e)
        {
            stack1.Push(stack2.Pop());
            inputTextBox.Text = stack1.Peek().Item1;
            outputTextBox.Text = stack1.Peek().Item2;

            ChangeButtonsState();
        }
        private void prevCalcButton_Click(object sender, EventArgs e)
        {
            stack2.Push(stack1.Pop());
            inputTextBox.Text = stack1.Peek().Item1;
            outputTextBox.Text = stack1.Peek().Item2;

            ChangeButtonsState();

        }
        //ВСПОМОГАТЕЛЬНЫЙ МЕТОД ДЛЯ ИЗМЕНЕНИЯ СОСТОЯНИЯ КНОПОК ОТМЕНЫ/ВОЗВРАТА
        private void ChangeButtonsState()
        {
            prevCalcButton.Enabled = stack1.Count > 1;
            nextCalcButton.Enabled = stack2.Count > 0;
        }
        //ОЧИСТКА ИСТОРИИ ВЫЧИСЛЕНИЙ
        private void clearHistoryButton_Click(object sender, EventArgs e)
        {
            if (stack1.Count == 1 && stack2.Count == 0)
            {
                MessageBox.Show("История калькулятора уже очищена.");
                return;
            }
            DialogResult result = MessageBox.Show
                ("Вы уверены, что хотите очистить историю калькулятора?", "",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                stack1.Clear();
                stack2.Clear();
                history.Clear();
                stack1.Push(("", ""));

                ChangeButtonsState();

                inputTextBox.Clear();
                outputTextBox.Clear();

                historyForm?.UpdateView();
            }
        }
        //ОТКРЫТИЕ ОКНА ИСТОРИИ ВЫЧИСЛЕНИЙ
        private void historyButton_Click(object sender, EventArgs e)
        {
            if (!HistoryOpen)
            {
                var form = new HistoryForm(history);
                form.ShowAsync();
            }
        }
    }
}
