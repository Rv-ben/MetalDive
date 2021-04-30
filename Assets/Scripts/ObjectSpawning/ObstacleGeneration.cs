using UnityEngine;
using System.Collections.Generic;
using System;

public class ObstacleGeneration
{
    public float minimumSpace;
    public int maximumObjectNumber;
    public float navMeshBoundary = 0.5f;

    private List<Vector2> objectPositions;
    private List<float> x_coordinates;
    private List<float> y_coordinates;

    /// <summary>
    /// Obstacle constructor
    /// </summary>
    /// <param name="listOfRooms">list of RoomNode</param>
    /// <param name="spawner">spawner</param>
    public ObstacleGeneration(List<RoomNode> listOfRooms, Spawner spawner) 
    {
        int elevatorRoom = UnityEngine.Random.Range(0, listOfRooms.Count);  // randomly selects a room to spawn elevator.

        this.objectPositions = new List<Vector2>();
        this.x_coordinates = new List<float>();
        this.y_coordinates = new List<float>();
        
        for (int i = 0; i < listOfRooms.Count; i++)     // spawn objects in each room
        {
            if (i == elevatorRoom)  // spawn Elevator in the Elevator room
            {
                spawner.SpawnMapAsset(GetRandomPosition(listOfRooms[i]), MapAssetEnum.Elevator);
            }
            else                    // spawn non-Elevator objects in a room
            {
                // a number of object to spawn in the room.
                int objectNum = UnityEngine.Random.Range(1, maximumObjectNumber + 1); 

                if (objectNum == 1) // randomly pick an object from MapAssetEnum except Elevator and spawn
                {
                    MapAssetEnum enum_ = (MapAssetEnum)UnityEngine.Random.Range(1, Enum.GetNames(typeof(MapAssetEnum)).Length);
                    spawner.SpawnMapAsset(GetRandomPosition(listOfRooms[i]), enum_);
                }
                else                // randomly generate coordinates for objects and spawn
                {
                    Get_Coordinates(listOfRooms[i], objectNum); // updates x_coordinates & y_coordinates
                    GetVector2(this.x_coordinates, this.y_coordinates); // updates objectPositions

                    for (int j = 0; j < objectNum; j++)  // spawn objects in the current room
                    {
                        // randomly pick object from MapAssetEnum except Elevator
                        MapAssetEnum enum_ = (MapAssetEnum)UnityEngine.Random.Range(1, Enum.GetNames(typeof(MapAssetEnum)).Length);
                        spawner.SpawnMapAsset(this.objectPositions[j], enum_);
                    }
                }
                
            }
            // clear the lists before move to the next room
            this.objectPositions.Clear();
            this.x_coordinates.Clear();
            this.y_coordinates.Clear();
        }
    }

    /// <summary>
    /// Call Generate_Coordinates() for x and y.
    /// </summary>
    /// <param name="room">roomNode</param>
    /// <param name="objectNum">a number of object will be spawn in the current roomNode</param>
    private void Get_Coordinates(RoomNode room, int objectNum) 
    {
        Generate_Coordinates(room.topLeft.x, room.width, objectNum, true);  // updates x_coordinates
        Generate_Coordinates(room.topLeft.y, room.length, objectNum, false);// updates y_coordinates
    }

    /// <summary>
    /// Generate coordinates
    /// </summary>
    /// <param name="topLeft">topLeft value of x or y</param>
    /// <param name="widthLength">width or length</param>
    /// <param name="objectNum">a number of object will be spawn in the current roomNode</param>
    /// <param name="isX">boolean for x or y</param>
    /// 
    /// instantiate x coordinate
    ///  _ _ _ _  
    /// |1|2|3|4|
    /// |_|_|_|_|
    /// |_|_|_|_|
    /// |_|_|_|_|
    /// 
    ///  y coordinate picks random spot.              
    ///  _ _ _ _  
    /// |_|_|3|_|
    /// |_|_|_|4|
    /// |1|_|_|_|
    /// |_|2|_|_|
    private void Generate_Coordinates(float topLeft, float widthLength, int objectNum, bool isX)
    {
        float eachSpace = (Math.Abs(widthLength - topLeft) - 2 * navMeshBoundary) / objectNum;

        for (float i = 1; i <= objectNum; i++)
        {
            if(isX) // x coordinate
            {
                // example: add 2nd coordinate with
                //          topLeft = 0 , i = 2, navMeshBoundary = 0.5, eachSpace = 1
                //          0 + (2 * 1) + 0.5 - (1 / 2) = 2.5 - 0.5 = 2.0
                AddCoordinate(topLeft + (i * eachSpace) + navMeshBoundary - (eachSpace / 2f), isX);
            }
            else    // y coordinate picks random spot.
            {
                float randomY = 0;
                // to avoide the object spawn right next to each other,
                // y coordinate should randomly pick even row when i is even, otherwise pick odd row.
                if ((int)i % 2 == 0)
                {
                    randomY = UnityEngine.Random.Range(1, (objectNum / 2) + 1) * 2;
                }
                else
                {
                    randomY = UnityEngine.Random.Range(1, ((objectNum + 1) / 2) + 1) * 2 - 1;
                }
                AddCoordinate(topLeft + (randomY * eachSpace) + navMeshBoundary - (eachSpace / 2f), isX);
            }
            
        }
    }

    /// <summary>
    /// Add coordinates to x_coordinates or y_coordinates
    /// </summary>
    /// <param name="num">a coordinate</param>
    /// <param name="isX">boolean for x or y</param>
    private void AddCoordinate(float num, bool isX)
    {
        if (isX)
        {
            this.x_coordinates.Add(num);
        }
        else
        {
            this.y_coordinates.Add(num);
        }
    }

    /// <summary>
    /// Build Vector2 from passed two lists
    /// </summary>
    /// <param name="xs">x_coordinates</param>
    /// <param name="ys">y_coordinates</param>
    private void GetVector2(List<float> xs, List<float> ys) 
    {
        for (int i = 0; i < xs.Count; i ++) 
        {
            this.objectPositions.Add(new Vector2(xs[i], ys[i]));
        }
    }

    /// <summary>
    /// Get a random position from the current room without checking space with other object.
    /// </summary>
    /// <param name="room">roomNode</param>
    /// <returns>a Vector2</returns>
    public Vector2 GetRandomPosition(RoomNode room)
    {
        float x = room.topLeft.x + UnityEngine.Random.Range(navMeshBoundary, room.width - navMeshBoundary);
        float y = room.topLeft.y + UnityEngine.Random.Range(navMeshBoundary, room.length - navMeshBoundary);

        return new Vector2(x, y);
    }
}