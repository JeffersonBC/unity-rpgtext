using UnityEngine;
using UnityEngine.UI;

public class DecisionBox : MonoBehaviour {

    public static DecisionBox decisionBox;
    public Button ButtonPrefab;
    public float buttonsMinHeight = 75f;

    Script currentScript;

    void Awake () {
        if (decisionBox == null)    decisionBox = this;
        else                        Destroy(gameObject);

        if (ButtonPrefab == null) Debug.LogError("Button Prefab has not been assigned");
        else AddButton("FirstButton", int.MinValue, int.MinValue);
    }
    
    void SetCurrentScript(Script script) {
        currentScript = script;
    }

    //After adding buttons, shows the decision box on screen
    public void StartDecisionBox(Script script) {
        gameObject.SetActive(true);
        SetCurrentScript(script);

#if !UNITY_ANDROID && !UNITY_IOS && !UNITY_WSA
        GetComponentInChildren<Button>().Select();
#endif
    }

    public void Clear() {
        gameObject.SetActive(false);
        foreach (Button button in gameObject.GetComponentsInChildren<Button>())
            Destroy(button.gameObject);
    }

    public void AddButton(string text, int optionButtonID, int nextNode) {
        Button newButton = (Button)Instantiate(ButtonPrefab, Vector3.zero, Quaternion.identity);
        
        newButton.transform.SetParent(gameObject.transform);
        newButton.transform.localScale = Vector3.one;
        newButton.GetComponentInChildren<Text>().text = text;
        
        if (newButton.GetComponentInChildren<LayoutElement>() == null)
            newButton.gameObject.AddComponent<LayoutElement>();
        newButton.GetComponentInChildren<LayoutElement>().minHeight = buttonsMinHeight;
        
        newButton.onClick.AddListener(() => {
            if(optionButtonID != int.MinValue)
                currentScript.ButtonActionSwitch(optionButtonID);

            currentScript.ClearDecision();
            currentScript.ClearInput();

            currentScript.EndNodeSwitch(currentScript.currentNode);
            currentScript.StartNode(nextNode);
        });
    }
}
