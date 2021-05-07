using UnityEngine;
using UnityEngine.Events;
using System;

public class TriggerEnemy : MonoBehaviour
{
    [Serializable] public class TriggerEvent : UnityEvent<Collider> { }

    [SerializeField] private TriggerEvent _stay = new TriggerEvent();

    public bool isFollowing;

    void Start()
    {
        isFollowing = false;
    }

    /// <summary>
    /// Event function that triggers by colliding objects.
    /// If passed collider is tagged as "Player", then enemy will start follow.
    /// </summary>
    /// <param name="other">object that touched this collider</param>
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _stay.Invoke(other);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowing = true;
            _stay.Invoke(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowing = false;
            _stay.Invoke(other);
        }
    }

}
