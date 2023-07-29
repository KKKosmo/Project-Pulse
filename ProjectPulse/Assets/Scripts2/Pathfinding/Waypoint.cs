using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Waypoint : MonoBehaviour
{
    public GUIStyle gui;
    
    void OnDrawGizmos()
    {
        Handles.Label(transform.position, transform.gameObject.name, gui);
    }
}
