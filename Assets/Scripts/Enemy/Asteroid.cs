using UnityEngine;

public class Asteroid : EnemyBase
{
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private float _lifeTime = 1f;
    [SerializeField] private Sprite[] _damagedSprites;

    private SpriteRenderer _spriteRenderer;

    protected override void OnDamaged()
    {
        if (_damagedSprites.Length == 0)
            return;
        
        switch (DurabilityNormalized)
        {
            case > 6f:
                _spriteRenderer.sprite = _damagedSprites[0];
                break;
            case > 0.3f:
                _spriteRenderer.sprite = _damagedSprites[1];
                break;
            case > 0f:
                _spriteRenderer.sprite = _damagedSprites[2];
                break;
        }
    }

    protected override void InnerStart()
    {
        Destroy(gameObject, _lifeTime);
        _spriteRenderer = GetComponent<SpriteRenderer>();

        RBody.velocity = (PlayerTransform.position - transform.position).normalized * MoveSpeed;
        RBody.angularVelocity = _rotationSpeed;
    }
}
