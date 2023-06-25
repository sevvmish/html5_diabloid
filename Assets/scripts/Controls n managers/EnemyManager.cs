using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyManager : MonoBehaviour, IHitable
{
    public Creature EnemyEntity { get; private set; }
    
    public CreatureTypes CreatureType = CreatureTypes.SimpleSkeleton;
    public int Level = 1;

    //skills
    private Skill skillOne;

    // Start is called before the first frame update
    void Start()
    {
        switch(CreatureType)
        {
            case CreatureTypes.SimpleSkeleton:
                EnemyEntity = new SimpleSkeleton(Level);
                EnemyEntity.SetInventory(new Inventory(1,3,5));
                break;
        }

        PlayerCustomization.InitPlayerData(EnemyEntity, gameObject);
        PlayerCustomization.SetSkills(EnemyEntity, gameObject, ref skillOne);

        gameObject.AddComponent<EnemyMovement>();
    }

    public bool SkillOneAttack(IHitable aim)
    {
        return skillOne.ExecuteSkill(aim);
    }

    //IHITABLE==============================================================
    public int OwnerID { get => EnemyEntity.OwnerID; }

    public float PlayerRadius { get => EnemyEntity.BodyRadius; }

    public Transform AimTransform { get => EnemyEntity.PlayerTransform; }

    public int CreatureSide { get => EnemyEntity.CreatureSide; }

    public void ReceiveHit(DamageOutput damage)
    {
        print("damage: " + damage.FinalDamageAmount);
        GameManager.Instance.GetMessageSystem.ShowMessageFrame
            (transform.position + Vector3.up * 1.8f, damage.FinalDamageAmount.ToString("f0"), Color.yellow);
        EnemyEntity.AnimationManager.DamageImpactAnimation();
        EnemyEntity.EffectsManager.PlaySound(SoundsType.meleeImpactMediumMoreBlunt);
    }
}
