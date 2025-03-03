using Cysharp.Threading.Tasks;
using SceneSound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
                SoundListManager.instance.PlaySound((int)TitleSystem.Start);

                // �J�n���̃A�j���[�V�����Đ�
                await GameObject.Find("Nail").GetComponent<NailAnim>().PlayAnim();
                
                // �t�F�[�h�A�E�g
                await FadeManager.instance.FadeOutAlpha();

                // �V�[���̓ǂݍ���
                await SceneManager.LoadSceneAsync(sceneName).ToUniTask();
                
                // �t�F�[�h�C��
                await FadeManager.instance.FadeInAlpha();
                break;
            case GameScene.Select:

                this.gameObject.GetComponent<Button>().navigation.selectOnLeft.interactable = false;
                this.gameObject.GetComponent<Button>().navigation.selectOnRight.interactable = false;

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
                // �t�F�[�h�A�E�g
                await FadeManager.instance.FadeOutAlpha();

                // �V�[���̓ǂݍ���
                await SceneManager.LoadSceneAsync(sceneName).ToUniTask();

                // �t�F�[�h�C��
                await FadeManager.instance.FadeInAlpha();
                break;
        }
        
    }
}