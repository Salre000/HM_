using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{

    private PlayerMove _playerMove;

    private CameraManager _cameraManager;
    // Start is called before the first frame update
    void Start()
    {
        _playerMove=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();

        _cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //X�{�^������������
        if (Input.GetKeyUp("joystick _button 0"))
        {

            _playerMove.SetAngle(_cameraManager.Get_CameraPositionAngle() * 180 / 3.14f);

            _cameraManager.SetCameraPosition();

        }

    }
}
