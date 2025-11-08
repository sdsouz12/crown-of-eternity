
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;
    private Entity_Stats stats;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }


    public void PerformAttack()
    {
        foreach(var target in GetDetectedColliders())
        {
           IDamgable damgable = target.GetComponent<IDamgable>();

        float fireDamage = stats.GetElementalDamage(ElementType.Fire);
        float iceDamage  = stats.GetElementalDamage(ElementType.Ice);
        float elementalDamage = fireDamage + iceDamage;

        damgable?.TakeDamage(stats.GetPhysicalDamage(), elementalDamage, transform);
           // vfx.CreateOnHitVFX(target.transform);
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position,targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
