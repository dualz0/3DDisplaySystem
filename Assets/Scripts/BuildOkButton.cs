using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class BuildOkButton : MonoBehaviour
{
    public Transform furnitureTransform;
    
    public Transform canvas;

    private Vector3 _cameraPos = Vector3.zero;

    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
        _cameraPos.Set(0, 100, 0);
    }
    private void OnClick()
    {
        int index = furnitureTransform.childCount; 
        if (index - 1 >= 0)
            furnitureTransform.GetChild(index - 1).transform.GetComponent<FurnitureController>().IsSelected = false;

        // 初始化播放按钮
        Object buttonPrefab = Resources.Load(MyConst.RESOURCES_BUTTON_PLAY_PATH) as GameObject;
        GameObject go = Instantiate(buttonPrefab, canvas) as GameObject;
        
        // 调整摄像头位置
        Camera.main.transform.position = _cameraPos;
        Camera.main.transform.LookAt(Vector3.zero);
        
        // 关闭家具按钮
        canvas.GetChild(0).gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
