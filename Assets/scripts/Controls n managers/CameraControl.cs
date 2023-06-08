using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private readonly Vector3 defaultCameraPosition = new Vector3(0, 6, -3.5f);
    private readonly Vector3 defaultCameraRotation = new Vector3(60, 0, 0);

    private readonly Vector3 defaultMinimapCameraPosition = new Vector3(0, 20, 0);
    private readonly Vector3 defaultMinimapCameraRotation = new Vector3(90, 0, 0);

    private Transform cameraBody;
    //private Transform minimapCameraBody;
    private Transform mainPlayerTransform;

    void Start()
    {
        cameraBody = GameManager.Instance.cameraBody;
        //minimapCameraBody = GameManager.Instance.minimapCameraBody;
        mainPlayerTransform = GameManager.Instance.mainPlayerTransform;

        cameraBody.position = defaultCameraPosition;
        cameraBody.rotation = Quaternion.Euler(defaultCameraRotation);

        //minimapCameraBody.position = defaultMinimapCameraPosition;
        //minimapCameraBody.rotation = Quaternion.Euler(defaultMinimapCameraRotation);
    }

    private void LateUpdate()
    {        
        cameraBody.position = mainPlayerTransform.position + defaultCameraPosition;
        //minimapCameraBody.position = mainPlayerTransform.position + defaultMinimapCameraPosition;
    }
}
