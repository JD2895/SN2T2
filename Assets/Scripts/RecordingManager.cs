using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecordingManager : MonoBehaviour
{
    public List<ActorItem> cast;

    public AudioSource metrnonomeStart;
    public AudioSource voiceClip;

    private void Start()
    {
        SetControl();
    }

    public void SetControl()
    {
        for(int i = 0; i < cast.Count; i++)
        {
            if(cast[i].actor != null)
            {
                cast[i].actor.GetComponent<MovementController>().SetControllable(cast[i].movementEnabled);
                cast[i].actor.GetComponent<SpecialControllerBase>().SetControllable(cast[i].specialEnabled);
            }
                
        }
    }

    public void SetMovementControlIndex(int i)
    {
        cast[i].actor.GetComponent<MovementController>().SetControllable(cast[i].movementEnabled);
    }

    public void SetSpecialControlIndex(int i)
    {
        cast[i].actor.GetComponent<SpecialControllerBase>().SetControllable(cast[i].specialEnabled);
    }

    public void StartRecording(RecordingMode recordingMode)
    {
        StopAudio();

        for (int i = 0; i < cast.Count; i++)
        {
            if (cast[i].actor != null)
            {
                cast[i].actor.GetComponent<ActionRecorder>().SaveInProgressRecording();
                cast[i].actor.GetComponent<ActionRecorder>().StopPlayback();
                cast[i].actor.GetComponent<SpecialRecorder>().SaveInProgressRecording();
                cast[i].actor.GetComponent<SpecialRecorder>().StopPlayback();
                cast[i].actor.GetComponent<MovementController>().Reset();
                cast[i].actor.GetComponent<SpecialControllerBase>().Reset();

                if (cast[i].recordingEnabled)
                {
                    cast[i].actor.GetComponent<ActionRecorder>().StartRecording(recordingMode);
                    cast[i].actor.GetComponent<SpecialRecorder>().StartRecording(recordingMode);
                }
                else
                {
                    cast[i].actor.GetComponent<ActionRecorder>().StartPlayback();
                    cast[i].actor.GetComponent<SpecialRecorder>().StartPlayback();
                }
            }
        }

        PlayAudio();
    }

    public void StartPlayback()
    {
        StopAudio();

        for (int i = 0; i < cast.Count; i++)
        {
            if (cast[i].actor != null)
            {
                cast[i].actor.GetComponent<ActionRecorder>().SaveInProgressRecording();
                cast[i].actor.GetComponent<ActionRecorder>().StopPlayback();
                cast[i].actor.GetComponent<SpecialRecorder>().SaveInProgressRecording();
                cast[i].actor.GetComponent<SpecialRecorder>().StopPlayback();

                cast[i].actor.GetComponent<MovementController>().Reset();
                cast[i].actor.GetComponent<ActionRecorder>().StartPlayback();
                cast[i].actor.GetComponent<SpecialControllerBase>().Reset();
                cast[i].actor.GetComponent<SpecialRecorder>().StartPlayback();
            }
        }

        PlayAudio();
    }

    public void SaveRecordings()
    {
        StopAudio();

        for (int i = 0; i < cast.Count; i++)
        {
            if (cast[i].actor != null)
            {
                cast[i].actor.GetComponent<ActionRecorder>().WriteToFile();
                cast[i].actor.GetComponent<SpecialRecorder>().WriteToFile();
            }
        }
    }

    public void Stop()
    {
        StopAudio();

        for (int i = 0; i < cast.Count; i++)
        {
            if (cast[i].actor != null)
            {
                cast[i].actor.GetComponent<ActionRecorder>().Stop();
                cast[i].actor.GetComponent<MovementController>().Reset();
                cast[i].actor.GetComponent<SpecialRecorder>().Stop();
                cast[i].actor.GetComponent<SpecialControllerBase>().Reset();
            }
        }
    }

    public void Cancel()
    {
        StopAudio();

        for (int i = 0; i < cast.Count; i++)
        {
            if (cast[i].actor != null && cast[i].recordingEnabled)
            {
                cast[i].actor.GetComponent<ActionRecorder>().CancelInProgressRecording();
                cast[i].actor.GetComponent<SpecialRecorder>().CancelInProgressRecording();
            }
        }
    }

    void PlayAudio()
    {
        metrnonomeStart.Play();
        voiceClip.Play(144000);
    }

    void StopAudio()
    {
        metrnonomeStart.Stop();
        voiceClip.Stop();
    }
}

[Serializable]
public class ActorItem
{
    public GameObject actor;
    public bool recordingEnabled;
    public bool movementEnabled;
    public bool specialEnabled;
}
