using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicNote : MonoBehaviour
{
    [SerializeField] private RectTransform rectNote;
    [SerializeField] private Image imgArrow;
    [SerializeField] private Image imgTrail;
    [SerializeField] private RectTransform rectTrail;
    private int idNote;
    private bool isClicked;

    private MusicNoteType musicNoteType;
    public MusicNoteType MusicNoteType => musicNoteType;
    public bool IsClicked => isClicked;


    public RectTransform GetRectTransform() => rectNote;


    public void SetClicked(bool status) => isClicked = status;
    public void SetPosition(Vector2 position) => rectNote.position = position;
    public void SetIdNote(int id) => idNote = id;
    public void SetNoteType(MusicNoteType type) => musicNoteType = type;
    public void SetImgArrow(Sprite sprArrow) => imgArrow.sprite = sprArrow;

    public void SetActiveImgArrow(bool status) => imgArrow.enabled = status;
    public void SetImgTrail(Sprite sprArrow) => imgTrail.sprite = sprArrow;



    public void StartMoveNote(RectTransform rectBattleBtn, float limitTime)
    {
        StartCoroutine(SmoothMoveRoutine(rectNote, rectBattleBtn, limitTime));
    }

    IEnumerator SmoothMoveRoutine(RectTransform rectMove, RectTransform rectBattleBtn, float limitTime)
    {
        var _elapsedTime = 0f;
        Vector2 _anchorBattleBtn = Vector2.zero;
        Vector2 _vectorStart = Vector2.zero;
        Vector2 _vectorEnd = Vector2.zero;

        var _posReduce = GameConfig.ADD_POS_Y_SPAWN_MUSIC_NOTE;

        if (CheckNoteOfOpponent())
        {
            _posReduce = 0;
            limitTime /= 2;
        }


        while (_elapsedTime < limitTime)
        {
            _anchorBattleBtn = rectBattleBtn.position;
            _vectorStart.x = _anchorBattleBtn.x;
            _vectorStart.y = _anchorBattleBtn.y + GameConfig.ADD_POS_Y_SPAWN_MUSIC_NOTE;
            _vectorEnd.x = _anchorBattleBtn.x;
            _vectorEnd.y = _anchorBattleBtn.y - _posReduce;

            var _t = Mathf.Clamp01(_elapsedTime / limitTime);
            rectMove.position = Vector2.Lerp(_vectorStart, _vectorEnd, _t);

            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        MoveDoneToDestination();
    }

    bool CheckNoteOfOpponent()
    {
        return musicNoteType == MusicNoteType.OpponentLeft
           || musicNoteType == MusicNoteType.OpponentDown
           || musicNoteType == MusicNoteType.OpponentUp
           || musicNoteType == MusicNoteType.OpponentRight;
    }

    public void MoveDoneToDestination()
    {
        if (CheckNoteOfOpponent())
            BattleHandle.Instance.OpponentKillNote(this);
        else
            BattleHandle.Instance.KillNote(this);
    }

    public void Hitting()
    {
        SetActiveImgArrow(false);
    }

}

public enum MusicNoteType
{
    None = 0,
    PlayerLeft = 1,
    PlayerDown = 2,
    PlayerUp = 3,
    PlayerRight = 4,
    OpponentLeft = 5,
    OpponentDown = 6,
    OpponentUp = 7,
    OpponentRight = 8,
}
