using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamAPI : MonoBehaviour
{
    public static SteamAPI instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } 
        else 
        {
            Destroy(this);
        }
        
        try
        {
            Steamworks.SteamClient.Init( 1522180  );
        }
        catch ( System.Exception e )
        {
            // Something went wrong - it's one of these:
            //
            //     Steam is closed?
            //     Can't find steam_api dll?
            //     Don't have permission to play app?
            //
        }
    }
    
    void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }

    public void Exit()
    {
        Steamworks.SteamClient.Shutdown();
    }

    private void OnDisable()
    {
        Steamworks.SteamClient.Shutdown();
    }
}
