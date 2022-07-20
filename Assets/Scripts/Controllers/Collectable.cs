using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
    //Events
    public static event System.Action OnInteract;

    //Methods
    /// <summary>
    /// Interact with the item.
    /// </summary>
    public void Interact()
    {
        CollectableData data = Data.game.collectables.Find(c => c.position == (Vector2)transform.position);
        Data.game.collectables.Remove(data);
        Data.game.score++;

        OnInteract?.Invoke();

        Destroy(gameObject);
    }
}
