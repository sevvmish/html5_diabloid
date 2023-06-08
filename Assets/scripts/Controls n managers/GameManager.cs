using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    //todel
    public TextMeshProUGUI texter;

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

        //Screen.SetResolution(1200, 600, true);
        //Application.targetFrameRate = -1;

        mainPlayerTransform = MainPlayer.GetComponent<Transform>();
        mainPlayerRigidbody = MainPlayer.GetComponent<Rigidbody>();
        cameraBody = mainCameraTransform;
        //minimapCameraBody = MinimapCamera.transform;
        //MinimapCamera.orthographicSize = 20;
        MainLight.intensity = 1.0f;

        texter.text = SystemInfo.deviceType.ToString();

        Barbarian b = new Barbarian();
        Creature cr = b;

        print(b.CurrentEnergy + " = " + b.CurrentHealth + " = " + b.StaminaModifier);
        print(cr.CurrentEnergy + " = " + cr.CurrentHealth);
    }

}
