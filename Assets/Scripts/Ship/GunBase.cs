using UnityEngine;

namespace Assets.Scripts.Ship
{
    public abstract class GunBase : MonoBehaviour
    {
        [SerializeField] protected GameObject BulletPrefab;
        [SerializeField] protected float FireRate = 1;

        private float _nextFireTime;

        public void Shoot()
        {
            if (Time.time > _nextFireTime)
            {
                _nextFireTime = Time.time + FireRate;
                OnShoot();
            }
        }

        protected void CheckShooting()
        {
            if (Input.GetKey(KeyCode.Space))
                Shoot();
        }

        protected abstract void OnShoot();
    }
}
