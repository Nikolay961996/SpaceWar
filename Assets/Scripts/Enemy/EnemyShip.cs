using Assets.Scripts.Ship;
using UnityEngine;

public class EnemyShip : EnemyBase
{
    [SerializeField] private float _minDistanceToPlayer = 10;
    [SerializeField] private GunBase _gun;

    private void FixedUpdate()
    {
        var currentDistanceToPlayer = Vector3.Distance(transform.position, PlayerTransform.position);
        if (currentDistanceToPlayer > _minDistanceToPlayer)
            ForceToPlayer();
        else
            StopAndShoot();
        LookToPlayer();
    }

    private void LookToPlayer() 
    {
        var direction = PlayerTransform.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void ForceToPlayer()
    {
        Vector2 direction = (PlayerTransform.position - transform.position).normalized;
        var deltaVelocity = direction * MoveSpeed - RBody.velocity;
        var moveForce = deltaVelocity * Time.fixedDeltaTime;
        RBody.AddForce(moveForce);
    }

    private void StopAndShoot()
    {
        RBody.velocity = Vector2.zero;
        _gun.Shoot();
    }
}