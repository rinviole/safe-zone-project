using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //Movement
    //public static float movementSpeed = 500; -> shield test
    public float movementSpeed = 500;
    float nextMove;
    Rigidbody2D rb;

    //INPUT
    public static bool keyboardControls;
    public static bool touchControls;
    bool touchEnded;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //RestrainPlayer();
        Movement(); //FOR TESTING

        if (Time.time > nextMove && !GameOver.instance.GameIsOver)
        {
            nextMove = Time.time + .025f;
          
            TouchMovement();
        }
    }
    

    void Movement()
    {
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 velocity = movementInput.normalized * Time.deltaTime;

        rb.AddForce(velocity * movementSpeed / 8, ForceMode2D.Impulse);
    }

    void TouchMovement()
    {
        if (touchEnded)
        {
            rb.velocity *= 0.9f;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = 0;
            Vector2 target = Vector2.zero;
            Vector2 endPos = Vector2.one;
            

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    target = (touchPos - transform.position) * endPos;
                    touchEnded = false;
                    break;
                case TouchPhase.Moved:
                    target = (touchPos - transform.position) * endPos;
                    break;
                case TouchPhase.Stationary:
                    target = touchPos - transform.position;
                    break;
                case TouchPhase.Ended:
                    endPos = touchPos;
                    touchEnded = true;
                    break;
                case TouchPhase.Canceled:
                    break;
                
            }
            rb.velocity = target.normalized * movementSpeed * Time.deltaTime;
           // rb.AddForce(target.normalized * movementSpeed * Time.deltaTime, ForceMode2D.Force);
        }
    }
    
   
}
