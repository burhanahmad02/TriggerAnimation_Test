
using System.IO;
using UnityEngine;
using TMPro;

public class TextLoader : MonoBehaviour
{
    public string fileName = "TextData.csv";
    public TMP_Text textField;
    public int index;

    void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            if (index < lines.Length)
            {
                textField.text = lines[index];
            }
        }
    }
}
