using UnityEngine;
using System.Collections;

public class TextEffects: Singleton<TextEffects> {

    public string DisplayCurrency(int amount)
    {
        if (amount > 0)
        {
            return "<color=#" + ColorRegistry.Instance.HexOfNamedColor("leaf") + ">" + amount + " hope</color>";
        }
        else
        {
            return "<color=#" + ColorRegistry.Instance.HexOfNamedColor("red") + ">" + Mathf.Abs(amount) + " hope</color>";
        }
    }

    public string DisplayFood(int amount)
    {
        if (amount > 0)
        {
            return "<color=#" + ColorRegistry.Instance.HexOfNamedColor("leaf") + ">" + amount + " food</color>";
        }
        else
        {
            return "<color=#" + ColorRegistry.Instance.HexOfNamedColor("red") + ">" + Mathf.Abs(amount) + " food</color>";
        }
    }

    public string DisplayTitle(string title)
    {
        return "<color=#" + ColorRegistry.Instance.HexOfNamedColor("magenta") + ">" + title + "</color>";
    }

    public string DisplayName(string name)
    {
        return "<color=#" + ColorRegistry.Instance.HexOfNamedColor("white") + ">" + name + "</color>";
    }

    public string DisplayColored(string msg, string color)
    {
        return "<color=#" + ColorRegistry.Instance.HexOfNamedColor(color) + ">" + msg + "</color>";
    }

}
