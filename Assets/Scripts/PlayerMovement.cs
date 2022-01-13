using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;     // movement speed of player
    public float jumpHeight = 10.0f;    // jump force of the player
    public int extraJumps = 0;        // number of allowed extra jumps

    [SerializeField]
    private Transform groundCheck;       // to check if player's feet is touching ground
    
    [SerializeField]
    private LayerMask groundLayer;       // what is ground 

    [SerializeField]
    private Transform frontCheck;        // to check if player is grabbing a wall
    
    [SerializeField]
    private Transform rearCheck;
    
    [SerializeField]
    private LayerMask wallLayer;         // what walls player can grab

    [Range (0.0f, 1.0f), SerializeField]
    private float wallSlidingSpeed;      // wall sliding speed
    
    [SerializeField]
    private Vector2 wallJumpForce;       // wall jump force
    
    [SerializeField]
    private bool debugBool = false;

    private Rigidbody2D _rigidBody;
    private bool facingRight = true;

    private bool isGrounded;
    private int availableJumps;

    private bool isTouchingWall;
    private bool wallSliding;
    private bool wallJumping;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // runs once per frame
    void Update()
    {
        // movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        if(moveHorizontal != 0) {
            
        }
        if(!wallSliding) {
            _rigidBody.velocity = new Vector2(moveHorizontal * moveSpeed, _rigidBody.velocity.y);
        }

        // flipping when necessary
        if(!facingRight && moveHorizontal > 0 && !wallSliding || facingRight && moveHorizontal < 0 && !wallSliding) {
            Flip();
        }

        // to make player slide from wall
        if(wallSliding) {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, Mathf.Clamp(_rigidBody.velocity.y, wallSlidingSpeed, float.MaxValue));
        }
            
        // jump
        if(Input.GetButtonDown("Jump") && availableJumps > 0 && !wallSliding) {
            _rigidBody.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
            availableJumps--;
        }
        else if(Input.GetButtonDown("Jump") && availableJumps == 0 && isGrounded && !wallSliding) {
            _rigidBody.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }

        // wall jump
        if((Input.GetButtonDown("Jump") && wallSliding)) {
            if(Physics2D.OverlapCircle(frontCheck.position, 0.1f, wallLayer)) {
                Flip();
            }
            if(facingRight) {
                _rigidBody.AddForce(new Vector2(0f, wallJumpForce.y), ForceMode2D.Impulse);
                _rigidBody.velocity = new Vector2(wallJumpForce.x, _rigidBody.velocity.y);
            }
            else {
                _rigidBody.AddForce(new Vector2(0f, wallJumpForce.y), ForceMode2D.Impulse);
                _rigidBody.velocity = new Vector2(-wallJumpForce.x, _rigidBody.velocity.y);
            }
        }
    }

    // can run multiple times each frame
    void FixedUpdate()
    {
        // check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // reset jumps if player is grounded
        if(isGrounded) {
            availableJumps = extraJumps;
        }

        // check if the player is grabbing a wall
        if(Physics2D.OverlapCircle(frontCheck.position, 0.1f, wallLayer) || Physics2D.OverlapCircle(rearCheck.position, 0.1f, wallLayer)) {
            isTouchingWall = true;
        } 
        else {
            isTouchingWall = false;
        }

        // check if wall sliding
        if (isTouchingWall && !isGrounded) {
            wallSliding = true;
        }
        else {
            wallSliding = false;
        }
    }
    
    // function to flip character
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void Move() {
        Debug.Log("Move");
    }
}
