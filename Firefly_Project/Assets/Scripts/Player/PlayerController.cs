using UnityEngine;

public class PlayerController : PlayerMovement2D
{
    [Header("---- PLAYER ATTRIBUTES ----")] 
    [Tooltip("Int of the number of lives the player has")]
    public int playerLives = 3;
    private Enemy_Base enemy;

    public void TakeDamagePlayer()
    {
        --playerLives;
        LevelManager.instance.RefreshPlayerLives(playerLives);
        if (playerLives != 0) return;
        death = true;
        Animator.SetTrigger("death");
    }

    public bool PlayerLives()
    {
        return playerLives <= 0;
    }
    
    public void LoseGame()
    {
        LevelManager.instance.LoseGame();
    }
}
