using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    private static MiniMap instance;

    public static MiniMap Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MiniMap>();
            }

            return instance;
        }
    }
    public Range miniMapSizeX;
    public Range miniMapSizeY;

    public Range MapSizeX;
    public Range MapSizeY;

    [SerializeField] private MiniMapUIIcon iconRef;

    private MiniMapUIIcon[] m_MiniMapIcons;
    private MiniMapItem[] miniMapItems;

    private RectTransform playerIcon;
    public RectTransform miniMap;
    Transform player;

    public int maxCount;

    private IEnumerator Start()
    {
        m_MiniMapIcons = new MiniMapUIIcon[maxCount];

        for (int i = 0; i < maxCount; i++)
        {
            yield return new WaitForEndOfFrame();
            m_MiniMapIcons[i] = Instantiate(iconRef, miniMap);
        }

        MiniMapItem[] items = FindObjectsOfType<MiniMapItem>();

        miniMapItems = new MiniMapItem[items.Length];

        for (int i = 0; i < m_MiniMapIcons.Length; i++)
        {
            MiniMapUIIcon icon = m_MiniMapIcons[i];

            if (i >= items.Length)
            {
                icon.gameObject.SetActive(false);
            }
            else
            {
                MiniMapItem item = items[i];
                icon.Set(item.miniMapIcon, item.transform, item.IsPlayer);
                if(item.IsPlayer)
                {
                    player = item.transform;
                    playerIcon = icon.rectTransform;
                }
                icon.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (player == null) return;
        foreach (MiniMapUIIcon icon in m_MiniMapIcons)
        {
            if (!icon.IsPlayer)
                icon.UpdatePosition(MapSizeX, miniMapSizeX, MapSizeY, miniMapSizeY);
        }

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
