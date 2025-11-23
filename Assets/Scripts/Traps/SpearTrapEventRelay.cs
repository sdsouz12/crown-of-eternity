using UnityEngine;

public class SpearTrapEventRelay : MonoBehaviour
{
    private SpearTrapDamage dmg;

    private void Awake()
    {
        dmg = GetComponentInChildren<SpearTrapDamage>();
    }

    public void EnableDamage()
    {
        if (dmg != null)
            dmg.EnableDamage();
    }

    public void DisableDamage()
    {
        if (dmg != null)
            dmg.DisableDamage();
    }
}
