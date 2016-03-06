using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Speech.Recognition;

public class PlayerController : MonoBehaviour {
	private Rigidbody rb ;
//	SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
	private int count;
	public float speed;
	public Text PickUpCount ;
	public Text WinText;
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		setCount ();
		WinText.text = "";
		Choices command = new Choices ();
		command.Add (new string[] { "Left", "Right", "Up", "Down" });
		GrammarBuilder grBuild = new GrammarBuilder ();
		grBuild.Append (command);
		Grammar gr = new Grammar (grBuild);

/*		recEngine.LoadGrammarAsync (gr);
		recEngine.SetInputToDefaultAudioDevice ();
		recEngine.RecognizeAsync (RecognizeMode.Multiple);
		recEngine.SpeechRecognized += RecEngine_SpeechRecognized;*/
	}
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce(movement*speed);
//		Vector3 v = RecEngine_SpeechRecognized;
//		rb.AddForce (v * speed);
	}

	void Update()
	{
	//	recEngine.RecognizeAsync (RecognizeMode.Multiple);
	//	recEngine.SpeechRecognized += RecEngine_SpeechRecognized;

	}
/*	Vector3 RecEngine_SpeechRecognized (object sender, SpeechRecognizedEventArgs e)
	{
			Vector3 movement = new Vector3 (0,0,0);
		switch (e.Result.Text) {
		case "Left":
			float f1 = -10;
			Vector3 moveL = new Vector3 (f1, 0.0f, 0.0f);
			return moveL;
			//rb.AddForce (moveL * 10);
			break;
		case "Right":
			float f2 = 10;
			Vector3 moveR = new Vector3 (f2, 0.0f, 0.0f);
			return moveR;
		//	rb.AddForce (moveR * 10);
			break;
		case "Up":
			Vector3 moveU = new Vector3 (0.0f, 0.0f, 10);
			return moveU;
	//		rb.AddForce (moveU * 10);
			//	movement.up;
			break ;
		case "Down":
			Vector3 moveD = new Vector3 (0.0f, 0.0f, -10);
			return moveD;
	//		rb.AddForce (moveD * 10);
			break ;
		}
		return movement;
		//	rb.AddForce (movement * 10);
	}*/

//	void OnCollisionEnter (Collider other)
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
