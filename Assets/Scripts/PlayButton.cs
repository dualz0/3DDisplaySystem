using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private LayerMask _layerMask = 1 << 8;   // 打开第 5, 8 层
    public GameObject line;
    [SerializeField] private int _pointCount = 0;
    private bool isDraw = true;
    public Transform cameraPoints;
    
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        
        line = GameObject.FindGameObjectWithTag("Line");

        cameraPoints = GameObject.Find("----- CameraPoints ------").transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;//碰撞点
            if (Physics.Raycast(ray, out hit, 1000, _layerMask))
            {
                if (hit.point.x >= 10 || hit.point.x <= -10 || hit.point.z >= 5 || hit.point.z <= -5)
                    return;
                
                Object buttonPrefab = Resources.Load(MyConst.RESOURCES_CAMERA_POINT_PREFAB_PATH) as GameObject;
                GameObject go = Instantiate(buttonPrefab, hit.point, transform.rotation) as GameObject;
                go.transform.SetParent(cameraPoints);
                go.GetComponent<Renderer>().material.color = Color.red;

                // 画线
                line.GetComponent<LineRenderer>().positionCount = _pointCount + 1;
                line.GetComponent<LineRenderer>().SetPosition(_pointCount++, go.transform.position);
            }
        }    
    }

    private void OnClick()
    {
        if (_pointCount > 1)
        {
            // 进入播放阶段
            line.SetActive(false);

            int count = cameraPoints.childCount;
            for (int i = 0; i < count; ++i)
            {
                cameraPoints.GetChild(i).transform.localPosition += Vector3.down * 10;
            }

            Camera.main.orthographic = false;
            Camera.main.fieldOfView = 60;
            Camera.main.GetComponent<CameraController>().IsMove = true;
            Camera.main.GetComponent<CameraController>().MoveInit();
                
            gameObject.SetActive(false);
        }
    }
}
