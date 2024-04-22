using System;

[Serializable]
public class SavedGameData
{
    public float[] positionPlayer;
    public int playerLives;
    public StateData[] enemyList;
    public StateData[] floorList;
}
