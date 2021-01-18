using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTrigger : MonoBehaviour
{
    // Set an event to trigger following the player.
    [SerializeField] private TriggerEvent stay = new TriggerEvent();

    // Once it's triggered, it keeps triggering while colliding.
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Player is inside!");
        stay.Invoke(other);
    }

    // An event handler with receiveing collider object
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {
    }
}
