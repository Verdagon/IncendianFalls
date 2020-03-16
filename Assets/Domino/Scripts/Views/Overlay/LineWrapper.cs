using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace Domino {
  public static class LineWrapper {
    public static string[] Wrap(string text, int margin) {
      var startingLines = text.Split(new char[] { '\n' });
      var resultingLines = new List<string>();
      foreach (var startingLine in startingLines) {
        var wrappedLines = WrapLine(startingLine, margin);
        resultingLines.AddRange(wrappedLines);
        if (wrappedLines.Length == 0) {
          resultingLines.Add("");
        }
      }
      return resultingLines.ToArray();
    }

    private static string[] WrapLine(string text, int margin) {
      int start = 0, end;
      var lines = new List<string>();
      text = Regex.Replace(text, @"\s", " ").Trim();

      while ((end = start + margin) < text.Length) {
        while (text[end] != ' ' && end > start)
          end -= 1;

        if (end == start)
          end = start + margin;

        lines.Add(text.Substring(start, end - start));
        start = end + 1;
      }

      if (start < text.Length)
        lines.Add(text.Substring(start));

      return lines.ToArray();
    }
  }
}
