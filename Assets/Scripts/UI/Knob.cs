using UnityEngine;
using DG.Tweening;

public class Knob : MonoBehaviour
{
    Tween _tween;
    float _tweenTime = .15f;

    public void OnPlay()
    {
        if (_tween != null && _tween.active) _tween.Kill();
        _tween = transform.DORotate(new(0, 0, -30), _tweenTime);
    }

    public void OnControls()
    {
        if (_tween != null && _tween.active) _tween.Kill();
        _tween = transform.DORotate(new(0, 0, 20), _tweenTime);
    }

    public void OnCredits()
    {
        if (_tween != null && _tween.active) _tween.Kill();
        _tween = transform.DORotate(new(0, 0, 80), _tweenTime);
    }

    public void OnMainMenu()
    {
        if (_tween != null && _tween.active) _tween.Kill();
        _tween = transform.DORotate(new(0, 0, 0), _tweenTime);
    }
}
