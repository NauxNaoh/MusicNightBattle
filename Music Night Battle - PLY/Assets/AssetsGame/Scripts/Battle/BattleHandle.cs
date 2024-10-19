using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandle : MonoBehaviour
{
    [SerializeField] private RectTransform rectMusicBoard;
    [SerializeField] private List<BattleButton> lstBattleButtons;
    [SerializeField] private List<SpriteMusicNoteInfo> lstSpriteData;

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

    public IEnumerator StartMusicBattle()
    {
        var _musicNoteCount = dataMusic.list.Count;
        var _currentNote = 0;
        while (battleTiming < 30)
        {
            if (battleTiming > dataMusic.list[_currentNote].timeAppear)
            {
                var _noteID = dataMusic.list[_currentNote].noteID;
                MusicNoteHandle(_noteID);

                _currentNote++;
            }

            battleTiming += Time.deltaTime;
            yield return null;
        }
    }

    void MusicNoteHandle(int noteID)
    {
        var _noteType = ConvertNoteIDToMusicNoteType(noteID);
        var _posSpawn = GetPosSpawnNote(_noteType);

        var _musicNote = SpawnHandler.Instance.SpawnMusicNote(rectMusicBoard);
        _musicNote.SetPosition(_posSpawn);
        _musicNote.SetIdNote(noteID);
        _musicNote.SetNoteType(_noteType);

        var _sprData = lstSpriteData.Find(x => x.musicNoteType == _noteType);
        if (_sprData == null) return;
        _musicNote.SetImgArrow(_sprData.sprArrow);
        _musicNote.SetImgTrail(_sprData.sprTrai);

    }

    Vector3 GetPosSpawnNote(MusicNoteType noteType)
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
            default:
                return Vector3.zero;
        }

        var _btn = lstBattleButtons.Find(x => x.ButtonType == _buttonType);
        return _btn.GetPosSpawnNote();
    }

    MusicNoteType ConvertNoteIDToMusicNoteType(int noteID)
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