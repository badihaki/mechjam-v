using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : InteractablePickup
{
    [SerializeField] private GameObject m_Pickup;
    [SerializeField] private GunTemplate gun;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetGun(GunTemplate newGun) => gun = newGun;

    // Update is called once per frame
    void Update()
    {
        
    }

	public override void Interact(Player player)
	{
        print($"picking up {gun.gunName}");

        GunPickup newPickup = Instantiate(m_Pickup, transform.position,Quaternion.identity).GetComponent<GunPickup>();
        newPickup.SetGun(player.AttackController.gun);
		newPickup.InitializePickup();
        newPickup.name = $"{newPickup.gun.gunName}Pickup";

        // Transform gfx = Instantiate(newPickup.gun.rightObj, newPickup.transform.position, newPickup.transform.rotation).transform;
        Transform gfx = Instantiate(newPickup.gun.rightObj, newPickup.transform).transform;
		gfx.localScale = new Vector3(175.0f, 175.0f, 175.0f);

        float forceX = UnityEngine.Random.Range(-2.0f, 2.0f);
		float forceY = UnityEngine.Random.Range(3.0f, 8.0f);
		float forceZ = UnityEngine.Random.Range(-3.0f, 3.0f);
		Vector3 spawnForce = new Vector3(forceX, forceY, forceZ);
        newPickup.transform.parent = GameMaster.Entity.gameplayManager.currentFloor.currentEnvironment.transform;
        newPickup.physicsController.AddForce(spawnForce, ForceMode.Impulse);

        player.AttackController.GetNewGun(gun);

        Destroy(gameObject);
	}
}
