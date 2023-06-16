using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Vector3 defaultCameraPosition = Vector3.zero;
    private Vector3 defaultCameraRotation = Vector3.zero;

    private Transform cameraBody;
    private Transform mainPlayerTransform;

    private Vector3 cameraSpeed;

    void Start()
    {
        if (Globals.IsPlatformMobile)
        {
            defaultCameraPosition = Globals.defaultCameraPositionMobile;
            defaultCameraRotation = Globals.defaultCameraRotationMobile;
        }
        else
        {
            defaultCameraPosition = Globals.defaultCameraPositionPC;
            defaultCameraRotation = Globals.defaultCameraRotationPC;
        }

        cameraBody = GameManager.Instance.cameraBody;
        mainPlayerTransform = GameManager.Instance.mainPlayerTransform;

        cameraBody.position = defaultCameraPosition;
        cameraBody.rotation = Quaternion.Euler(defaultCameraRotation);
    }

    private void LateUpdate()
    {        
        cameraBody.position = mainPlayerTransform.position + defaultCameraPosition;
    }
}
