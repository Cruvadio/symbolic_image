using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Symbol
{
    public class FormulaeParser
    {

        private string _src =
            @"using System;
                
            static class Code 
            {
                public static double Formula (double x, double y, double a, double b)
                {
                    return {source};
                }
            };";


        public FormulaeParser()
        {
            
        }

        private string TranslateFormula (string str)
        {
            string s = @"-?[\d.]+|\#\d+|[a-z]";
            string[] patterns =
            {
                $@"(?<A>sin|cos|log)?\((?:{s})\)",
                $@"({s})\^({s})",
                $@"(?:{s})[*/](?:{s})",
                $@"(?:{s})[+-](?:{s})"
            };
            str = Regex.Replace(str, @"\s+", "").ToLower().Replace(',', '.');
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Func<string, string> f1 = null;
            f1 = s1 =>
            {
                foreach (string p in patterns)
                {
                    var m = Regex.Match(s1, p);
                    if (m.Success)
                    {
                        string value = m.Value;
                        if (m.Groups["A"].Success)
                        {
                            value = value.Replace("sin", "Math.Sin");
                            value = value.Replace("cos", "Math.Cos");
                            value = value.Replace("log", "Math.Log");
                            value = value.Replace("exp", "Math.Exp");
                        }

                        if (m.Groups[1].Success && m.Groups[2].Success)
                        {
                            value = $"Math.Pow({m.Groups[1]}, {m.Groups[2]})";
                        }
                        string key = $"#{dic.Count}";
                        dic.Add(key, value);
                        string ss = s1.Substring(0, m.Index) + key + s1.Substring(m.Index + m.Length);

                        return f1(ss);
                    } 
                }
                return s1;
            };

            Func<string, string> f2 = null;

            f2 = s1 =>
            {
                string ss = Regex.Replace(s1, @"\#\d+", x => dic[x.Value]);
                return ss == s1 ? ss : f2(ss);
            };


            return f2(f1(str));

        }

        public Symbol.mappingFunction CreateFormula(string str)
        {
            str = TranslateFormula(str);

            string src = _src.Replace("{source}", str);

            var compiler = CodeDomProvider.CreateProvider("C#");
            var result = compiler.CompileAssemblyFromSource(new CompilerParameters(), src);
            if (result.Errors.Count == 0)
            {
                var assembly = result.CompiledAssembly;
                var type = assembly.GetType("Code");
                var method = type.GetMethod("Formula");
                return (Symbol.mappingFunction)Delegate.CreateDelegate(typeof(Symbol.mappingFunction), method);
            }

            return null;
        }
    }
}
