using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class UIController : MonoBehaviour
{
    public RecordingManager recordingManager;

    [Header("Right Panel")]
    public GameObject recordButton;
    public GameObject playButton;
    public GameObject additiveButton;
    public GameObject saveButton;
    public GameObject stopButton;

    // Start is called before the first frame update
    void Awake()
    {
        recordButton.GetComponent<
    }

    public void temp(RecordingMode r_mode)
    {
        recordingManager.StartRecording(r_mode);
        return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
