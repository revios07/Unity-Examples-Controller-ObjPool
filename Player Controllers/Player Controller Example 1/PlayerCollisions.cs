using UnityEngine;

public class PlayerCollisions : PlayerProps
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Block") && !gameOver)
        {
            gameOver = true;

            guiController.ScoreWrite(score);

            if (score > PlayerPrefs.GetFloat("HighScore", 0.0f))
            {
                PlayerPrefs.SetFloat("HighScore", score);
                guiController.HighScoreWrite(score);
            }
            else
            {
                guiController.HighScoreWrite(PlayerPrefs.GetFloat("HighScore",0));
            }

            guiController.GameOverScreen(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Point"))
        {
            if (transform.position.y - other.transform.position.z < 0.5f)
            {
                score += 10f;
            }
            else
            {
                score += 5f;
            }

            other.gameObject.SetActive(false);

            Debug.Log("xxxx");
        }
    }
}
