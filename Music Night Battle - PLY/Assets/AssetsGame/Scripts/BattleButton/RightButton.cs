using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightButton : BattleButton
{
    public override void Initialized()
    {
        buttonType = ButtonType.Right;
        base.Initialized();
    }
}
