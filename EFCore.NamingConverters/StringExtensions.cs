using System.Text;

namespace EFCore.NamingConverters
{
    /// <summary>Методы-расширения для <see cref="string" /></summary>
    public static class StringExtensions
    {
        /// <summary>Преобразование в snake_case</summary>
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var stringBuilder = new StringBuilder();

            for (var i = 0; i < input.Length; i++)
            {
                ProcessSymbol(input, i, stringBuilder);
            }

            return stringBuilder.ToString();
        }

        private static void ProcessSymbol(string input, int symbolNumber, StringBuilder stringBuilder)
        {
            var symbol = input[symbolNumber];

            if (symbolNumber > 0)
            {
                PrependWithUnderscore(symbol, input[symbolNumber - 1], stringBuilder);
            }

            stringBuilder.Append(char.ToLower(symbol));
        }

        private static void PrependWithUnderscore(char symbol, char previousSymbol, StringBuilder stringBuilder)
        {
            if (char.IsUpper(symbol) && char.IsLetterOrDigit(previousSymbol) && !char.IsUpper(previousSymbol))
            {
                stringBuilder.Append("_");
            }
        }
    }
}
