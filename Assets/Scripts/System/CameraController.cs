using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera virtualaCam;
    GameManager gameManager;
    private void Start()
    {
        virtualaCam = GetComponent<CinemachineVirtualCamera>();
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
    }
    private void Update()
    {
        if (!gameManager.playerDead)
        {
        SetFollow(GameObject.FindGameObjectWithTag("Player"));
        }
    }
    void SetFollow(GameObject toFollow)
    {
        virtualaCam.Follow = toFollow.transform;
        virtualaCam.LookAt = toFollow.transform;
    }
}
