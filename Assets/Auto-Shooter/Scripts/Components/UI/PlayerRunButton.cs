using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunButton : GameButton
{
    public override void OnPressed()
    {
        base.OnPressed();
        PlayerInputHandler.Instance.IsRunPressed = true;
    }

    public override void OnReleased()
    {
        base.OnReleased();
        PlayerInputHandler.Instance.IsRunPressed = false;
    }
}
