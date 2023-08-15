using System;
using TMPro;
using UI;
using UnityEngine;

public class SaveInstance : MonoBehaviour
{
    [SerializeField] private TMP_Text NameTxt;
    [SerializeField] private TMP_Text VersionTxt;
    [SerializeField] private TMP_Text DateTxt;
    [SerializeField] private TMP_Text PlayTimeTxt;

    public string Path;
    public string Name;
    public string Version;
    public long Date;
    public float PlayTime;
    
    public void Init()
    {
        NameTxt.text = Name;
        VersionTxt.text = Version;
        DateTxt.text = DateTimeOffset.FromUnixTimeMilliseconds(Date).UtcDateTime.ToString();;
        int days = Mathf.FloorToInt(PlayTime / 2880);
        int hours = (int)(PlayTime % 2880)/120;
        int minutes = (int)(PlayTime % 2880)%60;
        PlayTimeTxt.text = $"{days}d :{hours}h :{minutes}m";
    }

    public void SelectSave()
    {
        MenuManager.Instance.SaveNameInp.text = Name;
        MenuManager.Instance.SelectedSavePath = Path;
        MenuManager.Instance.SelectedSaveName = Name;
    }

    public void DeleteSave()
    {
        ConfirmScreenInstance.Instance.OpenDialog(ConfirmScreenDialogs.DeleteSaveTitle, ConfirmScreenDialogs.DeleteSaveDescription, ConfirmScreenDialogs.DeleteSavePositiveBtn, ConfirmScreenDialogs.DeleteSaveNegativeBtn);
        ConfirmScreenInstance.Instance.OnButtonClick += deleteSave;
    }

    void deleteSave(int result)
    {
        if ((Result)result == Result.Confirm)
        {
            MenuManager.Instance.RemoveSave(Path);
        }
        MenuManager.Instance.ConfirmScreen.SetActive(false);
        ConfirmScreenInstance.Instance.OnButtonClick -= deleteSave;
    }
}
