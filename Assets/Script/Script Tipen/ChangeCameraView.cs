using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraView : MonoBehaviour
{
    public RCC_Camera Cam;
    //public enum CameraMode { TPS, FPS, WHEEL, FIXED, CINEMATIC, TOP }
    //public CameraMode cameraMode;

    public void changePerspective(float time)
    {
        StartCoroutine(ChangePerspective(time));
    }
    IEnumerator ChangePerspective(float time)
    {
        Cam.useAutoChangeCamera = false;
        Cam.cameraMode = RCC_Camera.CameraMode.WHEEL;
        yield return new WaitForSeconds(time);
        Cam.cameraMode = RCC_Camera.CameraMode.TOP;
        yield return new WaitForSeconds(time);
        Cam.cameraMode = RCC_Camera.CameraMode.TPS;
    }
}
