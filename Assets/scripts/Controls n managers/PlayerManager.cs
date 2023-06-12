using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IHitable
{
    public Creature mainPlayerEntity { get; private set; }

    private Transform currentTransform;
    private GameObject skin;
    private Animator animator;
    private WeaponManager weaponManager;
    private PlayerData playerData;

    public bool IsCanMove { get; private set; }

    //skills
    

    // Start is called before the first frame update
    void Start()
    {
        mainPlayerEntity = Globals.GetPlayerEntity();
        playerData = Globals.MainPlayerData;
        mainPlayerEntity.SetInventory(new Inventory(playerData));

        IsCanMove = true;
        weaponManager = GetComponent<WeaponManager>();
        weaponManager.SetPlayerData(mainPlayerEntity);

        currentTransform = GetComponent<Transform>();
        skin = Instantiate(GameManager.Instance.GetAssetManager.GetPlayerSkinPack(1), transform.position, Quaternion.identity, currentTransform);
        
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

    public bool SkillOneAttack(IHitable aim)
    {
        return weaponManager.Attack(aim);
    }

    public bool SkillOneAttack()
    {
        return weaponManager.Attack();
    }

    public bool SkillTwoAttack()
    {
        //todo
        return false;
    }

    public bool SkillThreeAttack()
    {
        //todo
        return false;
    }

    public bool SkillFourAttack()
    {
        //todo
        return false;
    }

    public bool SkillFiveAttack()
    {
        //todo
        return false;
    }

    //HITABLE============================================================
    public PlayerManager owner { get => this; }
    public float PlayerRadius { get => 1f; }

    public void ReceiveHit(WeaponDamage wd)
    {
        print("player " + gameObject.name + " received hit: " + wd.DamageDistanceType + " = " + wd.DamageType + " = " + wd.DamageAmount);
        StartCoroutine(receiveHit());
    }
    private IEnumerator receiveHit()
    {
        IsCanMove = false;
        yield return new WaitForSeconds(0.2f);
        IsCanMove = true;
    }
}
