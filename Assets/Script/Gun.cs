using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 120f;



    public Camera fpsCam;
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
        
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
       


    }
    
    IEnumerator Reload()
    {
        if (maxAmmo > 0 )
        { 
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);


        animator.SetBool("Reloading", false);
            
            
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



            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Get the Rigidbody component of the bullet
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            // Shoot the bullet forward
            bulletRigidbody.velocity = firePoint.forward * bulletSpeed;

            // Destroy the bullet after a certain amount of time
            Destroy(bullet, 5f);

            Debug.Log("Shoot");

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

                // Instantiate the bullet prefab at the fire point position and rotation
                


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
