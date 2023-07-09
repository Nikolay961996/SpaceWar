using UnityEngine;

public class GunSelector : MonoBehaviour
{
    [SerializeField] private GameObject _machineGunPrefab; // 1
    [SerializeField] private GameObject _laserGunPrefab;   // 2
    [SerializeField] private GameObject _missileGunPrefab; // 3
    [SerializeField] private HUDController _hudController;

    private GameObject _activeGun;
    
    void Start()
    {
        ChangeActiveGun(_machineGunPrefab);
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            ChangeActiveGun(_machineGunPrefab);
        else if (Input.GetKey(KeyCode.Alpha2))
            ChangeActiveGun(_laserGunPrefab);
        else if (Input.GetKey(KeyCode.Alpha3))
            ChangeActiveGun(_missileGunPrefab);
    }

    private void ChangeActiveGun(GameObject newActiveGun)
    {
        _activeGun?.SetActive(false);
        newActiveGun.SetActive(true);
        _activeGun = newActiveGun;

        _hudController.UpdateWeapon(newActiveGun.name);
    }
}
