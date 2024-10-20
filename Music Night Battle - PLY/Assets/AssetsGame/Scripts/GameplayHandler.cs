using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameplayHandler : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private BattleHandle battleHandle;
    [SerializeField] private CountDownReady countDownReady;

    [SerializeField] private CanvasGroup cgStartPopup;
    [SerializeField] private CanvasGroup cgBoardGame;
    [SerializeField] private CanvasGroup cgEndGame;
    [SerializeField] private Button btnBattle;

    void Start()
    {
        ChaneStateGame(GameState.Init);
    }

    void Initialized()
    {
        cgStartPopup.alpha = 1f;
        cgStartPopup.interactable = true;
        cgStartPopup.blocksRaycasts = true;

        cgBoardGame.alpha = 0f;
        cgBoardGame.interactable = false;
        cgBoardGame.blocksRaycasts = false;

        cgEndGame.alpha = 0f;
        cgEndGame.interactable = false;
        cgEndGame.blocksRaycasts = false;

        countDownReady.Initialized();
        battleHandle.Initialized();
        btnBattle.onClick.AddListener(OnClickBattleGameButton);
    }
    void OnClickBattleGameButton()
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
    public IEnumerator CountReadyGameRoutine()
    {
        cgStartPopup.alpha = 0f;
        cgStartPopup.interactable = false;
        cgStartPopup.blocksRaycasts = false;

        cgBoardGame.alpha = 1f;
        cgBoardGame.interactable = true;
        cgBoardGame.blocksRaycasts = true;
        yield return StartCoroutine(countDownReady.StartCountDownRoutine());
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

    void EndGame()
    {
        cgBoardGame.alpha = 0f;
        cgBoardGame.interactable = false;
        cgBoardGame.blocksRaycasts = false;

        cgEndGame.alpha = 1f;
        cgEndGame.interactable = true;
        cgEndGame.blocksRaycasts = true;
    }
    public void ChaneStateGame(GameState state)
    {
        gameState = state;
        switch (gameState)
        {
            case GameState.Init:
                Initialized();
                break;
            case GameState.Playing:
                var _countDownCoroutine = CountReadyGameRoutine();
                var _PlayingCoroutine = PlayingRoutine();
                StartCoroutine(RunMultiCoroutine(_countDownCoroutine, _PlayingCoroutine));
                break;
            case GameState.End:
                EndGame();
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
