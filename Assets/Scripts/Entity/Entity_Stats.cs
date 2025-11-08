using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat_ResourceGroup resources;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;

    public float GetElementalDamage(ElementType element)
    {
        float fireDamage = offense.fireDamage.GetValue();
        float iceDamage = offense.iceDamage.GetValue();
        float bonusElementalDamage = major.intelligence.GetValue();

        float highestDamage = fireDamage;

        if(iceDamage > highestDamage)
            highestDamage = iceDamage;


        if (highestDamage <= 0)
            return 0;
        
        float bonusFire = (fireDamage == highestDamage) ? 0 : fireDamage * 0.5f;
        float bonusIce = (iceDamage == highestDamage) ? 0 : iceDamage * 0.5f;


        float weakerElementsDamage = bonusFire + bonusIce;
        float finalDamage = highestDamage + weakerElementsDamage + bonusElementalDamage;

        return finalDamage;
        
    }
    
    public float GetPhysicalDamage()
    {
        float baseDamage = offense.damage.GetValue();
        float bonusDamage = major.strength.GetValue();
        float totalBaseDamage = baseDamage + bonusDamage;

        float BaseCritChance = offense.critChance.GetValue();
        float bonusCritChance = major.agility.GetValue() * 0.3f;
        float critChance = BaseCritChance + bonusCritChance;

        float baseCritPower = offense.critPower.GetValue();
        float bonusCritPower = major.strength.GetValue() * 0.5f;
        float critPower = (baseCritPower + bonusCritPower) / 100;

        bool isCrit = Random.Range(0, 100) < critChance;
        float finalDamage = isCrit ? totalBaseDamage * critPower : totalBaseDamage;

        return finalDamage;

    }

    public float GetArmorMitigation(float armorReduction)
    {
        float baseArmor = defense.armor.GetValue();
        float bonusArmor = major.vitality.GetValue();
        float totalArmor = baseArmor + bonusArmor;

        float recutionMultiplier = Mathf.Clamp01(1 - armorReduction);
        float effectiveArmor = totalArmor * recutionMultiplier;

        float mitigation = effectiveArmor / (effectiveArmor + 100);
        float mitigationCap = 0.85f;
        
        float finalMitigation = Mathf.Clamp(mitigation, 0, mitigationCap);
        return finalMitigation;
    }

    public float GetArmorReduction()
    {
        float finalReduction = offense.armorReduction.GetValue() / 100;
        return finalReduction;
    }
    

    public float GetEvasion()
    {
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * 0.5f;
    
        float evasionCap = 85f;
        float totalEvasion = baseEvasion + bonusEvasion;
        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap);

        return finalEvasion;
    }

    public float GetMaxHealth()
    {
        float baseHp = resources.maxHealth.GetValue();
        float bonusHp = major.vitality.GetValue() * 5;

        return baseHp + bonusHp;
    }
}
