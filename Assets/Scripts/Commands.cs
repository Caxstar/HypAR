using UnityEngine;


public class Commands : MonoBehaviour
{
    public GameObject openButton;
    // Called by GazeGestureManager when the user performs a Select gesture
    void OnReset()
    {
        Instantiate(openButton);
    }
}
