using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;       // Bullet prefab that the enemy will fire
    public Transform firePoint;           // The position from which the enemy fires the bullets (e.g., the gun's muzzle)

    public bool isAutomatic = true;       // Whether the enemy shoots automatically (like a machine gun)
    public float fireInterval = 0.5f;     // Time between shots (for automatic fire)
    private float fireCooldown;           // Cooldown timer for automatic fire

    public float pitchRange = 0.1f;       // Range for randomizing audio pitch
    private AudioSource audioSource;      // AudioSource for shooting sound

    public Transform player;              // Reference to the player (target)

    void Start()
    {
        // Initialize audioSource
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Shoot when the player is in range (you could implement detection here)
        if (Vector2.Distance(transform.position, player.position) < 15f)
        {
            // Semi-Automatic Shooting (only fires on button press)
            if (!isAutomatic && Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

            // Automatic Shooting (fires continuously when holding the fire button)
            if (isAutomatic && fireCooldown <= 0)
            {
                Shoot();
                fireCooldown = fireInterval;
            }

            // Decrease the cooldown over time
            fireCooldown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the fire point with the correct rotation (direction)
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Pass the bullet owner (the enemy) to the bullet
        bullet.GetComponent<Bullet>().owner = gameObject;

        // Optionally, add random pitch for the shooting sound effect
        audioSource.pitch = Random.Range(1 - pitchRange, 1 + pitchRange);
        audioSource.PlayOneShot(audioSource.clip);
    }
}