using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Pistol : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float bulletLifetime = 2f;

    public AudioClip clip;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        audioSource.PlayOneShot(clip);

        if(rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        Destroy(bullet, bulletLifetime);
    }
}
