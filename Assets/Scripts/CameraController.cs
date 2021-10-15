using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public Transform cameraPoints;
    [SerializeField] public float speed = 3.0F;

    private int index = 0;

    private bool isMove = false;
    public bool IsMove
    {
        get { return isMove; }
        set { isMove = value; }
    }

    private bool isWalking = false;
    private bool isRotating = false;
    
    private Vector3 velocity = Vector3.zero;
    private Vector3 curPos = Vector3.zero;
    private int count = 0;

    private Quaternion _curRotation;
    private Quaternion _tarRotation;
    private float _rotateSpeed = 0.0f;
    private float _lerpTime = 0.0f;
    
    void Start()
    {
        // 相机位置初始化
        transform.LookAt(transform.GetChild(0));

        cameraPoints = GameObject.Find("----- CameraPoints ------").transform;
    }

    private void Update()
    {
        if (isMove && isWalking)
        {
            Move();
        }
        if (isRotating)
        {
            Rotate();
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
        
        isWalking = true;

        index++;
    }


    public void Move()
    {
        if (index <= count - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, curPos, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, curPos) < 0.01f)
            {
                transform.position = curPos;
                curPos.Set(cameraPoints.GetChild(index).position.x, 1, cameraPoints.GetChild(index).position.z);
    
                // TODO: 设置平滑旋转
                isWalking = false;
                isRotating = true;
                
                _curRotation = transform.rotation;
                transform.LookAt(curPos);
                _tarRotation = transform.rotation;
                transform.rotation = _curRotation;
                float rotateAngle = Quaternion.Angle(_curRotation, _tarRotation);
                _rotateSpeed = 100 / rotateAngle;
                _lerpTime = 0.0f;

                index++;
            }
        }
        else if (index == count)
        {
            transform.position = Vector3.MoveTowards(transform.position, curPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, curPos) < 0.01f)
            {
                index++;
            }
        }
    }

    private void Rotate()
    {
        Debug.Log(_lerpTime);
        if (_lerpTime >= 1)
        {
            transform.rotation = _tarRotation;
            
            isWalking = true;
        }

        _lerpTime += Time.deltaTime * _rotateSpeed;
        transform.rotation = Quaternion.Lerp(transform.rotation, _tarRotation, _lerpTime);
    }
    
}
