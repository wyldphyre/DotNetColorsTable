using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace DotNetColorsTable
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filename = "DotNetColorsTable.html";

            var builder = new StringBuilder();

            builder.AppendLine("<table>");

            var colorType = typeof(Color);
            var colors = colorType.GetProperties().Where(info => info.PropertyType == colorType).Select(info => (Color)info.GetValue(null, null));
            //var propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);

            foreach (var color in colors)
            {
                var htmlColour = ColorTranslator.ToHtml(color);
                var hex = $"#{color.R:X2}{color.G:X2}{color.B:X2}"; 

                builder.AppendLine("    <tr>");
                builder.AppendLine($"      <td>{color.Name}</td><td><span style=\"background: {hex};\">&nbsp;&nbsp;&nbsp;&nbsp;</span></td><td>{hex}</td>");
                builder.AppendLine("    </tr>");
            }

            builder.AppendLine("  </table");

            var template = File.ReadAllText("template.html");

            var page = template.Replace("<content>", builder.ToString());
            File.WriteAllText("DotNetColorsTable.html", page);

            Console.WriteLine(page);

            // TODO: Implement system colours output
            //foreach (var color in SystemColors)
            //{
            //}
        }
    }
}
