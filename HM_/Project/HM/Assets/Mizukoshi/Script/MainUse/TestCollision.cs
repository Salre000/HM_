using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    [SerializeField] private GameObject m_gameObject;

    private AudioSource _audioSource;
    void Start()
    {
     
        _audioSource = this.gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_gameObject == null)
        {
            m_gameObject = GameObject.Find("Last");
        }
    }
    private void FixedUpdate()
    {
        if(m_gameObject.name=="Last")return;
        this.transform.position = m_gameObject.transform.position;
        this.transform.eulerAngles = m_gameObject.transform.eulerAngles;
    }

    public GameObject GetGameObject() {  return m_gameObject; }
    public void SetGameObject(GameObject gameObject) {  m_gameObject = gameObject; }

    public AudioSource GetAudioSource() { return _audioSource; }
}
