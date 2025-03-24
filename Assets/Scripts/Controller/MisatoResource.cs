using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisatoResource : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite StaticSprite;
    public List<Sprite> StartupList = new List<Sprite>();
    public List<Sprite> AList = new List<Sprite>();
    public List<Sprite> BList = new List<Sprite>();
    public List<Sprite> EndAList = new List<Sprite>();
    public List<Sprite> EndBList = new List<Sprite>();
    public Sprite EndBStatic;

    [Header("Audio")]
    public AudioClip StartAudio;
    public List<AudioClip> FinishAudio = new List<AudioClip>();
    public List<AudioClip> StopAudio = new List<AudioClip>();
    public AudioClip DefaultSong;

    [Header("TextAsset")]
    public TextAsset DefaultSongTimeStamp;
}
