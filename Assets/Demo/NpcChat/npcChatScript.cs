using UnityEngine;
using UnityEngine.SceneManagement;

//Assets used in this scene were downloaded at
//http://opengameart.org/content/rpg-overworld-images
//http://opengameart.org/content/lpc-fairy
//http://opengameart.org/content/universal-lpc-sprite-male-01

public class npcChatScript : Script {
    
    public bool buttonDown = false;
    public bool isChat = false;

    void Start() {
        ClearAll();
    }

    void Update() {
        buttonDown = (Input.GetButtonDown("Submit") || Input.GetButtonDown("Fire1") );
        
        if (buttonDown && !isChat) {
            StartNode(0);
            isChat = true;                       
        }        
    }

    public override void StartNodeSwitch(int startingNode) {
        switch (startingNode) {
            case 0:
                StartText("Hello friend, I'm the tutorial fairy. A proper\nmanual is included, but I'm here to summarize \nhow the text system works.\n" +
                          "So, what do you want to know?"
                    , 1, "Tutorial Fairy");
                break;

            case 1:
                AddDecisionButton("What do i need to set up the text system?", 10);
                AddDecisionButton("After everything is set, how do I use the text system?", 20);
                AddDecisionButton("See you later",2);
                AddDecisionButton("Send me back to the main menu", 3);
                StartDecision();
                break;

            case 2: //End node
                StartText("See ya!");
                break;
            case 3: //Back to main menu node
                StartText("Ok!");
                break;
            case 4:
                StartText("What else do you need to know?", 1);
                break;

        //How to set up
            case 10:
                StartText("Basically, you will need to set a TextBox Panel, a\nDecisionBox Panel and a InputBox Panel, and then\nwrite a chat script that makes use of them.\n"+
                          "The panels must be the child of a Canvas, and\nhave their respective scripts attached to them.\n\n"+
                          "A prefab with everything already set is included,\nbut if you need to customize it, the documentation\nexplains in more detail how it works.\n"+
                          "Do you need more details on one of these?"
                    , 11);
                break;
            case 11:
                AddDecisionButton("Setting up a TextBox Panel", 12);
                AddDecisionButton("Setting up a DecisionBox Panel", 13);
                AddDecisionButton("Setting up a InputBox Panel", 14);
                AddDecisionButton("I don't need more details", 4);
                StartDecision();
                break;
            case 12:
                StartText("A TextBox Panel need at least a child object with\na Text component to be used to display the text you\nare reading right now, but can be improved\n"+
                    "if a Name, a Picture and a Clickable Indicator are\nalso present. These must be assigned at the\nTextBox script to work.\n"+
                    "Do you need more details on something else?"
                    , 11);
                break;
            case 13:
                StartText("A DecisionBox Panel needs a Layout component\nand a Button prefab with a Layout Element\ncomponent assigned to the DecisionBox script.\n" +
                        "The Button prefab will be instantiated as a child\nof the DecisionBox Panel and the Layout will do\nits magic.\n"+
                        "What each Button does is defined later in a script.\n\n\n"+
                        "Do you need more details on something else?"
                    , 11);
                break;
            case 14:
                StartText("An InputBox Panel needs a child Button and\nan child Input Field. Yep, that's it.\n\n"+
                        "You will need to define what is to be done\nwith the input later in a script.\n\n"+
                        "Do you need more details on something else?"
                    , 11);
                break;
        
        //How to use
            case 20:
                StartText("The text flow works as a graph, with nodes pointing\nto each other. "+
                          "To use the text system, you need to\ncreate a class that inherits the Script class.\n" +
                          "This new class needs to override the\nStartNodeSwitch, EndNodeSwitch and\nButtonActionSwitch functions.\n"+
                          "StartNodeSwitch defines what happens right\nwhen each node is set in motion. There you\ncan use the funcions StartText, StartDecision\n" +
                          "and StartInput to actually use the text system.\n\n\n"+
                          "The EndNodeSwitch defines what happens\nafter each node ends (text ends rendering\nor a button is clicked).\n" +
                          "ButtonActionSwitch defines what happens\nwhen a button is clicked (if something else other\nthan starting another node is needed) and it's\n" +
                          "executed before EndNodeSwitch."
                          , 4);
                break;
            
        }
    }

    public override void EndNodeSwitch(int finishedNode) {
        switch (finishedNode) {
            case 2: isChat = false;                         break;
            case 3: SceneManager.LoadScene("MainMenu");     break;
        }
    }
}
