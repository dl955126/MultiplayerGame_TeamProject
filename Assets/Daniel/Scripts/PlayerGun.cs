using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGun : MonoBehaviour
{
    [Header("Bullet Variables")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gunBarrelEnd;

    [SerializeField] NewPlayerInputs player;

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && player.isAiming)
        {
            Instantiate(bulletPrefab, gunBarrelEnd);
        }
    }
}
