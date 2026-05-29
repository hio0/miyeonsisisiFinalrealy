using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Data
{
    public List<int> episodecount = new List<int>();
    public List<float> hogamdo = new List<float>();

    public float money;
    public int alldivisioncount;
}

public class SaveManager : MonoBehaviour
{
    public Data data = new Data();

    private void Awake()
    {
        LoadData();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SaveData()
    {
        Debug.Log(data.episodecount == null);
        for (int i = 0;i<MainManager.main.alldivisioncount;i++)
        {
            data.episodecount.Add(MainManager.main.divisons[i].gameObject.GetComponent<DivisionControl>().my.episodeCount);
            data.hogamdo.Add(MainManager.main.divisons[i].gameObject.GetComponent<DivisionControl>().my.hogamdo);
        }
        
        data.money = MoneyManager.Money.money;
        data.alldivisioncount = MainManager.main.alldivisioncount;

        string json = JsonUtility.ToJson(data);

        string filepath = Path.Combine(Application.persistentDataPath, "savedata.json");
        File.WriteAllText(filepath, json);
    }
    
    public void LoadData()
    {
        string[] files = Directory.GetFiles(Application.persistentDataPath);

        if(files.Length != 0)
        {
            foreach (string file in files)
            {
                string json = File.ReadAllText(file);
                data = JsonUtility.FromJson<Data>(json);
            }

            for (int i = 0; i < data.episodecount.Count; i++)
            {
                MainManager.main.divisons[i].gameObject.GetComponent<DivisionControl>().my.episodeCount = data.episodecount[i];
            }
            for (int i = 0; i < data.hogamdo.Count; i++)
            {
                MainManager.main.divisons[i].gameObject.GetComponent<DivisionControl>().my.hogamdo = data.hogamdo[i];
            }

            MoneyManager.Money.money = data.money;
            MainManager.main.alldivisioncount = data.alldivisioncount;
        }
    }

}
