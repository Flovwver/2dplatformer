using UnityEngine;

public class Inputer : MonoBehaviour
{
    public const string Horizontal = "Horizontal";

    private bool _isJump;
    private bool _isAttack;
    private bool _isAbilityActive;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(KeyCode.Space))
            _isJump = true;

        if (Input.GetKeyDown(KeyCode.LeftControl))
            _isAttack = true;

        if (Input.GetKeyDown(KeyCode.V))
            _isAbilityActive = true;
    }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    public bool GetIsAttack() => GetBoolAsTrigger(ref _isAttack);

    public bool GetIsAbilityActive() => GetBoolAsTrigger(ref _isAbilityActive);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
