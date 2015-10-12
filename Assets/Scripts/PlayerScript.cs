using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float playerVelocity;
	private Vector3 playerPosition;
	public float boundary;

	private int playerLives;
	private int playerPoints;

	//Audio
	public AudioClip pointSound;
	public AudioClip lifeSound;
	private AudioSource audio;

	// Use this for initialization
	void Start () {
		// get the initial position of the game object
		playerPosition = gameObject.transform.position;

		playerLives = 3;
		playerPoints = 0;

		//Audio
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		// horizontal movement
		playerPosition.x += Input.GetAxis("Horizontal") * playerVelocity;

		// leave the game
		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}

		// update the game object transform
		transform.position = playerPosition;

		// boundaries
		if (playerPosition.x < -boundary) {
			transform.position = new Vector3 (-boundary, playerPosition.y, playerPosition.z);
			playerPosition = transform.position;
		} 
		if (playerPosition.x > boundary) {
			transform.position = new Vector3(boundary, playerPosition.y, playerPosition.z);
			playerPosition = transform.position;
		}

		WinLose();

	}

	void addPoints(int points){
		playerPoints += points;
		audio.PlayOneShot (pointSound);
	}

	void OnGUI(){
		GUI.Label (new Rect(5.0f,3.0f,200.0f,200.0f),"Live's: " + playerLives + "  Score: " + playerPoints);
	}

	void TakeLife(){
		playerLives--;
		audio.PlayOneShot (lifeSound);
	}

	void WinLose(){
		// restart the game
		if (playerLives == 0) {
			Application.LoadLevel("Level1");        
		}
		
		// blocks destroyed
		if ((GameObject.FindGameObjectsWithTag ("Block")).Length == 0) {
			// check the current level
			if (Application.loadedLevelName == "Level1") {
				Application.LoadLevel("Level2");
			} else {
				Application.Quit();
			}
		}
	}
}
