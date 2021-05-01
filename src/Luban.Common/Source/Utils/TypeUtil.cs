using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Luban.Common.Utils
{
    public static class TypeUtil
    {

        public static (string, string) SplitFullName(string fullName)
        {
            int index = fullName.LastIndexOf('.');
            return (fullName.Substring(0, index), fullName.Substring(index + 1));
        }

        public static string MakeFullName(Stack<object> path)
        {
            var reverse = new List<string>();
            int index = 0;
            foreach (var e in path.Reverse())
            {
                if (!(e is string))
                {
                    reverse.Add("[" + e + "]");
                }
                else
                {
                    // 索引 0 是 table名， 不应该加 .
                    if (index >= 1)
                    {
                        reverse.Add(".");
                    }
                    reverse.Add(e.ToString());
                }
                ++index;
            }
            return string.Join("", reverse);
        }

        public static string MakeCppNamespaceBegin(string module)
        {
            return string.Join("", module.Split('.').Select(n => $"namespace {n} {{"));
        }

        public static string MakeCppNamespaceEnd(string module)
        {
            return string.Join("", module.Split('.').Select(n => $"}}"));
        }

        public static string MakeCppFullName(string module, string name)
        {
            return string.Join("::", MakeFullName(module, name).Split('.'));
        }

        public static string MakeCppJoinedFullName(string module, string ueName)
        {
            return string.Join("", module.Split('.').Select(n => $"{n}_")) + ueName;
        }

        public static string MakeTypescriptNamespaceBegin(string module)
        {
            return string.Join("", module.Split('.').Select(n => $"export namespace {n} {{"));
        }

        public static string MakeFullName(string module, string name)
        {
            if (string.IsNullOrEmpty(module))
            {
                return name;
            }
            if (string.IsNullOrEmpty(name))
            {
                return module;
            }
            return module + "." + name;
        }

        public static string MakeGoPkgName(string module)
        {
            int index = module.LastIndexOf('.');
            return index >= 0 ? module.Substring(index + 1) : module;
        }

        public static string MakeGoNamespace(string module)
        {
            return string.Join("", module.Split('.').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => UpperCaseFirstChar(s)));
        }

        public static string MakeGoFullName(string module, string name)
        {
            return MakeGoNamespace(module) + "_" + name;
        }

        public static string MakePyFullName(string module, string name)
        {
            return module.Replace('.', '_') + "_" + name;
        }

        public static string MakeNamespace(string module, string subModule)
        {
            if (module.Length == 0)
            {
                return subModule;
            }
            if (subModule.Length == 0)
            {
                return module;
            }
            return module + "." + subModule;
        }

        /// <summary>
        ///  the return value in range [offset, 2^14) 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static uint ComputProtoHashIdByName(string name)
        {
            uint id = 0;
            foreach (char c in name)
            {
                id = 31 * id + c;
            }

            uint maxId = 2 << 14;
            uint offset = 1000;
            return id % (maxId - offset) + offset;
        }

        public static int ComputCfgHashIdByName(string name)
        {
            int id = 0;
            foreach (char c in name)
            {
                id = 31 * id + c;
            }

            return id;
        }

        private static readonly HashSet<string> s_reserveNames = new HashSet<string>()
        {
            "end",
            "base",
            "super",
            "const",
            "is",
            "as",
            "typeid",
            "typeof",
            "object",
            "ref",
            "out",
            "in",
            "os",
            "sb",
            "ele",
            "new",
            "friend",
            "public",
            "protected",
            "private",
            "internal",
            "return",
            "static",
        };

        public static bool IsValidName(string name)
        {
            name = name.Trim();
            return name.Length > 0 && !s_reserveNames.Contains(name);
        }

        public static string UpperCaseFirstChar(string s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string ToCsStyleName(string orginName)
        {
            return string.Join("", orginName.Split('_').Select(c => UpperCaseFirstChar(c)));
        }

        public static string ToJavaStyleName(string orginName)
        {
            var words = orginName.Split('_');
            var s = new StringBuilder();
            s.Append(words[0]);
            for (int i = 1; i < words.Length; i++)
            {
                s.Append(UpperCaseFirstChar(words[i]));
            }
            return s.ToString();
        }

        public static string ToJavaGetterName(string orginName)
        {
            var words = orginName.Split('_');
            var s = new StringBuilder("get");
            foreach (var word in words)
            {
                s.Append(UpperCaseFirstChar(word));
            }
            return s.ToString();
        }

        public static string GetNamespace(string fullName)
        {
            var index = fullName.LastIndexOf('.');
            return index >= 0 ? fullName.Substring(0, index) : "";
        }


        public static string GetName(string fullName)
        {
            var index = fullName.LastIndexOf('.');
            return index >= 0 ? fullName.Substring(index + 1, fullName.Length - index - 1) : fullName;
        }
    }
}
