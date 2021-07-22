using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    public AudioMixer outputt;
    public ScriptableInt1 musicVol;
    public ScriptableInt1 soundsVol;
    public ScriptableInt1 masterVol;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volumen;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.outputt;
        }
    }

    void Start()
    {
        SetMusicVolumen(musicVol.value);
        SetMusicVfx(soundsVol.value);
        Play("M_Lvl1");
      
    }
    void Update()
    {   //walking sound
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
           Play(ListAllAudio.Vfx_Foot);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            Stop(ListAllAudio.Vfx_Foot);
        }
    }

 

    #region autio manipulations
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void SetMusicVolumen(float volume)
    {
        outputt.SetFloat("VolumeMusic", volume);
        musicVol.value = (int)volume;
    }

    public void SetMusicVfx(float volume)
    {
        outputt.SetFloat("VolumeVfx", volume);
        float v = volume;
        soundsVol.value = (int)volume;
    }

    public void TurnOffSound(float volume)
    {
        //AudioManager.instance.Play("BtnClickSound");
        outputt.SetFloat("VolumeMaster", volume);
        volume = -80f;
        masterVol.value = -80;
    }

    public void TurnOnSound(float volume)
    {
        //AudioManager.instance.Play("BtnClickSound");
        outputt.SetFloat("VolumeMaster", volume);
        volume = 0f;
        masterVol.value = 0;

    }

    #endregion
}

public struct ListAllAudio
{
    //Weapon
    public static string W_Revolver     = "W_Revolver";
    public static string W_Shootgun     = "W_Shootgun";
    public static string W_Flamethrower = "W_Flamethrower";//-
    public static string W_Rifle        = "W_Rifle";//-

    //Music
    public static string M_MainMenu = "M_MainMenu";//-
    public static string M_Lvl1     = "M_Lvl1";
    public static string M_Lvl2     = "M_Lvl2";
    public static string M_Lvl3     = "M_Lvl3";//-
    public static string M_LvlComp  = "M_LvlComp";//-
    public static string M_LvlDeff  = "M_LvlDeff ";//-

    //EnemyDeath
    public static string EnemyDeath1 = "EnemyDeath1 ";
    public static string EnemyDeath2 = "EnemyDeath2 ";
    public static string EnemyDeath3 = "EnemyDeath3 ";
    public static string EnemyDeath4 = "EnemyDeath4 ";


    //Other vfx
    public static string Vfx_Foot       = "Vfx_Foot";
    public static string Vfx_HitEnemy   = "Vfx_HitEnemy";//-
    public static string Vfx_HitWall    = "Vfx_HitWall";//-

}