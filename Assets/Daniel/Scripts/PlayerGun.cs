using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGun : NetworkBehaviour
{
    [Header("Bullet Variables")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gunBarrelEnd;

    [SerializeField] NewPlayerInputs player;

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && player.isAiming && IsLocalPlayer)
        {
            ServerShootRpc();
        }
    }

    [Rpc(SendTo.Server)]
    public void ServerShootRpc()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunBarrelEnd);
        bullet.GetComponent<NetworkObject>().Spawn();
    }
}
