using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-50)]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerManager : MonoBehaviour, IHitable
{
    public Creature mainPlayerEntity { get; private set; }    
    //public WeaponTriggerMelee WeaponTriggerMelee { get; private set; }
    
    private GameObject mainWeapon, secondWeapon;
    
    //skills
    private Skill skillOne;

    // Start is called before the first frame update
    void Start()
    {
        mainPlayerEntity = GameManager.Instance.MainPlayerEntity;
        PlayerCustomization.InitPlayerData(mainPlayerEntity, gameObject);

        PlayerCustomization.SetWeaponSkins(mainPlayerEntity, ref mainWeapon, ref secondWeapon);
        PlayerCustomization.SetSkills(mainPlayerEntity, gameObject, ref skillOne);

        print(mainPlayerEntity.CurrentHealth + " = " + mainPlayerEntity.MaxHealth);
    }

    //to delete
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            mainPlayerEntity.ChangeCurrentHP(-7);
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
    public int OwnerID { get => mainPlayerEntity.OwnerID; }
    public float PlayerRadius { get => mainPlayerEntity.BodyRadius; }
    public Transform AimTransform { get => mainPlayerEntity.PlayerTransform; }
    public CreatureSides CreatureSide { get => mainPlayerEntity.CreatureSide; }
    public void ReceiveHit(DamageOutput wd)
    {
        print("damage: " + wd.FinalDamageAmount);
        mainPlayerEntity.EffectsManager.PlayRandomMeleeImpactMedium();
        StartCoroutine(receiveHit());
    }
    private IEnumerator receiveHit()
    {
        mainPlayerEntity.AnimationManager.DamageImpactAnimation();
        yield return new WaitForSeconds(0.2f);
        
    }

    
}
