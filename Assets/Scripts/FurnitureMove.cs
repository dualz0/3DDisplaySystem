using UnityEngine;

public class FurnitureMove : MonoBehaviour
{
    private const float _moveSpeed = 10.0f;
    private const float _rotateSpeed = 100.0f;

    private const float _leftLimit = -9f;
    private const float _rightLimit = 9f;
    private const float _topLimit = 4f;
    private const float _bottonLimit = -4f;

    private Vector3 _moveVector = Vector3.zero;

    private bool _isSelected = true;
    public bool IsSelected
    {
        get { return _isSelected; }
        set { _isSelected = value; }
    }
    
    void Update()
    {
        if (_isSelected)
        {
            // WASD控制上下左右移动
            #region
            float translationZ = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;
            float translationX = Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
            
            // 到达边界则不改变位置
            if ((transform.position.x <= _leftLimit && translationX < 0) || 
                (transform.position.x >= _rightLimit && translationX > 0))
            {
                translationX = 0;
            }

            if ((transform.position.z <= _bottonLimit && translationZ < 0) ||
                (transform.position.z >= _topLimit && translationZ > 0))
            {
                translationZ = 0;
            }

            _moveVector.Set(translationX, 0, translationZ);
            
            transform.position += _moveVector;
            #endregion

            // QE控制左右旋转
            #region
            float rotation = 0.0f;

            if (Input.GetKey(KeyCode.Q))
            {
                // 向左旋转
                rotation -= _rotateSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                // 向右旋转
                rotation += _rotateSpeed * Time.deltaTime;
            }

            transform.Rotate(0, rotation, 0);
            #endregion

            // 按下回车键或 J 键后不再控制当前物体
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.J))
            {
                _isSelected = false;
            }

            // 按下 Esc 键或 BackSpace 键或 Delete 键不再显示该家具
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete))
            {
                gameObject.SetActive(false);
            }

        }
    }
}
