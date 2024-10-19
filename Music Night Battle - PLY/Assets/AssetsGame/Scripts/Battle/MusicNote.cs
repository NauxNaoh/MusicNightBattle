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
    private MusicNoteType musicNoteType;


    public void SetPosition(Vector2 position) => rectNote.position = position;
    public void SetIdNote(int id) => idNote = id;
    public void SetNoteType(MusicNoteType type) => musicNoteType = type;
    public void SetImgArrow(Sprite sprArrow) => imgArrow.sprite = sprArrow;
    public void SetImgTrail(Sprite sprArrow) => imgTrail.sprite = sprArrow;

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
