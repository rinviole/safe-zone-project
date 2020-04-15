using UnityEngine;
using System.Collections;

public class Monster : Obstacle
{
    Animator animator;
    public float explosionWaitTime;
    public float explosionRadius;
    public float explosionForce;
    float nextExplosion;
    [SerializeField] AudioClip explosionSound;

     void Awake()
    {
        animator = GetComponent<Animator>();
    }
     void Update()
    {
        if (Time.time > nextExplosion)
        {
            nextExplosion = Time.time + 2;
            RandomImpact();
        }
    }

    public void RandomImpact()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (Collider2D nearbyObjects in colliders)
            {
                if (nearbyObjects.tag == "Player")
                {
                    Rigidbody2D rb = nearbyObjects.GetComponent<Rigidbody2D>();

                    if (rb != null)
                    {
                        rb.velocity = Vector3.zero;
                        StartCoroutine(Explosion(rb, explosionForce));
                    }
                }
            }
    }

    IEnumerator Explosion(Rigidbody2D _rb, float _explosionForce)
    {
        animator.SetTrigger("explode");
        yield return new WaitForSeconds(explosionWaitTime);
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
        yield return new WaitForSeconds(.2f);
        animator.SetTrigger("explosionEffect");
        if (Vector3.Distance(transform.position, _rb.transform.position) < explosionRadius)
        {
            Rigidbody2DExtension.AddExplosionForce(_rb, _explosionForce, transform.position, explosionRadius);
        }
        Destroy(gameObject, 1f);
    }

    public override void DestroyBehaviour()
    {
        if (GameManager.instance.isShieldActive)
        {
            animator.SetTrigger("destroyEffect");
            Destroy(gameObject, 3f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
