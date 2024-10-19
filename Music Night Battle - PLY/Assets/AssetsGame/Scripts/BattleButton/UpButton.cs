using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpButton : BattleButton
{
    public override void Initialized()
    {
        buttonType = ButtonType.Up;
        base.Initialized();
    }
}
