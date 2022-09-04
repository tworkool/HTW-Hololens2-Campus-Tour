using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MascotInteractionAction
{
    public string actionName;
    public AudioClip audioClip;

    public void PlayActionItem(AudioSource audioSource)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}

public class MascotInteractionScript : MonoBehaviour
{
    private AudioSource _virtualFollowerAudioSource;
    public MascotInteractionAction[] actionItems;
    private Dictionary<string, MascotInteractionAction> actionItemsDict;

    // Start is called before the first frame update
    void Start()
    {
        _virtualFollowerAudioSource = GetComponent<AudioSource>();
        actionItemsDict = actionItems.ToDictionary(item => item.actionName, item => item);
        // ADD "StartDialog" here
        actionItemsDict["StartDialog"].PlayActionItem(_virtualFollowerAudioSource);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
