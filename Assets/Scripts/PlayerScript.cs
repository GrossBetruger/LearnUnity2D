using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerScript : MonoBehaviour
{   
    const int COIN_LAYER = 6;
    const int LAVA_LAYER = 7;
    const int LEGS_LAYER = 8;

    const int SCORE_LAYER = 9;

    const int END_LAYER = 10;


    private AudioManager audioManager = new AudioManager();
    
    // private ScoreKeeper scoreKeeper = new ScoreKeeper();
    // public Transform groundCheckTransform;
    private bool jumpAllowed = false;
    private Rigidbody2D rigidBody;
    private float horizontalInput = 0.0f;

    // private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("start"); // start game sound
        rigidBody = GetComponent<Rigidbody2D>();

    }

    // Run on every physics update
    private void FixedUpdate() {

        bool run = Input.GetKey(KeyCode.LeftShift);
        int speedFactor = run ? 3 : 1;
        float hyperJumpFactor = run ? 1.5f: 1.0f;

        // jump(hyperJumpFactor);
        
        if (Input.GetKeyDown(KeyCode.Space) && jumpAllowed) {
                Debug.Log("jumping");
                FindObjectOfType<AudioManager>().Play("jump");
                // audioManager.Play("jump");
                rigidBody
                    .AddForce(new Vector2(0, 10.0f * hyperJumpFactor), ForceMode2D.Impulse);
                }

        float currentUpwardVelocity = GetComponent<Rigidbody2D>().velocity.y;
        rigidBody.velocity = new Vector2(horizontalInput * 5 * speedFactor, currentUpwardVelocity);
        dive();
    }

    private void jump(float jumpFactor) {
        Debug.Log("space key down");

        if (Input.GetKeyDown(KeyCode.Space) && jumpAllowed) {
                Debug.Log("jumping");
                audioManager.Play("jump");
                rigidBody
                    .AddForce(new Vector2(0, 10.0f * jumpFactor), ForceMode2D.Impulse);
                }
    }

    // Update is called once per frame
    void Update()
    {
       jumpAllowed = rigidBody.velocity.y == 0 ? true : false;
        
       horizontalInput = Input.GetAxis("Horizontal"); 	 
    }

    // private void OnCollisionEnter(Collision collision) {
    //      isGrounded = true;
    // }
    // private void OnCollisionExit(Collision collision) {
    //      isGrounded = false;
    // }

    private void moveLeft() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Debug.Log("->");     	
            rigidBody
                .AddForce(new Vector2(-3, 0), ForceMode2D.Impulse);
            }    	 
    }

    private void dive() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Debug.Log("diving");     	
            rigidBody
                .AddForce(new Vector2(0, -5), ForceMode2D.Impulse);
            } 
    }

    private void moveRight() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    Debug.Log("->");     	
                    rigidBody
                        .AddForce(new Vector2(3, 0), ForceMode2D.Impulse);
                    }    	 
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("colliding!");
        int layer = other.gameObject.layer;
        switch (layer) {
            case LEGS_LAYER:
                Debug.Log("got my feet on the ground"); 
                // Destroy(other.gameObject);
                break;

            case COIN_LAYER:
                Debug.Log("found coin!"); 
                // audioManager.Play("coin");
                FindObjectOfType<AudioManager>().Play("coin");
                FindObjectOfType<ScoreKeeper>().coins += 1;
                // scoreKeeper.score += 1;
                Destroy(other.gameObject);
                break;

            case LAVA_LAYER:
                FindObjectOfType<AudioManager>().Play("lava");
                Debug.Log("fell to lava... dead"); 
                // System.Threading.Thread.Sleep(400);
                Destroy(this.gameObject);
                break;

            case SCORE_LAYER:
                Debug.Log("Score Counting...");
                FindObjectOfType<ScoreKeeper>().clearLevel();
                FindObjectOfType<AudioManager>().Play("clear");
                // Destroy(this.gameObject);
                break;
            
            case END_LAYER:
                Debug.Log("Level Cleared!");
                Destroy(this.gameObject);
                Application.Quit();
                break;
            
            default:
                Debug.Log("Undefined Collision, layer: " + layer);
                break;
        }
    }
}
