using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public float MovingSpeed = 20f;                      // Tốc độ di chuyển của xe
	public float jumpSpeed = 10.0f;
	public float gravity = 20.0f;
    private Vector3 AccelerometerDirection;             // Trục cảm ứng nghiên
    public float AccelerometerSensitivity = 0.1f;      // Độ nhạy cảm ứng nghiên


	public AudioClip collectCoinSound;
	public AudioClip crashSound;
	public AudioClip gunCollectingSound;
	public AudioClip normalSound;
	public AudioClip nitroSound;
	public AudioClip getNitroSound;

	private Vector3 moveDirection = Vector3.zero;
	public Animator devilRiderAnimator;
	public float currentTime = 0;
	public GameObject nitroItem;
	public NitroControl nitroControl;
	public bool nitroState;
	public GameObject nitroTorch;
	public GameObject Sung;
	public GUILayer pauseLayer;
	//public bool canShoot = false;
	public bool flag = false;
	private bool newHighScore = false;
	private Vector3 turnLeft, turnRight;
	private float angle;
	public float baseSpeed = 20.0f;
	public GameObject[] car;
	private float[,] carPosX= new float[3,5]{{-1.5f ,-1.5f ,2.5f ,1 ,4},
											 {5 ,-1.5f ,2.4f ,3.5f , -2},
											 {2.4f ,-2,4 ,5 , -1.4f}};
	private float[,] carPosZ= new float[3,5]{{10 ,25 ,40 ,55 ,70},
										 	 {10 ,30 ,50 ,60 ,70},
											 {20 ,35, 45, 55 ,75}};
	private int carManage = 0;
	private float firstRoad = -10;
	public bool stopCameraStatus;
	public Vector3 stopCameraPosition;

	public bool callOneTime1 = true;
	public bool callOneTime2 = true;
	public bool dead = false;


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
		turnRight = transform.TransformDirection (new Vector3 (12, 0, 0));
		turnLeft = transform.TransformDirection (new Vector3 (-12, 0, 0));
		nitroState = false;
		stopCameraStatus = false;
		audio.clip = normalSound;
  }
	void Update () {
		detectPlatform ();
		if (baseSpeed < 40) {
			baseSpeed += Time.deltaTime/10;
			if (MovingSpeed < baseSpeed) 
				MovingSpeed = baseSpeed;
		}
		movementManagement ();
		if (this.transform.position.z > car[4+carManage].transform.position.z+10) 
		{
			firstRoad = car[4+carManage].transform.position.z+95;

			int x = Random.Range(0,2);
			for (int i=0; i<5; i++)
				car[i+carManage].transform.position = new Vector3 (carPosX [x, i]-1,1,carPosZ[x,i]+firstRoad);
			carManage  = (carManage+5)%10;
		}
		if ((nitroState == true) && (callOneTime1 == true)) {
			audio.clip = nitroSound;
			audio.Play ();
			callOneTime1 = false;
			callOneTime2 = true;
		}
		if ((nitroState == false) && (callOneTime2 == true)) {
			if(dead == false){
				callOneTime1 = true;
				audio.clip = normalSound;
				audio.Play ();
				callOneTime2 = false;
			}
		}
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
		if (stopCameraStatus == false)
						Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y + 1.7f, transform.position.z - 5.2f);
				else 
						Camera.main.transform.position = stopCameraPosition;
		nitroTorch.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

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

			//Debug.Log("right " + transform.eulerAngles.z);
			this.GetComponent<CharacterController>().Move (turnRight * Time.deltaTime * AccelerometerDirection.x * 1.0f);
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
			if(transform.eulerAngles.z >= 0 && transform.eulerAngles.z < 40)
			//Debug.Log("left " + transform.eulerAngles.z);
			this.GetComponent<CharacterController>().Move (turnLeft * Time.deltaTime * -AccelerometerDirection.x * 1.0f);

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
				for (int j = 9; j>i; j--){
					PlayerPrefs.SetString("Rank" + j.ToString() + "Name", PlayerPrefs.GetString("Rank" + (j-1).ToString() + "Name"));
					PlayerPrefs.SetInt("Rank" + j.ToString() + "Score", PlayerPrefs.GetInt("Rank" + (j-1).ToString() + "Score"));
				}
				PlayerPrefs.SetInt("Rank", i);
				Application.LoadLevel(5);

				newHighScore = true;
				break;
			}
		}

		if (newHighScore == false) 
			Application.LoadLevel(3);
	}
	
	void OnTriggerEnter (Collider other){

		if (other.name == "Car" || other.name == "Wall" || other.name == "GhostRider") {
			//other.renderer.enabled = false;
			MovingSpeed = 10;
			baseSpeed = 10;
			PlayerPrefs.SetInt("canShoot", 0);

			//			Vector3 np = other.transform.position;
//			np.x = 0;
//			np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(90, 110));
//			other.transform.position = np;
			Time.timeScale = 0.5f;
			AccelerometerSensitivity = 69;
			dead = true;
			stopCameraPosition = Camera.main.transform.position;
			stopCameraStatus = true;
			audio.Stop();
			devilRiderAnimator.Play("Dead");

			currentTime = Time.realtimeSinceStartup;
		
			audio.PlayOneShot(crashSound);

			StartCoroutine(timeToGameOver());
			//Application.LoadLevel(3);
			}
	
	
        if (other.name == "Gun") {

			audio.PlayOneShot(gunCollectingSound);
			PlayerPrefs.SetInt("canShoot", 3);
		 	Sung.renderer.enabled = true;
        }

		if (other.name == "Nitro") {
			audio.PlayOneShot(getNitroSound);		
		}
	}

}
 