using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float shootingRange = 10f;
    public Transform player;
    public LayerMask hitLayerMask;

    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCooldown = 0f;

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= shootingRange)
        {
            ShootRaycast();

            if (fireCooldown <= 0f)
            {
                ShootBullet();
                fireCooldown = fireRate;
            }

            fireCooldown -= Time.deltaTime;
        }
    }

    void ShootRaycast()
    {
        Vector2 direction = transform.right;

        Debug.DrawRay(transform.position, direction * shootingRange, Color.red, 1f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, shootingRange, hitLayerMask);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Health playerHealth = hit.collider.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.Damage(1);
                    Debug.Log("Player hit by raycast!");
                }
            }
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.right, transform.rotation);
        bullet.GetComponent<Bullet>().owner = gameObject;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * shootingRange);
    }
}