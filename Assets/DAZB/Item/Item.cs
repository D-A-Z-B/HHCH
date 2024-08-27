using UnityEngine;

public abstract class Item : ScriptableObject {
    public Item TempItemRegister;

    public abstract void TempItemApply();
    public abstract void ItemApply();
}