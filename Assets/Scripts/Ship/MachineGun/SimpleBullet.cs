using UnityEngine;

public class SimpleBullet : BulletBase
{
    protected override void InnerUpdate()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }
}
