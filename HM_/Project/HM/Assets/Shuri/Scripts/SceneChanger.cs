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
                // SEの再生
                SoundListManager.instance.PlaySound((int)TitleSystem.Start);

                // 開始時のアニメーション再生
                await GameObject.Find("Nail").GetComponent<NailAnim>().PlayAnim();
                
                // フェードアウト
                await FadeManager.instance.FadeOutAlpha();

                // シーンの読み込み
                await SceneManager.LoadSceneAsync(sceneName).ToUniTask();
                
                // フェードイン
                await FadeManager.instance.FadeInAlpha();
                break;
            case GameScene.Select:

                this.gameObject.GetComponent<Button>().navigation.selectOnLeft.interactable = false;
                this.gameObject.GetComponent<Button>().navigation.selectOnRight.interactable = false;

                // フェードアウト
                await FadeManager.instance.FadeOutSlide();

                // シーンの読み込み
                await SceneManager.LoadSceneAsync(sceneName);

                // フェードイン
                await FadeManager.instance.FadeInSlide();
                break;
            case GameScene.MainDragon:
                break;
            case GameScene.MainSpider:
                break;
            case GameScene.Result:
                // フェードアウト
                await FadeManager.instance.FadeOutAlpha();

                // シーンの読み込み
                await SceneManager.LoadSceneAsync(sceneName).ToUniTask();

                // フェードイン
                await FadeManager.instance.FadeInAlpha();
                break;
        }
        
    }
}