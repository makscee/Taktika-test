using System;
using UnityEngine.Serialization;

[Serializable]
public class GameConfig
{
    public float waveDuration;
    public int waveEnemyCountSpread;
    public int upgradeCost;
    public int startLives;
    public int startGold;
}