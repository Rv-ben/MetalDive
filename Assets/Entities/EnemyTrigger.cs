using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTrigger : MonoBehaviour
{
    // Set an event to trigger following the player.
    [SerializeField] private TriggerEvent stay = new TriggerEvent();

    // Once it's triggered, it keeps triggering while colliding.
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Player is inside!");
        stay.Invoke(other);
    }

    // An event handler with receiveing collider object
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {
    }


    private void OnTriggerExit(Collider other)
    {
        GameObject player = other.gameObject;
        if (player.CompareTag("Player"))
        {
            Debug.Log("Player left!");
            //em.ChangeMoveType();
        }
    }
}
