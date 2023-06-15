using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-50)]
public class PlayerManager : MonoBehaviour, IHitable
{
    public Creature mainPlayerEntity { get; private set; }
    public Rigidbody playerRigidbody { get; private set; }
    public WeaponTriggerMelee WeaponTriggerMelee { get; private set; }
    
    private Transform currentTransform;
    private Transform handContainer_right, handContainer_left;
    private GameObject skin, mainWeapon, secondWeapon;

    private AnimationManager animationManager;
    private EffectsManager effectsManager;
    private PlayerData playerData;
    private int ownerID;

    public static Transform searched;


    //skills
    private Skill skillOne;

    // Start is called before the first frame update
    void Start()
    {
        ownerID = UnityEngine.Random.Range(-100000, 100000);
        playerData = Globals.MainPlayerData;
        currentTransform = GetComponent<Transform>();
        playerRigidbody = GetComponent<Rigidbody>();
        mainPlayerEntity = Globals.MainPlayerEntity;
        mainPlayerEntity.IsPlayerCanMove = true;
        WeaponTriggerMelee = Weapon.CreateMeleeWeaponTrigger(currentTransform, ownerID, CreatureSides.AllGood);

        setPlayerSkin();

        animationManager = gameObject.AddComponent<AnimationManager>();
        animationManager.SetData(skin.GetComponent<Animator>(), this);

        setWeaponSkins();
        setEffects();
        setSkills();        
    }

    private void setSkills()
    {
        switch (playerData.PlayerClass)
        {
            case 1:
                skillOne = gameObject.AddComponent<SimpleMeleeHit1h>();
                skillOne.SetData(mainPlayerEntity, WeaponTriggerMelee, effectsManager, animationManager.HitAnimation, currentTransform);

                break;
        }
    }

    private void setEffects()
    {
        effectsManager = Instantiate(Resources.Load<GameObject>("full effects"), transform.position, Quaternion.identity, currentTransform).GetComponent<EffectsManager>();
    }

    private void setPlayerSkin()
    {
        skin = Instantiate(AssetManager.GetPlayerSkinByID(playerData.PlayerClass), transform.position, Quaternion.identity, currentTransform);

        ReturnChildOfParent(skin.transform, "hand_container_right");
        handContainer_right = searched;
        searched = null;

        ReturnChildOfParent(skin.transform, "hand_container_left");
        handContainer_left = searched;
        searched = null;
    }

    private void setWeaponSkins()
    {
        if (mainPlayerEntity.MainInventory.MainWeapon != null)
        {
            mainWeapon = Instantiate(AssetManager.GetWeaponByID(mainPlayerEntity.MainInventory.MainWeapon.WeaponSkin),
                Vector3.zero, Quaternion.identity, handContainer_right);
            mainWeapon.transform.localPosition = mainPlayerEntity.MainInventory.MainWeapon.LocalPosition;
            mainWeapon.transform.localEulerAngles = mainPlayerEntity.MainInventory.MainWeapon.LocalRotation;
            mainWeapon.transform.localScale = mainPlayerEntity.MainInventory.MainWeapon.LocalScale;
        }

        if (mainPlayerEntity.MainInventory.SecondWeapon != null)
        {
            secondWeapon = Instantiate(AssetManager.GetWeaponByID(mainPlayerEntity.MainInventory.SecondWeapon.WeaponSkin),
                Vector3.zero, Quaternion.identity, handContainer_left);
            secondWeapon.transform.localPosition = mainPlayerEntity.MainInventory.MainWeapon.LocalPosition;
            secondWeapon.transform.localEulerAngles = mainPlayerEntity.MainInventory.MainWeapon.LocalRotation;
            secondWeapon.transform.localScale = mainPlayerEntity.MainInventory.MainWeapon.LocalScale;
        }
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
        effectsManager.PlayRandomMeleeImpactMedium();
        StartCoroutine(receiveHit());
    }
    private IEnumerator receiveHit()
    {        
        animationManager.DamageImpactAnimation();
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
