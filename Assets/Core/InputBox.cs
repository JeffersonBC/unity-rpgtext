using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputBox : MonoBehaviour {
    public static InputBox inputBox;
    public static Button sendButton;

    public void Awake() {
        if (inputBox == null)   inputBox = this;
        else                    Destroy(gameObject);

        try { sendButton = GetComponentInChildren<Button>(); }
        catch { Debug.LogError("There's no Button to act as the send button on the input box panel"); }
    }
    
    public string GetText() {
        try { return GetComponentInChildren<InputField>().text; }
        catch {
            Debug.LogError("No child input field found on input box.");
            return null;
        }
    }

    public void SetTextType(InputField.ContentType type) {
       GetComponentInChildren<InputField>().contentType = type;
    }

    public void ClearText() {
        GetComponentInChildren<InputField>().text = string.Empty;
    }

    public void StartInputBox() {
        gameObject.SetActive(true);

#if !UNITY_ANDROID && !UNITY_IOS && !UNITY_WSA
        EventSystem.current.SetSelectedGameObject(GetComponentInChildren<InputField>().gameObject);
        GetComponentInChildren<InputField>().ActivateInputField();
#endif
    }

    public void Clear() {
        gameObject.SetActive(false);
    }
}
