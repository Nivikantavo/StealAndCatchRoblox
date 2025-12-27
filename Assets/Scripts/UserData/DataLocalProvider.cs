using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLocalProvider : IDataProvider
{
    private const string FileName = "PlayerSave";
    private const string SaveFileExtension = ".json";

    private IPersistenData _persistenData;

    public DataLocalProvider(IPersistenData persistenData) => _persistenData = persistenData;

    private string SavePath => Application.persistentDataPath;
    private string FullPath => Path.Combine(SavePath, $"{FileName}{SaveFileExtension}");

    public void Save()
    {
        File.WriteAllText(FullPath, JsonConvert.SerializeObject(_persistenData.UserData, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore}));
    }

    public bool TryLoad()
    {
        if(IsDataAlreadyExist() == false)
            return false;

        _persistenData.UserData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(FullPath));

        return true;
    }

    private bool IsDataAlreadyExist() => File.Exists(FullPath);
}
