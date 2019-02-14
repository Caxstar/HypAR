using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ShowOnVoiceCommand : MonoBehaviour, ISpeechHandler {

    public GameObject openButton;

    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {

            UpdateCommand(eventData.RecognizedText);

        
    }

    public void UpdateCommand(string command)
    {
        switch(command)
        {
            case "Show":
                Instantiate(openButton);
                break;


        }
    }
}
