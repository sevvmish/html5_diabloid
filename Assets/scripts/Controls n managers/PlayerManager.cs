using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IHitable
{
    private Transform currentTransform;
    private GameObject skin;
    private Animator animator;
    private WeaponManager weaponManager;

    public bool IsCanMove;

    

    // Start is called before the first frame update
    void Start()
    {
        IsCanMove = true;
        weaponManager = GetComponent<WeaponManager>();
        currentTransform = GetComponent<Transform>();
        skin = Instantiate(GameManager.Instance.GetAssetManager.GetGameObjectAsset(1), transform.position, Quaternion.identity, currentTransform);
        
        animator = skin.GetComponent<Animator>();
    }

    public void IdleAnimation()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.Play("Idle");
        }            
    }

    public void RunAnimation()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            animator.Play("Run");
        }            
    }

    public bool Attack(IHitable aim)
    {
        return weaponManager.Attack(aim);
    }

    public bool Attack()
    {
        return weaponManager.Attack();
    }

    //HITABLE============================================================
    public PlayerManager owner { get => this; }
    public float PlayerRadius { get => 1f; }

    public void ReceiveHit()
    {
        print("player " + gameObject.name + " received hit");
        StartCoroutine(receiveHit());
    }
    private IEnumerator receiveHit()
    {
        IsCanMove = false;
        yield return new WaitForSeconds(0.2f);
        IsCanMove = true;
    }
}
