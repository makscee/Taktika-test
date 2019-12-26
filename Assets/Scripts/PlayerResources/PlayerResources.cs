public class PlayerResources
{
    private int _gold, _lives;
    private static PlayerResources _instance;

    private PlayerResources(int gold, int lives)
    {
        _gold = gold;
        GameManager.Instance.Gold.text = gold.ToString();
        _lives = lives;
        GameManager.Instance.Lives.text = lives.ToString();
    }

    public static int Gold
    {
        get => _instance._gold;
        set
        {
            _instance._gold = value;
            GameManager.Instance.Gold.text = value.ToString();
        }
    }

    public static int Lives
    {
        get => _instance._lives;
        set
        {
            _instance._lives = value;
            GameManager.Instance.Lives.text = value.ToString();
            if (value <= 0)
            {
                GameManager.Instance.Defeat();
            }
        }
    }

    public static void Init()
    {
        var gold = GameManager.GameConfig.startGold;
        var lives = GameManager.GameConfig.startLives;
        _instance = new PlayerResources(gold, lives);
    }
}