﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ПР1_проект
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
   
        // Пункт 1. Неявное преобразование.
        // Неявное преобразование простых и ссылочных типов,
        public void ImplicitCasting()
        {
            int Value = 19;
            long result = Value;
            /*Из типа  | В тип
            |Sbyte     |short, int, long, float, double, decimal
            |Byte      |short, ushort, int, uint, long, ulong, float, double, decimal
            |Short     |int, long, float, double, decimal
            |Ushort    |int, uint, long, ulong, float, double, decimal
            |Int       |long, float, double, decimal
            |Uint      |long, ulong, float, double, decimal
            |Long      |float, double, decimal
            |Ulong     |float, double, decimal
            |Float     |double
            |Char      |ushort, int, uint, long, ulong, float, double, decimal*/

            Employee empl = new Employee();
            Person person = empl;  // Неявное преобразование ссылочного типа
            MessageBox.Show(result.ToString()); 
        }

        // Пункт 2. Явное преобразование.
  
        // Явное преобразование простых и ссылочных типов, 
  
        public void ExplicitCasting()
        {
            /*Таблица явных преобразований
            |Исходный тип |Целевой тип
            |sbyte        |byte, ushort, uint, ulong или char
            |byte         |sbyte или char
            |short        |sbyte, byte, ushort, uint, ulong или char
            |ushort       |sbyte, byte, short или char
            |int          |sbyte, byte, short, ushort, uint, ulong или char
            |uint         |sbyte, byte, short, ushort, int или char
            */
            long num1 = 1231232133333333;
            int num2 = (int)(num1 + 1000);
            Vec2 vector = new Vec2(1.0F, 1.0F);
            var raw_vector = (int[])vector;
            MessageBox.Show(raw_vector[0].ToString() + ", " + raw_vector[1].ToString());
        }
  
        // Пункт 3. Вызвать и обработать исключение преобразования типов.

        public void CatchCastingException()
        {
            bool flag = true;
            try
            {
                IConvertible conv = flag;
                Char ch = conv.ToChar(null);
            }
            catch (InvalidCastException e)
            {
                MessageBox.Show($"Поймано исключение пребразования типов {e}");
            }
        }
   
        // Пункт 4. Безопасное приведение ссылочных типов с помощью операторов as и is.

        public void SafeCasting()
        {
            Employee employee = new Employee();
            if (employee is Person)
            {
                Person person = employee as Person;
                MessageBox.Show("Успешно преобразован Person в Employee");
            }
            else
            {
                MessageBox.Show("нельзя");
            }

        }

        // Пункт 5. Пользовательское преобразование типов Implicit, Explicit;

        public void UserImplicitExplicit()
        {
            Vec2 vec = new Vec2(1.0F, 1.0F);
            float[] float_vector = vec;
            MessageBox.Show("Успешно выполнены явное и неявное пользовательские преобразования");
        }
  
        /// Пункт 6. 6)	Преобразование с помощью класса Convert и преобразование 
        /// строки в число с помощью методов Parse,
        /// TryParse класса System.Int32.
 
        public void ConvertTask()
        {
            double dNumber = 27.17;
            // Returns True
            bool bNumber = System.Convert.ToBoolean(dNumber);
            if (bNumber) {
                MessageBox.Show("Успешно конвертирован double во float при помощи Convert");
            }
            string string_number = "100";
            int number = System.Int32.Parse(string_number);
            if (number == 100)
            {
                MessageBox.Show("Успешно конвертирована строка 100 в число 100");
            }
            Random random = new Random();
            string possibly_number = (random.Next(0, 2) == 1) ? "сто": "100";
            bool success = int.TryParse(possibly_number, out number);
            if (success)
            {
                MessageBox.Show($"Конвертировано '{possibly_number}' в {number}.");
            }
            else
            {
                MessageBox.Show($"Не вышло преобразовать {possibly_number} в int");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ImplicitCasting();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExplicitCasting();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CatchCastingException();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SafeCasting();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UserImplicitExplicit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ConvertTask();
        }
        // Пункт 7. Написать программу, позволяющую ввод в текстовое поле TextBox
        // только символов, задающих правильный формат вещественного числа со знаком.

        // <param name="sender"></param>
        // <param name="e"></param>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char pressed = e.KeyChar;
            // 8 - это BackSpace
            //MessageBox.Show($"{textBox1.Text.Contains('1')}");
            if (!Char.IsDigit(pressed) && !((int)pressed == 8))
            {
                if (!(pressed == '.'))
                {
                    e.Handled = true;
                }
                if (textBox1.Text.Contains('.')) e.Handled = true;
            }
        }

        /// Возвращает динамический тип с каким-то значением типа type
  
        /// <param name="type"></param>
        /// <returns></returns>
        private dynamic SetType(string type)
        {
            //char, string, byte, int, float, double, decimal, bool, object
            switch (type)
            {
                case "char": return 'A';
                case "string": return "Какая-то строка";
                case "byte": return (byte)101;
                case "int": return 911;
                case "float": return 3.14F;
                case "double": return 3.141592653589793D;
                case "decimal": return 3.141592653589793M;
                case "bool": return true;
                case "object": return new object();
                default: return null;
            }
        }
        /// <summary>
        /// Проверяет явное пребразование типа в тип.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private bool TestExplicitIntoType(dynamic value1, string type) {
            try
            {
                switch (type)
                {
                    case "char":
                        var var1 = (char)value1;
                        break;
                    case "string":
                        var var2 = (string)value1;
                        break;
                    case "byte":
                        var var3 = (byte)value1;
                        break;
                    case "int":
                        var var4 = (int)value1;
                        break;
                    case "float":
                        var var5 = (float)value1;
                        break;
                    case "double":
                        var var6 = (double)value1;
                        break;
                    case "decimal":
                        var var7 = (decimal)value1;
                        break;
                    case "bool":
                        var var8 = (bool)value1;
                        break;
                    case "object":
                        var var9 = (object)value1;
                        break;
                    default: throw new ArgumentException();
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException) {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Проверяет неявное преобразование типа в тип.
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private bool TestImplicitIntoType(dynamic value1, string type)
        {
            try
            {
                switch (type)
                {
                    case "char":
                        char var1 = value1;
                        break;
                    case "string":
                        string var2 = value1;
                        break;
                    case "byte":
                        byte var3 = value1;
                        break;
                    case "int":
                        int var4 = value1;
                        break;
                    case "float":
                        float var5 = value1;
                        break;
                    case "double":
                        double var6 = value1;
                        break;
                    case "bool":
                        bool var7 = value1;
                        break;
                    case "object":
                        object var8 = value1;
                        break;
                    default: throw new ArgumentException();
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Кнопка, при нажатии которой происходит обработка преобразования из одного типа в другой.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is null || comboBox2.SelectedItem is null)
            {
                MessageBox.Show("Выберите типы для преобразования");
                return;
            }
            if (comboBox3.SelectedItem is null)
            {
                MessageBox.Show("Выберите явное или неявное преобразование");
                return;
            }
            dynamic value1 = SetType(comboBox1.SelectedItem.ToString());
            string type = comboBox2.SelectedItem.ToString();
            bool convertion_succeed;
            string conversion_way;
            if (comboBox3.SelectedItem.ToString() == "Явное преобразование")
            {
                conversion_way = "явно";
                convertion_succeed = TestExplicitIntoType(value1, type);
            }
            else
            {
                conversion_way = "неявно";
                convertion_succeed = TestImplicitIntoType(value1, type);
            }
            if (convertion_succeed) {
                var message = $"Успешно {conversion_way} конвертировано {value1.GetType()} в {type}";
                MessageBox.Show(message);
            }
            else
            {
                var message = $"Не получилось {conversion_way} конвертировать {value1.GetType()} в {type}";
                MessageBox.Show(message);
            }
        }
    }
}
