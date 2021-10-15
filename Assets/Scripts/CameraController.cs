using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public Transform cameraPoints;
    [SerializeField] public float speed = 5.0F;

    private int index = 0;

    private bool isMove = false;
    public bool IsMove
    {
        get { return isMove; }
        set { isMove = value; }
    }
    
    private Vector3 velocity = Vector3.zero;
    private Vector3 curPos = Vector3.zero;
    private int count = 0;

    void Start()
    {
        // 相机位置初始化
        transform.LookAt(transform.GetChild(0));

        cameraPoints = GameObject.Find("----- CameraPoints ------").transform;
    }

    private void Update()
    {
        if (isMove)
        {
            Move();
        }
    }

    public void MoveInit()
    {
        count = cameraPoints.childCount;

        curPos.Set(cameraPoints.GetChild(index).position.x, 1, cameraPoints.GetChild(index).position.z);
        ++index;
        
        transform.position = curPos;
        curPos.Set(cameraPoints.GetChild(index).position.x, 1, cameraPoints.GetChild(index).position.z);
        transform.LookAt(curPos);
    }


    public void Move()
    {
        while (index <= count - 1)
        {

            // tagret.localPosition = Vector3.MoveTowards(tagret.localPosition, ways1[index1].localPosition, speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, curPos, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, curPos) < 0.01f)
            {
                transform.position = curPos;
                curPos.Set(cameraPoints.GetChild(index).position.x, 1, cameraPoints.GetChild(index).position.z);
    
                transform.LookAt(curPos);

                index++;
            }
        }
    }
    
}
