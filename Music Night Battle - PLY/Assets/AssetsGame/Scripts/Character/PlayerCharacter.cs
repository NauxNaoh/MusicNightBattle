using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public override void Initialized()
    {
        idleAnim = AnimationHashes.PlayerIdle;
        leftAnim = AnimationHashes.PlayerLeft;
        downAnim = AnimationHashes.PlayerDown;
        upAnim = AnimationHashes.PlayerUp;
        rightAnim = AnimationHashes.PlayerRight;

        base.Initialized();
    }
}
