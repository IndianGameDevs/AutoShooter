using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public Range miniMapSizeX;
    public Range miniMapSizeY;

    public Range MapSizeX;
    public Range MapSizeY;

    public RectTransform playerIcon;
    public RectTransform miniMap;
    Transform player;
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        Vector2 pos;
        pos.x = Method.GetNewValue(MapSizeX.minRange, MapSizeX.maxRange, miniMapSizeX.minRange, miniMapSizeX.maxRange, player.position.x);
        pos.y = Method.GetNewValue(MapSizeY.minRange, MapSizeY.maxRange, miniMapSizeY.minRange, miniMapSizeY.maxRange, player.position.z);

        playerIcon.anchoredPosition = pos;
        playerIcon.rotation = Quaternion.Euler(0, 0, -1 * player.eulerAngles.y);
        miniMap.anchoredPosition = -1 * pos;
    }
}

[System.Serializable]
public struct Range
{
    public float maxRange;
    public float minRange;
}
