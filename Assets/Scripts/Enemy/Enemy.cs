using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp, speed;
    
    private EnemyPath _path;
    public static GameObject Prefab;
    public int reward, damage;

    public float PathPassed => _path.Passed;

    protected virtual void Start()
    {
        _path = GameManager.Instance.EnemyPathProvider.GetPath();
        transform.position = _path.Move(0, 0);
        EnemyGrid.InitPosition(this);
    }

    private void Update()
    {
        if (PathPassed >= 1f)
        {
            PlayerResources.Lives--;
            Die(false);
            return;
        }
        var newPosition = _path.Move(Time.deltaTime, speed);
        EnemyGrid.UpdatePosition(this, newPosition);
        transform.position = newPosition;
    }

    public virtual bool TakeShot(float damage)
    {
        hp -= damage;
        if (hp > 0) return false;
        Die();
        return true;
    }

    protected virtual void Die(bool giveGold = true)
    {
        EnemyGrid.ClearPosition(this);
        Destroy(gameObject);
        if (giveGold)
        {
            PlayerResources.Gold += reward;
        }
    }
}