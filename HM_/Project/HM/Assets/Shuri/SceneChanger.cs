using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string sceneName;
    string _nowScene;

    enum GameScene
    {
        Title,
        Select,
        MainDragon,
        MainSpider,
        Result,
        MAX,
    }

    GameScene sceneIndex = 0;

    readonly string title = "Title";
    readonly string select = "Select";
    readonly string main = "Main Dragon";
    readonly string result = "Result";

    private void Start()
    {
        sceneIndex = (GameScene)SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        if (sceneIndex == GameScene.Title)
        {
            NailAnim anim = GameObject.Find("Nail").GetComponent<NailAnim>();
            anim.AnimCancel();
        }
        SceneManager.LoadScene(sceneName);
    }
}