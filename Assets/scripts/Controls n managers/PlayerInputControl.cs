using DG.Tweening;
using System.Collections;
using UnityEngine;

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

    void Start()
    {
        joystick = GameManager.Instance.GetJoystick;
        playerRigidbody = GameManager.Instance.mainPlayerRigidbody;
        mainPlayerTransform = GameManager.Instance.mainPlayerTransform;
        mainCamera = GameManager.Instance.GetMainCamera;
    }
       
    private void FixedUpdate()
    {        
        if (joystick.Direction.magnitude > 0.1f)
        {
            mainPlayerTransform.DOLocalRotate(new Vector3(0, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * 180 / Mathf.PI, 0), 0.1f);
            if (playerRigidbody.velocity.magnitude < 5)
            {
                playerRigidbody.velocity = mainPlayerTransform.forward * 5;
            }
            
            isJoystick = true;
            isStopped = false;
        } 
        else if (Input.GetMouseButton(0) && joystick.Direction == Vector2.zero)
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, cameraRayCast))
            {
                isJoystick = false;

                if (hit.collider.gameObject.layer.Equals(7))
                {
                    destinationPoint = hit.point;                    
                }
                else if (TryGetComponent(out IHitable hitable))
                {

                }
                
            }
        }

        float distance = (mainPlayerTransform.position - destinationPoint).magnitude;

        if (distance > 0.1f && !isJoystick) 
        {
            transformation(destinationPoint);
            isStopped = false;
        }
        else if ((distance <= 0.1f && !isJoystick) || (isJoystick && joystick.Direction == Vector2.zero))
        {
            if (!isStopped)
            {
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
                isStopped = true;
            }            
        }
    }

    private void transformation(Vector3 point)
    {        
        Vector3 dir = (point - mainPlayerTransform.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(dir);
        if (mainPlayerTransform.rotation != _lookRotation)
        {
            //mainPlayerTransform.rotation = _lookRotation;
            mainPlayerTransform.DORotateQuaternion(_lookRotation, 0.1f);
        }
        
        if (playerRigidbody.velocity.magnitude<5)
        {
             playerRigidbody.velocity = mainPlayerTransform.forward * 5;
        }        
    }


}
