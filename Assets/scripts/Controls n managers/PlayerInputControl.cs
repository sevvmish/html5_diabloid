using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputControl : MonoBehaviour
{
    [SerializeField] private Button mainSpellButtonJ;

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
    private Creature player;
    //private float speed = 5;
    private float deltaLimit;
    private LayerMask ignoreMask;

    void Start()
    {
        isJoystick = Globals.IsPlatformMobile;
        joystick = GameManager.Instance.GetJoystick;
        if (!isJoystick) joystick.gameObject.SetActive(false);

        playerRigidbody = GameManager.Instance.mainPlayerRigidbody;
        mainPlayerTransform = GameManager.Instance.mainPlayerTransform;
        mainCamera = GameManager.Instance.GetMainCamera;
        playerManager = GetComponent<PlayerManager>();
        player = playerManager.mainPlayerEntity;

        ignoreMask = LayerMask.GetMask("Trigger", "UI");

        mainSpellButtonJ.onClick.AddListener(() =>
        {
            playerManager.SkillOneAttack(null);
        });
    }
       
    private void FixedUpdate()
    {
        //joystick===================================================
        if (isJoystick)
        {
            if (joystick.Direction.magnitude > 0f && player.IsPlayerCanMove)
            {
                mainPlayerTransform.DOLocalRotate(new Vector3(0, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * 180 / Mathf.PI, 0), 0.1f);
                if (playerRigidbody.velocity.magnitude < player.CurrentSpeed)
                {
                    playerRigidbody.velocity = mainPlayerTransform.forward * player.CurrentSpeed;
                }

                isStopped = false;
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
        }
        

        //keyboard====================================================        
        if (!isJoystick)
        {
            
            if (Input.GetMouseButton(0) && player.IsPlayerCanMove)
            {                
                ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out hit, cameraRayCast, ~ignoreMask))
                {                    
                    if (hit.collider.gameObject.layer.Equals(7))
                    {
                        destinationPoint = hit.point;
                        deltaLimit = 0.1f;
                    }
                    else if (hit.collider.GetComponent<IHitable>() != null)
                    {
                        
                        IHitable hitable = hit.collider.GetComponent<IHitable>();
                        
                        if (hitable.OwnerID != playerManager.OwnerID/* && hitable.CreatureSide != playerManager.CreatureSide*/)
                        {
                            
                            if (!playerManager.SkillOneAttack(hitable))
                            {                                
                                destinationPoint = hit.collider.gameObject.transform.position;
                                deltaLimit = hitable.PlayerRadius;
                            }
                            else
                            {                               
                                destinationPoint = mainPlayerTransform.position;
                            }
                            
                        }     
                        else
                        {
                            destinationPoint = hit.collider.gameObject.transform.position;
                            deltaLimit = hitable.PlayerRadius;
                        }
                    }
                }
            }

            if (Input.GetMouseButton(1))
            {
                playerManager.SkillTwoAttack();
            }


            float distance = (new Vector3(mainPlayerTransform.position.x, 0, mainPlayerTransform.position.z)
                - new Vector3(destinationPoint.x, 0, destinationPoint.z)).magnitude;

            if (distance > deltaLimit && player.IsPlayerCanMove)
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
        }
        
        

        if (Input.GetKey(KeyCode.A))
        {
            playerManager.SkillOneAttack(null);
        }
        
    }

    private void movementToPoint(Vector3 point)
    {        
        isStopped = false;

        Vector2 dir = new Vector2(point.x - mainPlayerTransform.position.x, point.z - mainPlayerTransform.position.z);

        mainPlayerTransform.DOLocalRotate(new Vector3(0, Mathf.Atan2(dir.x, dir.y) * 180 / Mathf.PI, 0), 0.1f);
                
        if (playerRigidbody.velocity.magnitude < player.CurrentSpeed)
        {
             playerRigidbody.velocity = mainPlayerTransform.forward * player.CurrentSpeed;
        }
        
        /*
        if (newSpeed < 0.09f)
        {            
            playerRigidbody.transform.position += (mainPlayerTransform.forward * 0.09f);
        }*/
    }


}
