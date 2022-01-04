using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public Transform cameraPoints;
 
    private LayerMask _layerMask = 1 << 8;   // 打开第 8 层
    
    private GameObject _line;

    private const float _rightLimit = 10f;
    private const float _leftLimit = -10f;
    private const float _topLimit = 5f;
    private const float _bottomLimit = -5f;

    private const float _cameraDistance = 1000f;
    private const float _downUnit = 10f;
    private const float _cameraFieldOfView = 60f;
    
    private int _pointCount = 0;
    
    private bool _isDraw = true;
    
    void Start()
    {
        _line = GameObject.FindGameObjectWithTag("Line");
        cameraPoints = GameObject.Find("----- CameraPoints ------").transform;
        
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _cameraDistance, _layerMask))
            {
                if (hit.point.x >= _rightLimit || hit.point.x <= _leftLimit || 
                    hit.point.z >= _topLimit || hit.point.z <= _bottomLimit)
                    return;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
                Object buttonPrefab = Resources.Load(MyConst.RESOURCES_CAMERA_POINT_PREFAB_PATH) as GameObject;
                GameObject go = Instantiate(buttonPrefab, hit.point, transform.rotation) as GameObject;
                go.transform.SetParent(cameraPoints);
                go.GetComponent<Renderer>().material.color = Color.red;                                                                                                                                                                                                                                                            

                // 画线
                _line.GetComponent<LineRenderer>().positionCount = _pointCount + 1;
                _line.GetComponent<LineRenderer>().SetPosition(_pointCount++, go.transform.position);
            }
        }    
    }

    private void OnClick()
    {
        if (_pointCount > 1)
        {
            // 进入播放阶段
            // 隐藏线
            _line.SetActive(false);

            int count = cameraPoints.childCount;
            for (int i = 0; i < count; ++i)
            {
                // 把小球隐藏到平面下
                cameraPoints.GetChild(i).transform.localPosition += Vector3.down * _downUnit;
            }
            
            Camera.main.orthographic = false;
            Camera.main.fieldOfView = _cameraFieldOfView;
            Camera.main.GetComponent<CameraController>().IsMove = true;
            Camera.main.GetComponent<CameraController>().MoveInit();
              
            gameObject.SetActive(false);
        }
    }
}
