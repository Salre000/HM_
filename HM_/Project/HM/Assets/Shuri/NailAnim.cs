using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailAnim : MonoBehaviour
{
    RectTransform rectTransform;

    [SerializeField] float startTime = 2;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        yield return new WaitForSeconds(startTime);

        while (true) 
        {
            if (transform.position == transform.parent.position) break;
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 8000 * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
    }
}
