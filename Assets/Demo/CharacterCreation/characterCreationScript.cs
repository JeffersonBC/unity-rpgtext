using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class characterCreationScript : Script {

    string  pGender;
    string  pName;
    string  pWeapon;
    int     pAge = 18;

    public SpriteRenderer dragon;   //Downloaded at http://opengameart.org/content/biomech-dragon-splice
    int state = 0;                  //Defines which animation is being played
    float startTime;

    //Controls the dragon god animations
    void Update() {
        switch (state) {
            //Shows up
            case 0:
                dragon.color += Color.white * Time.deltaTime * 0.5f;
                if (dragon.color.r >= 1) {
                    state = 2;
                    StartNode(0);
                }
                break;
            //Fades and returns to the main menu
            case 1:
                dragon.color -= Color.white * Time.deltaTime * 0.5f;
                if (dragon.color.r <= 0) {
                    SceneManager.LoadScene("MainMenu");
                }
                break;
            //Blank state
            case 2:
                dragon.transform.position = new Vector3(0, dragon.transform.position.y, 0);
                break;
            //Moving
            case 3:
                dragon.transform.position = new Vector3(Mathf.Sin(Time.time-startTime) * 2, dragon.transform.position.y, 0);
                break;
        }
    }

    //Overrides the default Scipt Start function so the script won't start the first node right away
    void Start() {
        ClearText();
        ClearDecision();
        ClearInput();
    }


    //Override this function to define the behaviour of each node
    public override void StartNodeSwitch(int startingNode) {
        switch (startingNode) {
            //Gender
            case 0:
                StartText(
                    "Greetings human, it is I, the Dragon God.\nI am in need of your assistance.\n\n" +
                    "The dark wizard has been ressurected\nonce again, and I need someone to end\nhis evil deeds.\n" +
                    "And this time, thou art the one choosen\nfor the quest and..."
                    , 1
                    , dragon.sprite
                    ,"Dragon God");                    
                break;
            case 1:
                state = 3; //Starts dragon animation
                startTime = Time.time;
                StartText(
                    "I'm sorry to ask, but you look far too fragile for a\n" +
                    "man and far too ugly for a woman, so... which\n" +
                    "one are you exactly?"
                    , 2
                    ,"Confused Dragon God");
                break;
            case 2:
                AddDecisionButton(10, "Man",        10);
                AddDecisionButton(11, "Woman",      10);
                AddDecisionButton(12, "Neither...", 11);
                StartDecision();
                break;
            
            //Name
            case 10:                
                StartText("Really? Wouldn't have guessed... Well, anyway,\nwhat ist thy name, so i can adress you properly?\n"
                    , 12
                    ,"Surprised Dragon God");                
                break;
            case 11:
                StartText(
                    "Huh, makes sense... Anyway, what ist thy\n" +
                    "name, so i can adress you properly?"
                    , 12
                    , "Dragon God");
                break;
            case 12:    StartInput(0, InputField.ContentType.Name, 14);
                        break;
            case 13:    StartText("Human? Are you listening? I'm asking your name", 12, "Angry Dragon God");
                        break;
            case 14:    StartText(pName + "? Well, it could be worse...", 20, "Disapointed Dragon God");
                        break;

            //Age
            case 20:    StartText("What about your age? How old are you?", 21);
                        break;
            case 21:    StartInput(31, InputField.ContentType.IntegerNumber, 30);
                        break;
            case 22:
                StartText("Human... your AGE??? As in how many years\nhave passed since you were born?"
                            , 21
                            ,"Angry Dragon God");
                break;

            //Weapon
            case 30:
                if (pAge < 15) StartText("Oh great, a baby hero... What about your\nweapon of choice?", 31, "Disapointed Dragon God");
                else if (pAge > 65) StartText("Dear me, is there a weapon you are still young\nenough to use?", 31, "Worried Dragon God");
                else StartText("And what's your weapon of choice?", 31, "Dragon God");
                break;
            case 31:
                AddDecisionButton(1, "Sword",          32);
                AddDecisionButton(2, "My Fists",       32);
                AddDecisionButton(3, "Dark Magic",     32);
                AddDecisionButton(4, "I'm a pacifist", 32);
                StartDecision();
                break;
            case 32:    
                //It's not impossible to use the same node for different messages, if it helps you organize your conversation
                if (pWeapon == "Sword" || pWeapon == "Fists")   StartText("That should do.", 40, "Dragon God");
                else if (pWeapon == "Dark Magic")               StartText("Dark Magic vs Dark Magic? That should be\ninteresting.", 40, "Happy Dragon God");
                else if (pWeapon == "None")                     StartText("Oh fu... I mean, you are the hero, you know\nwhat you are doing...", 40, "Extremely Worried Dragon God");
                break;

            //Informations
            case 40:
                StartText(  "Are these your informations?\n" + 
                            "Name: "    + pName     + "     Age: "    + pAge + '\n' +
                            "Gender: "  + pGender   + "     Weapon: " + pWeapon
                            , 41);
                break;
            case 41:
                //These buttons perform no action, just redirect to another node, 
                //so there's no need to assign them a button ID, only a next node.
                AddDecisionButton("Yes",            42);
                AddDecisionButton("Wrong Name",     43);
                AddDecisionButton("Wrong Age",      44);
                AddDecisionButton("Wrong Gender",   46);
                AddDecisionButton("Wrong Weapon",   30);
                StartDecision();
                break;
            case 42:    StartText("Very well. Then let your journey begin...");
                        break;
            case 43:    StartInput(21, InputField.ContentType.Name, 40);
                        break;
            case 44:    StartInput(32, InputField.ContentType.IntegerNumber, 40);
                        break;
            case 45:    StartText("Human... a number between 0 and 120, please...", 44, "Tired Dragon God");
                        break;
            case 46:
                AddDecisionButton(10, "Man",        40);
                AddDecisionButton(11, "Woman",      40);
                AddDecisionButton(12, "Neither...", 40);
                StartDecision();
                break;
                
        }
    }

    //Override this function to define the behaviour of the buttons
    public override void ButtonActionSwitch(int buttonID) {
        switch (buttonID) {
            case 0:     //Name
                pName = GetInputText();
                if (pName == string.Empty) nextNode = 13;
                break;
            
            //Weapon
            case 1: pWeapon = "Sword";       break;
            case 2: pWeapon = "Fists";       break;
            case 3: pWeapon = "Dark Magic";  break;
            case 4: pWeapon = "None";        break;

            //Gender
            case 10: pGender = "Male";       break;
            case 11: pGender = "Female";     break;
            case 12: pGender = "Neither";    break;

            case 21:    //Confirm Name
                if (GetInputText() != string.Empty)
                    pName = GetInputText();
                break;

            case 31:    //Age
                if ( !(int.TryParse(GetInputText(), out pAge) && pAge > 0 && pAge <= 120) ) nextNode = 22;
                break;
            case 32:    //Confirm Age
                int temp;
                if (int.TryParse(GetInputText(), out temp) && temp > 0 && temp <= 120) pAge = temp;
                else nextNode = 45;
                break;
        }
    }

    //Override this function to define what happens after each node
    public override void EndNodeSwitch(int finishedNode) {
        switch (finishedNode) {
            case 2:     //Stops the dragon animation
                state = 2;
                break;
            case 42:    //Returns to the main menu
                state = 1;
                break;
        }
    }
}
