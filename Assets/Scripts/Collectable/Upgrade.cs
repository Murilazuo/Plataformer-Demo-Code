using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    
    public float idUpgrade;
    // 0 = wall jump
    // 1 = +speed;
    private void Update()
    {
        GetComponent<Animator>().SetFloat("IdAnimation", idUpgrade);    
    }
}
