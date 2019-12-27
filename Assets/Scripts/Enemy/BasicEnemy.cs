using System.Collections;
using UnityEngine;

public class BasicEnemy : Enemy
{
    private Color _initColor;
    private const float BlinkTime = 0.2f;
    public SpriteRenderer spriteRenderer;

    public override void Init()
    {
        _initColor = spriteRenderer.color;
        base.Init();
    }

    public override bool TakeShot(float dmg)
    {
        StartCoroutine(nameof(Blink));
        return base.TakeShot(dmg);
    }
    
    private IEnumerator Blink()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(BlinkTime);
        spriteRenderer.color = _initColor;
    }

    protected override void Die(bool giveGold = true)
    {
        StopAllCoroutines();
        spriteRenderer.color = _initColor;
        base.Die(giveGold);
    }
}