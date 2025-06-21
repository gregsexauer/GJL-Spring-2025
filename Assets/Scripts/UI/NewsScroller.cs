using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class NewsScroller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI newsText;
    [SerializeField] RectTransform newsEndPosition;
    Vector2 _textStartPosition = new();
    CanvasGroup _canvasGroup;
    Tween _activeTween;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _textStartPosition = newsText.rectTransform.anchoredPosition;
    }

    public void ShowNews(string name)
    {
        StartCoroutine(DoNewsScroll(name));
    }

    IEnumerator DoNewsScroll(string name)
    {
        if (_activeTween != null) 
        {
            while (_activeTween.active)
                yield return null;
        }

        string text = "Breaking News: ";
        switch (name)
        {
            case "Walter":
                text += "Walter's Wallet Whiplash Welcomes 'Whoopee!', Witty Writeup.";
                break;
            case "Dave":
                text += "Local Man narrowly avoids gruesome death 3 times across 2 city blocks.";
                break;
            case "Kid":
                text += "Sad Kid no longer quite so sad.";
                break;
        }
        newsText.text = text;

        _activeTween = _canvasGroup.DOFade(1, .25f);
        yield return new WaitForSeconds(.15f);
        _activeTween = newsText.rectTransform.DOMoveX(newsEndPosition.position.x, 7f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(6.75f);
        _activeTween =_canvasGroup.DOFade(0, .25f).OnComplete(()=> newsText.rectTransform.anchoredPosition = _textStartPosition);
    }
}
