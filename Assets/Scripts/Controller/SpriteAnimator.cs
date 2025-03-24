using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    public Image Character;
    public List<Sprite> LoadedSet = new List<Sprite>();
    public List<Sprite> CurrentSet = new List<Sprite>();
    public float Speed = 1;
    public float SpritePerSecond = 1;

    [Header("Read Elements")]
    [Min(0)]
    public int CurrentFrame = 0;
    public bool IsPlay = false;
    public bool IsNext = false;
    public float Timer = 0;
    public float NextFrameTimer = 0;

    void Update()
    {
        if (IsPlay)
        {
            if (Timer >= NextFrameTimer) {
                CurrentFrame = (CurrentFrame >= CurrentSet.Count - 1) ? 0 : CurrentFrame + 1;
                if (CurrentFrame == 0 && IsNext) PlayTransat();
                NextFrameTimer += SpritePerSecond / Speed;
            }
            Character.sprite = CurrentSet[CurrentFrame];
            Timer += Time.deltaTime;
        }
    }

    public void PlayTransat()
    {
        CurrentSet = LoadedSet;
        IsNext = false;
    }

    public void Play()
    {
        if (IsPlay)
        {
            IsNext = true;
        } else
        {
            CurrentSet = LoadedSet;
            IsPlay = true;
        }
    }

    public void Stop()
    {
        IsPlay = false;
        IsNext = false;
        CurrentSet = null;
        CurrentFrame = 0;
    }

    public void ChangeAnimation(List<Sprite> spriteSet)
    {
        LoadedSet = spriteSet;
    }
}
