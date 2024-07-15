using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Laser : MonoBehaviour
{
    [field: SerializeField] public float effectiveRange = 99.98f;
    private LineRenderer laserLine;

    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(transform.position,transform.forward,out hit, effectiveRange))
        {
            Projectile projectile = GetComponent<Projectile>();

            laserLine.SetPosition(0, transform.position);
            laserLine.SetPosition(1, hit.point);
            print($"hit {hit.transform.name} in the hitter");
            
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.Damage(transform, projectile.damage, projectile.forces);
                print($"damaged {hit.transform.name} right in the damageable");
            }
        }
        else
        {
            Vector3 endPoint = transform.forward * effectiveRange;
            // laserLine.SetPosition(1, endPoint);
            laserLine.SetPosition(0, transform.position);
            laserLine.SetPosition(1, endPoint);
        }
        
        Debug.DrawRay(transform.position, transform.forward, Color.blue, 5.0f);
        Debug.DrawLine(transform.position, transform.forward * effectiveRange, Color.magenta, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
