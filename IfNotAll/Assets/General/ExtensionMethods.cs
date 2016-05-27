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
        string start = "<color=#"+ColorRegistry.Instance.HexOfNamedColor("yellow")+">";
        string end = "</color>";
        int offset = 0;
        int mod = 0;
        for (int i = 0; i < indicies.Length; i++)
        {
            if (mod % 2 == 0)
            {
                str = str.Insert(indicies[i] + offset + 1, start);
                offset += start.Length;
            }
            else
            {
                str = str.Insert(indicies[i] + offset, end);
                offset += end.Length;
            }
            mod++;
        }
        return str;
    }

    public static string Highlight(this string str, char enclosing, string color, bool keepEnclosingChar)
    {
        int[] indicies = str.IndexOfAll(enclosing);
        string start = "<color=#"+ ColorRegistry.Instance.HexOfNamedColor(color) + ">";
        string end = "</color>";
        int offset = 0;
        int mod = 0;
        for (int i = 0; i < indicies.Length; i++)
        {
            if (mod % 2 == 0)
            {
                if (!keepEnclosingChar)
                {
                    str = str.Remove(indicies[i] + offset, 1);
                    
                }

                str = str.Insert(indicies[i] + offset, start);
                
                offset += start.Length;
            }
            else
            {
                if (!keepEnclosingChar)
                {
                    str = str.Remove(indicies[i] + offset - 1, 1);
                    offset = offset - 2;
                }
                str = str.Insert(indicies[i] + offset + 1, end);
                offset += end.Length;
            }
            mod++;
        }
        return str;
    }

}
