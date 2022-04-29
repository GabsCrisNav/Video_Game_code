using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    public static PlayerController sharedInstance;

    public float jumpForce = 30.0f;
    public float runningSpeed = 10.0f;
    private Rigidbody2D rigidBody;
    private Vector3 startPosition;
    public LayerMask groundLayerMask;

    void Awake(){
        sharedInstance = this;
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        this.transform.position = startPosition;
        rigidBody.velocity = new Vector3(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Jump();
            }
        }
    }

    void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame )
        {
            if (rigidBody.velocity.x < runningSpeed && IsOnTheFloor())
            {
                rigidBody.velocity = new Vector3(runningSpeed, rigidBody.velocity.y);
            }
        }

    }

    void Jump(){
        if (IsOnTheFloor())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    bool IsOnTheFloor(){
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 1.1f, groundLayerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void KillPlayer()
    {
        GameManager.sharedInstance.GameOver();
        rigidBody.velocity = new Vector2(0, 0);
        if(PlayerPrefs.GetFloat("highscore",0) < this.GetDistance()){
            PlayerPrefs.SetFloat("highscore", this.GetDistance());
        }
    }

    public float GetDistance(){
        float distanceTravelled = Vector2.Distance(new Vector2(startPosition.x,0), new Vector2(this.transform.position.x,0));
        return distanceTravelled;
    }

}
