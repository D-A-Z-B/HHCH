using UnityEngine;

public class FieldItem : MonoBehaviour, IInteractable {
    public Item ItemSO;

    public void Interact() {
        ItemSO.TempItemApply();
    }
}