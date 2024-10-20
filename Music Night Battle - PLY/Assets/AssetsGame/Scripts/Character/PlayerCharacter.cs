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

    public override void UpdateAction(MusicNoteType musicNoteType)
    {
        switch (musicNoteType)
        {
            case MusicNoteType.PlayerLeft:
                ChangeStateAction(ActionState.Left);
                break;
            case MusicNoteType.PlayerDown:
                ChangeStateAction(ActionState.Down);
                break;
            case MusicNoteType.PlayerUp:
                ChangeStateAction(ActionState.Up);
                break;
            case MusicNoteType.PlayerRight:
                ChangeStateAction(ActionState.Right);
                break;            
        }
    }
}
