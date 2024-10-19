using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameplayHandler : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private BattleHandle battleHandle;
    [SerializeField] private CountDownReady countDownReady;

    [SerializeField] private CanvasGroup cgStartPopup;
    [SerializeField] private CanvasGroup cgBoardGame;
    [SerializeField] private Button btnBattle;

    void Start()
    {
        ChaneStateGame(GameState.Init);
    }

    void Initialized()
    {
        cgStartPopup.alpha = 1f;
        cgStartPopup.interactable = true;
        cgBoardGame.alpha = 0f;
        cgBoardGame.interactable = false;

        countDownReady.Initialized();
        battleHandle.Initialized();
        btnBattle.onClick.AddListener(OnClickBattleGameButton);
    }
    void OnClickBattleGameButton()
    {
        ChaneStateGame(GameState.Ready);
    }

    public IEnumerator CountReadyGame()
    {
        cgStartPopup.alpha = 0f;
        cgStartPopup.interactable = false;
        cgBoardGame.alpha = 1f;
        cgBoardGame.interactable = true;

        yield return StartCoroutine(countDownReady.StartCountDown());
        ChaneStateGame(GameState.Playing);
    }

    public void Playing()
    {
        battleHandle.StartCoroutine(battleHandle.StartMusicBattle());
    }

    public void ChaneStateGame(GameState state)
    {
        gameState = state;
        switch (gameState)
        {
            case GameState.Init:
                Initialized();
                break;
            case GameState.Ready:
                StartCoroutine(CountReadyGame());
                break;
            case GameState.Playing:
                AudioController.Instance.PlayAudio(SoundType.SongGame);
                Playing();
                break;
            case GameState.Eng:
                break;
            default:
                break;
        }
    }


}
public enum GameState
{
    Init = 0,
    Ready = 1,
    Playing = 2,
    Eng = 3,
}
