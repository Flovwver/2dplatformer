using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class HealthText : HealthUI
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    protected override void OnValueChanged(int currentValue, int maxValue)
    {
        _text.text = $"{currentValue} / {maxValue}";
    }
}
