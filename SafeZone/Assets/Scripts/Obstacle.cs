using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    private float slingForce;
    Vector2 startDirection;
    Transform target;
    bool canSling;
    bool canPlaySound;

    [SerializeField]AudioClip hitSound;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        canPlaySound = true;
        slingForce = Mathf.Lerp(300, 600, Difficulty.GetDifficultyPercent());
        canSling = true;
        if (target != null)
        {
            startDirection = target.transform.position - transform.position;
        }
    }

    void Update()
    {
        if (target != null && canSling )
        {
            rb.AddForce(startDirection * slingForce * Time.deltaTime);
            rb.velocity = Vector2.zero;
        }
    }
    public void PlayHitSound()
    {
        AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, .25f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" || collision.collider.tag == "ShieldActive")
        {
            if (hitSound != null && canPlaySound)
            {
                PlayHitSound();
                canPlaySound = false;
            }
            DestroyBehaviour();
        }
    }

    public virtual void DestroyBehaviour()
    {
        if (GameManager.instance.isShieldActive)
        {
            anim.SetTrigger("explodeObstacle");
            Destroy(gameObject, 0.2f);
        }
    }
}
