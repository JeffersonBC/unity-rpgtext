using UnityEngine.SceneManagement;

public class mainMenuScript : Script {

	public override void StartNodeSwitch(int startingNode) {
        switch (startingNode) {
            case 0:
                StartText("Select a scene", 1);
                break;
            case 1:
                AddDecisionButton(0, "Character Creation Demo");
                AddDecisionButton(1, "NPC Chat Demo (Tutorial)");
                StartDecision();
                break;
        }
    }

    public override void ButtonActionSwitch(int buttonID) {
        switch (buttonID) {
            case 0:
                SceneManager.LoadScene("CharacterCreation");
                break;
            case 1:
                SceneManager.LoadScene("NpcChat");
                break;
        }
    }
}
