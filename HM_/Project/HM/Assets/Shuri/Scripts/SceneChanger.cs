using Cysharp.Threading.Tasks;
using SceneSound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string sceneName;

    enum GameScene
    {
        Title,
        Select,
        MainDragon,
        MainSpider,
        Result,
        MAX,
    }

    GameScene _nowScene = 0;

    private void Start()
    {
        _nowScene = (GameScene)SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton3) && _nowScene == GameScene.Title)
        {
            ChangeScene();
        }
    }

    public async void ChangeScene()
    {
        switch (_nowScene)
        {
            case GameScene.Title:
                // SE�̍Đ�
                SoundListManager.instance.PlaySound(0, (int)Title.start);
                
                // �t�F�[�h�A�E�g
                await FadeManager.instance.FadeOutAlpha();

                // �A�j���[�V�����̃L�����Z��
                NailAnim anim = GameObject.Find("Nail").GetComponent<NailAnim>();
                anim.AnimCancel();

                // SE�̍Đ����I���܂őҋ@
                await UniTask.Delay((int)SoundListManager.instance.GetAudioClip((int)Title.system,(int)Titlesystem.start).length * 1000);
                
                // �V�[���̓ǂݍ���
                await SceneManager.LoadSceneAsync(sceneName);
                
                // �t�F�[�h�C��
                await FadeManager.instance.FadeInAlpha();
                break;
            case GameScene.Select:
                // �t�F�[�h�A�E�g
                await FadeManager.instance.FadeOutSlide();

                // �V�[���̓ǂݍ���
                await SceneManager.LoadSceneAsync(sceneName);

                // �t�F�[�h�C��
                await FadeManager.instance.FadeInSlide();
                break;
            case GameScene.MainDragon:
                break;
            case GameScene.MainSpider:
                break;
            case GameScene.Result:
                break;
        }
        
    }
}