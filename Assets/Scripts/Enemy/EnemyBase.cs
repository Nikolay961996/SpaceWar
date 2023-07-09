using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected float MoveSpeed = 10;
    [SerializeField] protected float MaxDurability = 100;
    [SerializeField] protected float CollisionDamage = 10;
    [SerializeField] protected int GivePoint = 0;

    protected HUDController HudController;
    protected Transform PlayerTransform;
    protected Rigidbody2D RBody;

    protected float Durability;
    protected float DurabilityNormalized => Durability / MaxDurability;

    protected EnemyBase()
    {
        Durability = MaxDurability;
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            return;

        Durability -= damage;
        OnDamaged();
        if (Durability <= 0)
        {
            HudController.AddScore(GivePoint);
            Destroy(gameObject);
        }
    }

    protected virtual void OnDamaged()
    {
    }

    protected virtual void InnerStart()
    {
    }

    private void Start()
    {
        RBody = GetComponent<Rigidbody2D>();
        PlayerTransform = GameObject.FindObjectOfType<PlayerController>().transform;
        HudController = GameObject.FindObjectOfType<HUDController>();
        InnerStart();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        player?.TakeDamage(CollisionDamage);
    }
}
