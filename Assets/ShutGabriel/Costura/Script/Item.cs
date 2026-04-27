using UnityEngine;
public enum SlotTag { None, Head, Chest, Legs, Feet}
[CreateAssetMenu(menuName = "Rpg 2D/Item" )]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public SlotTag itemTag;

}
