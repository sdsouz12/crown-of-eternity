using UnityEngine;

public class Player_Health : Entity_Health
{
    [SerializeField] private GameOverController gameOverController;

    protected override void Die()
    {
        base.Die();
        Debug.Log("[Player_Health] Player died -> show game over");

        if (gameOverController != null)
            gameOverController.ShowGameOver();
        else
            Debug.LogWarning("[Player_Health] GameOverController not assigned!");
    }

    // optional: disable regen for the player
    protected override void Awake()
    {
        base.Awake();
        // If you don’t want regen, uncheck “Can Regenerate Health” in the inspector
        // (no code needed if you already unchecked it)
    }

    // TEMP: press K to test Game Over quickly
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) currentHealth = 0f;
    }
}
