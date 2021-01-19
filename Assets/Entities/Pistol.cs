using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    Animator _animator;

    // Upon Awakening, fetch the Animator component and tell the game a Pistol is equipped.
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("PistolEquipped", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
