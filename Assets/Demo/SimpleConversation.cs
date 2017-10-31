using UnityEngine.UI;
using UnityEngine;

public class SimpleConversation : Script {
    string playerName;
    public bool buttonDown = false;
    public bool isChat = false;

    void Start() {
        ClearAll();
    }
    void Update() {
        buttonDown = (Input.GetButtonDown("Submit") || Input.GetButtonDown("Fire1"));
        if (buttonDown && !isChat) {
            StartNode(0);
            isChat = true;
        }
    }
    public override void StartNodeSwitch(int startingNode) {
        switch (startingNode) {
            case 0:
                StartText("Excuse me... Good morning! Tell me, what's your name?", 1, "Old Man");
                break;
            case 1:
                StartInput(0, InputField.ContentType.Name, 2);
                break;
            case 2:
                StartText("So " + playerName + ", is this addres close to where we are?", 3, "Old Man");
                break;
            case 3:
                AddDecisionButton("Yes", 4);
                AddDecisionButton("No...", 5);
                StartDecision();
                break;
            case 4:
                StartText("Oh, good, good... thank you!", "Old Man");
                break;
            case 5:
                StartText("Oh, god... well, thanks anyway", "Old Man");
                break;
        }
    }
    public override void ButtonActionSwitch(int buttonID) {
        switch (buttonID) {
            case 0:
                playerName = GetInputText();
                break;
        }
    }
    public override void EndNodeSwitch(int finishedNode) {
        switch (finishedNode) {
            case 4: isChat = false; break;
            case 5: isChat = false; break;
        }
    }
}
