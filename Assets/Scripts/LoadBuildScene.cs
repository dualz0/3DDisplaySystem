using UnityEngine;
using UnityEngine.UI;
public class LoadBuildScene : MonoBehaviour
{
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
        Object furniturePrefab = Resources.Load(MyConst.RESOURCES_FURNITURES_PATH[i]) as GameObject;
        GameObject go = Instantiate(furniturePrefab) as GameObject;
        go.AddComponent<FurnitureMove>();
    }
}
