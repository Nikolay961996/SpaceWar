using UnityEngine;

public class SimpleMissile : BulletBase
{
    [SerializeField] private GameObject _impactEffectPrefab;

    protected override void InnerUpdate()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    protected override void InnerOnTriggerEnter2D(Collider2D hitInfo)
    {
        var target = hitInfo.gameObject;
        if (target != null)
        {
            Instantiate(_impactEffectPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}