using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 200f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public float timeBetweenShooting, spread, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public float shootForce, upwardForce;

    public GameObject bullet;
    public Transform firePoint;
    public float bulletSpeed = 300f;



    public Camera fpsCam;
    public Transform attackPoint;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;


    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Animator animator;


    private UIManager _uiManager;
    void Start()
    {
        currentAmmo = 25;
        maxAmmo = 75;
        _uiManager = GameObject.Find("PlayerUI").GetComponent<UIManager>();
    }

    void Update()
    {
        

        if (isReloading)
            return;

        if (currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R)))
        {
            StartCoroutine(Reload());
            return;
       
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
       


    }
    
    IEnumerator Reload()
    {
        if (maxAmmo >= 0)
        { 
        isReloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);


        animator.SetBool("Reloading", false);

            FindObjectOfType<AudioManager>().Play("Reload");
            maxAmmo = maxAmmo - (25 - currentAmmo);
            currentAmmo = currentAmmo + (25 - currentAmmo);
        
        yield return new WaitForSeconds(.25f);
       
        _uiManager.UpdateAmmo(currentAmmo);
        _uiManager.UpdateMaxAmmo(maxAmmo);
        isReloading = false;
        }
    }



    void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("ShootAudio");




        if(currentAmmo > 0)
        { 
        muzzleFlash.Play();
        currentAmmo--;
        _uiManager.UpdateAmmo(currentAmmo);
        _uiManager.UpdateMaxAmmo(maxAmmo);



            Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
            RaycastHit hit;

            //check if ray hits something
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            { targetPoint = hit.point;

            

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

            Target2 target2 = hit.transform.GetComponent<Target2>();
                if (target2 != null)
                {
                    target2.TakeDamage2(damage);
                }


                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy (impactGO,2f);
            }
            else
                targetPoint = ray.GetPoint(75); //Just a point far away from the player
            //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
        }
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            currentAmmo = 10;
            Destroy(other.gameObject);
        }
    }

    public void AddMaxAmmo(int add)
    {
       
        maxAmmo = maxAmmo + add;
        _uiManager.UpdateMaxAmmo(maxAmmo);
        
    }
}
