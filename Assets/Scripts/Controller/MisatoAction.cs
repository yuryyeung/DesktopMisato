using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MisatoAction : MonoBehaviour
{
    Coroutine CurrentCoroutine;
    public enum MisatoStatus
    {
        Default, Start, A_Dance, B_Dance, End, Stop
    }
    public Image MisatoChan;
    public MisatoStatus Status = MisatoStatus.Default;
    public AudioSource SongAudio, VoiceAudio;
    public MisatoResource Resource;
    public SpriteAnimator Animator;
    public ButtonAct BtnAct;

    public bool IsStarting = false;
    public bool isPlay = false;
    public bool isEnding = false;
    public float SongTime = 0;
    [Min(0)]public float EndShake = 3;

    public List<float> TimeStamp = new List<float>();
    public int TimeStampNum = 0;

    public void Play()
    {
        if (isPlay) return;
        MisatoChan.sprite = Resource.StaticSprite;
        TimeStampNum = 0;
        IsStarting = true;
        isPlay = true;
        CurrentCoroutine = StartCoroutine(MisatoStateMachine());
        Animator.Speed = 1;
        VoiceAudio.clip = Resource.StartAudio;
        VoiceAudio.Play();
    }

    public void Stop()
    {
        Status = MisatoStatus.Stop;
        BtnAct.ChangeState(false);

        isEnding = true;
        SongAudio.Stop();
        
        Animator.ChangeAnimation(Resource.EndBList);
        Animator.Stop();
        Animator.Play();
        Animator.Speed = 0.25f;

        VoiceAudio.clip = Resource.StopAudio[Rand(Resource.StopAudio.Count)];
        VoiceAudio.Play();
    }

    public IEnumerator MisatoStateMachine()
    {
        while (true)
        {
            switch (Status)
            {
                case MisatoStatus.Default: Status = MisatoStatus.Start; break;
                case MisatoStatus.Start: StartState(); break;
                case MisatoStatus.A_Dance: DanceState(); break;  
                case MisatoStatus.B_Dance: DanceState(); break;
                case MisatoStatus.End: EndState(); break;
                case MisatoStatus.Stop: StopState(); break;
            }
            yield return null;
        }
    }

    public void StartState()
    {
        if (!VoiceAudio.isPlaying && VoiceAudio.clip == Resource.StartAudio)
        {
            Debug.Log("Clip Ended");
            VoiceAudio.clip = null;
            IsStarting = false;

            SongAudio.clip = Resource.DefaultSong;
            JObject jObjs = JObject.Parse(Resource.DefaultSongTimeStamp.text);
            JArray jArray = JArray.Parse(jObjs["Timers"].ToString());
            Debug.Log(jArray.Count);
            TimeStamp.Clear();
            foreach (JToken jt in jArray) { TimeStamp.Add(jt.ToObject<float>()); }

            SongAudio.Play();
            Animator.ChangeAnimation(Resource.StartupList);
            Animator.Play();
            Status = MisatoStatus.B_Dance;
        }
        SongTime = SongAudio.time;
    }
    public void DanceState()
    {
        if (!SongAudio.isPlaying)
        {
            Status = MisatoStatus.End;
            BtnAct.ButtonStatus = false;
            BtnAct.ChangeState(false);

            isEnding = true;
            Animator.ChangeAnimation(Resource.EndAList);
            //Animator.Stop();
            Animator.Play();
            Animator.Speed = 0.75f;

            VoiceAudio.clip = Resource.FinishAudio[Rand(Resource.FinishAudio.Count)];
            VoiceAudio.Play();
        } else 
        if (TimeStampNum < TimeStamp.Count && SongTime >= TimeStamp[TimeStampNum])
        {
            Debug.Log("Currnet TimeStamp: " + TimeStampNum + "-" + TimeStamp[TimeStampNum]);
            Animator.ChangeAnimation((Status == MisatoStatus.B_Dance) ? Resource.AList : Resource.BList);
            Status = (Status == MisatoStatus.B_Dance) ? MisatoStatus.A_Dance : MisatoStatus.B_Dance;
            //Animator.Stop();
            Animator.Play();
            TimeStampNum++;
        } 
        SongTime = SongAudio.time;
    }

    float _endTimer = 0;
    public void EndState()
    {
        _endTimer += Time.deltaTime;
        if (_endTimer >= EndShake)
        {
            Animator.Stop();
            MisatoChan.sprite = Resource.StaticSprite;
            Status = MisatoStatus.Default;
            StopCoroutine(CurrentCoroutine);
            CurrentCoroutine = null;
            isPlay = false;
            isEnding = false;
            _endTimer = 0;
        }
    }

    public void StopState()
    {
        _endTimer += Time.deltaTime;
        if (_endTimer >= EndShake / 2)
        {
            Animator.Stop();
            MisatoChan.sprite = Resource.EndBStatic;
            Status = MisatoStatus.Default;
            StopCoroutine(CurrentCoroutine);
            CurrentCoroutine = null;
            isPlay = false;
            isEnding = false;
            _endTimer = 0;
        }
    }

    public int Rand(int i)
    {
        System.Random rnd = new System.Random();
        return rnd.Next(0, i * 10) % i;
    }
}
