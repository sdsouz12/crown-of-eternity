using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour , IDamgable
{
    private Slider healthBar;
    private Entity_VFX entityVfx;
    private Entity entity;
    private Entity_Stats entityStats;

    
    [SerializeField] protected float currentHealth;
    [SerializeField] protected bool isDead;
    [Header("Health Regen")]
    [SerializeField] private float regenInterval = 1;
    [SerializeField] private bool canRegenerateHealth = true;
    private Renderer rend;


    protected virtual void Awake()
    {
        rend = GetComponentInChildren<Renderer>();

        entityVfx = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        entityStats = GetComponent<Entity_Stats>();
        healthBar = GetComponentInChildren<Slider>();

        currentHealth = entityStats.GetMaxHealth();
        UpdateHealthBar();

        InvokeRepeating(nameof(RegenerateHealth), 0, regenInterval);
    }
    
    public virtual void TakeDamage(float damage, float elementalDamage, Transform damageDealer)
    {
        if (isDead)
            return;
        
        if (AttackEvaded())
        {
            Debug.Log($"{gameObject.name} evaded the attack!");
            return;
        }

        Entity_Stats attackerStats = null;
        float armorReduction = 0;

        if (damageDealer != null)
        {
            attackerStats = damageDealer.GetComponent<Entity_Stats>();
            if (attackerStats != null)
                armorReduction = attackerStats.GetArmorReduction();
        }

        float mitigation = entityStats.GetArmorMitigation(armorReduction);
        float finalDamage = damage * (1 - mitigation);

        entityVfx?.PlayOnDamageVfx();
        
        ReduceHealth(finalDamage);
        Debug.Log("Elemental Damage taken: " + elementalDamage);
    }

    
    private bool AttackEvaded() => Random.Range(0f, 100f) < entityStats.GetEvasion();

    private void RegenerateHealth()
    {
        if (canRegenerateHealth == false)
            return;

        float healthRegenAmount = entityStats.resources.HealthRegen.GetValue();
        IncreaseHealth(healthRegenAmount);
    }

    public void IncreaseHealth(float healAmount)
    {
        if (isDead)
            return;
        
        float newHealth = currentHealth + healAmount;
        float maxHealth = entityStats.GetMaxHealth();

        currentHealth = Mathf.Min(newHealth, maxHealth);
        UpdateHealthBar();
    }


    public void ReduceHealth(float damage)
    {
        currentHealth = currentHealth - damage;
        UpdateHealthBar();
        

        if (currentHealth <= 0)
            Die();
    }

     private void Die()
    {
        isDead = true;
       //Debug.Log("you Died");
        entity?.EntityDeath();
    }

    private void UpdateHealthBar()
    {
        if(healthBar == null)
            return;

        if (rend != null && !rend.isVisible)
            return; // skip UI update

        healthBar.value = currentHealth / entityStats.GetMaxHealth();
    }
        
}
