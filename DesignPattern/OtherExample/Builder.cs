using System.Text;

namespace DesignPattern.BuilderPattern
{
    public class Builder
    {
        public static void Run()
        {
            Console.WriteLine("Bad Pritace");
            Console.WriteLine("------------------------------------");
            WriteHtmlElement_BadPractice();
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Best Pritace");
            Console.WriteLine("------------------------------------");
            WriteHtmlElement_BestPractice();
        }

        /// <summary>
        /// Bad Pritace
        /// </summary>
        private static void WriteHtmlElement_BadPractice()
        {
            var sb = new StringBuilder();
            var hello = "hello";
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            Console.WriteLine(sb.ToString());

            sb.Clear();
            var words = new string[] { "Hello", "World" };
            sb.Append("<ul>");
            foreach(var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("</ul>");

            Console.WriteLine(sb.ToString());
        }

        private static void WriteHtmlElement_BestPractice()
        {
            var htmlBuilder = new HtmlBuilder("ul");
            htmlBuilder.AddChild("li", "Hello").AddChild("li", "World");
            Console.WriteLine(htmlBuilder.ToString());
        }

        public class HtmlElement
        {
            public string Name { get; set; }
            public string? Text { get; set; }
            public List<HtmlElement> Elements { get; set; } = new List<HtmlElement>();
            private const int indentSize = 4;

            public HtmlElement(string name)
            {
                Name = name;
            }

            public HtmlElement(string name, string text)
            {
                Name = name;
                Text = text;
            }

            private string ToStringImpl(int indent)
            {
                var sb = new StringBuilder();

                // 間格的寬度
                var indentString = new string(' ', indentSize * indent);

                sb.AppendLine($"{indentString}<{Name}>");

                if (!string.IsNullOrWhiteSpace(Text))
                {
                    sb.Append(new string(' ', indentSize * (indent + 1)));
                    sb.AppendLine(Text);
                }

                foreach(var element in Elements)
                {
                    sb.Append(element.ToStringImpl(indent + 1));
                }

                sb.AppendLine($"{indentString}</{Name}>");

                return sb.ToString();
            }

            public override string ToString()
            {
                return ToStringImpl(0);
            }
        }

        private class HtmlBuilder
        {
            private readonly string _rootName;
            public HtmlElement _root;

            public HtmlBuilder(string rootName)
            {
                _rootName = rootName;
                _root = new HtmlElement(_rootName);
            }

            /// <summary>
            /// 這邊透過回傳HtmlBuilder可以做到Fluent Api的效果
            /// </summary>
            public HtmlBuilder AddChild(string childName)
            {
                _root.Elements.Add(new HtmlElement(childName));
                // this指向HtmlBuilder
                return this;
            }

            public HtmlBuilder AddChild(string childName, string childText)
            {
                _root.Elements.Add(new HtmlElement(childName, childText));
                return this;
            }

            public override string ToString()
            {
                return _root.ToString();
            }

            public void Clear()
            {
                _root = new HtmlElement(_rootName);
            }
        }
    }
}
