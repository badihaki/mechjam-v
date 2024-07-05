using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="gun", menuName ="Game/Weapons/New Gun")]
public class GunTemplate : ScriptableObject
{
	public string gunName;
	public int damage;
	public float fireRate;
	public int maxAmmo;
	public float reloadTime;
	public GameObject projectile;
}
