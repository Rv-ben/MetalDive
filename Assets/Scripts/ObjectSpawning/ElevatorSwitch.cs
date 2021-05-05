using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{
    public bool playerEnter = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerEnter = true;
        }
    }
}
