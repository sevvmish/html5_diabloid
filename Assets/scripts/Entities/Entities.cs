using System.Collections;
using System.Collections.Generic;

public abstract class Creature
{
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public CreatureTypes CreatureType { get; private set; }
    public float CurrentSpeed { get; private set; }
    public float MaxSpeed { get; private set; }
    public MainPlayerClasses MainPlayerClass { get; private set; }
    public int Strength { get; private set; }
    public int Agility { get; private set; }
    public int Intellect { get; private set; }
    public int Stamina { get; private set; }
}

public class Barbarian : Creature
{
    public Barbarian()
    {
    }
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
