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
	private Vector3 moveDirection = Vector3.zero;
	public Animator devilRiderAnimator;
	public float currentTime = 0;
	public GameObject nitroItem;
	public NitroControl nitroControl;
	public GameObject Sung;

	//public bool canShoot = false;
    void Start () {
		Time.timeScale = 1;
        PlayerPrefs.SetInt("canShoot", 0);
		devilRiderAnimator = GetComponent<Animator> ();
		devilRiderAnimator.SetBool ("Dead", false);
		nitroItem = GameObject.Find("Nitro");
		nitroControl = nitroItem.GetComponent<NitroControl> ();
		Sung.renderer.enabled = false;
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
		this.GetComponent<CharacterController>().Move (moveDirection * Time.deltaTime);
		
		// Camera cũng phải chạy theo, giữ 1 khoảng cách nhất định với xe
		Camera.main.transform.position = new Vector3(0, transform.position.y + 7, transform.position.z - 10);
		
		if (AccelerometerDirection.x > AccelerometerSensitivity)
		{
			// Khi nghiên phone thì cho xe quẹo trái
			transform.Rotate (new Vector3 (0, 30 * Time.deltaTime, -30 * Time.deltaTime), Space.Self);
		}
		else if (AccelerometerDirection.x < -AccelerometerSensitivity)
		{
			// Quẹo phải
			transform.Rotate (new Vector3 (0, -30 * Time.deltaTime, 30 * Time.deltaTime), Space.Self);
		}
		else
		{
			// Mặc định thì xoay xe hướng về phía trước nó sẽ chạy thẳng
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, 0), 7 * Time.deltaTime);
		}
	}
	

	IEnumerator timeToGameOver(){
		while (Time.realtimeSinceStartup - currentTime < 0.4)
			yield return null;
		Time.timeScale = 0;
		while (Time.realtimeSinceStartup - currentTime < 4)
			yield return null;
		Application.LoadLevel(2);
	}

    void OnTriggerEnter (Collider other){
		if (other.name == "Car") {
			if(nitroControl.nitroState == true || MovingSpeed > 40f){
				Vector3 np = other.transform.position;
				np.x = 0;
				np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(90, 110));
				other.transform.position = np;
				CoinNum += 10;
				CoinLabel.text = CoinLabel.text.Substring(0, 5) + CoinNum.ToString();
			}else{
				other.renderer.enabled = false;
				devilRiderAnimator.SetBool("Dead", true);
				currentTime = Time.realtimeSinceStartup;
				PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + CoinNum);
				
				StartCoroutine(timeToGameOver());
			}
		}

		if (other.name == "Coin") {
			CoinNum++;
			CoinLabel.text = CoinLabel.text.Substring(0, 5) + CoinNum.ToString();
			audio.PlayOneShot(collectCoinSound, 1);
		}

        if (other.name == "Gun") {
			PlayerPrefs.SetInt("canShoot", 3);
		 	Sung.renderer.enabled = true;
        }
    }
	
}
 