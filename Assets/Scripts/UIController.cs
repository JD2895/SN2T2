using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.InputSystem.UI;

public class UIController : MonoBehaviour
{
    public RecordingManager recordingManager;

    [Header("Right Panel")]
    public Button recordButton;
    public Button playButton;
    public Button additiveButton;
    public Button saveButton;
    public Button stopButton;

    [Header("Left Panel")]
    public Toggle recordToggle;
    public Toggle movementToggle;
    public Toggle specialToggle;
    
    public Transform castToggleListContainer;
    public GameObject castTogglePrefab;

    private Dictionary<Toggle, ActorItem> toggleReference = new Dictionary<Toggle, ActorItem>();

    // Start is called before the first frame update
    void Start()
    {
        recordButton.onClick.AddListener(() => recordingManager.StartRecording(RecordingMode.Overwrite));
        playButton.onClick.AddListener(() => recordingManager.StartPlayback());
        additiveButton.onClick.AddListener(() => recordingManager.StartRecording(RecordingMode.Additive));
        saveButton.onClick.AddListener(() => recordingManager.SaveRecordings());
        stopButton.onClick.AddListener(() => recordingManager.Stop());

        recordToggle.onValueChanged.AddListener(delegate { RecordToggled(); });
        movementToggle.onValueChanged.AddListener(delegate { MovementToggled(); });
        specialToggle.onValueChanged.AddListener(delegate { SpecialToggled(); });

        foreach (var castmember in recordingManager.cast)
        {
            GameObject newToggleObject = Instantiate(castTogglePrefab, castToggleListContainer);
            newToggleObject.GetComponentInChildren<Text>().text = castmember.actor.name;
            Toggle newToggle = newToggleObject.GetComponent<Toggle>();
            newToggle.onValueChanged.AddListener(delegate
            {
                CastSelected(newToggle);
            });
            toggleReference.Add(newToggleObject.GetComponent<Toggle>(), castmember);
        }
    }

    void RecordToggled()
    { 
        foreach(KeyValuePair<Toggle, ActorItem> kvp in toggleReference)
        {
            int itemIndex = FindInCastList(kvp.Value);
            recordingManager.cast[itemIndex].recordingEnabled = (kvp.Key.isOn && recordToggle.isOn);
        }
    }

    void MovementToggled()
    {
        foreach (KeyValuePair<Toggle, ActorItem> kvp in toggleReference)
        {
            int itemIndex = FindInCastList(kvp.Value);
            recordingManager.cast[itemIndex].movementEnabled = (kvp.Key.isOn && movementToggle.isOn);
            recordingManager.SetControlIndex(itemIndex);
        }
    }

    void SpecialToggled()
    {
        foreach (KeyValuePair<Toggle, ActorItem> kvp in toggleReference)
        {
            int itemIndex = FindInCastList(kvp.Value); recordingManager.cast[itemIndex].specialEnabled = (kvp.Key.isOn && specialToggle.isOn);
            //recordingManager.SetControlIndex(itemIndex); but for special
        }

    }

    void CastSelected(Toggle castSelected)
    {
        ActorItem actorSelected = toggleReference[castSelected];
        int itemIndex = FindInCastList(actorSelected);

        recordingManager.cast[itemIndex].recordingEnabled = (castSelected.isOn && recordToggle.isOn);
        
        recordingManager.cast[itemIndex].movementEnabled = (castSelected.isOn && movementToggle.isOn);
        recordingManager.SetControlIndex(itemIndex);

        recordingManager.cast[itemIndex].specialEnabled = (castSelected.isOn && specialToggle.isOn);
        //recordingManager.SetControlIndex(itemIndex); but for special
    }

    int FindInCastList(ActorItem toFind)
    {
        for (int i= 0; i < recordingManager.cast.Count; i++)
        {
            if (toFind == recordingManager.cast[i])
                return i;
        }
        return -1;
    }
}
