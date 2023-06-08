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


    void Start()
    {
        joystick = GameManager.Instance.GetJoystick;
        playerRigidbody = GameManager.Instance.mainPlayerRigidbody;
        mainPlayerTransform = GameManager.Instance.mainPlayerTransform;
        mainCamera = GameManager.Instance.GetMainCamera;
    }
       
    private void FixedUpdate()
    {        
        if (Mathf.Abs(joystick.Horizontal) > 0.1f || Mathf.Abs(joystick.Vertical) > 0.1f)
        {
            destinationPoint = mainPlayerTransform.position + new Vector3(joystick.Direction.x, 0, joystick.Direction.y)*0.5f;
        } 
        else if (Input.GetMouseButton(0) && joystick.Direction == Vector2.zero)
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, cameraRayCast))
            {
                if (hit.collider.gameObject.layer.Equals(7))
                {
                    destinationPoint = hit.point;
                }
                
            }
        }

        
        if ((new Vector3(mainPlayerTransform.position.x, 0, mainPlayerTransform.position.z) - new Vector3(destinationPoint.x, 0, destinationPoint.z)).magnitude > 0.1f) 
        {
            transformation(destinationPoint);
            isStopped = false;
        }
        else
        {
            if (!isStopped)
            {
                destinationPoint = mainPlayerTransform.position;
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
            StartCoroutine(rotateMainPlayer(_lookRotation));
            //mainPlayerTransform.rotation = _lookRotation;
            //mainPlayerTransform.localEulerAngles = new Vector3(0, mainPlayerTransform.localEulerAngles.y, 0);
        }
        
        if (playerRigidbody.velocity.magnitude<5)
        {
             playerRigidbody.velocity = mainPlayerTransform.forward * 5;
        }        
    }

    IEnumerator rotateMainPlayer(Quaternion q)
    {
        mainPlayerTransform.DORotateQuaternion(q, 0.1f);
        for (float i = 0; i < 0.1f; i += Time.deltaTime)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            mainPlayerTransform.localEulerAngles = new Vector3(0, mainPlayerTransform.localEulerAngles.y, 0);
        }

    }


}
