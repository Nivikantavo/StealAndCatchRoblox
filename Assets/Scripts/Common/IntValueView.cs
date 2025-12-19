using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntValueView : ValueView<int>
{
    protected override string CutBack(int value)
    {
        string result = value.ToString();
        float remains = 0;
        string remainsStr = string.Empty;

        if (value % 1000 > 1)
        {
            result = AdjustToPrefix(value, 1000) + "K";
        }
        else if(value % 1000000 > 1)
        {
            result = AdjustToPrefix(value, 1000000) + "M";
        }
        else if (value % 1000000000 > 1)
        {
            result = AdjustToPrefix(value, 1000000) + "B";
        }
        else if (value % 1000000000000 > 1)
        {
            result = AdjustToPrefix(value, 1000000) + "T";
        }

        return result;
    }

    private string AdjustToPrefix(int value, int divider)
    {
        float remains = 0;
        string remainsStr = string.Empty;

        remains = Mathf.Round(value / divider);
        remainsStr = remains > 0 ? string.Empty : remains.ToString().Substring(1);
        return (value % divider).ToString() + remainsStr;
    }
}
