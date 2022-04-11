using UnityEngine;

public class PlayerProps : MonoBehaviour
{
    [Header("Connect Go's")]
    [SerializeField]
    protected Transform connectRBGameObject;
    [SerializeField]
    protected Rigidbody connectRigidbody;

    [SerializeField]
    protected GameObject[] blockPrefabs;
    [SerializeField]
    protected LineRenderer lRenderer;
    [SerializeField]
    protected Rigidbody playerRigidbody;
    [SerializeField]
    protected PlayerFollower playerFollower;
    [SerializeField]
    protected GameObject pointPrefab;
    [SerializeField]
    protected GUIController guiController;

    [Header("Diffuculty Of Level Create")]
    [SerializeField]
    protected int diffuculty = 1;

    protected float score;

    protected bool gameOver = false;

    protected void SetScore()
    {
        if (playerRigidbody.velocity.z > 0.0f)
            score += playerRigidbody.velocity.z * Time.fixedDeltaTime * 0.1f;
        guiController.realtimeScoreText.text = score.ToString("0.00");
    }
}
