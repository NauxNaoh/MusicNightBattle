using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftButton : BattleButton
{
    public override void Initialized()
    {
        buttonType = ButtonType.Left;
        base.Initialized();
    }
}
