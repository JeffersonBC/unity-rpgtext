using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour {

    public float    renderSpeed = 15f;
    public int      newLinesPerMessage = 3;
    private string   nextButton = "Fire1";

    public CanvasRenderer textPanel;
    public CanvasRenderer namePanel;
    public Image textPicture;
    public Image clickableIndicator;
       
    public static TextBox textBox;  //Global reference to the text box in the scene
    Text uiText;                    //Reference to the ui text component
    Script currentScript;           //Necessary to end current mode
    
    //Helper variables
    string text;                //The full text currently being rendered
    string currentText;			//The part of the text currently being rendered
	float renderTime = 0;		//Counts how long has passed since the last character was rendered
	int curRendChar  = 0;		//Counts the current character being rendered
	int curTextPos	 = 0;		//Counts the current position at 'text'

	bool rendering      = true;     //Checks whether 'currentText' is being rendered
    bool wasRendering   = false;    //Was rendering on the previous frame
    bool hasEnded	    = true;     //Checks if the current text has already reached the end
    bool clickable      = true;     //Checks whether this text box is currently clickable

    void Awake() {
        if (textBox == null) textBox = this;
        else Destroy(gameObject);

        uiText = textPanel.GetComponentInChildren<Text>();

        if (textPanel == null)
            Debug.LogError("If no textPanel is referenced at the TextBox script, it just won't work");
        if (namePanel == null)
            Debug.LogWarning("If no namePanel is referenced at the TextBox script, you won't be able to show the name box");
        if (textPicture == null)
            Debug.LogWarning("If no textPicture is referenced at the TextBox script, you won't be able to properly show a textPicture." +
                            "If one is present at the scene, it will stay in its standard state");
        if (clickableIndicator == null)
            Debug.LogWarning("If no clickableIndicator is referenced at the TextBox script, the TextBox won't show anything showing the text has finished rendering");

#if UNITY_ANDROID || UNITY_IOS
        nextButton = "Fire1";
#endif
    }
    
	void Update () {
		controlTextBox ();
	}

	//Controls the behaviour of the textBox
	void controlTextBox(){
		if (!hasEnded) {
			//Rendering
			if (rendering) {
				updateText ();
				if (clickable && Input.GetButtonDown (nextButton) && wasRendering)
					skipText ();
			}
			//Paused
			else {
				if (clickable && Input.GetButtonDown (nextButton)) {
                    if (clickableIndicator != null) clickableIndicator.gameObject.SetActive(false);
                    rendering = true;
                    
                    Parse();
				}
			}
		}
        wasRendering = rendering;
	}

	//Rnders the characters one by one
	void updateText(){
        renderTime += Time.deltaTime;
        //If renderSpeed is 0 or less, render the whole text instantly
        if (renderSpeed <= 0 && rendering) {
            skipText();
        }
        //If enough time passed, render next character
		else if (renderTime >= 1f/renderSpeed && curRendChar < currentText.Length){
            uiText.text += currentText[curRendChar];
			curRendChar++;
			renderTime = 0f;
		}
        //If reached the end of the current text, pause rendering
		if (curRendChar >= currentText.Length) {
            if (clickableIndicator != null) clickableIndicator.gameObject.SetActive(true);
            rendering = false;            
            curRendChar = 0;
		}
	}

	//Renders the whole 'currentText'
	void skipText(){
		for (; curRendChar < currentText.Length; curRendChar++)
            uiText.text += currentText[curRendChar];

		curRendChar = 0;
		rendering = false;
        if (clickableIndicator != null) clickableIndicator.gameObject.SetActive(true);
    }

	//Parse the next 'currentText'
	void Parse(){
		if (curTextPos >= text.Length) {
			hasEnded = true;
			rendering = false;

            currentScript.EndNodeSwitch(currentScript.currentNode);
            currentScript.StartNode(currentScript.nextNode);
        }
        else {
            currentText = string.Empty;
            uiText.text = string.Empty;

            for (int newlineCount = 0; 
				newlineCount < newLinesPerMessage && curTextPos < text.Length; 
				curTextPos++) {

				currentText += text [curTextPos];
				if (text [curTextPos] == '\n') {
					newlineCount++;		
				}
			}
		}
	}

    //Loads another text to be rendered by the textbox
    void LoadNewText(string newText) {
        text = newText;
    }

    //Makes the textbox start rendering its current text
    void InitializeText() {
        curTextPos = 0;
        curRendChar = 0;

        Parse();

        rendering = true;
        hasEnded = false;

        if (clickableIndicator != null) clickableIndicator.gameObject.SetActive(false);
    }

    //Initialize currentScript variable so it can be used to end current node
    void SetCurrentScript(Script script) {
        currentScript = script;
    }
    
    //Pauses or unpauses the text box
    public void SetClickable (bool isClickable) {
        clickable = isClickable;
    }

    //Initializes and starts rendering
    public void StartTextBox(Script script, string newText, Sprite picture, string textName) {
        gameObject.SetActive(true);

        if (namePanel != null) {
            if (textName != string.Empty) {
                namePanel.gameObject.SetActive(true);
                namePanel.GetComponentInChildren<Text>().text = textName;
            }
            else namePanel.gameObject.SetActive(false);
        }
        if (textPicture != null) {
            if (picture != null) {
                textPicture.gameObject.SetActive(true);
                textPicture.sprite = picture;
            }
            else textPicture.gameObject.SetActive(false);
        }

        SetCurrentScript(script);
        LoadNewText(newText);
        InitializeText();
    }

    //Hides and restarts the TextBox
    public void Clear() {
        gameObject.SetActive(false);
    }
}

