using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewAbilityData", menuName = "Ibasho Protocol/Ability Data")]
public class AbilityData : ScriptableObject
{
    public string abilityName;
    public int manaCost;
    public Sprite sprite;
    public string abilityType;
    public string durationLeft;
}
