using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class DB_Manager : MonoBehaviour
{
    public string _DBurl = "https://a-free-trip-default-rtdb.firebaseio.com/";
    private DatabaseReference _reference;
    
    // Start is called before the first frame update
    void Start()
    {
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(_DBurl);
        WriteDB();
        ReadDB();
    }

    public void WriteDB()
    {
        GPSdata data1 = new GPSdata("Seoul", 37.0f, 23.4f, 123f);
        GPSdata data2 = new GPSdata("Busan", 137.0f, 1223.4f, 13.5f);
        GPSdata data3 = new GPSdata("Daegu", 237.0f, 223.4f, 0.3f);
        string jsonData1 = JsonUtility.ToJson(data1);
        string jsonData2 = JsonUtility.ToJson(data2);
        string jsonData3 = JsonUtility.ToJson(data3);

        _reference.Child("Korea").Child("area1").SetRawJsonValueAsync(jsonData1);
        _reference.Child("Korea").Child("area2").SetRawJsonValueAsync(jsonData2);
        _reference.Child("Korea").Child("area3").SetRawJsonValueAsync(jsonData3);
    }

    public void ReadDB() 
    {
        _reference = FirebaseDatabase.DefaultInstance.GetReference("Korea");
        _reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapShot = task.Result;

                foreach (DataSnapshot data in snapShot.Children)
                {
                    IDictionary GPSdata = (IDictionary)data.Value;
                    Debug.Log($"이름 : {GPSdata["_name"]}, 위도 : {GPSdata["_latitudeData"]}, 경도 {GPSdata["_longitudeData"]}, " +
                        $"      고도 {GPSdata["_altitudeData"]}");
                }
            }
        });
    }
}

public class GPSdata
{
    public string _name = "";
    public float _latitudeData = 0;
    public float _longitudeData = 0;
    public float _altitudeData = 0;

    public GPSdata(string name, float lat, float lon, float alt)
    {
        _name = name;
        _latitudeData = lat;
        _longitudeData = lon;
        _altitudeData = alt;
    }
}
