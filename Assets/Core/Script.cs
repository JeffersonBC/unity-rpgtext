using UnityEngine;
using UnityEngine.UI;

public abstract class Script : MonoBehaviour {
    public int currentNode {get; set;}
    public int nextNode { get; set; }

    //Standard Start function
    void Start() {
        ClearAll();        
        StartNode(0);
    }

    //Text Functions
    public void StartText(string text) {
        try {
            SetTextClickable(true);
            TextBox.textBox.StartTextBox(this, text, null, string.Empty);
            nextNode = int.MinValue;
        }
        catch { Debug.LogError("No text box found. Is there a properly set text box on scene?"); }
    }

    public void StartText(string text, Sprite picture) {
        try {
            SetTextClickable(true);
            TextBox.textBox.StartTextBox(this, text, picture, string.Empty);
            nextNode = int.MinValue;
        }
        catch { Debug.LogError("No text box found. Is there a properly set text box on scene?"); }
    }

    public void StartText(string text, string textName) {
        try {
            SetTextClickable(true);
            TextBox.textBox.StartTextBox(this, text, null, textName);
            nextNode = int.MinValue;
        }
        catch { Debug.LogError("No text box found. Is there a properly set text box on scene?"); }
    }

    public void StartText(string text, Sprite picture, string textName) {
        try {
            SetTextClickable(true);
            TextBox.textBox.StartTextBox(this, text, picture, textName);
            nextNode = int.MinValue;
        }
        catch { Debug.LogError("No text box found. Is there a properly set text box on scene?"); }
    }


    public void StartText(string text, int standardNextNode) {
        try {
            SetTextClickable(true);
            TextBox.textBox.StartTextBox(this, text, null, string.Empty);
            nextNode = standardNextNode;
        }
        catch { Debug.LogError("No text box found. Is there a properly set text box on scene?"); }
    }

    public void StartText(string text, int standardNextNode, Sprite picture) {
        try {
            SetTextClickable(true);
            TextBox.textBox.StartTextBox(this, text, picture, string.Empty);
            nextNode = standardNextNode;
        }
        catch { Debug.LogError("No text box found. Is there a properly set text box on scene?"); }
    }

    public void StartText(string text, int standardNextNode, string textName) {
        try {
            SetTextClickable(true);
            TextBox.textBox.StartTextBox(this, text, null, textName);
            nextNode = standardNextNode;
        }
        catch { Debug.LogError("No text box found. Is there a properly set text box on scene?"); }
    }

    public void StartText(string text, int standardNextNode, Sprite picture, string textName) {
        try {
            SetTextClickable(true);
            TextBox.textBox.StartTextBox(this, text, picture, textName);
            nextNode = standardNextNode;
        }
        catch { Debug.LogError("No text box found. Is there a properly set text box on scene?"); }
    }


    public void ClearText() {
        try { TextBox.textBox.Clear(); }
        catch { Debug.LogWarning("No text box found, so you won't get any result from StartText(). Is there a properly set text box on scene?"); }
    }

    void SetTextClickable(bool isClickable) {
        try { TextBox.textBox.SetClickable(isClickable); }
        catch { Debug.LogError("No text box found to " + (isClickable ? "resume" : "pause") + ". Is there a properly set text box on scene?"); }
    }
    

    //Decision Functions
    public void StartDecision() {
        try {
            SetTextClickable(false);
            DecisionBox.decisionBox.StartDecisionBox(this);
        }
        catch { Debug.LogError("No decision box found. Is there a properly set decision box on scene?"); }
    }

    public void ClearDecision() {
        try { DecisionBox.decisionBox.Clear(); }
        catch { Debug.LogWarning("No decision box found, so you won't get any result from AddDecisionButton() or StartDecision()."+
                                    "Is there a properly set decision box on scene?"); }
    }

    public void AddDecisionButton(int optionButtonID, string text, int buttonNextNode) {
        try { DecisionBox.decisionBox.AddButton(text, optionButtonID, buttonNextNode); }
        catch { Debug.LogError("No decision box found. Is there a properly set decision box on scene?"); }    
    }

    public void AddDecisionButton(int optionButtonID, string text) {
        try { DecisionBox.decisionBox.AddButton(text, optionButtonID, int.MinValue); }
        catch { Debug.LogError("No decision box found. Is there a properly set decision box on scene?"); }
    }

    public void AddDecisionButton(string text, int buttonNextNode) {
        try { DecisionBox.decisionBox.AddButton(text, int.MinValue, buttonNextNode); }
        catch { Debug.LogError("No decision box found. Is there a properly set decision box on scene?"); }
    }

    public void AddDecisionButton(string text) {
        try { DecisionBox.decisionBox.AddButton(text, int.MinValue, int.MinValue); }
        catch { Debug.LogError("No decision box found. Is there a properly set decision box on scene?"); }
    }

    //Input Field Functions
    public void StartInput(int sendButtonId, InputField.ContentType type, int standardNextNode) {
        nextNode = standardNextNode;

        try {
            SetTextClickable(false);
            InputBox.inputBox.StartInputBox();
        }
        catch { Debug.LogError("No input field box found. Is there a properly set input box on scene?"); }

        try {
            InputBox.inputBox.SetTextType(type);
            InputBox.inputBox.ClearText();
        }
        catch { Debug.LogError("No child input field found on input box."); }
        
        try {
            InputBox.sendButton.onClick.RemoveAllListeners();

            InputBox.sendButton.onClick.AddListener(() => {
                ButtonActionSwitch(sendButtonId);

                ClearInput();
                ClearText();

                EndNodeSwitch(currentNode);
                StartNode(nextNode);
            });
        }
        catch { Debug.LogError("Could not set send button function. Is there a button child to input box to be used as the send button?"); }
    }

    public void ClearInput() {
        try { InputBox.inputBox.Clear(); }
        catch { Debug.LogWarning("No input field box found, so you won't get any result from StartInput(). Is there a properly set input box on scene?"); }
    }
    
    public string GetInputText() {
        try { return InputBox.inputBox.GetText(); }
        catch {
            Debug.LogError("No input field box found. Is there a properly set input box on scene?");
            return string.Empty;
        }
    }


    //Script Functions
    public void ClearAll() {
        ClearText();
        ClearDecision();
        ClearInput();
    }

    public void StartNode(int startingNode) {
        if (startingNode != int.MinValue) {
            currentNode = startingNode;
            StartNodeSwitch(startingNode);
        }
        else
            ClearAll();
    }

    public virtual void StartNodeSwitch(int startingNode) { }

    public virtual void EndNodeSwitch(int finishedNode) { }

    public virtual void ButtonActionSwitch(int buttonID) { }
}
