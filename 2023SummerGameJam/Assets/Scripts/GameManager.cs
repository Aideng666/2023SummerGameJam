using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float worldGravity = 10;
    [SerializeField] CinemachineVirtualCamera cam; 

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateCameraTarget()
    {
        cam.LookAt = CommunityManager.Instance.activeAnimal.transform;
        cam.Follow = CommunityManager.Instance.activeAnimal.transform;
    }
}
