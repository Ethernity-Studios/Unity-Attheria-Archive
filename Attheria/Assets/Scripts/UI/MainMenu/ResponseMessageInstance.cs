using TMPro;
using UnityEngine;

public class ResponseMessageInstance : MonoBehaviour
{
    public TMP_Text Title;
    public TMP_Text Code;
    public TMP_Text Message;

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
