using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCameraInit : MonoBehaviour
{
    void Start()
    {
        transform.LookAt(transform.GetChild(0));
    }
}
