using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Text;
using System.IO;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ActionRecorder : MonoBehaviour
{
    public GameObject recordedObject;
    bool recordingEnabled = true;

    bool isRecording = false;
    bool isPlaying = false;
    bool isAdditive = false;

    Controls controls;
    MovementController movementController;

    float startTime;
    float recordedTime;
    Vector3 startPosition;
    [SerializeField]
    List<ActionTime> recordedActions = new List<ActionTime>();
    List<ActionTime> additiveActions = new List<ActionTime>();

    float playbackStartTime;
    Coroutine playbackRoutine;

    public TextAsset fileToLoad;

    private void Awake()
    {
        controls = new Controls();
        controls.Movement.Left.performed += _ => RecordMove(ActionType.LeftStart);
        controls.Movement.Left.canceled += _ => RecordMove(ActionType.LeftEnd);
        controls.Movement.Right.performed += _ => RecordMove(ActionType.RightStart);
        controls.Movement.Right.canceled += _ => RecordMove(ActionType.RightEnd);
        controls.Movement.Down.performed += _ => RecordMove(ActionType.DownStart);
        controls.Movement.Down.canceled += _ => RecordMove(ActionType.DownEnd);
        controls.Movement.Jump.performed += _ => RecordMove(ActionType.JumpStart);
        controls.Movement.Jump.canceled += _ => RecordMove(ActionType.JumpEnd);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        startTime = Time.time;
        startPosition = recordedObject.transform.position;
        movementController = recordedObject.GetComponent<MovementController>();
    }


    #region Playback
    public void StartPlayback()
    {
        //Debug.Log("Starting Move Playback");
        //ChangeControlState(false);
        //isRecording = isAdditive ? true :false;
        recordedObject.transform.position = startPosition;
        recordedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (recordedActions.Count > 0)
        {
            isPlaying = true;
            playbackRoutine = StartCoroutine(PlayingMoves());
        }
        else
        {
            Debug.Log(this.gameObject.name + ": No moves to play");
        }

    }

    public IEnumerator PlayingMoves()
    {
        playbackStartTime = Time.time;

        foreach (ActionTime nextMove in recordedActions)
        {
            while (isPlaying)
            {
                if (Time.time - playbackStartTime >= nextMove.time)
                {
                    switch (nextMove.action)
                    {
                        case ActionType.LeftStart:
                            movementController?.MoveHorizontalStart(MoveDir.Left);
                            break;
                        case ActionType.LeftEnd:
                            movementController?.MoveHorizontalEnd(MoveDir.Left);
                            break;
                        case ActionType.RightStart:
                            movementController?.MoveHorizontalStart(MoveDir.Right);
                            break;
                        case ActionType.RightEnd:
                            movementController?.MoveHorizontalEnd(MoveDir.Right);
                            break;
                        case ActionType.DownStart:
                            movementController?.DownStart();
                            break;
                        case ActionType.DownEnd:
                            movementController?.DownEnd();
                            break;
                        case ActionType.JumpStart:
                            movementController?.JumpStart();
                            break;
                        case ActionType.JumpEnd:
                            movementController?.JumpEnd();
                            break;
                    }
                    break;
                }

                yield return null;
            }
        }

        //Debug.Log("Playback Ended");
    }

    public void StopPlayback()
    {
        if (isPlaying)
        {
            //Debug.Log("Stopping playback");
            this.StopCoroutine(playbackRoutine);
            isPlaying = false;
        }
    }
    #endregion

    #region Recording
    public void StartRecording(RecordingMode recordingMode)
    {
        //StopPlayback();

        if (recordingMode == RecordingMode.Overwrite && recordingEnabled)
        {
            ClearRecordedMoves();
        }
        //ChangeControlState(true);

        Debug.Log("Starting Move Recording");
        startTime = Time.time;

        // Player Positioning
        recordedObject.transform.position = startPosition;
        recordedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if ((recordingMode == RecordingMode.Overwrite || recordingMode == RecordingMode.Additive) && recordingEnabled )
        {
            isRecording = true;
        }
        if (recordingMode == RecordingMode.Additive)
        {
            ClearRecordedMoves(true);
            StartPlayback();
            isAdditive = true;
        }
    }

    private void RecordMove(ActionType newAction)
    {
        if (isRecording)
        {
            recordedTime = Time.time - startTime;
            if (isAdditive)
                additiveActions.Add(new ActionTime(newAction, recordedTime));
            else
                recordedActions.Add(new ActionTime(newAction, recordedTime));
        }
    }

    private void ClearRecordedMoves(bool clearAdditive = false)
    {
        if (clearAdditive)
        {
            //Debug.Log("Clearing Recorded Moves");
            additiveActions.Clear();
        }
        else
        {
            //Debug.Log("Clearing Recorded Moves");
            recordedActions.Clear();
        }
    }

    public void StopRecording()
    {
        if (isRecording)
        {
            Debug.Log("Stopping Recording");
            isRecording = false;

            if (isAdditive)
            {
                CombineRecordings();
                isAdditive = false;
            }
        }
    }

    private void CombineRecordings()
    {
        int originalIndex = 0;
        List<ActionTime> tempCombined = new List<ActionTime>();

        int i = 0, j = 0;
        bool done = false;

        while (!done)
        {
            if (additiveActions[i].time < recordedActions[j].time)
            {
                tempCombined.Add(additiveActions[i]);
                i++;
            }
            else if (recordedActions[j].time <= additiveActions[i].time)
            {
                tempCombined.Add(recordedActions[j]);
                j++;
            }

            if (i == additiveActions.Count || j == recordedActions.Count)
                done = true;
        }

        if (i < additiveActions.Count)
        {
            for (; i < additiveActions.Count; i++)
                tempCombined.Add(additiveActions[i]);
        }
        else if (j < recordedActions.Count)
        {
            for (; j < recordedActions.Count; j++)
                tempCombined.Add(recordedActions[j]);
        }

        recordedActions.Clear();
        recordedActions = tempCombined;
    }
    #endregion

    #region ActionTime definition
    struct ActionTime
    {
        public ActionTime(ActionType newAction, float newTime)
        {
            action = newAction;
            time = newTime;
        }

        public ActionType action;
        public float time;
    }

    private enum ActionType
    {
        // MOVEMENT actions
        LeftStart,
        LeftEnd,
        RightStart,
        RightEnd,
        DownStart,
        DownEnd,
        JumpStart,
        JumpEnd
    }
    #endregion

    #region file i/o
    public string ToCSV()
    {
        var sb = new StringBuilder("ActionType,Time");
        foreach(var ActionItem in recordedActions)
        {
            sb.Append('\n');
            sb.Append(ActionItem.action.ToString());
            sb.Append(',');
            sb.Append(ActionItem.time.ToString());
        }
        return sb.ToString();
    }

    public void SaveToFile()
    {
        if (recordedActions.Count <= 0)
        {
            Debug.Log("Nothing to save for: " + this.gameObject.name);
            return;
        }

        var content = ToCSV();

#if UNITY_EDITOR
        var folder = Path.Combine(Application.streamingAssetsPath);
        if (!Directory.Exists(folder)) 
            Directory.CreateDirectory(folder);
#else
    var folder = Application.persistentDataPath;
#endif
        var filePath = Path.Combine(folder, SceneManager.GetActiveScene().name + "_" + this.gameObject.name + "_export.csv");

        using(var writer = new StreamWriter(filePath, false))
        {
            writer.Write(content);
        }

        Debug.Log($"CSV file written to \"{filePath}\"");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
    #endregion

    public void LoadFromFile()
    {

    }
}

public enum RecordingMode
{
    Overwrite,
    Additive
}
