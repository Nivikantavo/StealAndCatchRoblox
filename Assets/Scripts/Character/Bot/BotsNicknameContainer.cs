using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class BotsNicknameContainer
{
    private const string NicknamesFilePath = "Nikcnames1";
    private List<string> nicknames;
    private System.Random random = new System.Random();

    public BotsNicknameContainer()
    {
        LoadNicknamesFromResources(NicknamesFilePath);
    }

    private void LoadNicknamesFromResources(string resourcePath)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(resourcePath);
        Debug.Log($"Старт чтения никнеймов");
        if (textAsset != null)
        {
            nicknames = new List<string>();
            string[] lines = textAsset.text.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    nicknames.Add(line);
                }
            }

            Debug.Log($"Загружено {nicknames.Count} никнеймов");
        }
        else
        {
            Debug.LogError($"Не удалось загрузить файл никнеймов по пути: {resourcePath}");
            nicknames = new List<string> { "DefaultNickname" };
        }
    }

    public List<string> GetSeveralUniqueNicknames(int nicksCount)
    {
        int maxIterations = 5;
        List<string> result = new List<string>();
        for (int i = 0; i < nicksCount; i++)
        {
            int iterationCount = 0;
            int startNamesCount = result.Count;
            while (result.Count <= startNamesCount)
            {
                if(iterationCount >= maxIterations) 
                {
                    result.Add("DefaultNickname");
                    break; 
                }
                iterationCount++;
                string nickname = GetRandomNickname();
                if(result.Contains(nickname) == false)
                {
                    result.Add(nickname);
                }
            }

        }
        return result;
    }

    public string GetRandomNickname()
    {
        if (nicknames == null || nicknames.Count == 0)
            return "DefaultNickname";

        int index = random.Next(0, nicknames.Count);
        return nicknames[index];
    }
}
