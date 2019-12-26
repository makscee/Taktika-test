using UnityEngine;

public class BasicTower : Tower
{
    public LineRenderer LineRenderer;
    public static GameObject UpgradedPrefab;

    protected override void Update()
    {
        if (LineRenderer.enabled && FireCooldownLeft < fireCooldown - 0.1f)
        {
            LineRenderer.enabled = false;
        }
        base.Update();
    }

    protected override void Shoot(Enemy enemy)
    {
        LineRenderer.SetPositions(new[] {transform.position, enemy.transform.position});
        LineRenderer.enabled = true;
        base.Shoot(enemy);
    }

    protected override void Upgrade()
    {
        if (PlayerResources.Gold < GameManager.GameConfig.upgradeCost) return;
        PlayerResources.Gold -= GameManager.GameConfig.upgradeCost;
        
        var newTower = Instantiate(UpgradedPrefab);
        newTower.transform.position = transform.position;
        Destroy(gameObject);
    }
}