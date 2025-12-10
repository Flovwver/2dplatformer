using System.Collections;
using UnityEngine;

public class SmoothHealthBar : HealthBar
{
    [SerializeField] private float _valueChangeSpeed = 1f;

    private Coroutine _smoothCoroutine;

    protected override void OnValueChanged(int currentValue, int maxValue)
    {
        float targetValue = (float) currentValue / maxValue;

        if (_smoothCoroutine != null)
            StopCoroutine(_smoothCoroutine);

        _smoothCoroutine = StartCoroutine(ChangeValueSmooth(targetValue));
    }

    private IEnumerator ChangeValueSmooth(float targetValue)
    {
        while (_slider.value != targetValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetValue, _valueChangeSpeed * Time.deltaTime);
            yield return null;
        }

        _smoothCoroutine = null;
    }
}
