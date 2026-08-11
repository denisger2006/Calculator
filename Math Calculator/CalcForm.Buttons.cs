namespace Math_Calculator
{
    public partial class CalcForm
    {
        //КНОПКИ ЦИФР
        private void zeroButton_Click(object sender, EventArgs e) => AddInput(zeroButton);
        private void oneButton_Click(object sender, EventArgs e) => AddInput(oneButton);
        private void twoButton_Click(object sender, EventArgs e) => AddInput(twoButton);
        private void threeButton_Click(object sender, EventArgs e) => AddInput(threeButton);
        private void fourButton_Click(object sender, EventArgs e) => AddInput(fourButton);
        private void fiveButton_Click(object sender, EventArgs e) => AddInput(fiveButton);
        private void sixButton_Click(object sender, EventArgs e) => AddInput(sixButton);
        private void sevenButton_Click(object sender, EventArgs e) => AddInput(sevenButton);
        private void eightButton_Click(object sender, EventArgs e) => AddInput(eightButton);
        private void nineButton_Click(object sender, EventArgs e) => AddInput(nineButton);
        private void commaButton_Click(object sender, EventArgs e) => AddInput(commaButton);
        //КНОПКИ АРИФМЕТИЧЕСКИХ ДЕЙСТВИЙ И СКОБОК
        private void divideButton_Click(object sender, EventArgs e) => AddInput(divideButton);
        private void multiplyButton_Click(object sender, EventArgs e) => AddInput(multiplyButton);
        private void minusButton_Click(object sender, EventArgs e) => AddInput("-");
        private void plusButton_Click(object sender, EventArgs e) => AddInput(plusButton);
        private void leftParenButton_Click(object sender, EventArgs e) => AddInput(leftParenButton);
        private void rightParenButton_Click(object sender, EventArgs e) => AddInput(rightParenButton);
        //КОНСТАНТЫ
        private void piButton_Click(object sender, EventArgs e) => AddInput("PI");
        private void expButton_Click(object sender, EventArgs e) => AddInput("E");
        private void phiButton_Click(object sender, EventArgs e) => AddInput("PHI");
        //ФУНКЦИИ
        //1 аргумент
        private void sinButton_Click(object sender, EventArgs e) => AddFunc("sin()");
        private void cosButton_Click(object sender, EventArgs e) => AddFunc("cos()");
        private void tgButton_Click(object sender, EventArgs e) => AddFunc("tg()");
        private void ctgButton_Click(object sender, EventArgs e) => AddFunc("ctg()");
        private void sqrtButton_Click(object sender, EventArgs e) => AddFunc("sqrt()");
        private void absButton_Click(object sender, EventArgs e) => AddFunc("abs()");
        private void cubeRootButton_Click(object sender, EventArgs e) => AddFunc("cbrt()");
        private void exp10Button_Click(object sender, EventArgs e) => AddFunc("exp10()");
        private void exp2Button_Click(object sender, EventArgs e) => AddFunc("exp2()");
        private void sqrButton_Click(object sender, EventArgs e) => AddFunc("sqr()");
        private void cubeButton_Click(object sender, EventArgs e) => AddFunc("cube()");
        private void factorialButton_Click(object sender, EventArgs e) => AddFunc("fact()");
        private void lgButton_Click(object sender, EventArgs e) => AddFunc("lg()");
        private void lnButton_Click(object sender, EventArgs e) => AddFunc("ln()");
        //2 аргумента
        private void logButton_Click(object sender, EventArgs e) => AddFunc("log(,)", 2);
        private void powXYButton_Click(object sender, EventArgs e) => AddFunc("pow(,)", 2);
        private void rootButton_Click(object sender, EventArgs e) => AddFunc("root(,)", 2);
        //ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ 
        private void AddInput(Button button)
        {
            inputTextBox.Focus();
            inputTextBox.SelectedText = button.Text;
        }
        private void AddInput(string text)
        {
            inputTextBox.Focus();
            inputTextBox.SelectedText = text;
        }
        private void AddFunc(string text, int pos = 1)
        {
            AddInput(text);
            inputTextBox.SelectionStart -= pos;
        }
    }
}
