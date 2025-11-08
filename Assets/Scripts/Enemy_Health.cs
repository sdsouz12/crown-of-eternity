using UnityEngine;

public class Enemy_Health : Entity_Health 
{
    private Enemy enemy => GetComponent<Enemy>();

    public override void TakeDamage(float damage, float elementalDamage, Transform damageDealer)
    {
        base.TakeDamage(damage, elementalDamage, damageDealer);

        if (isDead)
            return;

        if(damageDealer.GetComponent<Player>() != null)
            enemy.TryEnterBattleState(damageDealer);
    }
}
