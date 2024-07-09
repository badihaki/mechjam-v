using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
	private Player player;
	[field: SerializeField] public GunTemplate gun { get; private set; }
	[field: SerializeField] public int currentAmmo { get; private set; }
	[field: SerializeField] public int ammoStock { get; private set; }
	[SerializeField] private float fireRateTimer = 0;
	[SerializeField] private bool canMeelee = true;
	[SerializeField] private float meleeRechargeTime = 1.2f;
	[SerializeField] private bool isReloading = false;
	[SerializeField] private Transform shootPoint;

	// events
	public delegate void AmmoChanged(int ammo);
	public event AmmoChanged onAmmoChanged;
	public delegate void StockChanged(int ammo);
	public event StockChanged onStockChanged;

    public void InitializeController(Player _player)
	{
		player = _player;
		StartCoroutine(SelectGunStyle());
		currentAmmo = gun.maxAmmo;
		if (onAmmoChanged != null) onAmmoChanged(currentAmmo);
        ammoStock = 3;
		if (onStockChanged != null) onStockChanged(ammoStock);
		shootPoint = transform.Find("ShootPoint");
	}

	private void Update()
	{
		if(fireRateTimer > 0) fireRateTimer -= Time.deltaTime;
		else if(fireRateTimer < 0) fireRateTimer = 0;
	}

	public void GetNewGun(GunTemplate newWeapon)
    {
        gun = newWeapon;
        StartCoroutine(SelectGunStyle());
    }

    private IEnumerator SelectGunStyle()
    {
		yield return null;
        switch (gun.gunStyle)
        {
            case GunStyle.oneHand:
				print($"gun style is switched to {gun.gunStyle}");
                player.AnimationController.SetBool("1H", true); // oneHand
                // player.AnimationController.SetBool("oneHand", true); // oneHand
                player.AnimationController.SetBool("freefall", true); // oneHand
                player.AnimationController.SetBool("2H", false);
                player.AnimationController.SetBool("dual", false);
                break;
        }
    }

    public void CanShoot()
	{
		if (gun && player.Controls.shootInput)
		{
			if (currentAmmo > 0 && fireRateTimer <= 0) Shoot();
		}
	}

	private void Shoot()
	{
		// print("shooting");
		currentAmmo--;
        onAmmoChanged(currentAmmo);
        fireRateTimer += gun.fireRate;
		Projectile projectile = Instantiate(gun.projectile, shootPoint.position, transform.rotation).GetComponent<Projectile>();
		projectile.InitializeProjectile(player.transform, gun.damage);
	}

	public void CanMelee()
	{
		if (player.Controls.meleeInput)
		{
			if (canMeelee)
			{
				print("melee-ing");
				canMeelee = false;
				player.Controls.UseMelee();
				StartCoroutine(MeleeRecharge());
			}
		}
	}

	public void CanReload()
	{
		if (player.Controls.reloadInput && !isReloading)
		{
			if (currentAmmo < gun.maxAmmo && ammoStock > 0) StartCoroutine(ReloadGun());
			player.Controls.UseReload();
		}
	}

	private IEnumerator ReloadGun()
	{
		// print("reloading");
		isReloading = true;
		currentAmmo = 0;
		onAmmoChanged(currentAmmo);
		ammoStock--;
        onStockChanged(ammoStock);
        yield return new WaitForSeconds(gun.reloadTime);
        currentAmmo = gun.maxAmmo;
        onAmmoChanged(currentAmmo);
		isReloading = false;
	}

	private IEnumerator MeleeRecharge()
	{
		yield return new WaitForSeconds(meleeRechargeTime);
		canMeelee = true;
	}
}
