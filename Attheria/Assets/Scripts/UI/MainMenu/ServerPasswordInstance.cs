using TMPro;
using UnityEngine;

public class ServerPasswordInstance : MonoBehaviour
{
    public TMP_Text Title;
    public TMP_InputField Input;
    
    public event OnPasswordDelegate OnPassword;

    public delegate void OnPasswordDelegate(string password);

    public void Close()
    {
        OnPassword?.Invoke(string.Empty);
        gameObject.SetActive(false);
    }

    public void Enter()
    {
        OnPassword?.Invoke(Input.text);
        gameObject.SetActive(false);
    }
}
