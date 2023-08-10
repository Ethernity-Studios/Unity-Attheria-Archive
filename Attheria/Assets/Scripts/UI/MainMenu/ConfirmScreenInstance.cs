using TMPro;
using UnityEngine;

public class ConfirmScreenInstance : MonoBehaviour
{
    public TMP_Text Title;
    public TMP_Text Description;

    public TMP_Text PositiveBtn;
    public TMP_Text NegativeBtn;

    public static ConfirmScreenInstance Instance;

    public event OnButtonClickDelegate OnButtonClick;
    public delegate void OnButtonClickDelegate(int result);

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    /// <summary>
    /// Open confirmation dialog
    /// </summary>
    /// <param name="title"> Title for confirmation dialog </param>
    /// <param name="description"> Description for confirmation dialog </param>
    /// <param name="positiveBtn"> Text for confirmation button </param>
    /// <param name="negativeBtn"> Text for cancel button </param>
    public void OpenDialog(string title, string description, string positiveBtn, string negativeBtn)
    {
        gameObject.SetActive(true);
        Title.text = title;
        Description.text = description;
        PositiveBtn.text = positiveBtn;
        NegativeBtn.text = negativeBtn;
    }

    /// <summary>
    /// Invoke event based on pressed button
    /// </summary>
    /// <param name="result"> 1 - confirm | 2 - cancel </param>
    public void OnButtonCLick(int result)
    {
        OnButtonClick?.Invoke(result);
    }
}