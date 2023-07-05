using System.Collections.Generic;
using System.IO;
using System.Linq;
using SaveSystem.WorldSettings;
using TMPro;
using UnityEngine;

public class SavedWorld : MonoBehaviour
{
    public string Path;

    public string WorldName;
    public string MapName;

    public WorldSettings WorldSettings;

    [SerializeField] private TMP_Text SaveNameText;
    [SerializeField] private TMP_Text MapNameText;

    [SerializeField] private GameObject Save;
    public List<GameObject> Saves;
    private void Start()
    {
        SaveNameText.text = WorldName;
        MapNameText.text = MapName;
    }

    public void LoadSaves()
    {
        MainMenuUIManager.Instance.Saves.SetActive(true);

        string path = $"{Path}/Data";
        foreach (var dir in Directory.GetDirectories(path))
        {
            GameObject g = Instantiate(Save, MainMenuUIManager.Instance.SavedGames.transform, true);
            g.transform.localScale = Vector3.one;

            SavedGame sg = g.GetComponent<SavedGame>();
            string savePath = $"{path}/{dir}";
            sg.Path = dir;
            sg.savedWorld = this;
            sg.SaveName = savePath.Split("\\").Last();
            Saves.Add(g);
        }
    }

    public void ShowWorldSettings()
    {
        
    }

    public void DeleteWorld()
    {
        Directory.Delete(Path, true);
        MainMenuSaveLoadManager.Instance.ReloadSaves();
    }
}
