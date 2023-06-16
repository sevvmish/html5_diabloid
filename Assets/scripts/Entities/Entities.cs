using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Creature
{
    public int Level { get; private set; } = 1;
    public void SetLevel(int level) => Level = level;

    public int OwnerID { get; private set; }
    public float BodyRadius { get; private set; }

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get { return (minHealthAmount + Stamina * StaminaModifierForHealth); } }
    private readonly float minHealthAmount;
    public void ChangeCurrentHP(float amount) => CurrentHealth += amount;
    
    public CreatureTypes CreatureType { get; private set; }
    public CreatureSides CreatureSide { get; private set; }
    public float CurrentSpeed { get; private set; }
    public float MaxSpeed { get; private set; }
    public MainPlayerClasses MainPlayerClass { get; private set; }
    
    public float Strength { get { return StrengthModifier * Level; } }
    public float StrengthModifier { get; private set; }
    
    public float Agility { get { return AgilityModifier * Level; } }
    public float AgilityModifier { get; private set; }

    public float Intellect { get { return IntellectModifier * Level; } }
    public float IntellectModifier { get; private set; }
    
    public float CurrentEnergy { get; private set; }
    public float MaxEnergy { get; private set; } = 100;

    public float Stamina { get { return StaminaModifier * Level; } }
    public float StaminaModifier { get; private set; }
    public float StaminaModifierForHealth { get; private set; }

    public Inventory MainInventory { get; private set; }
    public void SetInventory(Inventory inventory)
    {
        MainInventory = inventory;
    }

    public Transform PlayerTransform { get; private set; }
    public void SetTransform(Transform _transform) => PlayerTransform = _transform;
    
    public Transform RightHandContainer { get; private set; }
    public void SetRightHandContainerTransform(Transform _transform) => RightHandContainer = _transform;
    public Transform LeftHandContainer { get; private set; }
    public void SetLeftHandContainerTransform(Transform _transform) => LeftHandContainer = _transform;
    
    public Rigidbody PlayerRigidbody { get; private set; }
    public void SetRigidbody(Rigidbody rigidbody) => PlayerRigidbody = rigidbody;

    public WeaponTriggerMelee TriggerMelee { get; private set; }
    public void SetWeaponTriggerMelee(WeaponTriggerMelee melee) => TriggerMelee = melee;

    public AnimationManager AnimationManager { get; private set; }
    public void SetAnimationManager(AnimationManager manager) => AnimationManager = manager;

    public EffectsManager EffectsManager { get; private set; }
    public void SetEffectsManager(EffectsManager effects) => EffectsManager = effects;


    public Creature(
        int Level,
        float StrengthModifier, 
        float AgilityModifier, 
        float IntellectModifier, 
        float StaminaModifier,
        float StaminaModifierForHealth,
        float MaxSpeed,
        float BodyRadius,
        CreatureTypes CreatureType,
        CreatureSides CreatureSide,
        MainPlayerClasses MainPlayerClass

        )
    {
        OwnerID = UnityEngine.Random.Range(-100000, 100000);
        this.Level = Level;
        this.StrengthModifier = StrengthModifier;
        this.AgilityModifier = AgilityModifier;
        this.IntellectModifier = IntellectModifier;
        this.StaminaModifier = StaminaModifier;
        this.StaminaModifierForHealth = StaminaModifierForHealth;
        this.MaxSpeed = MaxSpeed;
        this.BodyRadius = BodyRadius;
        this.CreatureType = CreatureType;
        this.CreatureSide = CreatureSide;
        this.MainPlayerClass = MainPlayerClass;
        MainInventory = new Inventory();
        

        minHealthAmount = 40;
        CurrentHealth = MaxHealth;
    }

    private bool isPlayerCanMove = true;
    public bool IsPlayerCanMove
    {
        get
        {
            return isPlayerCanMove;
        }

        set
        {
            isPlayerCanMove = value;
        }
    }

    private bool isHitting = false;
    public bool IsHitting
    {
        get
        {
            return isHitting;
        }

        set
        {
            if (value)
            {
                isPlayerCanMove = false;
            }
            else
            {
                isPlayerCanMove = true;
            }

            isHitting = value;
        }
    }
    

    public Creature() { }

    
}

public class Barbarian : Creature
{   
    public Barbarian(int level, int playerClass) : base
        (
        level, //level
        1, //StrengthModifier
        1, //AgilityModifier
        1, //IntellectModifier
        1, //StaminaModifier
        10, //StaminaModifierForHealth
        1, //MaxSpeed
        0.4f, //body radius
        CreatureTypes.MainPlayer,
        CreatureSides.AllGood,
        (MainPlayerClasses)playerClass
        )
    {
        
    }

}

public class SimpleSkeleton : Creature
{
    public SimpleSkeleton(int level) : base
        (
        level, //level
        1, //StrengthModifier
        1, //AgilityModifier
        1, //IntellectModifier
        1, //StaminaModifier
        10, //StaminaModifierForHealth
        1, //MaxSpeed
        0.4f, //body radius
        CreatureTypes.Skeleton,
        CreatureSides.AllBad,
        MainPlayerClasses.none
        )
    {

    }

}



public interface IHitable
{
    int OwnerID { get; }
    float PlayerRadius { get; }
    void ReceiveHit(DamageOutput damage);
    Transform AimTransform { get; }
    CreatureSides CreatureSide { get; }
}



public enum CreatureTypes
{
    MainPlayer,
    Human,
    Skeleton,
    Undead
}

public enum CreatureSides
{
    AllGood,
    AllBad
}

public enum MainPlayerClasses
{
    none,
    Barbarian,
    Mage,
    Rogue
}
