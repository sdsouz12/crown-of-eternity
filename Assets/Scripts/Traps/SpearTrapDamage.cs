using UnityEngine;

public class SpearTrapDamage : MonoBehaviour
{
    [Header("Damage")]
    public float damageAmount = 20f;
    public float damageInterval = 0.4f;

    private float lastDamageTime;
    private bool canDealDamage = false;

    public void EnableDamage()
    {
        canDealDamage = true;
    }

    public void DisableDamage()
    {
        canDealDamage = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!canDealDamage)
            return;

        if (Time.time < lastDamageTime + damageInterval)
            return;

        IDamgable dmg = collision.GetComponent<IDamgable>();
        if (!collision.CompareTag("Player"))
            return;
        if (dmg != null)
        {
            dmg.TakeDamage(damageAmount, 0f, null);
        }

        lastDamageTime = Time.time;
    }
}
