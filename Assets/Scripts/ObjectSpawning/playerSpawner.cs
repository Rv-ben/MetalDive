using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class PlayerSpawner {

    public PlayerSpawner(List<RoomNode> listOfRooms, Spawner spawner) {
        var randomPosition = listOfRooms[0].GetRandomPosition();
        spawner.SpawnCharacter(randomPosition, CharacterEnum.Player);
    }
}