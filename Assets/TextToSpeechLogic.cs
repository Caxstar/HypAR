using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;

public class TextToSpeechLogic : MonoBehaviour{

    private TextToSpeech textToSpeech;
    public string speakText;
    // Use this for initialization
    private void Start()
    {
        textToSpeech = GetComponent<TextToSpeech>();
        var msg = string.Format(speakText, textToSpeech.Voice.ToString());
        textToSpeech.StartSpeaking(msg);
    }

}
