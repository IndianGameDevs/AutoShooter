using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpButton : GameButton
{
    private bool IsPressed;

    public override void OnPressed()
    {
        base.OnPressed();

        if (IsPressed) return;

        PlayerInputHandler.Instance.IsJumpPressed = true;
        IsPressed = true;
        StartCoroutine(TurnOff());
    }
    public override void OnReleased()
    {
        base.OnReleased();
        PlayerInputHandler.Instance.IsJumpPressed = false;
        IsPressed = false;
    }

    private IEnumerator TurnOff()
    {
        yield return new WaitForEndOfFrame();
        PlayerInputHandler.Instance.IsJumpPressed = false;
    }
}
