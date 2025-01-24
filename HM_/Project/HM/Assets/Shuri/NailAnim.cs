using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailAnim : MonoBehaviour
{
    RectTransform rectTransform;

    [SerializeField] int startTime = 2;

    void Start()
    {
        Application.targetFrameRate = 60;
        rectTransform = GetComponent<RectTransform>();
        Anim();
    }

    private async UniTask Anim()
    {
        await UniTask.DelayFrame(startTime * Application.targetFrameRate);

        while (true) 
        {
            if (transform.position == transform.parent.position) break;
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 8000 * Time.deltaTime);

            await UniTask.DelayFrame(1);
        }
    }
}
