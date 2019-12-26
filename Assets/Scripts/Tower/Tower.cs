using System;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public float damage = 1f, fireCooldown = 2f, radius = 2f;
    protected float FireCooldownLeft;
    protected Enemy Target;
    public GameObject RadiusVisual;

    private void Awake()
    {
        RadiusVisual.transform.localScale = new Vector3(radius * 2, radius * 2);
    }

    protected virtual void Update()
    {
        if (FireCooldownLeft > 0f)
        {
            FireCooldownLeft -= Time.deltaTime;
            return;
        }

        if (!Target && !GetNewTarget()) return;

        if ((Target.transform.position - transform.position).magnitude > radius)
        {
            Target = null;
            if (!GetNewTarget()) return;
        }
        Shoot(Target);
        // only works if fire rate is not greater than frame rate
        FireCooldownLeft = fireCooldown;
    }

    private bool GetNewTarget()
    {
        var enemies = EnemyGrid.GetEnemiesAroundPoint(transform.position, radius);
        if (enemies.Count == 0) return false;
        var passedMax = 0f;
        foreach (var enemy in enemies)
        {
            if (!(passedMax < enemy.PathPassed)) continue;
            Target = enemy;
            passedMax = enemy.PathPassed;
        }
        return true;
    }
    
    protected virtual void Shoot(Enemy enemy)
    {
        if (!enemy.TakeShot(damage)) return;
        GameManager.Instance.defeatedEnemies++;
        Target = null;
    }

    private void OnMouseDown()
    {
        Upgrade();
    }

    protected abstract void Upgrade();
}