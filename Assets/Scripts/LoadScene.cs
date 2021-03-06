using UnityEngine;
using UnityEngine.UI;
public class LoadScene : MonoBehaviour
{
    public Transform furnitureTransform;
    
    private int _iCount = -1;

    void Start()
    {
        LoadButtons();
    }

    private void LoadButtons()
    {
        Object buttonPrefab = Resources.Load(MyConst.RESOURCES_BUTTON_PATH) as GameObject;

        int len = MyConst.BUTTON_TEXT.Length;
        for (int i = 0; i < len; ++i)
        {
            GameObject go = Instantiate(buttonPrefab, transform) as GameObject;

            // 调整 button 文字
            go.transform.GetChild(0).GetComponent<Text>().text = MyConst.BUTTON_TEXT[i];
            Button button = go.GetComponent<Button>();
            int j = i;
            button.onClick.AddListener(delegate ()
            {
                OnClick(j);
            });
        }
    }

    private void OnClick(int i)
    {
        if (_iCount >= 0)
        {
            furnitureTransform.GetChild(_iCount).transform.GetComponent<FurnitureController>().IsSelected = false;
        }

        Object furniturePrefab = Resources.Load(MyConst.RESOURCES_FURNITURES_PATH[i]) as GameObject;
        GameObject go = Instantiate(furniturePrefab, furnitureTransform) as GameObject;
        go.AddComponent<FurnitureController>();

        ++_iCount;
    }
}
