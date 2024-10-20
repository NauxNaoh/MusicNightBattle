using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected CharacterType characterType;
    private ActionState actionState;
    private Animator animator;

    protected int idleAnim;
    protected int leftAnim;
    protected int downAnim;
    protected int upAnim;
    protected int rightAnim;

    private void Awake()
    {
        Initialized();
    }
    public virtual void Initialized()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Cant Animator Component in gameObject");
            return;
        }
        ChangeStateAction(ActionState.Idle);
    }

    public void ChangeStateAction(ActionState state)
    {
        actionState = state;

        switch (actionState)
        {
            case ActionState.None:
                break;
            case ActionState.Idle:
                animator.Play(idleAnim);
                break;
            case ActionState.Left:
                animator.Play(leftAnim);
                break;
            case ActionState.Down:
                animator.Play(downAnim);
                break;
            case ActionState.Up:
                animator.Play(upAnim);
                break;
            case ActionState.Right:
                animator.Play(rightAnim);
                break;
        }
    }

    public abstract void UpdateAction(MusicNoteType musicNoteType);

}
public enum CharacterType
{
    None = 0,
    Player = 1,
    Opponent = 2,
}
public enum ActionState
{
    None = 0,
    Idle = 1,
    Left = 2,
    Down = 3,
    Up = 4,
    Right = 5,
}