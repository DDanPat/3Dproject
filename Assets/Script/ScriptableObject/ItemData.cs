using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable,
    Resource,
    Buff
}

public enum ConsumableType
{
    Health,
    Hunger
}

public enum BuffType
{
    Speed
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[Serializable]
public class ItemDataBuff
{
    public BuffType type;
    public float value;
}


[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmout;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Buff")]
    public ItemDataBuff[] buffs;
    public float buffTime;

    [Header("Equip")]
    public GameObject equipPrefab;
}
