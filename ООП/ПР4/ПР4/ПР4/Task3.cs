using System;

namespace ПР4
{
    /// <summary>
    /// Структура, представляющая результат теста.
    /// </summary>
    struct TestCaseResult
    {
        /// <summary>
        /// Результат теста.
        /// </summary>
        public TestResult result;

        /// <summary>
        /// Причина отказа (если тест не пройден).
        /// </summary>
        public string reason_for_failure;

        /// <summary>
        /// Конструктор для создания объекта TestCaseResult.
        /// </summary>
        /// <param name="result">Результат теста.</param>
        /// <param name="reason_for_failure">Причина отказа (если тест не пройден).</param>
        public TestCaseResult(TestResult result, string reason_for_failure)
        {
            this.result = result;
            this.reason_for_failure = reason_for_failure;
        }

        /// <summary>
        /// Возвращает строковое представление объекта TestCaseResult.
        /// </summary>
        /// <returns>Строковое представление объекта TestCaseResult.</returns>
        public override string ToString()
        {
            return $"{result.ToString()}. reason_for_failure: {reason_for_failure}";
        }
    }
    /// <summary>
    /// Класс, содержащий статические методы для проведения тестов.
    /// </summary>
    internal class TestManager
    {
        /// <summary>
        /// Генерирует результат теста на основе материала, сечения и давления.
        /// </summary>
        /// <param name="material">Материал детали.</param>
        /// <param name="cross_section">Сечение детали.</param>
        /// <param name="pressure">Давление для теста.</param>
        /// <returns>Результат теста.</returns>
        public static TestCaseResult GenerateResult(Material material, CrossSection cross_section, in int pressure = 50)
        {
            int durability = 0;
            switch (material)
            {
                case Material.StainlessSteel:
                    durability += 10;
                    break;
                case Material.ReinforcedConcrete:
                    durability += 20;
                    break;
                case Material.Titanium:
                    durability += 10;
                    break;
                case Material.Aluminium:
                    durability += 10;
                    break;
                case Material.Composite:
                    durability += 30;
                    break;
            }
            switch (cross_section)
            {
                case CrossSection.IBeam:
                    durability += 10;
                    break;
                case CrossSection.ZShaped:
                    durability += 20;
                    break;
                case CrossSection.Box:
                    durability += 30;
                    break;
                case CrossSection.CShaped:
                    durability += 20;
                    break;
            }
            if (durability > pressure)
            {
                return new TestCaseResult(TestResult.Pass, "");
            }
            else
            {
                string fail_reason = "";
                Random random = new Random();
                switch (random.Next(3))
                {
                    case 1:
                        fail_reason = "Сломалась промежуточная часть";
                        break;
                    case 2:
                        fail_reason = "Сломалась деталь полностью";
                        break;
                    case 3:
                        fail_reason = "От детали откололся кусок";
                        break;
                }
                return new TestCaseResult(TestResult.Fail, fail_reason);
            }
        }
    }
    /// <summary>
    /// Класс, представляющий фабрику для производства материалов и сечений деталей.
    /// </summary>
    internal class Fabric
    {
        private Random random = new Random();

        /// <summary>
        /// Возвращает случайный материал для детали.
        /// </summary>
        /// <returns>Случайный материал для детали.</returns>
        public Material Produce_material()
        {
            var v = Enum.GetValues(typeof(Material));
            return (Material)v.GetValue(random.Next(v.Length));
        }

        /// <summary>
        /// Возвращает случайное сечение для детали.
        /// </summary>
        /// <returns>Случайное сечение для детали.</returns>
        public CrossSection Produce_cross_section()
        {
            var v = Enum.GetValues(typeof(CrossSection));
            return (CrossSection)v.GetValue(random.Next(v.Length));
        }
    }

    /// <summary>
    /// Класс, содержащий метод для проведения серии тестов деталей.
    /// </summary>
    internal class Task3
    {
        /// <summary>
        /// Проводит серию тестов деталей и возвращает массив строковых сообщений о результатах тестов.
        /// </summary>
        /// <param name="number_of_tests">Количество тестов.</param>
        /// <param name="passed">Количество пройденных тестов.</param>
        /// <param name="pressure">Давление для тестов.</param>
        /// <returns>Массив строковых сообщений о результатах тестов.</returns>
        public static string[] DoTests(int number_of_tests, out int passed, in int pressure = 50)
        {
            string[] messages = new string[number_of_tests];
            passed = 0;
            Fabric fabric = new Fabric();
            for (int i = 0; i < number_of_tests; i++)
            {
                Material material = fabric.Produce_material();
                CrossSection cross_section = fabric.Produce_cross_section();
                TestCaseResult test_result = TestManager.GenerateResult(material, cross_section, in pressure);
                if (test_result.result == TestResult.Pass)
                {
                    passed += 1;
                }
                messages[i] = $"detail: {material} {cross_section} result: {test_result}";
            }
            return messages;
        }
    }
}