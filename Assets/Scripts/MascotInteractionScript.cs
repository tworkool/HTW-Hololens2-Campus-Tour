using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class MascotInteractionAction
{
    public static MascotInteractionAction LastPlayedInstance;
    public string actionName;
    public AudioClip audioClip;

    public void PlayActionItem(AudioSource audioSource)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        LastPlayedInstance = this;
    }
}

public class MascotInteractionScript : MonoBehaviour
{
    private AudioSource _virtualFollowerAudioSource;
    [SerializeField]
    public List<MascotInteractionAction> actionItems = new List<MascotInteractionAction>();
    private Dictionary<string, MascotInteractionAction> actionItemsDict;

    void Start()
    {
        _virtualFollowerAudioSource = GetComponent<AudioSource>();
        actionItemsDict = actionItems.ToDictionary(item => item.actionName, item => item);
        actionItemsDict["StartDialog"].PlayActionItem(_virtualFollowerAudioSource);
    }

    void Update()
    {
        var speechIndicator = transform.Find("extra").Find("SpeechIndicator").gameObject;
        Debug.Log(speechIndicator.name);
        var speechIndicatorRenderer = speechIndicator.GetComponent<MeshRenderer>();
        if (_virtualFollowerAudioSource.isPlaying && !speechIndicatorRenderer.enabled)
        {
            speechIndicatorRenderer.enabled = true;
        }
        if (!_virtualFollowerAudioSource.isPlaying && speechIndicatorRenderer.enabled)
        {
            speechIndicatorRenderer.enabled = false;
        }
    }
}
