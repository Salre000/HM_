using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;

    [SerializeField]
    private GameObject _fadeSlideObj;

    [SerializeField]
    private GameObject _fadeAlphaObj;
    
    private RectTransform _fadeRect = null;
    private Image _fadeImage = null;

    private UniTask _fadeTask;

    Vector3 _basePos;

    private const float _DEFAULT_FADE_DURATION = 1.0f;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        
        DontDestroyOnLoad(this);

        _fadeRect = _fadeSlideObj?.GetComponent<RectTransform>();
        _fadeImage = _fadeAlphaObj?.GetComponent<Image>();

        _fadeRect.anchoredPosition = Vector2.right*3000;

        _basePos = _fadeRect.localPosition;
    }

    // ˆÃ‚­‚·‚é
    public async UniTask FadeOutSlide(float duration = _DEFAULT_FADE_DURATION)
    {
        _fadeTask = FadeTargetSlide(Vector3.zero, duration);

        await _fadeTask;
    }

    // –¾‚é‚­‚·‚é
    public async UniTask FadeInSlide(float duration = _DEFAULT_FADE_DURATION)
    {
        _fadeTask = FadeTargetSlide(-_basePos, duration);

        await _fadeTask;
        _fadeRect.localPosition = _basePos;
    }

    private async UniTask FadeTargetSlide(Vector3 targetRect, float duration)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / duration;

            _fadeRect.localPosition = Vector3.Lerp(_fadeRect.localPosition, targetRect, t);

            await UniTask.DelayFrame(1);
        }
        await UniTask.DelayFrame(1);
    }

    public async UniTask FadeOutAlpha(float duration = _DEFAULT_FADE_DURATION)
    {
        _fadeTask = FadeTargetAlpha(1.0f, duration);

        await _fadeTask;
    }

    // –¾‚é‚­‚·‚é
    public async UniTask FadeInAlpha(float duration = _DEFAULT_FADE_DURATION)
    {
        _fadeTask = FadeTargetAlpha(0.0f, duration);

        await _fadeTask;
    }

    private async UniTask FadeTargetAlpha(float targetAlpha, float duration)
    {
        float elapsedTime = 0.0f;
        float startAlpha = _fadeImage.color.a;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            Color fadeColor = _fadeImage.color;

            float t = elapsedTime / duration;

            fadeColor.a = Mathf.Lerp(startAlpha, targetAlpha, t);

            _fadeImage.color = fadeColor;

            await UniTask.DelayFrame(1);
        }
        await UniTask.DelayFrame(1);
    }
}
