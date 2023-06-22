using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject MainPlayer;
    [SerializeField] private Joystick mainJoystick;
    [SerializeField] private Transform mainCameraTransform;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Light MainLight;
        
    public Creature MainPlayerEntity { get; private set; }
    public Transform mainPlayerTransform { get; private set; }
    public Rigidbody mainPlayerRigidbody { get; private set; }
    public Transform cameraBody { get; private set; }
    public Joystick GetJoystick => mainJoystick;
    public Light GetMainLight => MainLight;
    public Camera GetMainCamera => mainCamera;
    public static GameManager Instance { get; private set; }

    private GameObject TransitionScreen;
    
    // Start is called before the first frame update
    void Awake()
    {
        Globals.GetPlayerEntity();
        MainPlayerEntity = Globals.MainPlayerEntity;
        Globals.IsPlatformMobile = Application.isMobilePlatform;

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        AudioListener.volume = 0.7f;

        //Screen.SetResolution(1200, 600, false);        
        Application.targetFrameRate = -1;
        StartCoroutine(fadeScreen());

        mainPlayerTransform = MainPlayer.GetComponent<Transform>();
        mainPlayerRigidbody = MainPlayer.GetComponent<Rigidbody>();
        cameraBody = mainCameraTransform;
        //minimapCameraBody = MinimapCamera.transform;
        //MinimapCamera.orthographicSize = 20;
        MainLight.intensity = 1.0f;

    }

    private IEnumerator fadeScreen()
    {
        TransitionScreen = Instantiate(Resources.Load<GameObject>("UI/TransitionCanvas"));
        TransitionScreen.transform.GetChild(0).GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 0);
        TransitionScreen.transform.GetChild(0).GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 1);
        yield return new WaitForSeconds(1);

        TransitionScreen.SetActive(false);
    }

}
