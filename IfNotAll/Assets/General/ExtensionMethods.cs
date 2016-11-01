using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods {

    public static string EnforceNewlines(this string str)
    {
        str = str.Replace("\t", "");
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


    public static string HighlightByReplacement(this string str, string color, char replacement = ' ')
    {
        str = str.Replace("["+color+"]", "|");
        if (replacement == ' ')
        {
            str = str.Highlight('|', color, false);
        }
        else
        {
            str = str.Highlight('|', color, replacement);
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

    public static string Highlight(this string str, char enclosing, string color, char replacement)
    {
        int[] indicies = str.IndexOfAll(enclosing);
        string start = "<color=#" + ColorRegistry.Instance.HexOfNamedColor(color) + ">";
        string end = "</color>";
        int offset = 0;
        int mod = 0;
        for (int i = 0; i < indicies.Length; i++)
        {
            if (mod % 2 == 0)
            {
               
                str = str.Remove(indicies[i] + offset, 1);
                str = str.Insert(indicies[i] + offset, replacement.ToString());
                str = str.Insert(indicies[i] + offset, start);

                offset += start.Length;
            }
            else
            {
              
                str = str.Remove(indicies[i] + offset, 1);
                str = str.Insert(indicies[i] + offset, replacement.ToString());
                str = str.Insert(indicies[i] + offset + 1, end);
                offset += end.Length;
            }
            mod++;
        }
        return str;
    }

    public static string HighlightResources(this string str)
    {
        int[] indicies = str.IndexOfAll('$');
        int mod = 0;
        int start = 0;
        int end = 0;
        for (int i = 0; i < indicies.Length; i++)
        {
            if (mod % 2 == 0)
            {
                start = indicies[i];
            }
            else
            {
                end = indicies[i];
                string subKey = str.Substring(start + 1, (end - start) - 1);
                string subValue = str.Substring(start, (end - start) + 1);
                Debug.Log(subValue);
                string[] parts = subKey.Split(' ');
                for(i = 0; i < parts.Length; i++)
                {
                    parts[i] = parts[i].Trim();
                    Debug.Log(parts[i]);
                }

                int value = int.Parse(parts[0]);
                Debug.Log(value);
                if(value > 0)
                {
                    str = str.Highlight('$', "leaf", false);
                }
                else
                {
                    str = str.Highlight('$', "red", false);
                    str = str.Replace(subKey, subKey.Substring(1));
                }


                //HighlightResources(str);
            }
            mod++;
        }
        return str;
    }

    public static string HighlightPresets(this string msg)
    {
        msg = msg.HighlightByReplacement("yellow", '\'');
        foreach(string colorName in ColorRegistry.Instance.ColorList.Keys)
        {
            msg = msg.HighlightByReplacement(colorName);
        }
        
        /*
        msg = msg.Highlight('|', "yellow", '\'');
        msg = msg.Highlight('\'', "yellow", true);
        msg = msg.Highlight('~', "white", false);
        msg = msg.Highlight('`', "cyan", false);
        msg = msg.Highlight('^', "magenta", false);
        */
        return msg;
    }

}
