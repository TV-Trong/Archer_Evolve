using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    public List<LevelUpAttribute> attributes;
    private void Awake()
    {
        LoadAttribute();
    }
    private void LoadAttribute()
    {
        string path = @"Assets/Scripts/Level Up System/Attributes.txt";
        string json = File.ReadAllText(path);
        attributes = JsonUtility.FromJson<AttributeWrapper>($"{{\"attributes\":{json}}}").attributes;
    }
}

[System.Serializable]
public class AttributeWrapper
{
    public List<LevelUpAttribute> attributes;
}
[System.Serializable]
public class LevelUpAttribute
{
    public string name;
    public string description;
    public AttributeType type;
    public float value;
    public int rarity;
}

public enum AttributeType
{
    Strength,
    Speed,
    FireRate,
    CriticalChance,
    CriticalDamage,
    Health,
    Defense,
    Regeneration
}
