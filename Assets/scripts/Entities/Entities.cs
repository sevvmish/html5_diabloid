using System.Collections;
using System.Collections.Generic;

public abstract class Creature
{
    public int Level { get; private set; } = 1;
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get { return Stamina * StaminaModifierForHealth; } }
    public CreatureTypes CreatureType { get; private set; }
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

    public Creature(
        int Level,
        float StrengthModifier, 
        float AgilityModifier, 
        float IntellectModifier, 
        float StaminaModifier,
        float StaminaModifierForHealth,
        float MaxSpeed,
        CreatureTypes CreatureType,
        MainPlayerClasses MainPlayerClass

        )
    {
        this.Level = Level;
        this.StrengthModifier = StrengthModifier;
        this.AgilityModifier = AgilityModifier;
        this.IntellectModifier = IntellectModifier;
        this.StaminaModifier = StaminaModifier;
        this.StaminaModifierForHealth = StaminaModifierForHealth;
        this.MaxSpeed = MaxSpeed;
        this.CreatureType = CreatureType;
        this.MainPlayerClass = MainPlayerClass;

        CurrentHealth = MaxHealth;        
    }
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
        CreatureTypes.MainPlayer, 
        MainPlayerClasses.Barbarian
        )
    {

    }

}

public interface IHitable
{
    PlayerManager owner { get; }
    float PlayerRadius { get; }
    void ReceiveHit();
}







    
public enum CreatureTypes
{
    MainPlayer,
    Human,
    Skeleton,
    Undead
}

public enum MainPlayerClasses
{
    none,
    Barbarian,
    Mage,
    Rogue
}
