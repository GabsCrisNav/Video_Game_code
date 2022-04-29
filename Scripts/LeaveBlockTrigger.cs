using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveBlockTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        LevelGenerator.sharedInstance.addNewBlock();
        LevelGenerator.sharedInstance.RemoveOldBlock();
    }
}
