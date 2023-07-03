using System.IO;
using UnityEngine;

[System.Serializable]
public class OptionData
{
    public bool Full;        
    public bool On;
    public bool GameSet_HasData;
    public bool AudioSet_HasData;
    public bool VideoSet_HasData;
    public bool High;
    public bool Middle;
    public bool Low;
    public int Resolution_Value;        
    public float Camera_Range;
    public float Total_Volume;
    public float Bgm;
    public float SE;

}

public partial class Save_Load
{
    public OptionData _optionData = new OptionData();
    private string SAVE_OPTIONNAME = "OptionSave.txt";

    public void LoadOptionData()
    {
        string filePath = SAVE_DATA_DIRECTORY + SAVE_OPTIONNAME;

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            _optionData = JsonUtility.FromJson<OptionData>(FromJsonData);
            print("불러오기 완료");
        }
    }

    public void SaveOptionData()
    {
        string ToJsonData = JsonUtility.ToJson(_optionData, true);
        string filePath = Application.persistentDataPath + "/" + SAVE_OPTIONNAME;

        File.WriteAllText(filePath, ToJsonData);

        print("저장 완료");
        print(filePath);
    }
}
