using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour, IDamgable
{
    private Slider healthBar;
    private Entity_VFX entityVfx;
    private Entity entity;
    private Entity_Stats entityStats;

    [SerializeField] protected float currentHealth;
    [SerializeField] protected bool isDead;

    [Header("Health Regen")]
    [SerializeField] private float regenInterval = 1f;
    [SerializeField] private bool canRegenerateHealth = true;

    protected virtual void Awake()
    {
        entityVfx  = GetComponent<Entity_VFX>();
        entity     = GetComponent<Entity>();
        entityStats= GetComponent<Entity_Stats>();
        healthBar  = GetComponentInChildren<Slider>();

        float maxHealth = entityStats != null ? entityStats.GetMaxHealth() : 100f;
        currentHealth = maxHealth;
        UpdateHealthBar();

        InvokeRepeating(nameof(RegenerateHealth), 0f, regenInterval);
    }

    public virtual void TakeDamage(float damage, float elementalDamage, Transform damageDealer)
    {
        if (isDead) return;

        if (AttackEvaded())
        {
            Debug.Log($"{gameObject.name} evaded the attack!");
            return;
        }

        Entity_Stats attackerStats = damageDealer != null ? damageDealer.GetComponent<Entity_Stats>() : null;
        float armorReduction = attackerStats != null ? attackerStats.GetArmorReduction() : 0f;

        float mitigation = entityStats != null ? entityStats.GetArmorMitigation(armorReduction) : 0f;
        float finalDamage = damage * (1f - mitigation);

        entityVfx?.PlayOnDamageVfx();

        ReduceHealth(finalDamage);
        Debug.Log("Elemental Damage taken: " + elementalDamage);
    }

    private bool AttackEvaded()
    {
        float evasion = entityStats != null ? entityStats.GetEvasion() : 0f;
        return Random.Range(0f, 100f) < evasion;
    }

    private void RegenerateHealth()
    {
        if (!canRegenerateHealth || isDead || entityStats == null) return;

        float healthRegenAmount = entityStats.resources.HealthRegen.GetValue();
        IncreaseHealth(healthRegenAmount);
    }

    public void IncreaseHealth(float healAmount)
    {
        if (isDead || entityStats == null) return;

        float maxHealth = entityStats.GetMaxHealth();
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        UpdateHealthBar();
    }

    public void ReduceHealth(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0f)
            Die();
    }

    // CHANGED: made overridable so subclasses (Player_Health, Boss_Health, etc.) can extend it
    protected virtual void Die()
    {
        if (isDead) return;

        isDead = true;

        // stop periodic regen when dead
        CancelInvoke(nameof(RegenerateHealth));

        // notify entity for animation/cleanup/etc.
        entity?.EntityDeath();
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null || entityStats == null) return;
        float maxHealth = entityStats.GetMaxHealth();
        healthBar.value = maxHealth > 0f ? currentHealth / maxHealth : 0f;
    }

    // (Optional) read-only accessor if other scripts need to check death state
    public bool IsDead => isDead;
}
