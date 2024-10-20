using Naux.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayHandler : Singleton<GameplayHandler>
{
    [SerializeField] private GameState gameState;
    [SerializeField] private BattleHandle battleHandle;
    [SerializeField] private CountDownReady countDownReady;

    [SerializeField] private StartPanel startPanel;
    [SerializeField] private BoardGamePanel boardGamePanel;
    [SerializeField] private EndPanel endPanel;

    void Start()
    {
        ChaneStateGame(GameState.Init);
    }

    void Initialized()
    {       
        countDownReady.Initialized();
        battleHandle.Initialized();
    }
    
    public void StartBattle()
    {
        ChaneStateGame(GameState.Playing);
    }

    IEnumerator RunMultiCoroutine(params IEnumerator[] enumerators)
    {
        var _lstCoroutine = new List<Coroutine>();
        for (int i = 0, _count = enumerators.Length; i < _count; i++)
        {
            _lstCoroutine.Add(StartCoroutine(enumerators[i]));
        }

        for (int i = 0, _count = _lstCoroutine.Count; i < _count; i++)
        {
            yield return _lstCoroutine[i];
        }
    }

    IEnumerator PlayingRoutine()
    {
        var _timeWait = 4 - (GameConfig.DURATION_MOVE_MUSIC_NOTE / 2); //4 is time countDown
        yield return new WaitForSeconds(_timeWait);
        battleHandle.StartCoroutine(battleHandle.StartMusicBattleRoutine());

        yield return new WaitForSeconds(GameConfig.DURATION_MOVE_MUSIC_NOTE / 2);
        AudioController.Instance.PlayAudio(SoundType.SongGame);
        yield return new WaitForSeconds(30);
        ChaneStateGame(GameState.End);
    }

    public void ChaneStateGame(GameState state)
    {
        gameState = state;
        switch (gameState)
        {
            case GameState.Init:
                startPanel.ShowPanel();
                boardGamePanel.HidePanel();
                endPanel.HidePanel();
                Initialized();
                break;
            case GameState.Playing:
                startPanel.HidePanel();
                boardGamePanel.ShowPanel();

                var _countDownCoroutine = countDownReady.StartCountDownRoutine();
                var _PlayingCoroutine = PlayingRoutine();
                StartCoroutine(RunMultiCoroutine(_countDownCoroutine, _PlayingCoroutine));
                break;
            case GameState.End:
                boardGamePanel.HidePanel();
                endPanel.ShowPanel();
                break;
            default:
                break;
        }
    }
}
public enum GameState
{
    Init = 0,
    Playing = 1,
    End = 2,
}
