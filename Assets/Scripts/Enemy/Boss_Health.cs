using UnityEngine;

// If you have Enemy_Health that extends Entity_Health, inherit from that instead:
public class Boss_Health : Entity_Health
{
    [SerializeField] private VictoryController victoryController;

    protected override void Die()
    {
        base.Die();
        Debug.Log("[Boss_Health] Boss died -> show victory");

        if (victoryController != null)
            victoryController.ShowVictory();
        else
            Debug.LogWarning("[Boss_Health] victoryController not assigned!");
    }
}
