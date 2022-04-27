using System.Collections;
using UnityEngine;

public class FrontObjectDetecter : MonoBehaviour
{
  [SerializeField]
  private GameObject _player;
  
    public bool AnyObjectIsFrontOfPlayer(Transform forwardCheckTransform,float wantedDistance,float wantedDetectAngle)
    {
        Debug.Log("Object Angle: " + angle)

        float angle = Vector3.Angle(forwardCheckTransform.forward, _player.transform.position - forwardCheckTransform.position);
        if (Mathf.Abs(angle) < wantedDetectAngle)
        {
            print("Object in front of the entity: " + angle);
            if (Vector3.Distance(_player.transform.position, _player.transform.position) < wantedDistance)
            {
                //Player On Front Of Object
                return true;
            }
        }

        //Not At Distance And Angle
        return false;
    }
}
