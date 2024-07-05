using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
	private Player player;
	[field: SerializeField] public GunTemplate gun { get; private set; }
	[SerializeField] private int currentAmmo;
	[SerializeField] private float fireRateTimer = 0;
	[SerializeField] private bool canMeelee = true;
	[SerializeField] private float meleeRechargeTime = 1.2f;

	public void InitializeController(Player _player)
	{
		player = _player;
		currentAmmo = gun.maxAmmo;
	}

	private void Update()
	{
		if(fireRateTimer > 0) fireRateTimer -= Time.deltaTime;
		else if(fireRateTimer < 0) fireRateTimer = 0;
	}

	public void GetNewGun(GunTemplate newWeapon)
	{
		gun = newWeapon;
	}

	public void CanShoot()
	{
		if (gun)
		{
			if (currentAmmo > 0 && fireRateTimer <= 0) Shoot();
		}
	}

	private void Shoot()
	{
		if (player.controls.shootInput)
		{
			print("shooting");
			currentAmmo--;
			fireRateTimer += gun.fireRate;
		}
	}

	public void CanMelee()
	{
		if (player.controls.meleeInput)
		{
			if (canMeelee)
			{
				print("melee-ing");
				canMeelee = false;
				StartCoroutine(MeleeRecharge());
			}
		}
	}

	private IEnumerator MeleeRecharge()
	{
		yield return new WaitForSeconds(meleeRechargeTime);
		canMeelee = true;
	}
}
