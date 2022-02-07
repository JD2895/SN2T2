using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionRecorder : MonoBehaviour
{
    public GameObject recordedObject;

    bool isRecording = false;
    bool isPlaying = false;

    Controls controls;
    MovementController movementController;

    float startTime;
    float recordedTime;
    Vector3 startPosition;
    [SerializeField]
    List<ActionTime> recordedActions = new List<ActionTime>();

    float playbackStartTime;
    Coroutine playbackRoutine;

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

        controls.Recorder.Record.performed += _ => StartRecording();
        controls.Recorder.Play.performed += _ => StartPlayback();
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
        if (recordedObject == null)
        {
            recordedObject = GameObject.Find("Player");
        }

        startTime = Time.time;
        startPosition = recordedObject.transform.position;
        movementController = recordedObject.GetComponent<MovementController>();
        StartRecording();
    }

    private void RecordMove(ActionType newAction)
    {
        if (isRecording)
        {
            recordedTime = Time.time - startTime;
            recordedActions.Add(new ActionTime(newAction, recordedTime));
        }
    }

    #region Playback
    private void StartPlayback()
    {
        Debug.Log("Starting Move Playback");
        //ChangeControlState(false);
        isRecording = false;
        recordedObject.transform.position = startPosition;
        recordedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (recordedActions.Count > 0)
        {
            isPlaying = true;
            playbackRoutine = StartCoroutine(PlayingMoves());
        }
        else
        {
            //Debug.Log("No moves to play");
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
                            movementController?.MoveLeftStart();
                            break;
                        case ActionType.LeftEnd:
                            movementController?.MoveLeftEnd();
                            break;
                        case ActionType.RightStart:
                            movementController?.MoveRightStart();
                            break;
                        case ActionType.RightEnd:
                            movementController?.MoveRightEnd();
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
    private void StartRecording()
    {
        // Prep
        ClearRecordedMoves(false);
        StopPlayback();
        //ChangeControlState(true);

        Debug.Log("Starting Move Recording");
        startTime = Time.time;

        // Player Positioning
        recordedObject.transform.position = startPosition;
        recordedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        isRecording = true;
    }

    private void ClearRecordedMoves(bool stopOtherActions = true)
    {
        //Debug.Log("Clearing Recorded Moves");
        if (stopOtherActions)
        {
            StopRecording();
            StopPlayback();
        }
        recordedActions.Clear();
    }

    private void StopRecording()
    {
        if (isRecording)
        {
            //Debug.Log("Stopping Recording");
            isRecording = false;
        }
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
}
