using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public DungeonCreator dungeonCreator;

    // Start is called before the first frame update
    void Start()
    {
        dungeonCreator.CreateDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
