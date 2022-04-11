using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;

    [SerializeField] protected BallController ballController;

    [SerializeField] protected Rigidbody rigidbodyBall;
    [SerializeField] protected Collider colliderBall;
    [SerializeField] protected Collider rageCollider;

    [SerializeField] protected ParticleSystem airPressureParticle;

    //Ball Score
    [HideInInspector] public int score = 0;
    //Reached End Game Score 
    protected int endGameReachedScore = 0;

    protected int endGameHowMuchBallPicked = 0; //If == 9 Reached Final x10

    //Ball Rage
    [Tooltip("Control For Rageing")]
    [HideInInspector]
    public bool isBallRageing = false;

    [Header("Rage Time Scale (\"Takes Second\")")]
    [SerializeField]
    protected float rageTimeScale = 3f;

    protected int ballRageController = 0; //If This > 10 Ball Rage True

    //Set This Number For Change Score Bigger Than 1
    protected int scoreIndexChange = 0;

    [Tooltip("If Game End This Variable = True")]
    protected bool isGameEnd = false;

    [Tooltip("Ball Mesh UI Controller")]
    protected bool isRightBallMesh = true;
    protected bool isLeftMostBallMeshUI = false;
    protected bool isLeftBallMeshUI = false;

    //Controller Script Variables
    protected float sensitivity;
    protected float forwardSpeed;
    protected float jumpForce;

    protected float scaleChangeMultiplier = 0.12f;
    protected float forwardSpeedChangeMultiplier;

    //Start Scale Index X of Ball
    protected float ballScale;

    protected float targetScaleUpper;
    protected float targetScaleDown;

    [Header("Scale Changes")]

    [SerializeField] protected float animChangeScaleUpper;
    [SerializeField] protected float scaleChangeTimeUpper;
    [SerializeField] protected float scaleChangeAnimTimeUpper;

    [SerializeField] protected float animChangeScaleDown;
    [SerializeField] protected float scaleChangeTimeDown;
    [SerializeField] protected float scaleChangeAnimTimeDown;

    [Header("Anim Scale Time When Bigger Scale > 1")]
    [SerializeField] protected float animChangeTimeScaleForBiggest = 0.27f;

    private void Awake()
    {
        ballScale = transform.localScale.x;
    }


    protected void ChangeScoreAndWriteUI(int scoreIndex)
    {
        score += scoreIndex;
        gameManager.scoreText.text = score.ToString();
    }

    protected void BallMeshUIPosControl()
    {
        //Ball Mesh UI Controll
        if (isRightBallMesh && score > 9)
        {
            isRightBallMesh = false;
            gameManager.ballMeshUIPosChange(-0.09f);
        }
        if (!isRightBallMesh && score < 10)
        {
            isRightBallMesh = true;
            gameManager.ballMeshUIPosChange(0.09f);
        }
        else if (score > 20 && !isLeftBallMeshUI)
        {
            isLeftBallMeshUI = true;
            gameManager.ballMeshUIPosChange(-0.1025f);
        }
        else if (score > 99 && !isLeftMostBallMeshUI)
        {
            isLeftMostBallMeshUI = true;
            gameManager.ballMeshUIPosChange(-0.1025f);
        }

        //Ball Mesh UI Control
    }

    public void ScaleAndSpeedChange(int scoreIndexChange)
    {
        this.scoreIndexChange = scoreIndexChange;
        forwardSpeed += forwardSpeedChangeMultiplier * scoreIndexChange;
        ballScale += scaleChangeMultiplier * scoreIndexChange;
    }

    public void ScaleAnimTimeUppersChange(float target)
    {
        scaleChangeAnimTimeUpper = target;
        scaleChangeTimeUpper = target;
    }

    public void BallRageControll(int indexOfPickBall)
    {
        if (ballRageController > 9)
            ballRageController = 0;

        ballRageController += indexOfPickBall;
    }
}
