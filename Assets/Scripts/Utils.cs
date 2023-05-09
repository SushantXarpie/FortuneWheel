using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera camera,Vector3 position)
    {
        position.z = camera.nearClipPlane; // Set this to be the distance you want the object to be placed in front of the camera.
        return camera.ScreenToWorldPoint(position);
    }
}
