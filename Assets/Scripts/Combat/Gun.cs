using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    public Camera fpsCam; // We will link our player camera here

    void Update()
    {
        // Check for left mouse click
        if (Input.GetButtonDown("Fire1")) // "Fire1" is the default for Left Click
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        // Fire a ray from the center of the camera
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // Check if the object we hit has a Health script
            Health targetHealth = hit.transform.GetComponent<Health>();
            if (targetHealth != null)
            {
                // If it does, make it take damage
                targetHealth.TakeDamage(damage);
            }
        }
    }
}