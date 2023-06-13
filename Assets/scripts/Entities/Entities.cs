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
    public float MaxHealth { get { return Stamina * StaminaModifierForHealth; } }
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
        CurrentHealth = MaxHealth;        
    }

    public Creature() { }

    
}

public class Barbarian : Creature
{    
    public Barbarian() : base
        (
        1, //level
        1, //StrengthModifier
        1, //AgilityModifier
        1, //IntellectModifier
        1, //StaminaModifier
        10, //StaminaModifierForHealth
        1, //MaxSpeed
        0.3f, //body radius
        CreatureTypes.MainPlayer, 
        CreatureSides.AllGood,
        MainPlayerClasses.Barbarian
        )
    {

    }

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
