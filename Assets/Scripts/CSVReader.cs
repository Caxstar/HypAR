/* 
 * CSVREADER.CS
 * DESCRIPTION: This file contains the script for reading the spectra data file in the RESOURCES folder. 
 * 
 * 
 * AUTHOR: CASEY ROGERS
 * EMAIL: Casey.Rogers@data61.csiro.au
 * CREATED: 04/01/2018
 * LAST UPDATED: 04/01/2019
 * LAST UPDATED BY: CASEY ROGERS
 * 
 * BUGS: There are some optimization issues when depoying on the Hololens using this method. (Lag occurs)
 * PROGRESS: ONGOING
 */
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class CSVReader
{

    public static List<double> Read(string file, int rowNumber)
    {
        var list = new List<double>(); //contains specified value
        double n;
        TextAsset data = Resources.Load(file) as TextAsset;
        string[] lines = Regex.Split(data.text, "\n"); //each line 
        for(int i = 1; i < lines.Length - 1; i = i + 30)
        {
            string[] str = lines[i].Split(',');
            double.TryParse(str[rowNumber], out n);
            list.Add(n);
        }
        return list;
    }   
        
    
}
