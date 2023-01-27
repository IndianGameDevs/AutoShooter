using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public abstract class GameButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IPointerClickHandler
{
    public Image graphicImage;

    [Range(0.0f, 0.3f)]
    public float transitionTime;

    public virtual void OnPressed()
    {
        graphicImage.transform.DOScale(Vector3.one * 1.2f, transitionTime);
    }
    public virtual void OnReleased()
    {
        graphicImage.transform.DOScale(Vector3.one, transitionTime);
    }

    public virtual void OnClick()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPressed();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnReleased();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }
}
