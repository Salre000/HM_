using Cysharp.Threading.Tasks;
using SceneSound;
using UnityEngine;

public class NailAnim : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
    }

    public async UniTask PlayAnim()
    {
        // SE�̍Đ�
        SoundListManager.instance.PlaySound((int)TitleSystem.Anim);

        while (true)
        {
            // �ڕW�n�_�ɒ����܂ňړ�
            if (transform.position == transform.parent.position) break;
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 8000 * Time.deltaTime);

            await UniTask.DelayFrame(1);
        }
    }
}
