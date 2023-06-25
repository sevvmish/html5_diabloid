using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test3 : MonoBehaviour
{
    private Transform main;
    public Transform aim;


    // Start is called before the first frame update
    void Start()
    {
        main = GameManager.Instance.mainPlayerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3.Angle(main.position, aim.position);

        Vector3 dir = aim.position - main.position;
        print(Vector3.Angle(main.forward, dir) + " !!!!!!!!!!");
    }
}
