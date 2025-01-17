using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class SerializeData : MonoBehaviour
{
    void Start()
    {
        Seriablize();
    }

    void Seriablize()
    {
        string path = Application.persistentDataPath + "/SampleJson.json";

        string json = JsonConvert.SerializeObject(new Models(), Formatting.Indented);
        File.WriteAllText(path, json);
    }

    void DeSerialize(string json)
    {
    }
}