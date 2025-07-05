using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class TextLoader : MonoBehaviour
{
    public TMP_Text textField;
    public string boxId = "RedBoxText"; // Or GreenBoxText, etc.
    public string fileName = "TextData.csv";

    void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            Dictionary<string, string> textDict = new Dictionary<string, string>();

            for (int i = 1; i < lines.Length; i++) // skip header
            {
                string[] parts = lines[i].Split('\t'); // <== TAB separator

                if (parts.Length >= 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    textDict[key] = value;
                }
            }

            if (textDict.ContainsKey(boxId))
            {
                textField.text = textDict[boxId];
            }
            else
            {
                textField.text = "[No text found for " + boxId + "]";
            }
        }
        else
        {
            textField.text = "[CSV file not found]";
        }
    }
}
