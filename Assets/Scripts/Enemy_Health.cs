using UnityEngine;

public class Enemy_Health : Entity_Health 
{
    private Enemy enemy => GetComponent<Enemy>();

    public override void TakeDamage(float damage, float elementalDamage, Transform damageDealer)
    {
        base.TakeDamage(damage, elementalDamage, damageDealer);

        if (isDead)
            return;

        if (damageDealer != null)
        {
            Player p = damageDealer.GetComponent<Player>();
            if (p != null)
                enemy.TryEnterBattleState(damageDealer);
        }
    }
}
