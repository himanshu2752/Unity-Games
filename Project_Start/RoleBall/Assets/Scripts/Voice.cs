using System ;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Speech.Recognition;

public class Voice : MonoBehaviour {


	SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
	private Rigidbody rb ;
	private int count;
	public Text PickUpCount ;
	public Text WinText;
	// Use this for initialization
	void Start () {
		count = 0;
		setCount ();
		WinText.text = "";
		rb = GetComponent<Rigidbody> ();
		Choices command = new Choices ();
		command.Add (new string[] { "Left", "Right", "Up", "Down" });
		GrammarBuilder grBuild = new GrammarBuilder ();
		grBuild.Append (command);
		Grammar gr = new Grammar (grBuild);

		recEngine.LoadGrammarAsync (gr);
		recEngine.SetInputToDefaultAudioDevice ();


	}

	void RecEngine_SpeechRecognized (object sender, SpeechRecognizedEventArgs e)
	{
	//	Vector3 movement = new Vector3 ();
		switch (e.Result.Text) {
		case "Left":
			float f1 = -10;
			Vector3 moveL = new Vector3 (f1, 0.0f, 0.0f);
			rb.AddForce (moveL * 10);
			break;
		case "Right":
			float f2 = 10;
			Vector3 moveR = new Vector3 (f2, 0.0f, 0.0f);
			rb.AddForce (moveR * 10);
			break;
		case "Up" :
			Vector3 moveU = new Vector3 (0.0f, 0.0f, 10);
			rb.AddForce (moveU * 10);
		//	movement.up;
			break ;
		case "Down" :
			Vector3 moveD = new Vector3 (0.0f, 0.0f, -10);
			rb.AddForce (moveD * 10);
			break ;
		}
	//	rb.AddForce (movement * 10);
	}

	void FixedUpdate(){
		recEngine.RecognizeAsync (RecognizeMode.Multiple);
		recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag("PickUps"))
		{
			other.gameObject.SetActive(false);
			count++;
			setCount ();
		}
	}

	void setCount()
	{
		PickUpCount.text = "Count: " + count.ToString();
		if (count >= 21) {
			WinText.text = "You Win !!";
			OnApplicationQuit ();
		}
	}
	void OnApplicationQuit(){

	}
}
