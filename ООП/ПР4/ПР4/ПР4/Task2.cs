using System;

namespace ПР4
{
    /// <summary>
    /// Enumeration of girder material types
    /// </summary>
    public enum Material
    {
        StainlessSteel,
        Aluminium,
        ReinforcedConcrete,
        Composite,
        Titanium
    }
    /// <summary>
    /// Enumeration of girder cross-sections
    /// </summary>
    public enum CrossSection
    {
        IBeam,
        Box,
        ZShaped,
        CShaped
    }
    /// <summary>
    /// Enumeration of test results
    /// </summary>
    public enum TestResult
    {
        Pass,
        Fail
    }
    /// <summary>
    /// Класс, содержащий статические методы для преобразования строковых значений в перечисление Material.
    /// </summary>
    internal class Task2
    {
        /// <summary>
        /// Преобразует строковое значение в перечисление Material.
        /// </summary>
        /// <param name="mat_string">Строковое значение.</param>
        /// <returns>Перечисление Material.</returns>
        public static Material material_from_string_builder(string mat_string)
        {
            switch (mat_string)
            {
                case "StainlessSteel":
                    return Material.StainlessSteel;
                case "Aluminium":
                    return Material.Aluminium;
                case "ReinforcedConcrete":
                    return Material.ReinforcedConcrete;
                case "Composite":
                    return Material.Composite;
                case "Titanium":
                    return Material.Titanium;
            }
            throw new FormatException("Невозможно собрать материал(строка имеет неверный формат");
        }

        /// <summary>
        /// Преобразует строковое значение в перечисление CrossSection.
        /// </summary>
        /// <param name="mat_string">Строковое значение.</param>
        /// <returns>Перечисление CrossSection.</returns>
        public static CrossSection cross_sect_from_string_builder(string mat_string)
        {
            switch (mat_string)
            {
                case "IBeam":
                    return CrossSection.IBeam;
                case "Box":
                    return CrossSection.Box;
                case "ZShaped":
                    return CrossSection.ZShaped;
                case "CShaped":
                    return CrossSection.CShaped;
            }
            throw new FormatException("Невозможно собрать CrossSection(строка имеет неверный формат");
        }

        /// <summary>
        /// Преобразует строковое значение в перечисление TestResult.
        /// </summary>
        /// <param name="mat_string">Строковое значение.</param>
        /// <returns>Перечисление TestResult.</returns>
        public static TestResult test_res_from_string_builder(string mat_string)
        {
            switch (mat_string)
            {
                case "Pass":
                    return TestResult.Pass;
                case "Fail":
                    return TestResult.Fail;
            }
            throw new FormatException("Невозможно собрать CrossSection(строка имеет неверный формат");
        }
    }
}