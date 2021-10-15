using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureMove : MonoBehaviour
{
    [SerializeField] private bool isSelected = true;
    public bool IsSelected
    {
        get { return isSelected; }
        set { isSelected = value; }
    }

    private float moveSpeed = 10.0f;
    private float rotateSpeed = 100.0f;

    private Vector3 moveVector = Vector3.zero;

    void Update()
    {
        if (isSelected)
        {
            // WASD控制上下左右移动
            #region
            float translationZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            float translationX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            
            if (transform.position.x <= -9 && translationX < 0) 
                translationX = 0;
            else if (transform.position.x >= 9 && translationX > 0)
                translationX = 0;
            
            if (transform.position.z <= -4 && translationZ < 0)
                translationZ = 0;
            else if (transform.position.z >= 4 && translationZ > 0)
                translationZ = 0;

            moveVector.Set(translationX, 0, translationZ);
            
            //以本地坐标系移动
            //transform.localPosition += moveVector;
            
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

            // 按下 Esc 键或 BackSpace 键或 Delete 键删除该家具
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete))
            {
                // 由于会引起 child count 的变化，此处直接隐藏 Game Object
                // GameObject.Destroy(gameObject);
                gameObject.SetActive(false);
            }

        }
    }
}
