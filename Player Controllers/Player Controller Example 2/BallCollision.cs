using UnityEngine;

public class BallCollision : BallAnimations
{
    private void OnTriggerEnter(Collider other)
    {
        if (isBallRageing && (other.gameObject.CompareTag("Score") || other.gameObject.CompareTag("Trap")))
        {
            other.gameObject.GetComponent<Collectable>().ChangeScaleWithFollow(other.transform.localScale.x / 1.5f, transform, ballController);

            airPressureParticle.startSize += (3.75f * scoreIndexChange);
            airPressureParticle.Play();

            gameManager.SetGainScoreParticle(other.gameObject.transform.position);

            ChangeScoreAndWriteUI(scoreIndexChange);
        }
        else if (other.gameObject.CompareTag("Score"))
        {
            other.gameObject.GetComponent<Collectable>().ChangeScaleWithFollow(other.transform.localScale.x / 1.3f, transform, ballController);

            airPressureParticle.startSize += (3.75f * scoreIndexChange);
            airPressureParticle.Play();

            gameManager.SetGainScoreParticle(other.gameObject.transform.position + Vector3.forward * 0.1f);

            ChangeScoreAndWriteUI(scoreIndexChange);

            BallRageControll(scoreIndexChange);

            //Control For Rage
            if (ballRageController > 9 && !isBallRageing)
            {
                isBallRageing = true;
                gameManager.RageAnimActive();
                StartCoroutine(RageCall());
            }

        }
        else if (other.gameObject.CompareTag("Trap"))
        {

            //Play Animation Of Trap Animation
            other.gameObject.GetComponent<CollectableTrap>().ChangeScaleCall(other.transform.localScale.x / 1.3f, out scoreIndexChange);

            airPressureParticle.startSize -= (3.75f * scoreIndexChange);

            ScaleAndSpeedChange(scoreIndexChange);

            gameManager.SetLoseScoreParticle(other.gameObject.transform.position);

            StartCoroutine(ChangeScaleToDown());

            ChangeScoreAndWriteUI(scoreIndexChange);

            BallRageControll(scoreIndexChange);

            //Ball Mesh UI Control
            if (!isRightBallMesh && score < 10)
            {
                isRightBallMesh = true;
                gameManager.ballMeshUIPosChange(0.095f);
            }
        }

        //End Game Run Triggers
        else if (other.gameObject.CompareTag("EndGameSoccerBall"))
        {
            other.enabled = false;

            ChangeScoreAndWriteUI(endGameReachedScore);

            gameManager.ChangeScale(other.transform);

            gameManager.SetGainScoreParticle(other.gameObject.transform.position);

            endGameHowMuchBallPicked += 1;


            if (endGameHowMuchBallPicked == 9)
            {
                //Reached x10 Animation

                isGameEnd = true;

                rigidbodyBall.isKinematic = true;

                MoveToTarget(gameManager.endGameReachedT);

                gameManager.EndGameReachedX10Camera();

                gameManager.rageParticle.Play();

                EndGameAnim(0.8f);

                gameManager.levelManager.NextLevel();
            }
        }
        else if (other.gameObject.CompareTag("EndGameBasketBall"))
        {
            isGameEnd = true;

            other.enabled = false;

            rigidbodyBall.isKinematic = true;

            gameManager.EndGameCamera();

            EndGameAnim(0.1f);

            gameManager.ChangeScale(other.transform);

            gameManager.SetLoseScoreParticle(other.gameObject.transform.position);
        }
        else if (other.gameObject.CompareTag("EndGameRun"))
        {
            other.enabled = false;

            endGameReachedScore = score;

            rageCollider.enabled = false;

            forwardSpeed *= 2.75f;

            sensitivity *= 1.35f;
        }

        //Game End Triggers
        else if (other.gameObject.CompareTag("EndGame") && !isBallRageing)
        {
            colliderBall.enabled = false;

            gameManager.RestartLevel();
        }

        else if (other.gameObject.CompareTag("RageEnder") && isBallRageing)
        {
            other.enabled = false;
            isBallRageing = false;
        }

        BallMeshUIPosControl();
    }
}
