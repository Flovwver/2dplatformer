using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : HealthUI
{
    protected Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected override void OnValueChanged(int currentValue, int maxValue)
    {
        _slider.value = (float) currentValue / maxValue;
    }
}
