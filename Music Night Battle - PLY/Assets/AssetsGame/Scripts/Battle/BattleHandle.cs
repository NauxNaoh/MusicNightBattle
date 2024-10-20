using Naux.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandle : Singleton<BattleHandle>
{
    [SerializeField] private FightingHeathBattle fightingHeathBattle;
    [SerializeField] private PlayerCharacter playerCharacter;
    [SerializeField] private OpponentCharacter opponentCharacter;
    [SerializeField] private RectTransform rectMusicBoard;
    [SerializeField] private List<BattleButton> lstBattleButtons;
    [SerializeField] private List<SpriteMusicNoteInfo> lstSpriteData;
    private List<MusicNote> lstLeftNotes = new List<MusicNote>();
    private List<MusicNote> lstDownNotes = new List<MusicNote>();
    private List<MusicNote> lstUpNotes = new List<MusicNote>();
    private List<MusicNote> lstRightNotes = new List<MusicNote>();
    private DataMusic dataMusic;

    private string fileName = "GMNB_Mr.Krabs-shows-up_GMNB--data";
    private float battleTiming;




    public void Initialized()
    {
        battleTiming = 0;

        LoadDataFromJSon();

        var _countBtn = lstBattleButtons.Count;
        if (_countBtn < 4)
            Debug.LogError($"Need add enough 4 btn Battle");
        for (int i = 0; i < _countBtn; i++)
        {
            lstBattleButtons[i].Initialized();
        }

        fightingHeathBattle.Initialized();
    }


    void LoadDataFromJSon()
    {
        var _textAsset = Resources.Load<TextAsset>(fileName);
        if (_textAsset == null)
        {
            Debug.LogError("Can't find file");
            return;
        }

        var _data = JsonUtility.FromJson<DataMusic>(_textAsset.text);
        if (_data == null)
        {
            Debug.LogError("Can't convert from .json");
            return;
        }

        dataMusic = new DataMusic();
        dataMusic.list = new List<MusicNoteInfo>();
        for (int i = 0, _count = _data.list.Count; i < _count; i++)
        {
            dataMusic.list.Add(_data.list[i]);
        }
    }

    public IEnumerator StartMusicBattleRoutine()
    {
        var _musicNoteCount = dataMusic.list.Count;
        var _currentNote = 0;
        battleTiming = 0;
        while (battleTiming < 30)
        {

            if (_currentNote >= _musicNoteCount) break;
            if (battleTiming > dataMusic.list[_currentNote].timeAppear)
            {
                var _noteID = dataMusic.list[_currentNote].noteID;
                //MusicNoteHandle(_noteID, dataMusic.list[_currentNote].duration);
                MusicNoteHandle(_noteID, GameConfig.DURATION_MOVE_MUSIC_NOTE);
                _currentNote++;
            }

            battleTiming += Time.deltaTime;
            yield return null;
        }
    }

    void MusicNoteHandle(int noteID, float duration)
    {
        var _noteType = GetMusicNoteType(noteID);
        var _rectTBattleBtn = GetRectTransformBattleButton(_noteType);

        var _musicNote = SpawnHandler.Instance.SpawnMusicNote(rectMusicBoard);
        var _posBattleBtn = _rectTBattleBtn.position;
        _musicNote.SetClicked(false);
        _musicNote.SetPosition(new Vector2(_posBattleBtn.x, _posBattleBtn.y + GameConfig.ADD_POS_Y_SPAWN_MUSIC_NOTE));
        _musicNote.SetIdNote(noteID);
        _musicNote.SetNoteType(_noteType);
        _musicNote.SetActiveImgArrow(true);

        var _sprData = lstSpriteData.Find(x => x.musicNoteType == _noteType);
        if (_sprData == null)
            Debug.LogError($"not find sprite data type: {_noteType}");
        _musicNote.SetImgArrow(_sprData.sprArrow);
        _musicNote.SetImgTrail(_sprData.sprTrai);

        var _listNote = GetListHoldNote(_noteType);
        _listNote.Add(_musicNote);

        _musicNote.StartMoveNote(_rectTBattleBtn, duration);
    }

    public void KillNote(MusicNote musicNote)
    {
        var _listNote = GetListHoldNote(musicNote.MusicNoteType);
        _listNote.Remove(musicNote);
        SpawnHandler.Instance.PoolingMusicNote(musicNote);
    }

    public void OpponentKillNote(MusicNote musicNote)
    {
        opponentCharacter.UpdateAction(musicNote.MusicNoteType);
        fightingHeathBattle.AddOpponentScore();

        var _listNote = GetListHoldNote(musicNote.MusicNoteType);
        _listNote.Remove(musicNote);
        SpawnHandler.Instance.PoolingMusicNote(musicNote);
    }

    RectTransform GetRectTransformBattleButton(MusicNoteType noteType)
    {
        ButtonType _buttonType = ButtonType.Left;

        switch (noteType)
        {
            case MusicNoteType.PlayerLeft:
            case MusicNoteType.OpponentLeft:
                _buttonType = ButtonType.Left;
                break;
            case MusicNoteType.PlayerDown:
            case MusicNoteType.OpponentDown:
                _buttonType = ButtonType.Down;
                break;
            case MusicNoteType.PlayerUp:
            case MusicNoteType.OpponentUp:
                _buttonType = ButtonType.Up;
                break;
            case MusicNoteType.PlayerRight:
            case MusicNoteType.OpponentRight:
                _buttonType = ButtonType.Right;
                break;
        }

        var _btn = lstBattleButtons.Find(x => x.ButtonType == _buttonType);
        return _btn.GetRectTransformBattleButton();
    }

    MusicNoteType GetMusicNoteType(int noteID)
    {
        switch (noteID)
        {
            case 72:
                return MusicNoteType.PlayerLeft;
            case 73:
                return MusicNoteType.PlayerDown;
            case 74:
                return MusicNoteType.PlayerUp;
            case 75:
                return MusicNoteType.PlayerRight;
            case 76:
                return MusicNoteType.OpponentLeft;
            case 77:
                return MusicNoteType.OpponentDown;
            case 78:
                return MusicNoteType.OpponentUp;
            case 79:
                return MusicNoteType.OpponentRight;
        }

        return MusicNoteType.PlayerLeft;
    }

    MusicNoteType GetMusicNoteType(ButtonType buttonType)
    {
        switch (buttonType)
        {
            case ButtonType.Left:
                return MusicNoteType.PlayerLeft;
            case ButtonType.Down:
                return MusicNoteType.PlayerDown;
            case ButtonType.Up:
                return MusicNoteType.PlayerUp;
            case ButtonType.Right:
                return MusicNoteType.PlayerRight;
        }

        return MusicNoteType.PlayerLeft;
    }

    List<MusicNote> GetListHoldNote(ButtonType buttonType)
    {
        switch (buttonType)
        {
            case ButtonType.Left:
                return lstLeftNotes;
            case ButtonType.Down:
                return lstDownNotes;
            case ButtonType.Up:
                return lstUpNotes;
            case ButtonType.Right:
                return lstRightNotes;
        }

        return lstLeftNotes;
    }

    List<MusicNote> GetListHoldNote(MusicNoteType noteType)
    {
        switch (noteType)
        {
            case MusicNoteType.PlayerLeft:
            case MusicNoteType.OpponentLeft:
                return lstLeftNotes;
            case MusicNoteType.PlayerDown:
            case MusicNoteType.OpponentDown:
                return lstDownNotes;
            case MusicNoteType.PlayerUp:
            case MusicNoteType.OpponentUp:
                return lstUpNotes;
            case MusicNoteType.PlayerRight:
            case MusicNoteType.OpponentRight:
                return lstRightNotes;
        }

        return lstLeftNotes;
    }


    public void OnClickedBattleArrow(BattleButton battleButton)
    {
        var _noteType = GetMusicNoteType(battleButton.ButtonType);
        var _listNote = GetListHoldNote(_noteType);
        var _rectTrans = battleButton.GetRectTransformBattleButton();
        var _rectBtnArrow = GetWorldRect(_rectTrans);

        for (int i = 0; i < _listNote.Count; i++)
        {
            if (_listNote[i].MusicNoteType != _noteType && !_listNote[i].IsClicked) continue;
            var _rectTransNote = _listNote[i].GetRectTransform();
            var _rectArrowNote = GetWorldRect(_rectTransNote);

            if (!_rectBtnArrow.Overlaps(_rectArrowNote)) continue;
            playerCharacter.UpdateAction(_noteType);
            _listNote[i].Hitting();
            battleButton.HitArrow();
            fightingHeathBattle.AddPlayerScore();
            break;
        }
    }


    Rect GetWorldRect(RectTransform rectTransform)
    {
        var _corners = new Vector3[4];
        rectTransform.GetWorldCorners(_corners);

        var _size = new Vector2(_corners[2].x - _corners[0].x, _corners[2].y - _corners[0].y);
        return new Rect(_corners[0], _size);
    }

}

[Serializable]
public class DataMusic
{
    public List<MusicNoteInfo> list;
}

[Serializable]
public class MusicNoteInfo
{
    public float timeAppear;
    public int noteID;
    public float duration;
}

[Serializable]
public class SpriteMusicNoteInfo
{
    public MusicNoteType musicNoteType;
    public Sprite sprArrow;
    public Sprite sprTrai;

}