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

    public override void UpdateAction(MusicNoteType musicNoteType)
    {
        switch (musicNoteType)
        {
            case MusicNoteType.OpponentLeft:
                ChangeStateAction(ActionState.Left);
                break;
            case MusicNoteType.OpponentDown:
                ChangeStateAction(ActionState.Down);
                break;
            case MusicNoteType.OpponentUp:
                ChangeStateAction(ActionState.Up);
                break;
            case MusicNoteType.OpponentRight:
                ChangeStateAction(ActionState.Right);
                break;
        }
    }
}
