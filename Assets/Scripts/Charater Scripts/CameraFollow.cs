using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject tPlayer;
    private Transform tFollowTarget;
    private CinemachineVirtualCamera vcam;
    
    private Cinemachine3rdPersonFollow rdPerson;
    public TextAsset cameraJson;
    void Start()
    {
        this.vcam = GetComponent<CinemachineVirtualCamera>();
        this.rdPerson = this.vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        this.tPlayer = GameObject.Find("Player");
        tFollowTarget = tPlayer.transform;
        vcam.LookAt = tFollowTarget;
        vcam.Follow = tFollowTarget;

        var cameraPosition = CameraInfo.CreateFromJSON(cameraJson.ToString());
        rdPerson.ShoulderOffset = cameraPosition.GetCameraOffset();
        rdPerson.CameraDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
