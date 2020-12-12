using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathYEET = new Vector2(-0,0);
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    float gravityScale;
    // State
    bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScale = myRigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive) { return; }
        StartCoroutine(Die());
        Move();
        Jump();
        ClimbLadder();
        FlipSprite();
    }
    private void Move()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 to 1
        Vector2 playerVelocity = new Vector2(controlThrow * moveSpeed, myRigidbody2D.velocity.y);
        myRigidbody2D.velocity = playerVelocity;
        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHorizontalSpeed);
    }
    private void ClimbLadder()
    {
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            myAnimator.SetBool("Climbing", false);
            myRigidbody2D.gravityScale = gravityScale;
            return; 
        }
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidbody2D.velocity.x, controlThrow * climbSpeed);
        myRigidbody2D.velocity = climbVelocity;
        myRigidbody2D.gravityScale = 0f;

        bool playerVerticalSpeed = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerVerticalSpeed);
    }
    private void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            myRigidbody2D.velocity += jumpVelocity;
        }
    }
    private void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon; // epsilon is 0, check if we're moving
        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x),1); // Mathf.Sign return 1 when f is positive or zero, else -1
        }
    }
    private IEnumerator Die()
    {
        if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazard")))
        {
            myAnimator.SetTrigger("Dying");
            myRigidbody2D.velocity = deathYEET;
            isAlive = false;
            yield return new WaitForSecondsRealtime(1f);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
