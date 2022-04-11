using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]

public class PlayerController : PlayerCollisions
{
    //For Game Started Control
    private bool isGameStart;

    //Joint Inside This Ref
    HingeJoint[] hingeJoints = new HingeJoint[1];

    [Tooltip("Anchor Connect Pos")]
    private Vector3 connectPos;

    //For Line Renderer
    private bool anchorConnectLineRenderer;

    //For Fixed Update Call Func
    private bool anchorConnectJoint;

    void Awake()
    {
        //Create Level by 40 Boxes
        BlockCreator.GetSingleton().Initialize(42, blockPrefabs, pointPrefab, diffuculty);

        //Camera Load
        playerFollower.SetPosition(transform);
    }

    private void Update()
    {
        SetScore();
    }

    //Physic System
    private void FixedUpdate()
    {
        if (anchorConnectJoint)
        {
            StartCoroutine(WaitForConnect(0.2f));
        }
    }
    private void LateUpdate()
    {
        LineRendererControl();
    }

    //When Click Down
    public void PointerDown()
    {
        if (!isGameStart)
        {
            //First Anchor Except Object Pooling
            FindRelativePosForSpringJoint(new Vector3(0, 0, -1));
            anchorConnectLineRenderer = true;

            guiController.holdStartText.SetActive(false);

            playerRigidbody.useGravity = true;
            isGameStart = true;
        }
        //Wait 0.2 Sec's For Connect Anchor
        else if (playerRigidbody.velocity.z > 0.25f && !anchorConnectJoint)
        {
            anchorConnectJoint = true;
        }
    }

    //When Click Release
    public void PointerUp()
    {
        //Destroy Joint
        Destroy(hingeJoints[0]);

        //Line Renderer
        lRenderer.SetPosition(1, Vector3.zero);
        anchorConnectLineRenderer = false;

        //Add Force Up For Dynamic Play
        //Add Force Back For Slow Down Ball
        playerRigidbody.AddForce(Vector3.up * 30f + Vector3.forward * -10f);
    }


    public void FindRelativePosForSpringJoint(Vector3 blockPosition)
    {
        //For First Click Still Roping
        Destroy(hingeJoints[0]);

        //Add Hinge
        hingeJoints[0] = gameObject.AddComponent<HingeJoint>();

        //Down Of 5 unit y for; Upper Blocks Down bound
        blockPosition.y -= 5f;

        //Connector Gameobjects Transform.position = blockPosition
        connectRBGameObject.position = blockPosition;

        //Connect Hinge Joint To HingeJointConnectorRigidbody
        hingeJoints[0].connectedBody = connectRigidbody;

        //Motor Set Props
        var motor = hingeJoints[0].motor;
        motor.freeSpin = true;

        motor.force = 30f;
        motor.targetVelocity = 80f;

        hingeJoints[0].motor = motor;
        hingeJoints[0].useMotor = true;
    }

    private IEnumerator WaitForConnect(float sec = 0.2f)
    {
        //Block Pos +2 When Moving Fast
        if (playerRigidbody.velocity.z > 12)
            connectPos = BlockCreator.GetSingleton().GetRelativeBlock((((int)transform.position.z) + 5) % 41).position;
        
        else if(connectRigidbody.velocity.z > 8)
            connectPos = BlockCreator.GetSingleton().GetRelativeBlock((((int)transform.position.z) + 4) % 41).position;
        
        else
            connectPos = BlockCreator.GetSingleton().GetRelativeBlock((((int)transform.position.z) + 3) % 41).position;

        
        yield return new WaitForSeconds(sec);

        //Hinge Joint Create
        FindRelativePosForSpringJoint(connectPos);

        //Show Line Renderer
        anchorConnectLineRenderer = true;
        lRenderer.positionCount = 2;

        //Its Done Cancel
        anchorConnectJoint = false;
    }
    private void LineRendererControl()
    {
        if (!anchorConnectLineRenderer) return;
        lRenderer.SetPosition(1, connectRBGameObject.position - transform.position);
    }
}
