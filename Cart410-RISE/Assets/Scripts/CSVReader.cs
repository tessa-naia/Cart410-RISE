using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    private TextAsset seaLevelData; // store the string from csv
    private List<string[]> dataLists = new List<string[]>(); // store each cell
    
    // Start is called before the first frame update
    void Start()
    {
        // load Assets/Resources/ipcc_ar6_sea_level_projection_global/Total.csv
        // remove "Assets/Resources/" and ".csv"
        if (load("ipcc_ar6_sea_level_projection_global/Total")){
            parse();
        }

        print(getNumber("ssp119", 5, 2150));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool load(string path){
        seaLevelData = Resources.Load<TextAsset>(path);

        if (seaLevelData != null){
            return true;
        }else{
            print("Data not loaded!");
        }
        return false;
    }

    // separate all the elements and push them to a list of arrays
    void parse(){
        // separate the data with new lines
        string[] dataRows = seaLevelData.text.Split('\n');

        for(int i = 0; i < dataRows.Length; i++){
            // separate each line with commas
            string[] column = dataRows[i].Split(',');
            dataLists.Add(column); // add the array to the list
        }
    }

    // return the number by giving specific scenario, quantile, year
    string getNumber(string scenario, int quantile, int year){
        // the first row is the first array
        // find the index of the year
        var yearId = -1;
        // the year starts from index 4 (column)
        for(int i = 4; i < dataLists[0].Length; i++){
            int temp;
            int.TryParse(dataLists[0][i], out temp);
            if (temp == year){
                yearId = i;
            }
        }
        // if no year is found
        if (yearId < 0){
            print("Year is invalid.");
            return null;
        }
         // the real data starts from index 1 (row)
        for(int i = 1; i < dataLists.Count; i++){
            // find the scenario
            if (dataLists[i][2] == scenario){
                // find the quantile
                 int temp;
                 int.TryParse(dataLists[i][3], out temp);
                if (temp == quantile){
                    return dataLists[i][yearId];
                }
            }
        }
        print("Nothing is found.");
        return null;
    }
}
