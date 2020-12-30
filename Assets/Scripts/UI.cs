using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Linq;

public class UI : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;

    public GameObject HUD;
    public GameObject MainMenuPanel;
    public GameObject PausePanel;
    public GameObject SettingsPanel;
    public GameObject AuthorsPanel;

    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;

    private GameObject currentPanel;
    private GameObject previousPanel;
    
    private Resolution[] resolutions;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.canMove = false;
        }
        
        Cursor.lockState = CursorLockMode.None;
        
        HUD.SetActive(false);
        MainMenuPanel.SetActive(true);
        PausePanel.SetActive(false);
        SettingsPanel.SetActive(false);
        AuthorsPanel.SetActive(false);

        currentPanel = MainMenuPanel;
    }

    void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;        
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && 
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (currentPanel == MainMenuPanel)
            {
                Debug.Log("Нельзя выйти из главного меню");
            }

            if (currentPanel == HUD)
            {
                HUD.SetActive(false);
                MainMenuPanel.SetActive(true);
                
                player = GameObject.FindGameObjectWithTag("Player");
                if (player.TryGetComponent<PlayerController>(out PlayerController playerController))
                {
                    playerController.canMove = false;
                }
        
                Cursor.lockState = CursorLockMode.None;

                currentPanel = MainMenuPanel;
            }

            if (currentPanel == SettingsPanel)
            {
                SettingsPanel.SetActive(false);
                MainMenuPanel.SetActive(true);
            }

            if (currentPanel == AuthorsPanel)
            {
                AuthorsPanel.SetActive(false);
                MainMenuPanel.SetActive(true);
            }
        }
    }


    // Main menu buttons
    public void ExperienceButton()
    {
        HUD.SetActive(true);
        MainMenuPanel.SetActive(false);
        
        Cursor.lockState = CursorLockMode.Locked;
        
        if (player.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.canMove = true;
        }

        currentPanel = HUD;
    }

    public void SettingsButton()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);

        currentPanel = SettingsPanel;
    }
    
    public void AurthorsButton()
    {
        MainMenuPanel.SetActive(false);
        AuthorsPanel.SetActive(true);

        currentPanel = AuthorsPanel;
    }
    
    public void LeaveButton()
    {
        MainMenuPanel.SetActive(false);
        Application.Quit();
    }
    
    // Options menu buttons

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        Debug.Log(qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetSensivity(float sensivity)
    {
        if (camera.TryGetComponent<CameraConroller>(out CameraConroller cameraConroller))
        {
            cameraConroller.mouseSensitivity = sensivity;
            Debug.Log(cameraConroller.mouseSensitivity);
        }
    }
}
