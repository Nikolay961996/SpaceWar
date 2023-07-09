using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] protected int Damage = 5;
    [SerializeField] protected float Speed = 10;
    [SerializeField] protected float LifeTimeInSeconds = 2;

    protected virtual bool DestroyAfterTouch => true;
    protected virtual bool CanDamagePlayer => true;

    protected virtual void InnerStart()
    {
    }

    protected virtual void InnerUpdate()
    {
    }

    protected virtual void InnerOnTriggerEnter2D(Collider2D hitInfo)
    {
        var target = hitInfo.GetComponent<IDamageable>();
        if (!CanDamagePlayer && target is PlayerController)
            return;
        if (target != null)
        {
            target.TakeDamage(Damage);
            if (DestroyAfterTouch)
                Destroy(gameObject);
        }
    }

    private void Start()
    {
        Destroy(gameObject, LifeTimeInSeconds);
        InnerStart();
    }

    private void Update()
    {
        InnerUpdate();
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        InnerOnTriggerEnter2D(hitInfo);
    }
}