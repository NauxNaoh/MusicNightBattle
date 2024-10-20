using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGamePanel : UIPanel
{
    [SerializeField] private RectTransform rectFightingHealth;
    [SerializeField] private RectTransform rectCountdown;
    [SerializeField] private RectTransform rectBattleBtns;

    public override void LandscapeUI()
    {
        rectFightingHealth.pivot = new Vector2(0.5f, 1);
        rectFightingHealth.anchoredPosition = new Vector2(0, -100);

        rectBattleBtns.pivot = new Vector2(0.5f, 0);
        rectBattleBtns.anchoredPosition = new Vector2(0, 150);

        rectCountdown.pivot = new Vector2(0.5f, 0.5f);
        rectCountdown.anchoredPosition = new Vector2(0, 80);
    }

    public override void PortraitUI()
    {
        rectFightingHealth.pivot = new Vector2(0.5f, 1);
        rectFightingHealth.anchoredPosition = new Vector2(0, -250);

        rectBattleBtns.pivot = new Vector2(0.5f, 0);
        rectBattleBtns.anchoredPosition = new Vector2(0, 400);

        rectCountdown.pivot = new Vector2(0.5f, 0.5f);
        rectCountdown.anchoredPosition = new Vector2(0, 80);
    }

}
