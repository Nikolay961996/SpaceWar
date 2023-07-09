using Assets.Scripts.Ship;
using UnityEngine;

public class MissileGun : GunBase
{
    [SerializeField] private Transform _firePoint;

    private void Update()
    {
        CheckShooting();
    }

    protected override void OnShoot()
    {
        Instantiate(BulletPrefab, _firePoint.position, _firePoint.rotation);
    }
}
