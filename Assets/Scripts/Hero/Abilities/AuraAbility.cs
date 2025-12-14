using UnityEngine;
using System.Collections;

public abstract class AuraAbility : MonoBehaviour
{
    protected Coroutine _auraRoutine;

    public virtual void Activate(float duration, float radius, float frequency, float cooldown)
    {
        if (_auraRoutine == null)
            _auraRoutine = StartCoroutine(RunAura(duration, radius, frequency, cooldown));
    }

    protected abstract IEnumerator RunAura(float duration, float radius, float frequency, float cooldown);
}
