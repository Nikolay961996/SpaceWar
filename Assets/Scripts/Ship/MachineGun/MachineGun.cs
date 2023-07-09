using Assets.Scripts.Ship;
using UnityEngine;

public class MachineGun : GunBase
{
    [SerializeField] private Transform _rightFirePoint;
    [SerializeField] private Transform _leftFirePoint;

    private void Update()
    {
        CheckShooting();
    }

    protected override void OnShoot()
    {
        Instantiate(BulletPrefab, _rightFirePoint.position, transform.rotation);
        Instantiate(BulletPrefab, _leftFirePoint.position, transform.rotation);
    }
}
