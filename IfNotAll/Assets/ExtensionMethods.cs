using UnityEngine;
using System.Collections;

public static class ExtensionMethods {

    public static string EnforceNewlines(this string str)
    {
        str = str.Replace("\\n", "\n");
        return str;
       
    }

	
}
