using System.Collections;
using UnityEngine;

public class BasicEnemy : Enemy
{
    private Color _initColor;
    private const float BlinkTime = 0.2f;
    public SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        _initColor = spriteRenderer.color;
        base.Start();
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
}