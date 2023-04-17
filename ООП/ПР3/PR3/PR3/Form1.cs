using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR3
{
    public partial class Form1 : Form
    {
        private Switch reactor_switch = new Switch();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string error;
            // DisconnectPowerGenerator
            if (Task1.TryDisconnectPowerGenerator(reactor_switch, out error))
            {
                textBox1.Text = "Disconnect Power Generator: SUCCESS";
            }
            else
            {
                if (error.Length > 0)
                {
                    textBox1.Text = error;
                }
                else
                {
                    textBox1.Text = "Disconnect Power Generator: FAILED";
                }
            }
            // VerifyBackupCoolantSystem
            if (Task1.TryVerifyBackupCoolantSystem(reactor_switch, out error))
            {
                textBox1.Text += "\r\nVerify Backup Coolant System: SUCCESS";
            }
            else
            {
                if (error.Length > 0)
                {
                    textBox1.Text += "\r\n" + error;
                    
                }
                else
                {
                    textBox1.Text += "\r\nVerify Backup Coolant System: FAILED";
                }
            }
            // GetCoreTemperature
            double temperature;
            if (Task1.TryGetCoreTemperature(reactor_switch, out temperature, out error)) {
                textBox1.Text += $"\r\nCore temperature: {temperature}";
            }
            else
            {
                textBox1.Text += $"\r\n" + error.ToString();
            }
            // InsertRodCluster
            if (Task1.TryInsertRodCluster(reactor_switch, out error))
            {
                textBox1.Text += $"\r\nInsert Rod Cluster: SUCCESS";
            }
            else
            {
                if (error.Length > 0)
                {
                    textBox1.Text += "\r\n" + error;

                }
                else
                {
                    textBox1.Text += "\r\nInsert Rod Cluster: FAILED";
                }
            }
            // GetRadiationLevel
            double radiation;
            if (Task1.TryGetRadiationLevel(reactor_switch, out radiation, out error))
            {
                textBox1.Text += $"\r\nRadiation Level: {radiation}";
            }
            else
            {
                if (error.Length > 0)
                {
                    textBox1.Text += "\r\n" + error;
                }
                else
                {
                    textBox1.Text += "\r\nGet Radiation Level: FAILED";
                }
            }
            // SignalShutdownComplete
            if (Task1.TrySignalShutdownComplete(reactor_switch, out error))
            {
                textBox1.Text += "\r\nSignal Shutdown Complete: SUCCESS";
            }
            else
            {
                if (error.Length > 0)
                {
                    textBox1.Text += "\r\n" + error;
                }
                else 
                { 
                    textBox1.Text += "\r\nSignal Shutdown Complete: FAILED";
                }
            }
        }
        /// <summary>
        /// Проверяет, является ли введенный символ цифрой или клавишей управления (например, Backspace),
        /// и разрешает или запрещает его ввод в поле.
        /// </summary>
        /// <param name="e">Аргументы события KeyPress.</param>
        private void OnlyValidNumeric(ref KeyPressEventArgs e)
        {
            char pressed = e.KeyChar;
            // 8 - это BackSlash
            // Проверяем, является ли введенный символ цифрой или Backspace
            if (!Char.IsDigit(pressed) && !((int)pressed == 8))
            {
                // Если символ не является цифрой или Backspace, то запрещаем его ввод
                e.Handled = true;
            }
        }

        /// <summary>
        /// Обработчик события KeyPress для текстовых полей, которые должны содержать только цифры.
        /// </summary>
        /// <param name="sender">Объект, инициировавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void textBox_OnlyValid_Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Вызываем метод OnlyValidNumeric для проверки введенного символа
            OnlyValidNumeric(ref e);
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Пересоздать матрицу 1".
        /// </summary>
        /// <param name="sender">Объект, инициировавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void button5_Click(object sender, EventArgs e)
        {
            // Удаляем предыдущую матрицу с панели, если она была создана
            if (panel1.Controls.Count > 0)
            {
                panel1.Controls.RemoveAt(panel1.Controls.Count - 1);
            }
            // Создаем матрицу
            int columns, rows;
            try
            {
                (columns, rows) = (int.Parse(textBox12.Text), int.Parse(textBox13.Text));
            }
            catch (FormatException)
            {
                MessageBox.Show("Значения столбцов или строк имеют неверный формат!");
                return;
            }
            int[,] matrix1 = new int[rows, columns];
            Array.Clear(matrix1, 0, matrix1.Length);
            try
            {
                // Отображаем матрицу на панели
                Task2.ShowMatrix(panel1, in matrix1);
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Слишком большая матрица");
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Пересоздать матрицу 2".
        /// </summary>
        /// <param name="sender">Объект, инициировавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void button6_Click(object sender, EventArgs e)
        {
            // Удаляем предыдущую матрицу с панели, если она была создана
            if (panel2.Controls.Count > 0)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
            }
            // Создаем матрицу
            int columns, rows;
            try
            {
                (columns, rows) = (int.Parse(textBox15.Text), int.Parse(textBox14.Text));
            }
            catch (FormatException)
            {
                MessageBox.Show("Значения столбцов или строк имеют неверный формат!");
                return;
            }
            int[,] matrix2 = new int[rows, columns];
            Array.Clear(matrix2, 0, matrix2.Length);
            try
            {
                // Отображаем матрицу на панели
                Task2.ShowMatrix(panel2, in matrix2);
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Слишком большая матрица");
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Выполнить умножение".
        /// </summary>
        /// <param name="sender">Объект, инициировавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void button4_Click(object sender, EventArgs e)
        {
            int[,] matrix1, matrix2;
            // Получаем первую матрицу
            // Если не удалось получить матрицу, выводим сообщение об ошибке и выходим из метода
            if (!Task2.try_get_matrix(panel1, out matrix1) || !Task2.try_get_matrix(panel2, out matrix2))
            {
                MessageBox.Show("Сначала нужно создать матрицы");
                return;
            }
            // Получаем размеры матриц
            var (rows1, columns1) = (matrix1.GetLength(0), matrix1.GetLength(1));
            var (rows2, columns2) = (matrix2.GetLength(0), matrix2.GetLength(1));
            try
            {
                // Умножаем матрицы
                int[,] matrix3 = Task2.MultiplyMatrices(matrix1, matrix2);
                // Очищаем панель, если на ней уже есть элементы
                if (panel3.Controls.Count > 0)
                {
                    panel3.Controls.RemoveAt(panel3.Controls.Count - 1);
                }
                // Отображаем результат на панели
                Task2.ShowMatrix(panel3, in matrix3);
            }
            catch (ArgumentException err)
            {
                // Выводим сообщение об ошибке, если возникло исключение
                MessageBox.Show(err.Message);
            }
        }
        /// <summary>
        /// Обработчик нажатия на кнопку "Заполнить матрицы случайными числами".
        /// </summary>
        /// <param name="sender">Объект, инициировавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void button7_Click(object sender, EventArgs e)
        {
            int[,] matrix1, matrix2;
            if (Task2.try_get_matrix(panel1, out matrix1))
            {
                Task2.RandomMatrix(ref matrix1);
                panel1.Controls.RemoveAt(panel1.Controls.Count - 1);
                Task2.ShowMatrix(panel1, matrix1);
            }
            if (Task2.try_get_matrix(panel2, out matrix2))
            {
                Task2.RandomMatrix(ref matrix2);
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                Task2.ShowMatrix(panel2, matrix2);
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Умножить".
        /// </summary>
        /// <param name="sender">Объект, инициировавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            int num1, num2;
            if (!int.TryParse(textBox2.Text, out num1) || !int.TryParse(textBox3.Text, out num2))
            {
                MessageBox.Show("Слишком большие числа");
                return;
            }
            try
            {
                checked
                {
                    int result = num1 * num2;
                    textBox4.Text = result.ToString();
                }
            }
            catch (OverflowException ex)
            {
                MessageBox.Show("Переполнение: " + ex.Message);
            }
        }
    }
}
