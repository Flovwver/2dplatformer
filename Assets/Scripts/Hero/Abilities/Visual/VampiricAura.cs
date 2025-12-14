using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Visual
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class VampiricAura : AuraAbility
    {
        [SerializeField, Range(0, 1)] private float _auraTransparencyMax = 0.6f;
        [SerializeField, Range(0, 1)] private float _auraTransparencyMin = 0.3f;
        [SerializeField] private Slider _cooldown;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override IEnumerator RunAura(float duration, float radius, float frequency, float cooldown)
        {
            _cooldown.gameObject.SetActive(true);

            float activeTime = 0f;
            _spriteRenderer.enabled = true;
            Color color = _spriteRenderer.color;
            color = new (color.r, color.g, color.b, _auraTransparencyMin);

            while (activeTime < duration)
            {
                float pingPongValue = Mathf.PingPong((activeTime + frequency / 2f) / frequency * 2f, 1f);
                float transparency = Mathf.Lerp(_auraTransparencyMin, _auraTransparencyMax, pingPongValue);
                
                color.a = transparency;

                _spriteRenderer.color = color;

                _cooldown.value = Mathf.MoveTowards(_cooldown.value, 0f, Time.deltaTime / duration);

                activeTime += Time.deltaTime;
                yield return null;
            }

            _spriteRenderer.enabled = false;

            float cooldownTimer = 0f;

            while (cooldownTimer <= cooldown)
            {
                cooldownTimer += Time.deltaTime;
                _cooldown.value = Mathf.MoveTowards(_cooldown.value, 1f, Time.deltaTime / cooldown);

                yield return null;
            }

            _cooldown.gameObject.SetActive(false);

            _auraRoutine = null;
        }
    }
}
