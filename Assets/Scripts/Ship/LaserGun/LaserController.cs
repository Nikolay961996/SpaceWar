using Assets.Scripts.Ship;
using UnityEngine;

public class LaserController : GunBase
{
    [SerializeField] private Transform _firePoint;

    void Update()
    {
        CheckShooting();
    }

    protected override void OnShoot()
    {
        Instantiate(BulletPrefab, _firePoint.position, _firePoint.rotation);
    }
}
