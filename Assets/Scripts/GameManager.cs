using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static GameConfig GameConfig;
    public EnemyPathProvider EnemyPathProvider;
    
    // UI
    public Text Gold, Cost, Lives;
    public GameObject DefeatScreen;
    private void Awake()
    {
        Instance = this;
        
        Enemy.Prefab = Resources.Load<GameObject>("Enemy");
        BasicTower.UpgradedPrefab = Resources.Load<GameObject>("UpgradedTower");
        
        var filePath = Path.Combine(Application.streamingAssetsPath, "config.json");
        if(File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            GameConfig = JsonUtility.FromJson<GameConfig>(json);
            Cost.text = "Upgrade cost: " + GameConfig.upgradeCost;
            PlayerResources.Init();
        }
        else
        {
            Debug.LogError("Cannot load config.json.");
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Defeat()
    {
        Time.timeScale = 0;
        DefeatScreen.SetActive(true);
    }
}
