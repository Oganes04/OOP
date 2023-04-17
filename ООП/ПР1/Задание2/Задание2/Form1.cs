using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Задание2
{
    public partial class Form1 : Form
    {
        // Начальное приближение для метода Ньютона
        decimal guess;

        // Точность вычисления
        decimal delta = Convert.ToDecimal(Math.Pow(10, -28));

        // Точное значение корня, если оно известно
        decimal? exact = null;
        public Form1()
        {
            InitializeComponent();
        }

   
        // Проверяет корректность вводимых символов в текстовом поле, разрешая ввод только десятичных чисел.

        // <param name="textbox">Текстовое поле, в котором происходит ввод.</param>
        // <param name="e">Аргументы события.</param>
        private void OnlyValidDecimal(in TextBox textbox, ref KeyPressEventArgs e)
        {
            char pressed = e.KeyChar;
            // 8 - это BackSpace
            //MessageBox.Show($"{textBox1.Text.Contains('1')}");
            if (!Char.IsDigit(pressed) && !((int)pressed == 8))
            {
                if ((pressed != '.') && (pressed != ','))
                {
                    e.Handled = true;
                }
                if (textbox.Text.Contains('.') || textbox.Text.Contains(',')) e.Handled = true;
            }
        }
        /// <summary>
        /// Обработчик события нажатия клавиши в текстовом поле "textBox1". Проверяет корректность вводимых символов и очищает результаты вычислений.
        /// </summary>
        /// <param name="sender">Объект, инициировавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyValidDecimal(in textBox1,ref e);
            clear();
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "button1". Выполняет вычисление квадратного корня стандартным методом и выводит результат на форму.
        /// </summary>
        /// <param name="sender">Объект, инициировавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            string entered_value = textBox1.Text.ToString();
            double parsed_value;
            string error_message;
            if (!parse_double_value(in entered_value, out parsed_value, out error_message))
            {
                MessageBox.Show(error_message);
                return;
            }
            label1.Text = $"{Math.Sqrt(parsed_value)}";
        }

        /// <summary>
        /// Парсит строковое представление дробного числа в число с плавающей точкой.
        /// </summary>
        /// <param name="to_parse">Строковое представление дробного числа.</param>
        /// <param name="parsed">Число с плавающей точкой, полученное в результате парсинга.</param>
        /// <param name="error_message">Сообщение об ошибке, если парсинг не удался.</param>
        /// <returns>True, если парсинг прошел успешно, иначе - false.</returns>
        private bool parse_double_value(in string to_parse, out double parsed, out string error_message)
        {
            error_message = "";
            if (!double.TryParse("0" + to_parse.Replace('.', ','), out parsed))
            {
                error_message = "Пожалуйста, введите дробное число";
                return false;
            }
            if (parsed <= 0.0)
            {
                error_message = "Пожалуйста, введите положительное число, не равное нулю";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Парсит строковое представление десятичного числа в десятичное число.
        /// </summary>
        /// <param name="to_parse">Строковое представление десятичного числа.</param>
        /// <param name="parsed">Десятичное число, полученное в результате парсинга.</param>
        /// <param name="error_message">Сообщение об ошибке, если парсинг не удался.</param>
        /// <returns>True, если парсинг прошел успешно, иначе - false.</returns>
        private bool parse_decimal_value(in string to_parse, out decimal parsed, out string error_message)
        {
            double parsed_double;
            parsed = 0;
            if (!parse_double_value(in to_parse, out parsed_double, out error_message)) {
                return false;
            }
            try
            {
                parsed = (decimal)parsed_double;
            }
            catch (System.OverflowException)
            {
                error_message = "Слишком большое число";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Вычисляет квадратный корень числа методом Ньютона.
        /// </summary>
        /// <param name="number">Число, из которого вычисляется квадратный корень.</param>
        /// <param name="initial">Начальное приближение к корню.</param>
        /// <param name="iterations">Количество итераций, выполненных методом.</param>
        /// <param name="guess">Предыдущее приближение к корню.</param>
        /// <param name="delta">Точность вычисления.</param>
        /// <returns>Вычисленный квадратный корень.</returns>
        private decimal newton_sqrt(decimal number, decimal initial, out int iterarions, ref decimal guess, decimal delta)
        {
            decimal result = initial;
            iterarions = 0;
            while (Math.Abs(result - guess) > delta)
            {
                do_newton_iter(in number, ref result, ref guess);
                iterarions += 1;
            }
            return result;
        }
        /// <summary>
        /// Вычисляет начальное приближение для метода Ньютона.
        /// </summary>
        /// <param name="initial">Строковое представление числа, для которого вычисляется квадратный корень.</param>
        /// <param name="appr">Начальное приближение.</param>
        void calc_initial_appr(in string initial, out decimal appr)
        {
            decimal initial_decimal = Convert.ToDecimal(initial.Replace('.', ','));
            if (initial_decimal < 1)
            {
                appr = initial_decimal / 2;
            }
            double r = Math.Round(initial.Length * 1.6);  // Число бит в двоичной записи числа / 2
            appr = (decimal)Math.Pow(2, r);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "button2". Выполняет вычисление квадратного корня методом Ньютона и выводит результат на форму.
        /// </summary>
        /// <param name="sender">Объект, инициировавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            clear();
            string entered_value = textBox1.Text.ToString();
            decimal number_decimal;
            string error_message;
            if (!parse_decimal_value(in entered_value, out number_decimal, out error_message))
            {
                MessageBox.Show(error_message);
                return;
            }
            int iterations;
            decimal initial;
            calc_initial_appr(entered_value, out initial);
            decimal result = newton_sqrt(number_decimal, initial, out iterations, ref this.guess, this.delta);
            decimal change = this.guess - result;
            label6.Text = $"{iterations}";
            label9.Text = $"{Math.Abs(change)}";
            label2.Text = $"{result}";
        }

        /// <summary>
        /// Выполняет одну итерацию метода Ньютона для вычисления квадратного корня.
        /// </summary>
        /// <param name="number">Число, из которого вычисляется квадратный корень.</param>
        /// <param name="result">Текущее приближение к корню.</param>
        /// <param name="guess">Предыдущее приближение к корню.</param>
        private void do_newton_iter(in decimal number, ref decimal result, ref decimal guess)
        {
            guess = result;
            result = ((number / guess) + guess) / 2;
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку "button3". Выполняет вычисление квадратного корня методом Ньютона.
        /// </summary>
        /// <param name="sender">Объект, инициировавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void button3_Click(object sender, EventArgs e)
        {
            string entered_value = textBox1.Text.ToString();
            decimal number_decimal;
            string error_message;
            decimal result;
            int _;
            if (!parse_decimal_value(in entered_value, out number_decimal, out error_message))
            {
                MessageBox.Show(error_message);
                return;
            }
            if (this.exact == null)
            {
                decimal initial;
                calc_initial_appr(entered_value, out initial);
                this.exact = newton_sqrt(number_decimal, initial, out _, ref this.guess, this.delta);
            }
            entered_value = label2.Text.ToString();
            if (!parse_decimal_value(entered_value, out result, out error_message))
            {
                calc_initial_appr(entered_value, out result);
                label11.Text = $"{result}";
            }
            do_newton_iter(in number_decimal, ref result, ref this.guess);
            decimal change = Math.Abs(this.guess - result);
            decimal error = Math.Abs(result - (decimal)this.exact);
            label6.Text = $"{int.Parse(label6.Text.ToString()) + 1}";
            label9.Text = $"{change}";
            label10.Text = $"{error}";
            label2.Text = $"{result}";
        }
        /// Метод для очистки значений в различных элементах управления формы. ///
        private void clear()
        {
            this.exact = null;
            label2.Text = "0.00";
            label6.Text = "0";
            label9.Text = "0";
            label10.Text = "0";
            label11.Text = "0";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
