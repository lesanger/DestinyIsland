using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject player;

    public GameObject HUD;
    public GameObject MainMenuPanel;
    public GameObject PausePanel;
    public GameObject SettingsPanel;
    public GameObject AuthorsPanel;
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
    }

    void Update()
    {
        
    }

    public void ExperienceButton()
    {
        HUD.SetActive(true);
        MainMenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        
        if (player.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.canMove = true;
        }
    }

    public void SettingsButton()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
    
    public void AurthorsButton()
    {
        MainMenuPanel.SetActive(false);
        AuthorsPanel.SetActive(true);
    }
    
    public void LeaveButton()
    {
        MainMenuPanel.SetActive(false);
        Application.Quit();
    }
}
