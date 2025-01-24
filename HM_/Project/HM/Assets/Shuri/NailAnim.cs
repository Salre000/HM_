using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NailAnim : MonoBehaviour
{
    RectTransform rectTransform;

    [SerializeField] int startTime = 2;

<<<<<<< HEAD
    CancellationTokenSource _cancel;
=======
    private UniTask _task;
>>>>>>> 283e44c025f6b5dc4f3bd4813e00a19e381c75e4

    void Start()
    {
        _cancel = new CancellationTokenSource(); 
        Application.targetFrameRate = 60;
        rectTransform = GetComponent<RectTransform>();
<<<<<<< HEAD

        CancellationToken token = _cancel.Token;
        Anim(token);
=======
        _task = Anim();
>>>>>>> 283e44c025f6b5dc4f3bd4813e00a19e381c75e4
    }

    private async UniTask Anim(CancellationToken token)
    {
        await UniTask.DelayFrame(startTime * Application.targetFrameRate);

        if (this.gameObject == null) return;

        while (true)
        {
            token.ThrowIfCancellationRequested();

            if (transform.position == transform.parent.position) break;
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 8000 * Time.deltaTime);

            await UniTask.DelayFrame(1);
        }
    }

<<<<<<< HEAD
    public void AnimCancel()
    {
        _cancel.Cancel();
=======
    public void AnimSkip()
    {
        _task = UniTask.CompletedTask;
>>>>>>> 283e44c025f6b5dc4f3bd4813e00a19e381c75e4
    }
}
