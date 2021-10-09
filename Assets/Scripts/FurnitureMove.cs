﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureMove : MonoBehaviour
{
    private bool isSelected = true;

    private float moveSpeed = 10.0f;
    private float rotateSpeed = 100.0f;

    private Vector3 moveVector = Vector3.zero;

    void Start()
    {

    }

    void Update()
    {
        if (isSelected)
        {
            // WASD控制上下左右移动
            #region
            float translationZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            float translationX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

            moveVector.Set(translationX, 0, translationZ);

            // 以世界坐标系移动
            transform.position += moveVector;
            #endregion

            // QE控制左右旋转
            #region
            float rotation = 0.0f;

            if (Input.GetKey(KeyCode.Q))
            {
                // 向左旋转
                rotation -= rotateSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                // 向右旋转
                rotation += rotateSpeed * Time.deltaTime;
            }

            transform.Rotate(0, rotation, 0);
            #endregion

            // 按下回车键或 J 键后不再控制当前物体
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.J))
            {
                isSelected = false;
            }

            // 按下 Esc 键或 BackSpace 键或 Delete 键销毁当前物体
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete))
            {
                GameObject.Destroy(gameObject);
            }

        }
    }
}