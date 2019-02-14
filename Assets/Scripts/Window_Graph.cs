/* 
 * WINDOW_GRAPH.CS
 * DESCRIPTION: This file contains the script to create a graph on the hololens when deployed at run time. It reads an xml file of the data and parses its rows.
 *              The format of the xml file is row by row. The algorithm which I referenced to plot each dat point was found at: https://catlikecoding.com/unity/tutorials/basics/building-a-graph/ 
 * 
 * 
 * AUTHOR: CASEY ROGERS
 * EMAIL: Casey.Rogers@data61.csiro.au
 * CREATED: 23/12/2018
 * LAST UPDATED: 03/01/2019
 * LAST UPDATED BY: CASEY ROGERS
 * 
 * BUGS: The graph scales depending on the row selected. The start point will need to be set to a default value
 *  
 * PROGRESS: The graph is able to be plotted and I have fixed the optimization issue
 */



using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;





public class Window_Graph : MonoBehaviour {

    //sprite where the points will be mapped onto
    [SerializeField] private Sprite circleSprite;

    List<double> list1a;
    List<GameObject> listOfCircles;
    List<GameObject> listOfConnections;


    //Chnage the row in the CSV file
    [Tooltip("Row number")]
    public int rowNumber = 1;

    [SerializeField]private RectTransform graphContainer;



    //This function is called when the graph is first spawned
    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();

        //ChangeGraph(rowNumber);
        List<double> list1a = CSVReader.Read("VNIR_SWIR_Spectra", rowNumber); //contains all the reflectance values of 1a mineral
        listOfCircles = new List<GameObject>();
        listOfConnections = new List<GameObject>();
        ShowGraph(list1a, listOfCircles, listOfConnections);
    }

    public void ChangeGraph(int number)
    {
        rowNumber = number;
        List<double> list1a = CSVReader.Read("VNIR_SWIR_Spectra", rowNumber); //contains all the reflectance values of 1a mineral
        DeleteOldGraph();
        ShowGraph(list1a, listOfCircles, listOfConnections);
    }

    /*
     * Here I will need to create a temporary graph where list1a will be held(default)
     * I create a new list which will be the updated list and then delete the temporary list
     * TODO: The temporary list will need to use some sort of delete method 
     * templist1a = CSVReader.Read(file) - row 1
     * newlist1a = CSVReader.Read(file) - row 2
     * delete templist1a*/
    public void DeleteOldGraph()
    {
        foreach (GameObject circle in listOfCircles)
            Destroy(circle);
        foreach (GameObject dot in listOfConnections)
            Destroy(dot);
    }

    //Initialize all values for the circle when called
    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        //size and dimentions of the circle created
        rectTransform.sizeDelta = new Vector2(0.005f,0.005f); //these values can be customized
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        return gameObject;
    }


    private void ShowGraph(List<double> list1a, List<GameObject> listOfCircles, List<GameObject> listOfConnections)
    {

        //Initialize the size of the graph and plot all the values onto it
        double graphHeight = graphContainer.sizeDelta.y; // very large value

        //These values control the length and width of the graph
        //The lines and dots need to stay between the bounds of the graphContainer
        double yMaximum = 0.6f;
        double xSize = 0.02;

        GameObject lastCircleGameObject = null; //This is the first value
        for (int i = 1; i < list1a.Count; i++)
        {
            double xPosition = xSize + i * xSize;
            double yPosition = (list1a[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2((float)xPosition, (float)yPosition));
            listOfCircles.Add(circleGameObject); //For deletion when changing graphs
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    //Connect dots together with a line
    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        //size and dimensions of the line created
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 0.005f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
        listOfConnections.Add(gameObject);
    }


}
