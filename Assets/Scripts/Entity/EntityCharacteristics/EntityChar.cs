using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityChar", menuName = "Ibasho Protocol/Entity Data")]
public class EntityChar : ScriptableObject
{
    public int baseHp;
    public int baseArmor;
    public int baseDamage;
    public string unlocalizedName;
    public AnimationClip idleAnimation;
    public AnimationClip attackAnimation;
    public List<string> abilities;
}
