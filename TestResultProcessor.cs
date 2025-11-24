using System;
using System.Globalization;
using System.Text;

namespace WinFormsQuizApp
{
    /// <summary>
    ///     Класс-обработчик ответов пользователя.
    ///     Инкапсулирует логику проверки и формирования отчёта по прохождению теста.
    /// </summary>
    public class TestResultProcessor
    {
        private const int TotalQuestions = 3;

        public bool? Question1Correct { get; private set; }
        public bool? Question2Correct { get; private set; }
        public bool? Question3Correct { get; private set; }

        /// <summary>
        ///     Вопрос 1: одиночный выбор (RadioButton).
        ///     Правильный ответ — значение <paramref name="selectedOptionKey"/> равно "int".
        /// </summary>
        public void SetQuestion1Answer(string? selectedOptionKey)
        {
            if (string.IsNullOrWhiteSpace(selectedOptionKey))
            {
                Question1Correct = null;
                return;
            }

            Question1Correct = string.Equals(selectedOptionKey, "int", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Вопрос 2: множественный выбор (CheckBox).
        ///     Правильный ответ — включены только C# и Java.
        /// </summary>
        public void SetQuestion2Answer(bool csharpChecked, bool javaChecked, bool htmlChecked, bool sqlChecked)
        {
            // Правильная комбинация: C# и Java — да, HTML и SQL — нет.
            Question2Correct = csharpChecked && javaChecked && !htmlChecked && !sqlChecked;
        }

        /// <summary>
        ///     Вопрос 3: текстовый ответ.
        ///     Принимаются варианты: "ооп" или "объектно-ориентированное программирование".
        /// </summary>
        public void SetQuestion3Answer(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Question3Correct = null;
                return;
            }

            var normalized = text.Trim().ToLower(CultureInfo.CurrentCulture);

            Question3Correct = normalized switch
            {
                "ооп" => true,
                "объектно-ориентированное программирование" => true,
                _ => false
            };
        }

        /// <summary>
        ///     Формирует текстовый отчёт о прохождении теста.
        /// </summary>
        public string GetReport()
        {
            int answered = 0;
            int correct = 0;

            if (Question1Correct.HasValue)
            {
                answered++;
                if (Question1Correct.Value) correct++;
            }

            if (Question2Correct.HasValue)
            {
                answered++;
                if (Question2Correct.Value) correct++;
            }

            if (Question3Correct.HasValue)
            {
                answered++;
                if (Question3Correct.Value) correct++;
            }

            var sb = new StringBuilder();
            sb.AppendLine("Отчёт о прохождении теста");
            sb.AppendLine("--------------------------");
            sb.AppendLine($"Всего вопросов: {TotalQuestions}");
            sb.AppendLine($"Отвечено: {answered}");
            sb.AppendLine($"Правильных ответов: {correct}");
            sb.AppendLine();

            sb.AppendLine($"Вопрос 1: {(FormatQuestionStatus(Question1Correct))}");
            sb.AppendLine($"Вопрос 2: {(FormatQuestionStatus(Question2Correct))}");
            sb.AppendLine($"Вопрос 3: {(FormatQuestionStatus(Question3Correct))}");

            return sb.ToString();
        }

        private static string FormatQuestionStatus(bool? value)
        {
            return value switch
            {
                true => "правильно",
                false => "неправильно",
                null => "нет ответа"
            };
        }
    }
}


