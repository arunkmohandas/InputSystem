using System;
using System.Linq;
using System.Text;

namespace ISX.Utilities
{
    internal static class CSharpCodeHelpers
    {
        public static bool IsProperIdentifier(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            if (char.IsDigit(name[0]))
                return false;

            for (var i = 0; i < name.Length; ++i)
            {
                var ch = name[i];
                if (!char.IsLetterOrDigit(ch) && ch != '_')
                    return false;
            }

            return true;
        }

        public static bool IsEmptyOrProperIdentifier(string name)
        {
            if (string.IsNullOrEmpty(name))
                return true;

            return IsProperIdentifier(name);
        }

        public static bool IsEmptyOrProperNamespaceName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return true;

            return name.Split('.').All(IsProperIdentifier);
        }

        public static string MakeIdentifier(string name, string suffix = "")
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (char.IsDigit(name[0]))
                name = "_" + name;

            // See if we have invalid characters in the name.
            var nameHasInvalidCharacters = false;
            for (var i = 0; i < name.Length; ++i)
            {
                var ch = name[i];
                if (!char.IsLetterOrDigit(ch) && ch != '_')
                {
                    nameHasInvalidCharacters = true;
                    break;
                }
            }

            // If so, replace them.
            if (nameHasInvalidCharacters)
            {
                var buffer = new StringBuilder();
                for (var i = 0; i < name.Length; ++i)
                {
                    var ch = name[i];
                    if (char.IsLetterOrDigit(ch) || ch == '_')
                        buffer.Append(ch);
                    else if (ch != ' ') // Spaces we just cut out.
                        buffer.Append('_');
                }

                name = buffer.ToString();
            }

            return name + suffix;
        }

        public static string MakeTypeName(string name, string suffix = "")
        {
            var symbolName = MakeIdentifier(name, suffix);
            if (char.IsLower(symbolName[0]))
                symbolName = char.ToUpper(symbolName[0]) + symbolName.Substring(1);
            return symbolName;
        }
    }
}
