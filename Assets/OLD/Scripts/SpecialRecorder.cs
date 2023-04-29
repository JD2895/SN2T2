using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Text;
using System.IO;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpecialRecorder : MonoBehaviour
{
    public GameObject recordedObject;
    bool recordingEnabled = true;

    bool isRecording = false;
    bool isPlaying = false;
    bool isAdditive = false;

    Controls controls;
    SpecialControllerBase specialController;

    float startTime;
    float recordedTime;
    [SerializeField]
    List<SpecialTime> currentSpecials = new List<SpecialTime>();
    List<SpecialTime> savedSpecials = new List<SpecialTime>();
    List<SpecialTime> additiveSpecials = new List<SpecialTime>();

    float playbackStartTime;
    Coroutine playbackRoutine;

    public UnityEngine.Object fileToLoad;

    private void Awake()
    {
        controls = new Controls();
        controls.Special.Q.performed += _ => RecordSpecial(SpecialType.Q);
        controls.Special.W.performed += _ => RecordSpecial(SpecialType.W);
        controls.Special.E.performed += _ => RecordSpecial(SpecialType.E);
        controls.Special.R.performed += _ => RecordSpecial(SpecialType.R);
        controls.Special.T.performed += _ => RecordSpecial(SpecialType.T);
        controls.Special.Y.performed += _ => RecordSpecial(SpecialType.Y);
        controls.Special.U.performed += _ => RecordSpecial(SpecialType.U);

        controls.Special.A.performed += _ => RecordSpecial(SpecialType.A);
        controls.Special.S.performed += _ => RecordSpecial(SpecialType.S);
        controls.Special.D.performed += _ => RecordSpecial(SpecialType.D);
        controls.Special.F.performed += _ => RecordSpecial(SpecialType.F);
        controls.Special.G.performed += _ => RecordSpecial(SpecialType.G);
        controls.Special.H.performed += _ => RecordSpecial(SpecialType.H);
        controls.Special.J.performed += _ => RecordSpecial(SpecialType.J);

        controls.Special.Z.performed += _ => RecordSpecial(SpecialType.Z);
        controls.Special.X.performed += _ => RecordSpecial(SpecialType.X);
        controls.Special.C.performed += _ => RecordSpecial(SpecialType.C);
        controls.Special.V.performed += _ => RecordSpecial(SpecialType.V);
        controls.Special.B.performed += _ => RecordSpecial(SpecialType.B);
        controls.Special.N.performed += _ => RecordSpecial(SpecialType.N);
        controls.Special.M.performed += _ => RecordSpecial(SpecialType.M);
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
        specialController = recordedObject.GetComponent<SpecialControllerBase>();
        ReadFromFile();
    }

    #region Playback
    public void StartPlayback()
    {
        //TODO: Have a way to reset special. Each special will define its own reset

        if (savedSpecials.Count > 0)
        {
            isPlaying = true;
            playbackRoutine = StartCoroutine(PlayingSpecials());
        }
        else
        {
            Debug.Log(this.gameObject.name + ": No Specials to play");
        }

    }

    public IEnumerator PlayingSpecials()
    {
        playbackStartTime = Time.time;

        foreach (SpecialTime nextMove in savedSpecials)
        {
            while (isPlaying)
            {
                if (Time.time - playbackStartTime >= nextMove.time)
                {
                    switch (nextMove.special)
                    {
                        case SpecialType.Q:
                            specialController?.Q_start();
                            break;
                        case SpecialType.W:
                            specialController?.W_start();
                            break;
                        case SpecialType.E:
                            specialController?.E_start();
                            break;
                        case SpecialType.R:
                            specialController?.R_start();
                            break;
                        case SpecialType.T:
                            specialController?.T_start();
                            break;
                        case SpecialType.Y:
                            specialController?.Y_start();
                            break;
                        case SpecialType.U:
                            specialController?.U_start();
                            break;

                        case SpecialType.A:
                            specialController?.A_start();
                            break;
                        case SpecialType.S:
                            specialController?.S_start();
                            break;
                        case SpecialType.D:
                            specialController?.D_start();
                            break;
                        case SpecialType.F:
                            specialController?.F_start();
                            break;
                        case SpecialType.G:
                            specialController?.G_start();
                            break;
                        case SpecialType.H:
                            specialController?.H_start();
                            break;
                        case SpecialType.J:
                            specialController?.J_start();
                            break;

                        case SpecialType.Z:
                            specialController?.Z_start();
                            break;
                        case SpecialType.X:
                            specialController?.X_start();
                            break;
                        case SpecialType.C:
                            specialController?.C_start();
                            break;
                        case SpecialType.V:
                            specialController?.V_start();
                            break;
                        case SpecialType.B:
                            specialController?.B_start();
                            break;
                        case SpecialType.N:
                            specialController?.N_start();
                            break;
                        case SpecialType.M:
                            specialController?.M_start();
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
            ClearRecordedSpecials();
        }
        //ChangeControlState(true);

        Debug.Log("Starting Move Recording");
        startTime = Time.time;

        // Player Positioning
        recordedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if ((recordingMode == RecordingMode.Overwrite || recordingMode == RecordingMode.Additive) && recordingEnabled)
        {
            isRecording = true;
            isAdditive = false;
        }
        if (recordingMode == RecordingMode.Additive)
        {
            ClearRecordedSpecials(true);
            StartPlayback();
            isAdditive = true;
        }
    }

    private void RecordSpecial(SpecialType newSpecial)
    {
        if (isRecording)
        {
            recordedTime = Time.time - startTime;
            if (isAdditive)
                additiveSpecials.Add(new SpecialTime(newSpecial, recordedTime));
            else
                currentSpecials.Add(new SpecialTime(newSpecial, recordedTime));
        }
    }

    private void ClearRecordedSpecials(bool clearAdditive = false)
    {
        if (clearAdditive)
        {
            //Debug.Log("Clearing Recorded Moves");
            additiveSpecials.Clear();
        }
        else
        {
            //Debug.Log("Clearing Recorded Moves");
            currentSpecials.Clear();
        }
    }

    public void SaveInProgressRecording()
    {
        if (isRecording)
        {
            Debug.Log("Stopping Recording");
            isRecording = false;

            if (isAdditive)
            {
                if (savedSpecials.Count == 0)
                {
                    savedSpecials = additiveSpecials;
                }
                else if (additiveSpecials.Count > 0)
                    CombineRecordings();
                isAdditive = false;
            }
            else
            {
                savedSpecials = currentSpecials;
            }
        }
    }

    public void CancelInProgressRecording()
    {
        Debug.Log("Canelling Recording");
        if (isRecording)
        {
            isRecording = false;

            if (isAdditive)
            {
                isAdditive = false;
            }
        }
    }

    private void CombineRecordings()
    {
        Debug.Log("Combining Recordings");
        List<SpecialTime> tempCombined = new List<SpecialTime>();

        int i = 0, j = 0;
        bool done = false;

        // Go through both lists while comparing time stamps
        while (!done)
        {
            if (additiveSpecials[i].time < savedSpecials[j].time)
            {
                tempCombined.Add(additiveSpecials[i]);
                i++;
            }
            else if (savedSpecials[j].time <= additiveSpecials[i].time)
            {
                tempCombined.Add(savedSpecials[j]);
                j++;
            }

            if (i == additiveSpecials.Count || j == savedSpecials.Count)
                done = true;
        }

        // Fill out with remaining timestamps
        if (i < additiveSpecials.Count)
        {
            for (; i < additiveSpecials.Count; i++)
                tempCombined.Add(additiveSpecials[i]);
        }
        else if (j < savedSpecials.Count)
        {
            for (; j < savedSpecials.Count; j++)
                tempCombined.Add(savedSpecials[j]);
        }

        savedSpecials = tempCombined;
    }
    #endregion

    #region File i/o
    public string ToCSV()
    {
        var sb = new StringBuilder();
        foreach (var SpecialItem in savedSpecials)
        {
            sb.Append(SpecialItem.special.ToString());
            sb.Append(',');
            sb.Append(SpecialItem.time.ToString());
            sb.Append('\n');
        }
        return sb.ToString();
    }

    public void WriteToFile()
    {
        if (savedSpecials.Count <= 0)
        {
            Debug.Log("No Special to save for: " + this.gameObject.name);
            return;
        }

        var content = ToCSV();

#if UNITY_EDITOR
        var folder = Path.Combine(Application.streamingAssetsPath);
        folder += "/" + SceneManager.GetActiveScene().name;
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
#else
    var folder = Application.persistentDataPath;
#endif
        var filePath = Path.Combine(folder, SceneManager.GetActiveScene().name + "_" + this.gameObject.name + "_SP.csv");

        using (var writer = new StreamWriter(filePath, false))
        {
            writer.Write(content);
        }

        Debug.Log($"CSV file written to \"{filePath}\"");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    public void ReadFromFile()
    {
        //removing for web build
        //string path = AssetDatabase.GetAssetPath(fileToLoad);
        string path = "";
        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                SpecialType newAction;
                float newActionTime;
                savedSpecials.Clear();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitString = line.Split(char.Parse(","));
                    newAction = (SpecialType)System.Enum.Parse(typeof(SpecialType), splitString[0]);
                    newActionTime = float.Parse(splitString[1]);
                    savedSpecials.Add(new SpecialTime(newAction, newActionTime));
                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Debug.Log(this.gameObject.name + ": The file could not be read:");
            Debug.Log(e.Message);
        }
    }
    #endregion

    #region Stop
    public void Stop()
    {
        StopPlayback();
        SaveInProgressRecording();
    }
    #endregion

    #region Special definition
    struct SpecialTime
    {
        public SpecialTime(SpecialType newSpecial, float newTime)
        {
            special = newSpecial;
            time = newTime;
        }

        public SpecialType special;
        public float time;
    }

    private enum SpecialType
    {
        // Special actions
        Q, W, E, R, T, Y, U,
        A, S, D, F, G, H, J,
        Z, X, C, V, B, N, M

    }
    #endregion
}
