using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public float MovingSpeed = 15.0f;                      // Tốc độ di chuyển của xe
	public float jumpSpeed = 10.0f;
	public float gravity = 20.0f;
    private Vector3 AccelerometerDirection;             // Trục cảm ứng nghiên
    public float AccelerometerSensitivity = 0.05f;      // Độ nhạy cảm ứng nghiên
	public GUIText CoinLabel;
	public int CoinNum = 0;
	public AudioClip collectCoinSound;
	private Vector3 moveDirection = Vector3.zero;

	//public bool canShoot = false;
    void Start () {
        PlayerPrefs.SetInt("canShoot", 0);
		//this.GetComponent<Rigidbody> ().AddForce (Vector3.down*10);
        //transform.GetChild(0).renderer.enabled = false;
    }
    
    void Update () {
		MovingSpeed += Time.deltaTime / 5;
        // Detect xem đang chạy trên mobile hay trên các thiết bị khác
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            AccelerometerDirection = Input.acceleration;   
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                AccelerometerDirection.x = AccelerometerSensitivity - 1;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                AccelerometerDirection.x = -AccelerometerSensitivity + 1;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                AccelerometerDirection.x = 0.0f;
            }
        }

        // Di chuyển xe thẳng hướng phía trước
        //transform.Translate(new Vector3(0, 0, MovingSpeed * Time.deltaTime));

		moveDirection = transform.TransformDirection (new Vector3 (0, 0, MovingSpeed));

		this.GetComponent<CharacterController>().Move (moveDirection * Time.deltaTime);

		// Camera cũng phải chạy theo, giữ 1 khoảng cách nhất định với xe
        Camera.main.transform.position = new Vector3(0, transform.position.y + 4, transform.position.z - 10);
        
        if (AccelerometerDirection.x > AccelerometerSensitivity)
        {
            // Khi nghiên phone thì cho xe quẹo trái
            transform.Rotate (new Vector3 (0, 30 * Time.deltaTime, 0), Space.Self);
        }
        else if (AccelerometerDirection.x < -AccelerometerSensitivity)
        {
            // Quẹo phải
            transform.Rotate (new Vector3 (0, -30 * Time.deltaTime, 0), Space.Self);
        }
        else
        {
            // Mặc định thì xoay xe hướng về phía trước nó sẽ chạy thẳng
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, 0), 7 * Time.deltaTime);
        }
//
//        // Di chuyển xe thẳng hướng phía trước
//        //transform.Translate(new Vector3(0, 0, MovingSpeed * Time.deltaTime));
//		this.GetComponent<CharacterController>().Move (transform.TransformDirection(new Vector3 (0, 0, MovingSpeed * Time.deltaTime)));
//        // Camera cũng phải chạy theo, giữ 1 khoảng cách nhất định với xe
//        Camera.main.transform.position = new Vector3(0, transform.position.y + 8, transform.position.z - 4);
//        
//		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
//		    foreach (Touch touch in Input.touches) {
//		        if ((btnLeft.HitTest (touch.position)) && (this.transform.position.x > -2.5)) {
//					transform.Rotate (new Vector3 (0, -20 * Time.deltaTime, 0), Space.Self);
//				} else
//			    if ((btnRight.HitTest (touch.position)) && (this.transform.position.x < 2.5)) {
//			        transform.Rotate (new Vector3 (0, 20 * Time.deltaTime, 0), Space.Self);
//				} 
//			}
//		    if (Input.touchCount == 0)
//		        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, 0), 5 * Time.deltaTime);
//		}
//        else
//        {
//            if (Input.GetKey(KeyCode.LeftArrow))
//            {
//                transform.Rotate (new Vector3 (0, -20 * Time.deltaTime, 0), Space.Self);
//            }else 
//            if (Input.GetKey(KeyCode.RightArrow))
//            {
//                transform.Rotate (new Vector3 (0, 20 * Time.deltaTime, 0), Space.Self);
//            }else 
//                transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, 0), 5 * Time.deltaTime);
//        }        
	}

    void OnTriggerEnter (Collider other){
		if (other.name == "Coin") {
            Vector3 np = other.transform.position;
            np.x = 0;
            np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(58, 60));
            other.transform.position = np;

			CoinNum++;
			CoinLabel.text = CoinLabel.text.Substring(0, 5) + CoinNum.ToString();
			audio.PlayOneShot(collectCoinSound, 1);
		}

        if (other.name == "Gun") {
            Vector3 np = other.transform.position;
            np.x = 0;
            np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(110, 130));
            other.transform.position = np;

            PlayerPrefs.SetInt("canShoot", 1);
        }
    }

	void OnCollisionEnter(Collision other){
		if (other.collider.name == "Car") {
			other.collider.renderer.enabled = false;
			PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + CoinNum);
			Application.LoadLevel(2);
		}
	}
}
 