using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownButton : BattleButton
{
    public override void Initialized()
    {
        buttonType = ButtonType.Down;
        base.Initialized();
    }
}
