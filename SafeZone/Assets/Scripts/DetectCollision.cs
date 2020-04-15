using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    ScreenShake screenShake;
    UIManager uiManager;

    private void Awake()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            screenShake.Shake();
        }
    }

    private void OnTriggerEnter2D(Collider2D triggerEnterCollider)
    {
        if (triggerEnterCollider.tag == "Safe Zone")
        {
            GameManager.instance.InZone = true;
        }
        if (triggerEnterCollider.tag == "ShieldPowerUp")
        {
            GameManager.instance.shieldAmount++;
            Destroy(triggerEnterCollider.gameObject);
        }
        if (triggerEnterCollider.tag == "DestroyAll")
        {
            PlayerPrefs.SetInt("DestroyAll", PlayerPrefs.GetInt("DestroyAll") + 1);
            Destroy(triggerEnterCollider.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D triggerStayCollider)
    {
        if (triggerStayCollider.tag == "Safe Zone")
        {
            GameManager.instance.UpdateZone(false, "SAFE ZONE", Color.yellow);
            uiManager.UpdateScore(10, true);
        }
    }
    private void OnTriggerExit2D(Collider2D triggerExitCollider)
    {
        if (triggerExitCollider.tag == "Safe Zone")
        {
            GameManager.instance.UpdateZone(true, "DANGER ZONE", Color.red);
            uiManager.UpdateScore(0, false);
            GameManager.instance.InZone = false;
        }
    }
  
}
