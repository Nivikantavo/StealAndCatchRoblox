using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MobInfoCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _valueText;

    private string _valueFormat = "{0}/s";

    public void Initialize(string name, string value, bool hasOwner)
    {
        _nameText.text = name;
        if (hasOwner)
        {
            _valueText.text = string.Format(_valueFormat, value);
        }
        else
        {
            _valueText.text = value;
        }
    }
}
