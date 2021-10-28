using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraPoints;
    
    private const float _speed = 3.0F;

    private int _index = 0;

    private bool _isMove = false;
    public bool IsMove
    {
        get { return _isMove; }
        set { _isMove = value; }
    }

    private bool _isWalking = false;
    private bool _isRotating = false;
    
    private Vector3 _curPos = Vector3.zero;
    private int _count = 0;
    private const float _posY = 1f;

    private Quaternion _curRotation;
    private Quaternion _tarRotation;
    private const float _rotateSpeedUnit = 100f;
    private float _rotateSpeed = 0.0f;
    private float _lerpTime = 0.0f;
    private float _deltaTime = 0.01f;
    
    void Start()
    {
        // 相机位置初始化
        transform.LookAt(transform.GetChild(0));

        cameraPoints = GameObject.Find("----- CameraPoints ------").transform;
    }

    private void Update()
    {
        if (_isMove && _isWalking)
        {
            Move();
        }
        if (_isRotating)
        {
            Rotate();
        }
}

    public void MoveInit()
    {
        _count = cameraPoints.childCount;

        _curPos.Set(cameraPoints.GetChild(_index).position.x, _posY, cameraPoints.GetChild(_index).position.z);
        ++_index;
        
        transform.position = _curPos;
        _curPos.Set(cameraPoints.GetChild(_index).position.x, _posY, cameraPoints.GetChild(_index).position.z);
        transform.LookAt(_curPos);
        
        _isWalking = true;

        _index++;
    }


    public void Move()
    {
        if (_index <= _count - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, _curPos, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _curPos) < _deltaTime)
            {
                transform.position = _curPos;
                _curPos.Set(cameraPoints.GetChild(_index).position.x, _posY, cameraPoints.GetChild(_index).position.z);
                
                _isWalking = false;
                _isRotating = true;
                
                _curRotation = transform.rotation;
                transform.LookAt(_curPos);
                _tarRotation = transform.rotation;
                transform.rotation = _curRotation;
                float rotateAngle = Quaternion.Angle(_curRotation, _tarRotation);
                _rotateSpeed = _rotateSpeedUnit / rotateAngle;
                _lerpTime = 0f;

                _index++;
            }
        }
        else if (_index == _count)
        {
            transform.position = Vector3.MoveTowards(transform.position, _curPos, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _curPos) < _deltaTime)
            {
                _index++;
            }
        }
    }

    private void Rotate()
    {
        if (_lerpTime >= 1)
        {
            transform.rotation = _tarRotation;
            
            _isWalking = true;
        }

        _lerpTime += Time.deltaTime * _rotateSpeed;
        transform.rotation = Quaternion.Lerp(transform.rotation, _tarRotation, _lerpTime);
    }
    
}
