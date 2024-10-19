using UnityEngine;

public static class AnimationHashes
{
    public static readonly int PlayerIdle = Animator.StringToHash("player_idle");
    public static readonly int PlayerLeft = Animator.StringToHash("player_left");
    public static readonly int PlayerDown = Animator.StringToHash("player_down");
    public static readonly int PlayerUp = Animator.StringToHash("player_up");
    public static readonly int PlayerRight = Animator.StringToHash("player_right");

    public static readonly int OpponentIdle = Animator.StringToHash("opponent_idle");
    public static readonly int OpponentLeft = Animator.StringToHash("opponent_left");
    public static readonly int OpponentDown = Animator.StringToHash("opponent_down");
    public static readonly int OpponentUp = Animator.StringToHash("opponent_up");
    public static readonly int OpponentRight = Animator.StringToHash("opponent_right");
}
