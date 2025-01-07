using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Mode{
    LookAt,//正方向
    LookAtInverted,//相反方向
    CameraForward,//摄像机正方向
    CameraForwardInverted//摄像机反方向
}      
public class LookAtCemare : MonoBehaviour
{

    [SerializeField]private Mode mode=Mode.LookAt;
   void LateUpdate()
   {
       switch (mode)
       { 
        case Mode.LookAt:
            transform.LookAt(Camera.main.transform);
        break;
        case Mode.LookAtInverted:
            Vector3 dirFromCamera=transform.position-Camera.main.transform.position;
            transform.LookAt(transform.position+dirFromCamera);
        break;
        case Mode.CameraForward:
            transform.forward=Camera.main.transform.forward;
        break;
        case Mode.CameraForwardInverted:
            transform.forward=-Camera.main.transform.forward;
        break;
       }
   }
}
