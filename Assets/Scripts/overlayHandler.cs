using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overlayHandler : MonoBehaviour {


    public void turnOnMineral()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);

        }
    }

    public void turnOffMineral()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
