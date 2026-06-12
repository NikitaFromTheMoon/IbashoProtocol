using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewAbilityData", menuName = "Ibasho Protocol/Ability")]
public class AbilityData : ScriptableObject
{
    public string abilityName;
    public int manaCost;
    public Sprite sprite;
    public string abilityType;
}
