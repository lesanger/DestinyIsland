using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGenerator : MonoBehaviour
{
    public bool mainThemeTime = false;
    private int partIndex = 0;
    public AudioSource mainThemeSource;

    public bool[] noteAllowed;
    private int allowedNotesAmount = 1;

    public AudioClip[] Bass;
    public AudioClip[] Note_2;
    public AudioClip[] Note_3;
    public AudioClip[] Note_4;
    public AudioClip[] Note_5;
    public AudioClip[] Note_6;
    public AudioClip[] Note_7;
    public AudioClip[] Note_8;

    public AudioSource BassSource;
    public AudioSource Note_2Source;
    public AudioSource Note_3Source;
    public AudioSource Note_4Source;
    public AudioSource Note_5Source;
    public AudioSource Note_6Source;
    public AudioSource Note_7Source;
    public AudioSource Note_8Source;

    public static MusicGenerator instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else {
            Destroy(this);
        }

        for (int i = 0; i < 7; i++)
        {
            noteAllowed[i] = false;
        }
    }

    public void NewAllowedNote()
    {
        allowedNotesAmount += 1;
        noteAllowed[allowedNotesAmount] = true;
    }

    public void PlayBass()
    {
        BassSource.PlayOneShot(Bass[partIndex]);
    }
    
    public void PlayNote_2()
    {
        if (noteAllowed[2])
        {
            Note_2Source.PlayOneShot(Note_2[partIndex]);
        }
    }
    
    public void PlayNote_3()
    {
        if (noteAllowed[3])
        {
            Note_3Source.PlayOneShot(Note_3[partIndex]);
        }
    }
    
    public void PlayNote_4()
    {
        if (noteAllowed[4])
        {
            Note_4Source.PlayOneShot(Note_4[partIndex]);
        }
    }
    
    public void PlayNote_5()
    {
        if (noteAllowed[5])
        {
            Note_5Source.PlayOneShot(Note_5[partIndex]);
        }
    }
    
    public void PlayNote_6()
    {
        if (noteAllowed[6])
        {
            Note_6Source.PlayOneShot(Note_6[partIndex]);
        }
    }
    
    public void PlayNote_7()
    {
        if (noteAllowed[7])
        {
            Note_7Source.PlayOneShot(Note_7[partIndex]);
        }
    }
    
    public void PlayNote_8()
    {
        if (noteAllowed[8])
        {
            Note_8Source.PlayOneShot(Note_8[partIndex]);
        }
    }

    public void OneMoreTime()
    {
        if (partIndex < 3)
        {
            partIndex += 1;
        }
        else
        {
            partIndex = 0;
        }
    }
}
