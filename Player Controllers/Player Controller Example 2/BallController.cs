using UnityEngine;

public class BallController : BallCollision
{
    private Transform forwardTransform;

    private float horizontalInput;

    private Vector3 moveVec;

    void Start()
    {
        forwardTransform = gameManager.forwardTransform.transform;

        sensitivity = gameManager.sensitivity;
        
        forwardSpeed = gameManager.forwardSpeed;
        forwardSpeedChangeMultiplier = gameManager.forwardSpeedChangeMultiplier;

        jumpForce = gameManager.jumpForce;

        scaleChangeMultiplier = gameManager.ballsScaleChangeMultiplier;
    }

    void Update()
    {
        //Input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            horizontalInput += Input.touches[0].deltaPosition.x / Screen.width;
        else
            horizontalInput = Mathf.Lerp(horizontalInput,0.0f,5.0f * Time.deltaTime); //Reset Horizontal Input With Lerping
    }

    private void FixedUpdate()
    {
        if (isGameEnd)
            return;

        //Physics Movement
          
        moveVec = forwardTransform.forward * forwardSpeed * Time.fixedDeltaTime; //Move Forward By Forward.transforms Forward

        moveVec.y = Mathf.Clamp(rigidbodyBall.velocity.y,-100,1f); //Not Change Y Axis Velocity Need To Jump

        moveVec.x += horizontalInput * sensitivity * Time.fixedDeltaTime; //Horizontal Movement

        rigidbodyBall.velocity = moveVec;
    }
}
