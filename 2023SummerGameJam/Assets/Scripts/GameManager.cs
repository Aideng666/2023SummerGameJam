using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float worldGravity = 10;
    [SerializeField] CinemachineFreeLook cam;
    [SerializeField] GameObject pauseCanvas;
    public static bool isPaused = false;

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

        AudioManager.Instance.Stop("MainTheme");
        AudioManager.Instance.Play("DayTheme");
    }

    public void UpdateCameraTarget()
    {
        cam.LookAt = CommunityManager.Instance.activeAnimal.transform;
        cam.Follow = CommunityManager.Instance.activeAnimal.transform;
    }

    void Update()
    {
        if (InputManager.Instance.Pause())
        {
            isPaused = !isPaused;        
        }

        if (isPaused)
        {         
            PauseGame();
        }
        else if (!isPaused)
        {      
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        isPaused = false;    
        pauseCanvas.SetActive(false);
        AudioManager.Instance.Stop("DayTheme");
        AudioManager.Instance.Play("MainTheme");
    }
}
