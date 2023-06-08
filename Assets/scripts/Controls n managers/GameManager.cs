using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public GameObject MainPlayer;
    public Joystick mainJoystick;
    public Transform mainCameraTransform;
    public Camera mainCamera;
    //[SerializeField] private Camera MinimapCamera;
    public Light MainLight;

    public Transform mainPlayerTransform { get; private set; }
    public Rigidbody mainPlayerRigidbody { get; private set; }
    public Transform cameraBody { get; private set; }
    //public Transform minimapCameraBody { get; private set; }
    public Joystick GetJoystick => mainJoystick;
    public Camera GetMainCamera => mainCamera;

    public static GameManager Instance { get; private set; }
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        Screen.SetResolution(1200, 600, true);
        Application.targetFrameRate = -1;

        mainPlayerTransform = MainPlayer.GetComponent<Transform>();
        mainPlayerRigidbody = MainPlayer.GetComponent<Rigidbody>();
        cameraBody = mainCameraTransform;
        //minimapCameraBody = MinimapCamera.transform;
        //MinimapCamera.orthographicSize = 20;
        MainLight.intensity = 1.0f;
    }

}
