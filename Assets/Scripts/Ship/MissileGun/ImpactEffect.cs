using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    [SerializeField] private int _damage = 30;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        var target = hitInfo.GetComponent<IDamageable>();
        if (target != null)
            target.TakeDamage(_damage);
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
