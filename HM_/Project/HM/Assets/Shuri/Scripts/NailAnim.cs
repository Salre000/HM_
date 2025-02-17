using Cysharp.Threading.Tasks;
using SceneSound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NailAnim : MonoBehaviour
{
    RectTransform rectTransform;

    [SerializeField] int startTime = 2;

    CancellationTokenSource _cancel;

    void Start()
    {
        _cancel = new CancellationTokenSource(); 
        Application.targetFrameRate = 60;
        rectTransform = GetComponent<RectTransform>();

        CancellationToken token = _cancel.Token;

        Anim(token).Forget();
    }

    private async UniTask Anim(CancellationToken token)
    {
        await UniTask.DelayFrame(startTime * Application.targetFrameRate);
        
        if (token.IsCancellationRequested) return;

        SoundListManager.instance.PlaySound((int)TitleSystem.Anim);

        while (true)
        {
            if (token.IsCancellationRequested) return;
            
            if (transform.position == transform.parent.position) break;
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 8000 * Time.deltaTime);

            await UniTask.DelayFrame(1);
        }
    }

    public void AnimCancel()
    {
        _cancel.Cancel(); 
    }
}
