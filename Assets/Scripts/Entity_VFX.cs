using UnityEngine;
using System.Collections;


public class Entity_VFX : MonoBehaviour
{
   
    private SpriteRenderer sr;

    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVfxDuration = .15f;
    private Material originalMaterial;
    private Coroutine onDamageVfxCoroutine;


    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    // public void CreateOnHitVFX(Transform target)
    // {
    //     GameObject vfx = Instantiate(hitVfx, target.position, Quaternion.identity);
    //     vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;
    // }

    public void PlayOnDamageVfx()
    {
        if(onDamageVfxCoroutine != null)
            StopCoroutine(onDamageVfxCoroutine);

        onDamageVfxCoroutine = StartCoroutine(OnDamageVfxCo());
    }

    private IEnumerator OnDamageVfxCo()
    {
        sr.material = onDamageMaterial;

        yield return new WaitForSeconds(onDamageVfxDuration);
        sr.material = originalMaterial;
    }
}
