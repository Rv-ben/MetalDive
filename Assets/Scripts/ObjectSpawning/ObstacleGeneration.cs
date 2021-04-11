using UnityEngine;
using System.Collections.Generic;
using System;

public class ObstacleGeneration
{
    public float minimumSpace;
    public int maximumObjectNumber;
    
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
        Vector2 elevatorPosition = new Vector2(0f, 0f);
        bool hasElevator = false;

        int elevatorRoom = UnityEngine.Random.Range(0, listOfRooms.Count);  // randomly selects a room to spawn elevator.
        
        for (int i = 0; i < listOfRooms.Count; i++)     // spawn objects in each room
        {
            if (i == elevatorRoom) // spawn Elevator
            {
                elevatorPosition = GetRandomPosition(listOfRooms[i]);
                spawner.SpawnMapAsset(elevatorPosition, MapAssetEnum.Elevator);
                hasElevator = true;
            }

            int objectNum = UnityEngine.Random.Range(1, maximumObjectNumber); // a number of object to spawn in the room.

            Get_Coordinates(listOfRooms[i], objectNum, hasElevator, true, elevatorPosition.x); // updates x_coordinates
            Get_Coordinates(listOfRooms[i], objectNum, hasElevator, false, elevatorPosition.y); // updates y_coordinates

            GetVector2(this.x_coordinates, this.y_coordinates); // updates objectPositions

            for (int j = 0; j < objectNum; j++)  // spawn objects in the current room
            {
                // randomly pick object from MapAssetEnum except Elevator
                MapAssetEnum enum_ = (MapAssetEnum)UnityEngine.Random.Range(1, Enum.GetNames(typeof(MapAssetEnum)).Length);
                spawner.SpawnMapAsset(this.objectPositions[j], enum_);
            }

        }
    }

    /// <summary>
    /// Get a random coordinate within on the current room
    /// </summary>
    /// <param name="topLeft">topLeft value of x or y</param>
    /// <param name="widthLength">width or length</param>
    /// <returns>a random x or y coordinate</returns>
    private float GetRandom_Coordinate(float topLeft, float widthLength) 
    { 
        return topLeft + UnityEngine.Random.Range(0, widthLength);
    }

    /// <summary>
    /// Receive a roomNode and boolean for if its x or y.
    /// Pass a correct coordinate to GetRandom_Coordinate() based on x or y.
    /// </summary>
    /// <param name="room">a current roomNode</param>
    /// <param name="isX">boolean for x or y</param>
    /// <returns>a random x or y coordinate</returns>
    private float GetCoordinatesXY(RoomNode room, bool isX)
    {
        if (isX)
        {
            return GetRandom_Coordinate(room.topLeft.x, room.width);
        }
        else
        {
            return GetRandom_Coordinate(room.topLeft.y, room.length);
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
            x_coordinates.Add(num);
        }
        else
        {
            y_coordinates.Add(num);
        }
    }

    /// <summary>
    /// Sort coordinates x_coordinates or y_coordinates
    /// </summary>
    /// <param name="isX">boolean for x or y</param>
    private void SortCoordinate(bool isX)
    {
        if (isX)
        {
            x_coordinates.Sort();
        }
        else
        {
            y_coordinates.Sort();
        }
    }

    /// <summary>
    /// Get a previous coordinate from x_coordinates or y_coordinates
    /// </summary>
    /// <param name="j">index of the list</param>
    /// <param name="isX">boolean for x or y</param>
    /// <returns>a previous coordinate</returns>
    private float GetPreviousCoordinate(int j, bool isX)
    {
        if (isX)
        {
            return x_coordinates[j - 1];
        }
        else
        {
            return y_coordinates[j - 1];
        }
    }

    /// <summary>
    /// Generate random coordinates for x and y.
    /// Each coordinate compared with existing coordinates to make sure there are enough space between those.
    /// </summary>
    /// <param name="room">roomNode</param>
    /// <param name="objectNum">a number of object will be spawn in the current roomNode</param>
    /// <param name="hasElevator">boolean for Elevator existance</param>
    /// <param name="isX">boolean for x or y</param>
    /// <param name="elevatorPosition">x or y coordinate of elevator</param>
    private void Get_Coordinates(RoomNode room, int objectNum, bool hasElevator, bool isX, float elevatorPosition) 
    {
        float num = GetCoordinatesXY(room, isX);

        for (int j = 0; j < objectNum; j++)
        {
            if (j == 0)
            {
                if (hasElevator == true)
                {
                    while (Math.Abs(num - elevatorPosition) < minimumSpace)
                    {
                        num = GetCoordinatesXY(room, isX);
                    }
                }
                AddCoordinate(num, isX);
            }
            else
            {
                if (hasElevator == true)
                {
                    float prev = GetPreviousCoordinate(j, isX);
                    while (Math.Abs(num - prev) < minimumSpace || Math.Abs(num - elevatorPosition) < minimumSpace)
                    {
                        num = GetCoordinatesXY(room, isX);
                    }
                }
                AddCoordinate(num, isX);
            }
            SortCoordinate(isX);
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
        float x = room.topLeft.x + UnityEngine.Random.Range(0, room.width);
        float y = room.topLeft.y + UnityEngine.Random.Range(0, room.length);

        return new Vector2(x, y);
    }
}