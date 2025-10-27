using UnityEngine;

public class Entity_Health : MonoBehaviour , IDamgable
{

     private Entity_VFX entityVfx;
     private Entity entity;

    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;


    protected virtual void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
         entity = GetComponent<Entity>();
        // healthBar = GetComponentInChildren<Slider>();

         //currentHp = maxHp;
        // UpdateHealthBar();
    }
    
     public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
            return;

        entityVfx?.PlayOnDamageVfx();
        
        ReduceHp(damage);
    }

    protected void ReduceHp(float damage)
    {
        maxHp -= damage;
        

        if (maxHp <= 0)
            Die();
    }

     private void Die()
    {
        isDead = true;
       //Debug.Log("you Died");
        entity?.EntityDeath();
    }
}
