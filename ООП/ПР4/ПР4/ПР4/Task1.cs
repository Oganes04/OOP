using System.Text.RegularExpressions;
using System;
using System.ComponentModel;

namespace ПР4
{
    /// <summary>
    /// Структура, представляющая комплексное число.
    /// </summary>
    struct Complex
    {
        public double real;
        public double imaginary;

        public Complex(double real, double imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        /// <summary>
        /// Вычисляет модуль комплексного числа.
        /// </summary>
        /// <returns>Модуль комплексного числа.</returns>
        public double Modulus()
        {
            return Math.Sqrt(real * real + imaginary * imaginary);
        }

        /// <summary>
        /// Перегруженный оператор сложения комплексных чисел.
        /// </summary>
        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.real + b.real, a.imaginary + b.imaginary);
        }

        /// <summary>
        /// Перегруженный оператор вычитания комплексных чисел.
        /// </summary>
        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.real - b.real, a.imaginary - b.imaginary);
        }

        /// <summary>
        /// Перегруженный оператор умножения комплексных чисел.
        /// </summary>
        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a.real * b.real - a.imaginary * b.imaginary, a.real * b.imaginary + a.imaginary * b.real);
        }

        /// <summary>
        /// Перегруженный оператор деления комплексных чисел.
        /// </summary>
        public static Complex operator /(Complex c1, Complex c2)
        {
            double denominator = c2.real * c2.real + c2.imaginary * c2.imaginary;
            double real = (c1.real * c2.real + c1.imaginary * c2.imaginary) / denominator;
            double imaginary = (c1.imaginary * c2.real - c1.real * c2.imaginary) / denominator;
            return new Complex(real, imaginary);
        }

        /// <summary>
        /// Перегруженный оператор равенства комплексных чисел.
        /// </summary>
        public static bool operator ==(Complex a, Complex b)
        {
            return a.real == b.real && a.imaginary == b.imaginary;
        }

        /// <summary>
        /// Перегруженный оператор неравенства комплексных чисел.
        /// </summary>
        public static bool operator !=(Complex a, Complex b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Преобразует комплексное число в строку.
        /// </summary>
        public override string ToString()
        {
            return $"{real} + {imaginary}i";
        }
    }
}