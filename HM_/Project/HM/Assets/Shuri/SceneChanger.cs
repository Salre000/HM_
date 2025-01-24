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

    readonly string title = "Title";
    readonly string select = "Select";
    readonly string main = "Main Dragon";
    readonly string result = "Result";

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

    public void ChangeScene()
    {
        if (_nowScene == GameScene.Title)
        {
            NailAnim anim = GameObject.Find("Nail").GetComponent<NailAnim>();
            anim.AnimCancel();
        }
        SceneManager.LoadScene(sceneName);
    }
}