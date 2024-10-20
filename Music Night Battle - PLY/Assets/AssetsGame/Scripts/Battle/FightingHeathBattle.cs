using UnityEngine;
using UnityEngine.UI;

public class FightingHeathBattle : MonoBehaviour
{
    [SerializeField] private Image imgFill;
    [SerializeField] private RectTransform rectFightHealBar;
    [SerializeField] private RectTransform rectPVE;
    private float opponentScore;
    private float playerScore;

    public void Initialized()
    {
        opponentScore = 20;
        playerScore = 20;
        UpdateFightingBar();
    }

    public void AddOpponentScore()
    {
        opponentScore++;
        UpdateFightingBar();
    }
    public void AddPlayerScore()
    {
        playerScore++;
        UpdateFightingBar();
    }
    void UpdateFightingBar()
    {
        var _value = opponentScore / (opponentScore + playerScore);
        if (_value < 0.1f)
            _value = 0.1f;
        else if (_value > 0.9f)
            _value = 0.9f;
        imgFill.fillAmount = _value;

        var _posX = rectFightHealBar.sizeDelta.x * _value;
        rectPVE.anchoredPosition = new Vector2(_posX, 0);
    }

}
