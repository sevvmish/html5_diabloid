using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject MainPlayer;
    [SerializeField] private Joystick mainJoystick;
    [SerializeField] private Transform mainCameraTransform;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Light MainLight;

    //todel
    public TextMeshProUGUI texter;

    public Transform mainPlayerTransform { get; private set; }
    public Rigidbody mainPlayerRigidbody { get; private set; }
    public Transform cameraBody { get; private set; }
    public Joystick GetJoystick => mainJoystick;
    public Camera GetMainCamera => mainCamera;
    public static GameManager Instance { get; private set; }
    
    // Start is called before the first frame update
    void Awake()
    {
        Globals.GetPlayerEntity();
        Globals.IsPlatformMobile = Application.isMobilePlatform;

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //Screen.SetResolution(1200, 600, false);        
        Application.targetFrameRate = -1;

        mainPlayerTransform = MainPlayer.GetComponent<Transform>();
        mainPlayerRigidbody = MainPlayer.GetComponent<Rigidbody>();
        cameraBody = mainCameraTransform;
        //minimapCameraBody = MinimapCamera.transform;
        //MinimapCamera.orthographicSize = 20;
        MainLight.intensity = 1.0f;

        string message = "";

        if (Application.isMobilePlatform)
        {
            message = "its mobile";
        }
        else
        {
            message = "its PC";
        }

        texter.text = message;
    }

}
