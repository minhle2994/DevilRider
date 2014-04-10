using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public float MovingSpeed = 20f;                      // Tốc độ di chuyển của xe
	public float jumpSpeed = 10.0f;
	public float gravity = 20.0f;
    private Vector3 AccelerometerDirection;             // Trục cảm ứng nghiên
    public float AccelerometerSensitivity = 0.1f;      // Độ nhạy cảm ứng nghiên

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
	public bool nitroState;
	public GameObject nitroTorch;
	public Vector3 turnLeft, turnRight;
	private float angle;


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
		turnRight = transform.TransformDirection (new Vector3 (10, 0, 0));
		turnLeft = transform.TransformDirection (new Vector3 (-10, 0, 0));
		nitroState = false;

  }
    
	void Update () {
		detectPlatform ();
		if(MovingSpeed < 35) MovingSpeed += Time.deltaTime / 6;
		movementManagement ();
	}

	void FixedUpdate(){

	}

	// Detect the platform which is running
	void detectPlatform(){
		AccelerometerDirection = Input.acceleration;   
		AccelerometerDirection.x = Mathf.Min (AccelerometerDirection.x, 0.7f);
	}


	
	void movementManagement(){

		moveDirection = transform.TransformDirection (new Vector3 (0, 0, MovingSpeed));


		this.GetComponent<CharacterController>().Move (moveDirection * Time.deltaTime);
		// Camera cũng phải chạy theo, giữ 1 khoảng cách nhất định với xe
		Camera.main.transform.position = new Vector3(transform.position.x , transform.position.y + 5, transform.position.z - 8);
		nitroTorch.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

		if (nitroState == true)
						nitroTorch.renderer.enabled = true;
		if(nitroState == false)
						nitroTorch.renderer.enabled = false;
		// turn right 
		if ( AccelerometerDirection.x > AccelerometerSensitivity){
			if (transform.eulerAngles.z > 330 || Mathf.Abs(transform.eulerAngles.z) < 1
			    || (transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= 32)){

				if (transform.eulerAngles.z > 320 || Mathf.Abs(transform.eulerAngles.z) < 1)
					angle = Mathf.Min(transform.eulerAngles.z, 360 - transform.eulerAngles.z);
				else 
					angle = 0;
				
				if (angle != 0)
					transform.Rotate (new Vector3 (0, 0 * Time.deltaTime, -50 * Time.deltaTime * AccelerometerDirection.x * 2.0f), Space.Self);
				else 
					transform.Rotate (new Vector3 (0, 0 * Time.deltaTime, -100 * Time.deltaTime * AccelerometerDirection.x * 2.0f), Space.Self);
			}
			this.GetComponent<CharacterController>().Move (turnRight * Time.deltaTime * AccelerometerDirection.x * 2.0f);
		}

		// turn left
		else if (AccelerometerDirection.x < -AccelerometerSensitivity)
		{
			if (transform.eulerAngles.z < 30 || Mathf.Abs(transform.eulerAngles.z) < 1
			    || (transform.eulerAngles.z <= 360 && transform.eulerAngles.z >= 328)){

				if (transform.eulerAngles.z < 30 || Mathf.Abs(transform.eulerAngles.z) < 1)
					angle = transform.eulerAngles.z;
				else 
					angle = 0;

				if (angle != 0)
					transform.Rotate (new Vector3 (0, -0 * Time.deltaTime, 50 * Time.deltaTime * -AccelerometerDirection.x * 2.0f), Space.Self);
				else 
					transform.Rotate (new Vector3 (0, -0 * Time.deltaTime, 100 * Time.deltaTime -AccelerometerDirection.x * 2.0f), Space.Self);
			}
			this.GetComponent<CharacterController>().Move (turnLeft * Time.deltaTime * -AccelerometerDirection.x * 2.0f);

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
		if (GUI.Button (new Rect (Screen.width - 55, 10, 50, 50), "=")) {
			Time.timeScale = 0;
			flag = true;

		}
		if (flag) {
			GUI.Box(new Rect (Screen.width/2-100, Screen.height/2-50, 200, 100),"");
			if (GUI.Button (new Rect (Screen.width/2+30, Screen.height/2-25, 50, 50), ">")) 
			{
				Time.timeScale = 1;
				flag = false;
			}
			if (GUI.Button (new Rect (Screen.width/2-80, Screen.height/2-25, 50, 50), "<")) 
			{
				Time.timeScale = 1;
				flag = false;
				Application.LoadLevel(1);
			}
		}

	}
}
 