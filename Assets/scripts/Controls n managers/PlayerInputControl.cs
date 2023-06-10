using DG.Tweening;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInputControl : MonoBehaviour
{
    private Joystick joystick;
    private Rigidbody playerRigidbody;
    private Transform mainPlayerTransform;
    private Camera mainCamera;

    private Ray ray;
    private RaycastHit hit;
    private readonly float cameraRayCast = 30f;
    private Vector3 destinationPoint;
    private bool isStopped;
    private bool isJoystick;

    private PlayerManager playerManager;
    private float speed = 5;
    private float deltaLimit;


    //todel
    private Vector3 oldPos;

    void Start()
    {
        joystick = GameManager.Instance.GetJoystick;
        playerRigidbody = GameManager.Instance.mainPlayerRigidbody;
        mainPlayerTransform = GameManager.Instance.mainPlayerTransform;
        mainCamera = GameManager.Instance.GetMainCamera;
        
        playerManager = GetComponent<PlayerManager>();
    }
       
    private void FixedUpdate()
    {
        //joystick===================================================
        if (joystick.Direction.magnitude > 0f && playerManager.IsCanMove)
        {
            mainPlayerTransform.DOLocalRotate(new Vector3(0, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * 180 / Mathf.PI, 0), 0.1f);
            if (playerRigidbody.velocity.magnitude < speed)
            {
                playerRigidbody.velocity = mainPlayerTransform.forward * speed;
            }
            isJoystick = true;
            isStopped = false;
        }
        else
        {
            isJoystick = false;

            if (!isStopped)
            {
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
                isStopped = true;
            }
        }

       
        
        //keyboard====================================================        
        if (Input.GetMouseButton(0) && playerManager.IsCanMove && joystick.Direction.magnitude <= 0f)
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, cameraRayCast))
            {
                if (hit.collider.gameObject.layer.Equals(7))
                {
                    destinationPoint = hit.point;
                    deltaLimit = 0.1f;
                }
                else if (hit.collider.GetComponent<IHitable>() != null)
                {
                    IHitable hitable = hit.collider.GetComponent<IHitable>();

                    playerManager.Attack(hitable);
                    destinationPoint = hitable.owner.transform.position;
                    deltaLimit = hitable.PlayerRadius;
                }
            }
        }


        float distance = (new Vector3(mainPlayerTransform.position.x, 0, mainPlayerTransform.position.z)
            - new Vector3(destinationPoint.x, 0, destinationPoint.z)).magnitude;

        if (distance > deltaLimit && playerManager.IsCanMove)
        {
            movementToPoint(destinationPoint);
        }
        else
        {
            if (!isStopped)
            {
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
                isStopped = true;
            }
        }
        

        if (Input.GetKey(KeyCode.A))
        {
            playerManager.Attack();
        }

        //animator data about run-idle
        if (playerRigidbody.velocity.magnitude>0.2f)
        {
            playerManager.RunAnimation();
        }
        else
        {
            playerManager.IdleAnimation();
        }
    }

    private void movementToPoint(Vector3 point)
    {
        isStopped = false;

        Vector2 dir = new Vector2(point.x - mainPlayerTransform.position.x, point.z - mainPlayerTransform.position.z);

        mainPlayerTransform.DOLocalRotate(new Vector3(0, Mathf.Atan2(dir.x, dir.y) * 180 / Mathf.PI, 0), 0.1f);

        if (playerRigidbody.velocity.magnitude < speed)
        {
             playerRigidbody.velocity = mainPlayerTransform.forward * speed;
        }
    }


}
