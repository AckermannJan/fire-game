using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    void Awake() {
        current = this;
    }

    // Event for igniting GameObjects
    // The id is the instanceID of the gameObject that should get ignited
    public event Action<int> onIgniteGameObject;
    public void IgniteObject(int id) {
        if (onIgniteGameObject != null) {
            onIgniteGameObject(id);
        }
    }

    // Event for telling that a unit was selected
    public event Action<GameObject> onTriggerUnitSelected;
    public void TriggerUnitSelected(GameObject gameObject) {
        if (onTriggerUnitSelected != null) {
            Debug.Log("Unit selected");
            onTriggerUnitSelected(gameObject);
        }
    }

    // Event for telling that a unit was deselected
    public event Action<GameObject> onTriggerUnitDeselected;
    public void TriggerUnitDeselected(GameObject gameObject) {
        if (onTriggerUnitDeselected != null) {
            Debug.Log("Unit deselected");
            onTriggerUnitDeselected(gameObject);
        }
    }
}
