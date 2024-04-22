using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private int sceneNum;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<PlayerController>()) return;

        GameManager.instance.gameWon = LevelManager.instance.EnemiesLeftToDefeat() == 0;
        
        GameManager.instance.OpenScene(sceneNum);
    }
}
