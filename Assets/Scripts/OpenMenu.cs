using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour {

    public GameObject openButton;
    public GameObject toggleObject;

    public void activateButton()
    {

       openButton.SetActive(true);

        
    }

    public void activateButton2()
    {
        if(toggleObject.activeSelf)
        {
            openButton.SetActive(true);
        }



    }

    public void checkMineralMap()
    {
        openButton.SetActive(false);
    }

}
