using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyConst : MonoBehaviour
{
    public const string RESOURCES_BUTTON_PATH = "Prefabs/ButtonFurniturePrefab";

    public static readonly string[] BUTTON_TEXT = new string[4]
    { 
        "椅子",
        "桌子", 
        "门", 
        "墙",
    };

    public static readonly string[] RESOURCES_FURNITURES_PATH = new string[4]
    {
        "Prefabs/Chair",
        "Prefabs/RoundTable",
        "Prefabs/Door",
        "Prefabs/Wall",
    };

}
