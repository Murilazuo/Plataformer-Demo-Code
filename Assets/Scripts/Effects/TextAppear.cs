using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAppear : MonoBehaviour
{
    [SerializeField] float timeToApear;
    void Awake()
    {
        StartCoroutine(nameof(TextA));
    }
    IEnumerator TextA()
    {
        yield return new WaitForSeconds(timeToApear);
        GetComponent<Animator>().SetTrigger("Appear");
    }
}
