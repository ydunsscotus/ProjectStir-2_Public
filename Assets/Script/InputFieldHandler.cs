using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputFieldHandler : MonoBehaviour
{
    public TMP_InputField tmpInputField; // Assign your TMP_InputField in the Unity Inspector.
    public TMP_Text displayText; // Assign your TMP_Text UI element in the Unity Inspector.
    public Button btnName;
    void Start()
    {
        // Pastikan displayText kosong saat aplikasi dimulai.
        displayText.text = "";
        btnName.interactable = false;
    }

    public void checkEmpty()
    {
        if (tmpInputField.text == "")
        {
            btnName.interactable = false;
        }
        else
        {
            btnName.interactable = true;
        }
    }

    public void OnButtonClick()
    {
        // Ambil teks dari TMP_InputField dan tampilkan di displayText
        string inputText = tmpInputField.text;
        GameManager.Instance.myName = inputText;
        displayText.text = "Oke! " + inputText+ " gw terima tantangan lu! ";
    }
}
