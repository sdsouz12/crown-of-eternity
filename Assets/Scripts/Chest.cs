using UnityEngine;

public class Chest : MonoBehaviour , IDamgable
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();

    private Animator anim => GetComponentInChildren<Animator>();

    private Entity_VFX fx => GetComponent<Entity_VFX>();

    [Header("Open Details")]
    [SerializeField] private Vector2 knockback;

    public void TakeDamage(float damage, float elementalDamage, Transform damageDealer)
    {
        fx.PlayOnDamageVfx();
        anim.SetBool("chestOpen", true);
        rb.linearVelocity = knockback;
        rb.angularVelocity = Random.Range(-200f, 200f);
    }
}
