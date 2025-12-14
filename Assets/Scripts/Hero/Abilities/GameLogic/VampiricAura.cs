using System.Collections;
using UnityEngine;

namespace GameLogic
{
    public class VampiricAura : AuraAbility
    {
        [SerializeField] private int _damage = 2;
        [SerializeField] private int _heal = 1;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private Health _health;

        protected override IEnumerator RunAura(float duration, float radius, float frequency, float cooldown)
        {
            if (_health == null)
            {
                Debug.LogWarning("Не указан компонент Health владельца способности");
                _auraRoutine = null;
                yield break;
            }

            float activeTime = 0f;
            var waitForSeconds = new WaitForSeconds(frequency);

            while (activeTime < duration)
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, _enemyLayer);

                foreach (var enemy in enemies)
                {
                    if (enemy.TryGetComponent(out Health enemyHealth))
                    {
                        enemyHealth.TakeDamage(_damage);
                        _health.TakeHeal(_heal);
                    }
                }

                activeTime += frequency;
                yield return waitForSeconds;
            }

            yield return new WaitForSeconds(cooldown);
            _auraRoutine = null;
        }
    }
}
