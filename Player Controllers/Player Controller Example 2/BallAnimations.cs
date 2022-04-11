using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BallAnimations : Ball
{
    //Big Animation
    protected IEnumerator ChangeScaleToUpper()
    {
        targetScaleUpper = ballScale + animChangeScaleUpper * scoreIndexChange;

        //Anim Effect Time Normal
        if (scoreIndexChange == 1)
        {
            transform.DOScale(targetScaleUpper, scaleChangeTimeUpper);
            yield return new WaitForSeconds(scaleChangeTimeUpper);
        }
        //If Big Effect Triggered So Big Anim Effect Time Scale Decreased by Multiplied Scale Bigger Index
        else
        {
            transform.DOScale(targetScaleUpper - 0.225f * scoreIndexChange, (scaleChangeTimeUpper + animChangeTimeScaleForBiggest));
            yield return new WaitForSeconds((scaleChangeTimeUpper + animChangeTimeScaleForBiggest));
        }

        transform.DOScale(ballScale, scaleChangeAnimTimeUpper);

        yield return new WaitForSeconds(scaleChangeAnimTimeUpper);
    }

    //Small Animation
    protected IEnumerator ChangeScaleToDown()
    {
        targetScaleDown = ballScale + animChangeScaleDown * scoreIndexChange;

        transform.DOScale(targetScaleDown, scaleChangeTimeDown);

        yield return new WaitForSeconds(scaleChangeTimeDown);

        transform.DOScale(ballScale, scaleChangeAnimTimeDown);

        yield return new WaitForSeconds(scaleChangeAnimTimeDown);
    }

    //Faster Move And Eat Everything
    protected IEnumerator RageCall()
    {
        isBallRageing = true;
        rageCollider.enabled = true;
        gameManager.rageParticle.Play();

        ScaleAnimTimeUppersChange(scaleChangeAnimTimeUpper / 1.4f);

        float rageSpeed = forwardSpeed * 2f;

        while (forwardSpeed < rageSpeed)
        {
            forwardSpeed = Mathf.Lerp(forwardSpeed, rageSpeed, 5 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();

        }

        forwardSpeed = rageSpeed;

        yield return new WaitForSeconds(rageTimeScale);

        rageSpeed /= 2;

        while (forwardSpeed > rageSpeed && isBallRageing)
        {
            forwardSpeed = Mathf.Lerp(forwardSpeed, rageSpeed - 2.0f, 5 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        if (!isBallRageing)
            forwardSpeed = rageSpeed;

        ScaleAnimTimeUppersChange(scaleChangeAnimTimeUpper * 1.4f);

        isBallRageing = false;
        rageCollider.enabled = false;
        gameManager.rageParticle.Stop();
    }

    public void ChangeScaleUpperCall()
    {
        StartCoroutine(ChangeScaleToUpper());
    }

    private IEnumerator EndGameAnimation(float waitForSecondsStart)
    {
        yield return new WaitForSeconds(waitForSecondsStart);

        rigidbodyBall.velocity = Vector3.zero;

        while (true)
        {
            airPressureParticle.Play();

            transform.DOMoveY(transform.position.y + 0.8f, 0.5f);

            yield return new WaitForSeconds(0.5f);

            transform.DOMoveY(transform.position.y - 0.8f, 0.45f);

            yield return new WaitForSeconds(0.45f);
        }
    }

    protected void EndGameAnim(float waitForSecStart)
    {
        StartCoroutine(EndGameAnimation(waitForSecStart = 1f));
    }

    protected void MoveToTarget(Transform target)
    {
        float degree = ballScale / 15f;

        Debug.Log(ballScale);

        transform.DOMove(target.transform.position + Vector3.up * degree, 1f);

        transform.DORotateQuaternion(Quaternion.Euler(10000, 100, -500), 2);
    }
}
