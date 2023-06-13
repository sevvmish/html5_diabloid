using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Translations", menuName = "Languages", order = 1)]
public class Translation : ScriptableObject
{
    //player classes
    public string BarbarianClassName;
    public string MageClassName;
    public string RogueClassName;

    //weapons
    public string ShortSword;

    //skills
    public string Skill01Name;
    public string Skill01Description;
}
