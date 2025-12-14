using UnityEngine;

[RequireComponent(typeof(GameLogic.VampiricAura))]
[RequireComponent(typeof(Visual.VampiricAura))]
public class VampiricAura : MonoBehaviour
{
    [SerializeField] private float _frequency = 0.5f;
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldown = 4f;

    private GameLogic.VampiricAura _logic;
    private Visual.VampiricAura _visual;

    private void Awake()
    {
        _logic = GetComponent<GameLogic.VampiricAura>();
        _visual = GetComponent<Visual.VampiricAura>();
    }

    public void Activate()
    {
        _logic.Activate(_duration, _radius, _frequency, _cooldown);
        _visual.Activate(_duration, _radius, _frequency, _cooldown);
    }

}
