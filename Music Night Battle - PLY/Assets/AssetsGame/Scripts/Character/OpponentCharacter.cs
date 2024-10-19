using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCharacter : Character
{
    public override void Initialized()
    {
        idleAnim = AnimationHashes.OpponentIdle;
        leftAnim = AnimationHashes.OpponentLeft;
        downAnim = AnimationHashes.OpponentDown;
        upAnim = AnimationHashes.OpponentUp;
        rightAnim = AnimationHashes.OpponentRight;

        base.Initialized();
    }
}
