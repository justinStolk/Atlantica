using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public Vector3 playerPosition;
    public Vector3 playerRotation;

    public Vector3 backpackPosition;
    public Vector3 backpackRotation;

    public GameData(Vector3 plPosition, Vector3 plRotation, Vector3 bpPosition, Vector3 bpRotation)
    {
        playerPosition = plPosition;
        playerRotation = plRotation;
        backpackPosition = bpPosition;
        backpackRotation = bpRotation;
        
    }


}
