using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("----- UI IN GAME -----")]
    [SerializeField] private GameObject[] playerLives_Img;
    [SerializeField] private TextMeshProUGUI enemiesLeft_Txt;

    [Header("----- REFERENCE OBJ IN SCENE -----")]
    private PlayerController playerController;

    public int playerLives;
    public List<float> playerPos;

    [SerializeField] private List<Enemy_Base> enemiesList = new List<Enemy_Base>();
    [SerializeField] private List<FloorNode> floorList = new List<FloorNode>();


    private SavedGameData SavedGameData;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        if (GameManager.instance.ContinueGame)
        {
            SavedGameData = DataStore<SavedGameData>.LoadLocalData("GameData.dat");
            StartCoroutine("LoadSavedLists");
            LoadSavedPlayer(SavedGameData.playerLives, SavedGameData.positionPlayer[0], SavedGameData.positionPlayer[1]);
        }
        
        EnemiesLeftToDefeat();
    }

    public void StopStartTime()
    {
        Time.timeScale = Time.timeScale >= 1 ? 0 : 1;
    }
    
    public void LoseGame()
    {
        var index = SceneManager.GetActiveScene().buildIndex == 1 ? 1 : 2;
        GameManager.instance.OpenScene(index);
    }
    
    #region UI
    
    public int EnemiesLeftToDefeat()
    {
        var numEnemies = 0;
        foreach (var enemy in enemiesList)
        {
            if (!enemy.pure)
                ++numEnemies;
        }
        
        enemiesLeft_Txt.text = $"{numEnemies}";
        return numEnemies;
    }

    public void RefreshPlayerLives(int lives)
    {
        if (lives < 0) return;
        playerLives_Img[lives].SetActive(false);
    }

    public void OpenScene()
    {
        GameManager.instance.OpenScene(0);
    }
    
    #endregion
    
    #region Json Data
    
    IEnumerator LoadSavedLists()
    {
        var index = 0;
        while (index < enemiesList.Count)
        {
            enemiesList[index].pure = SavedGameData.enemyList[index].pure;
            enemiesList[index].ChangeToPure();
            ++index;
        }
        
        index = 0;
        while (index < floorList.Count)
        {
            floorList[index].pure = SavedGameData.floorList[index].pure;
            floorList[index].CheckState();
            ++index;
        }
        
        return null;
    }

    private void LoadSavedPlayer(int playerLives, float xpos, float ypos)
    {
        playerController.playerLives = playerLives;
        playerController.transform.position = new Vector3(xpos, ypos);
    }

    private void SaveGameInfo()
    {
        if (SceneManager.GetActiveScene().name != "Game_Scene") return;

        SavedGameData = new SavedGameData();

        var enemyListTMP = new List<StateData>();
        var floorListTMP = new List<StateData>();

        foreach (var state in enemiesList)
        {
            StateData enemyTMP = new StateData();
            enemyTMP.pure = state.pure;
            enemyListTMP.Add(enemyTMP);
        }

        SavedGameData.enemyList = enemyListTMP.ToArray();
        
        foreach (var state in floorList)
        {
            StateData floorTMP = new StateData();
            floorTMP.pure = state.pure;
            floorListTMP.Add(floorTMP);
        }

        SavedGameData.floorList = floorListTMP.ToArray();

        playerLives = playerController.playerLives;
        playerPos.Add(playerController.transform.position.x);
        playerPos.Add(playerController.transform.position.y);
        
        SavedGameData.playerLives = playerLives;
        SavedGameData.positionPlayer = playerPos.ToArray();
        
        DataStore<SavedGameData>.SaveLocalData(SavedGameData, "GameData.dat");
    }
    
    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus) return;
        SaveGameInfo();
    }
    
    private void OnApplicationQuit()
    {
        SaveGameInfo();
    }
    
    #endregion
}
