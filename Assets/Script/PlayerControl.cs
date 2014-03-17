using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public float MovingSpeed = 20f;                      // Tốc độ di chuyển của xe
	public float jumpSpeed = 10.0f;
	public float gravity = 20.0f;
    private Vector3 AccelerometerDirection;             // Trục cảm ứng nghiên
    public float AccelerometerSensitivity = 0.05f;      // Độ nhạy cảm ứng nghiên
	public GUIText CoinLabel;
	public int CoinNum = 0;
	public AudioClip collectCoinSound;
	public AudioClip crashSound;
	public AudioClip gunCollectingSound;
	public AudioClip nitroSound;
	private Vector3 moveDirection = Vector3.zero;
	public Animator devilRiderAnimator;
	public float currentTime = 0;
	public GameObject nitroItem;
	public NitroControl nitroControl;
	public GameObject Sung;
	public GUILayer pauseLayer;
	//public bool canShoot = false;
	public bool flag = false;
	private bool newHighScore = false;
	public GameObject Nitro;
	public Vector3 turnLeft, turnRight;

    void Start () {
		Time.timeScale = 1;
        PlayerPrefs.SetInt("canShoot", 0);
		devilRiderAnimator = GetComponent<Animator> ();
//		devilRiderAnimator.SetBool ("Dead", false);
		nitroItem = GameObject.Find("Nitro");
		nitroControl = nitroItem.GetComponent<NitroControl> ();
		Sung.renderer.enabled = false;
		PlayerPrefs.SetInt ("Score", 0);
		flag = false;
		newHighScore = false;
  }
    
	void Update () {
	}

	void FixedUpdate(){
		detectPlatform ();
		if(MovingSpeed < 35) MovingSpeed += Time.deltaTime / 6;
		movementManagement ();
	}

	// Detect the platform which is running
	void detectPlatform(){
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
						AccelerometerDirection = Input.acceleration;   
		} else {
				if (Input.GetKey (KeyCode.LeftArrow)) {
						AccelerometerDirection.x = AccelerometerSensitivity - 1;
				}
				if (Input.GetKey (KeyCode.RightArrow)) {
						AccelerometerDirection.x = -AccelerometerSensitivity + 1;
				}
				if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.RightArrow)) {
						AccelerometerDirection.x = 0.0f;
				}
		}
	}

	void movementManagement(){
		// Di chuyển xe thẳng hướng phía trước
		//transform.Translate(new Vector3(0, 0, MovingSpeed * Time.deltaTime));
		moveDirection = transform.TransformDirection (new Vector3 (0, 0, MovingSpeed));
		turnLeft = transform.TransformDirection (new Vector3 (5, 0, 0));
		turnRight = transform.TransformDirection (new Vector3 (-5, 0, 0));

		this.GetComponent<CharacterController>().Move (moveDirection * Time.deltaTime);
		
		// Camera cũng phải chạy theo, giữ 1 khoảng cách nhất định với xe
		Camera.main.transform.position = new Vector3(transform.position.x , transform.position.y + 5, transform.position.z - 8);
		Nitro.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
		if (AccelerometerDirection.x > AccelerometerSensitivity)
		{
			// Khi nghiên phone thì cho xe quẹo trái
			transform.Rotate (new Vector3 (0, 7 * Time.deltaTime, -30 * Time.deltaTime), Space.Self);
			this.GetComponent<CharacterController>().Move (turnLeft * Time.deltaTime);
		}
		else if (AccelerometerDirection.x < -AccelerometerSensitivity)
		{
			// Quẹo phải
			transform.Rotate (new Vector3 (0, -7 * Time.deltaTime, 30 * Time.deltaTime), Space.Self);
			this.GetComponent<CharacterController>().Move (turnRight * Time.deltaTime);
		}
		else
		{
			// Mặc định thì xoay xe hướng về phía trước nó sẽ chạy thẳng
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, 0), 7 * Time.deltaTime);
		}
	}
	

	IEnumerator timeToGameOver(){
		while (Time.realtimeSinceStartup - currentTime < 1.8)
			yield return null;
		Time.timeScale = 0;
		while (Time.realtimeSinceStartup - currentTime < 2)
			yield return null;
		for (int i=0; i<10; i++){
			
			if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("Rank" + i.ToString() + "Score")){
				//Debug.Log(PlayerPrefs.GetInt("Rank" + i.ToString() + "Score"));
				//Debug.Log(PlayerPrefs.GetInt("Score"));
				for (int j = 9; j>i; j--){
					PlayerPrefs.SetString("Rank" + j.ToString() + "Name", PlayerPrefs.GetString("Rank" + (j-1).ToString() + "Name"));
					PlayerPrefs.SetInt("Rank" + j.ToString() + "Score", PlayerPrefs.GetInt("Rank" + (j-1).ToString() + "Score"));
				}
				PlayerPrefs.SetInt("Rank", i);
				Application.LoadLevel(5);
				//Debug.Log(i);
				newHighScore = true;
				break;
			}
		}

		if (newHighScore == false) 
			Application.LoadLevel(3);
	}
	
	void OnTriggerEnter (Collider other){
		if (other.name == "Car") {
			other.renderer.enabled = false;

			if(nitroControl.nitroState == true || MovingSpeed > 40f){
				Vector3 np = other.transform.position;
				np.x = 0;
				np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(90, 110));
				other.transform.position = np;
				CoinNum += 10;
				CoinLabel.text = CoinLabel.text.Substring(0, 5) + CoinNum.ToString();
			}else{
				Vector3 np = other.transform.position;
				np.x = 0;
				np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(90, 110));
				other.transform.position = np;
				Time.timeScale = 0.5f;
				devilRiderAnimator.Play("Dead");
				//devilRiderAnimator.SetBool("Dead", true);
				currentTime = Time.realtimeSinceStartup;
				PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + CoinNum);
				audio.Stop();
				audio.PlayOneShot(crashSound);

				StartCoroutine(timeToGameOver());
				//Application.LoadLevel(3);
			}
		}

		if (other.name == "Coin") {
			CoinNum++;
			CoinLabel.text = CoinLabel.text.Substring(0, 5) + CoinNum.ToString();
			audio.PlayOneShot(collectCoinSound, 1);
		}

        if (other.name == "Gun") {

			audio.PlayOneShot(gunCollectingSound);
			PlayerPrefs.SetInt("canShoot", 3);
		 	Sung.renderer.enabled = true;
        }

		if (other.name == "Nitro"){
			audio.PlayOneShot(nitroSound);
		}
    }

	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width - 35, 10, 30, 30), "=")) {
			Time.timeScale = 0;
			flag = true;

		}
		if (flag) {
			GUI.Box(new Rect (Screen.width/2-50, Screen.height/2-30, 100, 60),"");
			if (GUI.Button (new Rect (Screen.width/2+5, Screen.height/2-15, 30, 30), ">")) 
			{
				Time.timeScale = 1;
				flag = false;
			}
			if (GUI.Button (new Rect (Screen.width/2-35, Screen.height/2-15, 30, 30), "<")) 
			{
				Time.timeScale = 1;
				flag = false;
				Application.LoadLevel(1);
			}
		}

	}
}
 