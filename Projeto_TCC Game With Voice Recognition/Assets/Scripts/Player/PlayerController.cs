using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public float jumpForce = 525;
    public float iceForce = 5;
    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask groundLayer;

    public AudioClip jumpSfx;
    public AudioClip[] footStepsSfx;

    [SerializeField]
    private float walkSpeed;
    public float pushSpeed;
    public float maxSpeed = 10;
    private bool pushing;

    private Rigidbody2D rb;
    private Vector2 newMoviment;

    private bool facingRight = true;

    private bool jump;
    private bool grounded;
    private bool doubleJump;

    private bool onIce;

    private PlayerAnimation playerAnimation;
    private bool canControl = true;

    private PassThroughPlatform platform;
    private AudioManager audioManager;



    private void Awake() {

        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        audioManager = GetComponent<AudioManager>();

    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update () {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        playerAnimation.SetOnGround(grounded);

        if (grounded)
            doubleJump = false;

        //Debug.Log(rb.velocity.y);
    }

    private void FixedUpdate() {

        playerAnimation.SetVSpeed(rb.velocity.y);

        if (!canControl) {
            return;
        }

        if (!onIce)
            rb.velocity = newMoviment;
        else if (onIce) {
            rb.AddForce(new Vector2(newMoviment.x * iceForce, 0), ForceMode2D.Force);
            if(Mathf.Abs (rb.velocity.x) >= maxSpeed) {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            }
        }

        if (jump){
            jump = false;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);

            if(!doubleJump && !grounded){
                doubleJump = true;
            }
        }

    }

    public void Jump(){

        if (grounded || (!doubleJump && PlayerSkills.instance.skills.Contains(Skills.DoubleJump))) {

            audioManager.PlayAudio(jumpSfx);
            jump = true;

        }

    }

    // codigo modificado 
    public void Move(float direction)
    {
        float currentSpeed;
        if (pushing)
            currentSpeed = pushSpeed;
        else
            currentSpeed = walkSpeed;

        newMoviment = new Vector2(direction * currentSpeed, rb.velocity.y);

        playerAnimation.SetSpeed((int)Mathf.Abs(direction));

        if (facingRight && direction < 0)
        {
            Flip();
        }
        else if (!facingRight && direction > 0)
        {
            Flip();
        }
    }

    public void Stop()
    {
        float currentSpeed;
        if (pushing)
            currentSpeed = pushSpeed;
        else
            currentSpeed = walkSpeed;

        newMoviment = new Vector2(rb.velocity.x, rb.velocity.y);

    }

    public void SetPushing(bool state) {

        pushing = state;

    }

    void Flip() {
        facingRight = !facingRight;

        transform.Rotate(0, 180, 0);
    }

    public void DisableControls() {
        canControl = false;
        jump = false;
        rb.velocity = Vector2.zero;
    }

    public void EnableControls() {
        newMoviment = Vector2.zero;
        canControl = true;
    }

    public bool GetGrounded() {
        return grounded;
    }

    public void SetOnIce(bool state) {
        onIce = state;
        if (onIce) {
            rb.drag = 2;
        }
        else {
            rb.drag = 0;
        }
    }

    public void SetPlataform(PassThroughPlatform passPlatform) {
        platform = passPlatform;

    }

    public void PassThroughPlatform() {
        if(platform != null) {
            platform.PassingThrough();
   
        }

    }

    public bool IsOnIce() {
        return onIce;
    }

    public void Footsteps() {

        audioManager.PlayAudio(footStepsSfx[Random.Range(0, footStepsSfx.Length)]);

    }

}
