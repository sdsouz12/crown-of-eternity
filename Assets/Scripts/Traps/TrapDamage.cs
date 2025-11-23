using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float damageInterval = 0.5f;

    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForceX = 6f;
    [SerializeField] private float knockbackForceY = 4f;

    private float lastDamageTime;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (Time.time < lastDamageTime + damageInterval)
            return;

        IDamgable damagable = collision.GetComponent<IDamgable>();
        if (!collision.CompareTag("Player"))
            return;

        if (damagable != null)
        {
            damagable.TakeDamage(damageAmount, 0f, null);
            ApplyKnockback(collision.transform);
            lastDamageTime = Time.time;
        }
    }

    private void ApplyKnockback(Transform target)
    {
        Entity entity = target.GetComponent<Entity>();
        if (entity == null)
            return;

        // Knockback direction (push away from the spike)
        float xDir = (target.position.x < transform.position.x) ? -1 : 1;

        // Apply knockback using your SetVelocity()
        entity.SetVelocity(knockbackForceX * -xDir, knockbackForceY);
    }
}
