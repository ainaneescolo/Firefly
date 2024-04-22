using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("----- Jason Variables -----")]
    private bool continueGame;
    public bool ContinueGame => continueGame;

    [Header("----- Game Variables -----")] 
    public bool gameWon;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ChangeStateContinueGame(bool newState)
    {
        continueGame = newState;
    }
    
    public void OpenScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
