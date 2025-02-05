using Cysharp.Threading.Tasks;
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
                await FadeManager.instance.FadeOutAlpha();
                NailAnim anim = GameObject.Find("Nail").GetComponent<NailAnim>();
                anim.AnimCancel();
                SceneManager.LoadScene(sceneName);
                await FadeManager.instance.FadeInAlpha();
                break;
            case GameScene.Select:
                await FadeManager.instance.FadeOutSlide();
                SceneManager.LoadScene(sceneName);
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