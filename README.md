# Unity RPG Text

A simple project that implements an textbox system like the ones from old console RPGs like Final Fantasy or Pokémon.

#Introduction

This asset is meant to make easy to implement an RPG text system. With just a few lines of code you can make text boxes, decision boxes and input boxes. The flow of a conversation is modeled as a graph, with each node representing one type of "box". Each node can point to a next one, none (which ends the conversation) or multiple ones.
It works with Unity's UI system, and comes with a canvas prefab with everything needed already set, but you can move or resize things as you see fit.
The coding is performed inside a class that inherits from "Script". In there, you can override the functions StartNodeSwitch(), EndNodeSwitch() and ButtonActionSwitch() to set the behaviour of a conversation.

# Tutorial

A simple conversation

In this tutorial, you are going to make a conversation for an "Old Man" NPC that will greet the player, ask his/her name and ask if an adress is near or far from where they are (so you can learn to display a simple text and use the input and decision boxes).
First of all, drag the Text Canvas prefab into the scene, and also add an EventSystem. With that, the scene is ready to use the text system.
After that, create a new empty object and add a new script with a class inheriting from "Script". Inside that class, override the methods StartNodeSwitch(), EndNodeSwitch() and ButtonActionSwitch().
```
public class SimpleConversation : Script {
    public override void StartNodeSwitch(int startingNode) { }
    public override void ButtonActionSwitch(int buttonID) { }
    public override void EndNodeSwitch(int finishedNode) { }
}
```

Right now nothing happens except that running this code will hide the panels from the text canvas. 
That's kinda boring, and we don't want boring, so we add an text to the node 0. To do so, add a switch(startingNode) to StartNodeSwitch, and in case 0, add a StartText().
public override void StartNodeSwitch(int startingNode) { 
```
switch (startingNode) {
	case 0:
		StartText("Excuse me... Good morning! Tell me, what's your name?", 1, "Old Man");
        break;
}
```

StartText can take as an argument a string containing the text to be displayed, an int pointing to the next node, a Sprite containing a picture to be displayed with the text and another string containing a name. Every argument, except for the text string, is optional. Not setting an next node will just end the conversation.
After that, let's allow the player to type their name. For that, we create another node with StartInput().
```
case 1:
   StartInput(0, InputField.ContentType.Name, 2);
   break;
```

StartInput takes as arguments an in representing a buttonID, a InputField.ContentType, telling the InputField what kind of content it's going to receive, and another int representing the next node (also optional, and ends the conversation when not set).
We don't want the player to just type in their name for nothing, so we save it to a string playerName. That is done with the string GetInputText() method, which should be called insinde ButtonActionSwitch() or EndNodeSwitch(). In this case, we will use ButtonActionSwitch()
```
string playerName;
public override void ButtonActionSwitch(int buttonID) {
   switch (buttonID) {
      case 0:
         playerName = GetInputText();
         break;
   }
}
```

After that, the old man will ask a question with two choices. For that, we create a node with StartText() to ask the question, and another node with a StartDecision(). After that, we create two other nodes with StartText() for the reaction to each choice.

```
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
```

For each choice avaliable in the DecisionBox, you call AddDecisionButton() before StartDecision(). AddDecisionButton() can take as an argument an int representing a buttonID, if the button should perform an action other than moving to another node (in which case the action should be defined the same way as the InputBox button), a string text, which will be displayed in the button, and a int representing the next node, unless the button should end the conversation.
One last problem with the conversation is that it starts as soon as the scene loads, and that's usually not what you would want. to change that, overload the Start() method, and don't call StartNode(0) in it (but do call the ClearAll() method). Instead, call it inside the Update() method when a button is pressed.
```
public bool buttonDown = false;
public bool isChat = false;

void Start() {ClearAll();}

void Update() {
   buttonDown = (Input.GetButtonDown ("Submit") || Input.GetButtonDown ("Fire1") );
   if (buttonDown && !isChat) {
      StartNode(0);
      isChat = true;                       
   }        
}

public override void EndNodeSwitch(int finishedNode) {
   switch (finishedNode) {
      case 4: isChat = false; break;
      case 5: isChat = false; break;
   }
}
```

You should probably improve this by checking if your character is close enough and is facing the NPC, or the player clicked the NPC, but that's up to you.

