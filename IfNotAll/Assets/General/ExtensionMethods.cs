using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods {

    public static string EnforceNewlines(this string str)
    {
        str = str.Replace("\\n", "\n");
        return str;

    }

    public static int[] IndexOfAll(this string str, char ch)
    {
        var foundIndexes = new List<int>();
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == ch)
                foundIndexes.Add(i);
        }
        return foundIndexes.ToArray();
    }

    public static string HighlightCommands(this string str)
    {
        int[] indicies = str.IndexOfAll('\'');
        string start = "<color=yellow>";
        string end = "</color>";
        int offset = 0;
        int mod = 0;
        for (int i = 0; i < indicies.Length; i++)
        {
            if (mod % 2 == 0)
            {
                str = str.Insert(indicies[i] + offset, start);
                offset += start.Length;
            }
            else
            {
                str = str.Insert(indicies[i] + offset + 1, end);
                offset += end.Length;
            }
            mod++;
        }
        return str;
    }

    public static string BoldCommands(this string str)
    {
        int[] indicies = str.IndexOfAll('\'');
        string start = "<b>";
        string end = "</b>";
        int offset = 0;
        int mod = 0;
        for (int i = 0; i < indicies.Length; i++)
        {
            if (mod % 2 == 0)
            {
                str = str.Insert(indicies[i] + offset, start);
                offset += start.Length;
            }
            else
            {
                str = str.Insert(indicies[i] + offset + 1, end);
                offset += end.Length;
            }
            mod++;
        }
        return str;
    }

    public static string Highlight(this string str, char enclosing, Color32 color)
    {
        int[] indicies = str.IndexOfAll(enclosing);
        string start = "<color=#"+HexConverter.ColorToHex(color)+">";
        string end = "</color>";
        int offset = 0;
        int mod = 0;
        for (int i = 0; i < indicies.Length; i++)
        {
            if (mod % 2 == 0)
            {
                str = str.Insert(indicies[i] + offset, start);
                offset += start.Length;
            }
            else
            {
                str = str.Insert(indicies[i] + offset + 1, end);
                offset += end.Length;
            }
            mod++;
        }
        return str;
    }

}
