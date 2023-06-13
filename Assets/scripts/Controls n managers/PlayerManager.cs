using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-50)]
public class PlayerManager : MonoBehaviour, IHitable
{
    public Creature mainPlayerEntity { get; private set; }
    public WeaponTriggerMelee WeaponTriggerMelee { get; private set; }
    
    private Transform currentTransform;
    private GameObject skin;
    private AnimationManager animationManager;
    private PlayerData playerData;
    private int ownerID;

    public static Transform searched;

    public bool IsCanMove { get; private set; }
    public void SetHittingInformer(bool isHitting)
    {
        if (isHitting)
        {
            IsCanMove = false;
        }
        else
        {
            IsCanMove = true;
        }
    }

    //skills
    private Skill skillOne;

    // Start is called before the first frame update
    void Start()
    {
        ownerID = UnityEngine.Random.Range(-100000, 100000);
        playerData = Globals.MainPlayerData;
        currentTransform = GetComponent<Transform>();
        mainPlayerEntity = Globals.MainPlayerEntity;
        WeaponTriggerMelee = Weapon.CreateMeleeWeaponTrigger(currentTransform, ownerID, CreatureSides.AllGood);

        IsCanMove = true;

        switch(playerData.PlayerClass)
        {
            case 1:
                skillOne = gameObject.AddComponent<SimpleMeleeHit1h>();
                skillOne.SetData(mainPlayerEntity, WeaponTriggerMelee, HitAnimation, SetHittingInformer, currentTransform);
             
                break;
        }
        
        skin = Instantiate(GameManager.Instance.GetAssetManager.GetPlayerSkinPack(1), transform.position, Quaternion.identity, currentTransform);
                
        animationManager = gameObject.AddComponent<AnimationManager>();
        animationManager.SetData(skin.GetComponent<Animator>(), this);

        ReturnChildOfParent(this.transform, "R_hand_container");

    }

    public void PlayIdleAnimation()
    {        
        animationManager.IdleAnimation();
    }

    public void PlayRunAnimation()
    {
        if (!IsCanMove) return;
        animationManager.RunAnimation();
    }

    public void HitAnimation()
    {
        animationManager.HitAnimation();
    }



    public bool SkillOneAttack(IHitable aim)
    {        
        return skillOne.ExecuteSkill(aim);
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
    public int OwnerID { get => ownerID;/*mainPlayerEntity.OwnerID;*/ }
    public float PlayerRadius { get => mainPlayerEntity.BodyRadius; }
    public Transform AimTransform { get => currentTransform; }
    public CreatureSides CreatureSide { get => mainPlayerEntity.CreatureSide; }
    public void ReceiveHit(DamageOutput wd)
    {
        print("damage: " + wd.FinalDamageAmount);
        StartCoroutine(receiveHit());
    }
    private IEnumerator receiveHit()
    {        
        yield return new WaitForSeconds(0.2f);
        
    }

    public static void ReturnChildOfParent(Transform parent, string _name)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).name == _name)
            {
                searched = parent.GetChild(i);
            }
            else if (parent.GetChild(i).childCount > 0)
            {
                ReturnChildOfParent(parent.GetChild(i), _name);
            }
        }
    }
}
