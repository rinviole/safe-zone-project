using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using MK.Glow;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public event System.Action OnGameOverEvent;
    [HideInInspector]
    public bool gameOver;
    
    //Zone Transform - Object
    public Transform safeZone;
    public static float safeZoneMax;
    public float minScale;
    public Vector3 safeZoneScaleChanger = Vector3.zero;
    public Vector2 scaleTimeMinMax;
    public float scaleLerpTime;
    public static float zoneChangeAmount;

    //Zone Communication and UI Var
    #region
    public bool InZone { get; set; }
    bool canIncrease;
    public Text zoneText;
    [SerializeField] private GameObject dangerZoneIcon;
    ScreenColor screenColor;
    #endregion

    //SHIELD Var
    #region
    public bool isShieldActive;
    float nextPowerUp = 10f;
    [SerializeField] private GameObject shieldPowerUp; // For picking up
    [SerializeField] private GameObject shieldObject; // Use of shield object attached to the player
    [SerializeField] private Text shieldAmountText;
    [HideInInspector]
    public int shieldAmount;
    PlayerController playerController;
    Rigidbody2D playerRb;
    float startRb;
    #endregion
    //Destroy All Power
    [SerializeField] private GameObject destroyAllPowerUp;
    bool isDestroyAllActive = false;
    [HideInInspector]
    public int destroyAmount; // Increase with ads.
    public bool canSpawn = true;
    [SerializeField] private Text destroyAllAmountText;
    [SerializeField] private AudioClip swoosh;

    private void Awake()
    {
        //References
        screenColor = Camera.main.GetComponent<ScreenColor>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerRb = playerController.GetComponent<Rigidbody2D>();
        startRb = playerRb.mass;
    }

    private void Start()
    {
        shieldAmount = 1;

        safeZone.localScale = Vector3.one * safeZoneMax;

        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    private void Update()
    {
        if (!gameOver && Time.timeScale >= 1)
        {
            //CHANGE ZONE SCALE BASED ON INZONE VAR.
            if (!InZone)
            { //When out of safe zone and game is not over
                ChangeZoneScale(); // Shrink Zone
                screenColor.ChangeGlowTint(true); //Change screen color
                canIncrease = false;
            }
            else
            {   
                screenColor.ChangeGlowTint(false); //Reset screen color 
                ChangeZoneScale(); //Increase zone
                canIncrease = true;
            }

            //Shield Amount
            shieldAmountText.text = $"x {shieldAmount}";
            //Destroy All Amount
            destroyAllAmountText.text = $"x {PlayerPrefs.GetInt("DestroyAll").ToString()}";
            //Power Up
            PowerUpSpawner();
            //Activate Power Ups
            if (Input.GetKeyDown(KeyCode.F))
                ShieldActivate();

            if (Input.GetKeyDown(KeyCode.Space))
                DestroyAllActivate();
        }
        else if (gameOver)
        {
            dangerZoneIcon.SetActive(false);
            zoneText.text = "";
        }

        CheckGameOverConditions();
    }

    public void CheckGameOverConditions()
    {
        if (gameOver && OnGameOverEvent != null)
        {
            OnGameOverEvent();
        }
    }

    //ZONE SIZE
    public void ChangeZoneScale()
    {
        if (!canIncrease && safeZone.localScale.x >= minScale)
        {
            safeZoneScaleChanger = (zoneChangeAmount + 0.002f) * Vector3.one;
            safeZone.localScale = Vector2.Lerp(safeZone.localScale, (safeZone.localScale - safeZoneScaleChanger), scaleLerpTime);
        }
        else if (canIncrease && safeZone.localScale.x <= safeZoneMax)
        {
            safeZoneScaleChanger = zoneChangeAmount * Vector3.one;
            safeZone.localScale = Vector2.Lerp(safeZone.localScale, (safeZone.localScale + safeZoneScaleChanger), scaleLerpTime);
        }
    }

    //ZONE INFO
    public void UpdateZone(bool isDangerZone, string zoneInfo, Color zoneTextColor)
    {
        zoneText.text = zoneInfo;
        zoneText.color = zoneTextColor;

        if (isDangerZone)
           dangerZoneIcon.SetActive(true);
        else
           dangerZoneIcon.SetActive(false);
    }

    public void ShieldActivate() //Call from a button on mobile.
    {
        if (shieldAmount > 0 && !isShieldActive)
        {
            shieldAmount--;
            StartCoroutine(ShieldRoutine());
        }
    }
    IEnumerator ShieldRoutine()
    {
        isShieldActive = true;
        shieldObject.SetActive(true);
        playerRb.mass = 300;
        yield return new WaitForSeconds(5f);
        shieldObject.SetActive(false);
        isShieldActive = false;
        playerRb.mass = startRb;
    }

    public void DestroyAllActivate() // Call from a button on mobile
    {
        if (!isDestroyAllActive && PlayerPrefs.GetInt("DestroyAll") > 0) 
        {
            StartCoroutine(DestroyAllRoutine());
            //Sound 
            AudioSource.PlayClipAtPoint(swoosh, Camera.main.transform.position);
            PlayerPrefs.SetInt("DestroyAll", PlayerPrefs.GetInt("DestroyAll") - 1);
        }
    }
    IEnumerator DestroyAllRoutine()
    {
        canSpawn = false;
        screenColor.DestroyAllAnimation(true);
        FindActiveObjects();
        yield return new WaitForSeconds(1f);
        isDestroyAllActive = false;
        canSpawn = true;
        screenColor.DestroyAllAnimation(false);
    }

    void FindActiveObjects()
    {
        List<GameObject> activeObjects = new List<GameObject> (GameObject.FindGameObjectsWithTag("Obstacle"));
        activeObjects.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Monster")));

        foreach (GameObject activeObject in activeObjects)
        {
            Destroy(activeObject);
        }
    }

    public void PowerUpSpawner()
    {
        int randomIndex = Random.Range(0, 4);
        GameObject[] randomPowerUp = { shieldPowerUp, destroyAllPowerUp };
        int powerUpIndex;
        int randomPowerUpIndex = Random.Range(0,10);
        if (randomPowerUpIndex < 2)
        {
            powerUpIndex = 1;
        }
        else
        {
            powerUpIndex = 0;
        }

        if (Time.time > nextPowerUp)
        {
            float randomInterval = Random.Range(8, 54);
            nextPowerUp = Time.time + randomInterval;
            Instantiate(randomPowerUp[powerUpIndex], ScreenUtility.GetRandomPos()[randomIndex], Quaternion.identity);
        }
    }
 
    public void GiveReward() //For rewarded ads
    {
        PlayerPrefs.SetInt("DestroyAll", PlayerPrefs.GetInt("DestroyAll") + 1);
        StartCoroutine(RewardRoutine());
        Debug.Log($"REWARD: {PlayerPrefs.GetInt("DestroyAll")}");
    }
    IEnumerator RewardRoutine()
    {
        UIManager.instance.PlayRewardAnimation(true);
        yield return new WaitForSeconds(2);
        UIManager.instance.PlayRewardAnimation(false);
    }
}
