using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapUIIcon : MonoBehaviour
{
    public Image icon;
    public Transform ItemPosition;
    public bool IsPlayer;
    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Set(Sprite s, Transform t, bool IsPlayer)
    {
        this.IsPlayer = IsPlayer;
        icon.sprite = s;
        ItemPosition = t;
    }

    public void UpdatePosition(Range MapSizeX, Range miniMapSizeX, Range MapSizeY, Range miniMapSizeY)
    {
        if (ItemPosition == null) return;
        Vector2 pos;
        pos.x = Method.GetNewValue(MapSizeX.minRange, MapSizeX.maxRange, miniMapSizeX.minRange, miniMapSizeX.maxRange, ItemPosition.position.x);
        pos.y = Method.GetNewValue(MapSizeY.minRange, MapSizeY.maxRange, miniMapSizeY.minRange, miniMapSizeY.maxRange, ItemPosition.position.z);

        rectTransform.anchoredPosition = pos;
        rectTransform.rotation = Quaternion.Euler(0, 0, -1 * ItemPosition.eulerAngles.y);
    }
}
