using UnityEngine;
using UnityEngine.UI;

public class StartGameUIManager : MonoBehaviour
{
    [Header("----- Btn References -----")] 
    [SerializeField] private Button continueBtn;
    
    void Start()
    {
        if (!DataStore<SavedGameData>.FileExists("GameData.dat")) return;
        if (DataStore<SavedGameData>.LoadLocalData("GameData.dat").playerLives > 0)
        {
            continueBtn.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("No Info Found");
        }
    }
    
    public void ChangeScene(int num)
    {
        GameManager.instance.OpenScene(num);
    }
    
    public void ChangeBoolGame(bool sate)
    {
        GameManager.instance.ChangeStateContinueGame(sate);
    }
}
