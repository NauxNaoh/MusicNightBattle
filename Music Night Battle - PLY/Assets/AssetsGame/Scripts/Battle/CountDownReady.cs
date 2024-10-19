using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownReady : MonoBehaviour
{
    [SerializeField] private Image imgFadeCountDown;
    [SerializeField] private Image imgCountDown;

    [SerializeField] private List<CountDownInfo> lstCountDown;
    private float limitOneCountTime = 1f;
    public void Initialized()
    {
        ActiveCountDown(false);
        if (lstCountDown.Count > 0)
        {
            imgCountDown.sprite = lstCountDown[0].sprite;
            imgFadeCountDown.sprite = lstCountDown[0].sprite;
        }
    }
    void ActiveCountDown(bool status)
    {
        imgFadeCountDown.gameObject.SetActive(status);
        imgCountDown.gameObject.SetActive(status);
    }

    public IEnumerator StartCountDown()
    {
        ActiveCountDown(true);

        for (int i = 0, _count = lstCountDown.Count; i < _count; i++)
        {
            imgFadeCountDown.rectTransform.localScale = Vector3.one;
            var _color = imgFadeCountDown.color;
            _color.a = 1;
            imgFadeCountDown.color = _color;
            imgCountDown.sprite = lstCountDown[i].sprite;
            imgFadeCountDown.sprite = lstCountDown[i].sprite;
            AudioController.Instance.PlayAudio(lstCountDown[i].soundType);

            var scaleRoutine = SmoothScaleRoutine(imgFadeCountDown.rectTransform, Vector3.one, Vector3.one * 2, limitOneCountTime);
            var fadeRoutine = SmoothFadeImageRoutine(imgFadeCountDown, 1, 0, limitOneCountTime);

            yield return StartCoroutine(RunMultiCoroutine(scaleRoutine, fadeRoutine));
        }

        ActiveCountDown(false);
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

    IEnumerator SmoothScaleRoutine(RectTransform transform, Vector3 startScale, Vector3 endScale, float limitTime)
    {
        var _elapsedTime = 0f;

        while (_elapsedTime < limitTime)
        {
            var _t = Mathf.Clamp01(_elapsedTime / limitTime);
            transform.localScale = Vector3.Lerp(startScale, endScale, _t);

            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale;
    }
    IEnumerator SmoothFadeImageRoutine(Image image, float startAlpha, float endAlpha, float limitTime)
    {
        var _color = image.color;
        var _elapsedTime = 0f;

        while (_elapsedTime < limitTime)
        {
            var _t = Mathf.Clamp01(_elapsedTime / limitTime);
            _color.a = Mathf.Lerp(startAlpha, endAlpha, _t);
            image.color = _color;

            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        _color.a = endAlpha;
        image.color = _color;
    }

}
[Serializable]
public class CountDownInfo
{
    public Sprite sprite;
    public SoundType soundType;
}
