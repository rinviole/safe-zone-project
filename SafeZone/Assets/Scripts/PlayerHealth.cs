using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    Animator animator;
    public float damageAmount;
    public float health;
    private float startHealth = 100;
    public Image healthBar;
    [SerializeField] private Text healthText;

    private void Start()
    {
        health = startHealth;
        animator = GetComponent<Animator>();
        healthText.text = "100";
    }

    private void Update()
    {
        if (!GameManager.instance.InZone)
        {
            UpdateHealth();
        }

        if (health <= 0)
        {
            PlayerDied();
        }
    }

    void PlayerDied()
    {
        health = 0;
        healthText.text = "0";
        GameManager.instance.gameOver = true;
        StartCoroutine(ExplosionRoutine());
    }

    IEnumerator ExplosionRoutine()
    {
        animator.SetBool("playerExplosion", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("playerExplosion", false);
        this.gameObject.SetActive(false);
    }

    public void UpdateHealth()
    {
        if (!GameManager.instance.isShieldActive && Time.timeScale >= 1)
        {
            health -= damageAmount;
            healthBar.fillAmount = health / startHealth;
            healthText.text = health.ToString("0.0");
        }
    }

}
