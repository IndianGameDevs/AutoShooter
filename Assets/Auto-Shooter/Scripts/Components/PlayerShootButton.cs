using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootButton : GameButton
{
    public override void OnPressed()
    {
        base.OnPressed();
        PlayerInputHandler.Instance.IsShootPressed = true;
    }

    public override void OnReleased()
    {
        base.OnReleased();
        PlayerInputHandler.Instance.IsShootPressed = false;
    }
}
