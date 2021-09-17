using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventUtill : MonoBehaviour
{

    public Animator myAnim;
    public GameObject PlayerControl;
    public new CameraManager camera;

    public void CameraZoomInSpeed(float f) { camera.zoomSpeed = f; }
    public void CameraZoomIn(float f) { camera.ZoomIn(f); }
    public void CameraZoomOut() { camera.ZoomOut(); }
    public void CameraShakeTrue(float f) { camera.StartShakeOnly(f); }
    public void CameraShakeFalsd() { camera.EndShakeOnly(); }
    public void CameraDfStart(float f) { camera.StartDf(f); }
    public void CameraDfEnd() { camera.EndDf(); }

    public void A_AnimationSpeed(float speed)
    {
        myAnim.SetFloat("AnimationSpeed", speed);
    }

    public void U_TurnbeforeSkill()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);
            PlayerControl.transform.LookAt(new Vector3(pointTolook.x, PlayerControl.transform.position.y, pointTolook.z));
        }
    }
}
