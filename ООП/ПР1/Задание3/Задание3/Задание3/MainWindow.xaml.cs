using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Задание3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Проверяет, что введенные символы являются допустимыми целыми числами и отклоняет ввод, если это не так.
        /// </summary>
        /// <param name="textbox">Текстовое поле, в котором происходит ввод</param>
        /// <param name="e">Аргументы события TextCompositionEventArgs</param>
        private void only_valid_integers(in TextBox textbox, ref TextCompositionEventArgs e)
        {
            // Проверяем, что аргументы не равны null
            if (e is null)
            {
                return;
            }

            // Получаем нажатый символ
            char pressed = e.Text[0];

            // Проверяем, что нажатый символ является цифрой
            if (!Char.IsDigit(pressed))
            {
                e.Handled = true; // Отклоняем ввод
            }
        }

        /// <summary>
        /// Обработчик события PreviewKeyDown для текстового поля.
        /// Отклоняет ввод пробела.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Аргументы события KeyEventArgs</param>
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Если нажата клавиша пробел, отклоняем ввод
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Конвертирует число из заданной системы счисления в десятичную систему счисления.
        /// </summary>
        /// <param name="text">Число в заданной системе счисления</param>
        /// <param name="from">Основание системы счисления числа</param>
        /// <param name="result">Результат конвертации в виде числа типа long</param>
        /// <returns>True, если конвертация прошла успешно, иначе - false</returns>
        private bool convert_to_dec(string text, uint from, out long result)
        {
            // Инициализируем переменную result нулем
            result = 0;

            // Проверяем, что основание системы счисления меньше или равно 10 и что текст можно преобразовать в целое число
            int _;
            if (from > 10 || !int.TryParse(text, out _))
            {
                return false;
            }

            // Вычисляем позицию старшего разряда
            uint pos = (uint)text.Length - 1;

            // Проходим по всем символам числа и вычисляем его десятичное значение
            foreach (char ch in text)
            {
                result += (int)(uint.Parse($"{ch}") * Math.Pow(from, pos));
                pos -= 1;
            }

            // Возвращаем true, если конвертация прошла успешно
            return true;
        }

        /// <summary>
        /// Конвертирует число из заданной системы счисления в десятичную систему счисления.
        /// </summary>
        /// <param name="text">Число в заданной системе счисления</param>
        /// <param name="from">Основание системы счисления числа</param>
        /// <param name="to">Основание десятичной системы счисления</param>
        /// <param name="result">Результат конвертации в виде строки</param>
        /// <returns>True, если конвертация прошла успешно, иначе - false</returns>
        private bool convert_bases(string text, int from, int to, out string result)
        {
            // Инициализируем переменную result пустой строкой
            result = string.Empty;

            // Конвертируем число в десятичную систему счисления
            long decimalNumber;
            if (!convert_to_dec(text, (uint)from, out decimalNumber))
            {
                return false;
            }

            // Объявляем переменную для остатка от деления
            int remainder = 0;

            // Пока десятичное число больше 0, делим его на основание десятичной системы счисления и записываем остаток в строку result
            while (decimalNumber > 0)
            {
                remainder = (int)(decimalNumber % to);
                decimalNumber /= to;
                result = remainder.ToString() + result;
            }

            // Возвращаем true, если конвертация прошла успешно
            return true;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку button.
        /// Конвертирует число из одной системы счисления в другую и выводит результат в текстовое поле output.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Аргументы события RoutedEventArgs</param>
        private void button_click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что текстовое поле textbox1 не пустое
            if (textbox1.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Введите число");
                return;
            }

            try
            {
                // Получаем основания систем счисления из текстовых полей textBoxFromBase и textBoxIntoBase
                int fromBase = int.Parse(textBoxFromBase.Text.ToString().Trim());
                int toBase = int.Parse(textBoxIntoBase.Text.ToString().Trim());

                // Объявляем переменную для результата конвертации
                string result = string.Empty;

                // Если оба основания меньше или равны 10, то используем свою функцию конвертации
                if (fromBase <= 10 && toBase <= 10)
                {
                    convert_bases(textbox1.Text.ToString(), fromBase, toBase, out result);
                }
                else
                {
                    // Иначе используем стандартную функцию конвертации
                    result = Convert.ToString(Convert.ToInt64(textbox1.Text.ToString(), fromBase), toBase);
                }

                // Выводим результат в текстовое поле output
                output.Text = result;
            }
            catch (System.OverflowException)
            {
                MessageBox.Show("Слишком большое число");
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Неверный формат записи числа или конвертация невозможна");
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Неверное основание");
            }
        }

        /// <summary>
        /// Обработчик события получения фокуса текстового поля textboxFromBase.
        /// Очищает текстовое поле textbox1.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Аргументы события RoutedEventArgs</param>
        private void textboxFromBaseGotFocus(object sender, RoutedEventArgs e)
        {
            // Очищаем текстовое поле textbox1
            textbox1.Text = "";
        }

        /// <summary>
        /// Обработчик события изменения текста в текстовом поле textbox1.
        /// Проверяет, что введенные символы соответствуют заданному основанию системы счисления.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Аргументы события TextCompositionEventArgs</param>
        private void textbox1_text_changed(object sender, TextCompositionEventArgs e)
        {
            // Проверяем, что аргумент e не равен null
            if (e is null)
            {
                return;
            }

            // Получаем нажатую клавишу в нижнем регистре
            char pressed = Char.ToLower(e.Text[0]);

            // Получаем основание системы счисления из текстового поля textBoxFromBase
            int num_base;
            if (!int.TryParse(textBoxFromBase.Text, out num_base))
            {
                // Если основание не удалось получить, выводим сообщение об ошибке и выходим из метода
                MessageBox.Show("Неверное основание");
                return;
            }

            // Если основание меньше или равно 10 и нажата клавиша a-f, то игнорируем ее
            if (num_base <= 10 && (97 <= pressed && pressed <= 102))
            {
                e.Handled = true;
            }

            // Если нажата не цифра и не буква a-f, то игнорируем ее
            if (!(48 <= pressed && pressed <= 57) && !(97 <= pressed && pressed <= 102))
            {
                e.Handled = true;
            }

            // Если нажата цифра и ее значение больше, чем основание системы счисления - 1, то игнорируем ее
            if (Char.IsDigit(pressed) && (pressed - '0' > num_base - 1))
            {
                e.Handled = true;
            }

            // Если основание больше 10 и нажата буква, то проверяем, что ее значение меньше, чем основание - 11
            if (num_base > 10)
            {
                if (pressed - 'a' > (num_base - 11))
                {
                    e.Handled = true;
                }
            }
        }
    }
}