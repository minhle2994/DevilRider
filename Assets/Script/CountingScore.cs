using UnityEngine;
using System.Collections;

public class CountingScore : MonoBehaviour {
	public GUIText ScoreLabel;
	public int Score = 0;
	int[] isCounted = new int[10];
	public GameObject aim, DevilRider;
	public GameObject sung;
	// Use this for initialization
	void Start () {
        aim.transform.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        int nearestObj = -1;
        float nearestDist = -1;
		int childNum = transform.childCount;

		for (int i=0; i < childNum; i++){
			Transform child = transform.GetChild(i);
			if (child.position.z < DevilRider.transform.position.z && isCounted[i] == 0){
				Score ++;
				isCounted[i] = 1;
				Debug.Log(Score);
				ScoreLabel.text = ScoreLabel.text.Substring(0, 6) + Score.ToString();
			}
			if (child.position.z > DevilRider.transform.position.z && isCounted[i] == 1){
				isCounted[i] = 0;
			}			 
			float dist = child.position.z - DevilRider.transform.position.z;
            if (dist > 0 && (nearestDist < 0 || nearestDist > dist)) {
                nearestObj = i;
                nearestDist = dist;
				Debug.Log(DevilRider.transform.position.z);
            }
		}
		PlayerPrefs.SetInt("Score", Score);
		//Debug.Log (nearestDist);

		if (nearestDist > 2 && nearestDist < 25 && PlayerPrefs.GetInt ("canShoot") > 0) {
            Vector3 tmp = transform.GetChild(nearestObj).position;
			aim.transform.position = new Vector3(DevilRider.transform.position.x, tmp.y, tmp.z - 1);
            aim.transform.renderer.enabled = true;
           
			if (Mathf.Abs(DevilRider.transform.position.x - tmp.x) < 0.5) {
                Vector3 np = transform.GetChild(nearestObj).position;
                np.x = 0;
                np += new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(58, 60));
                transform.GetChild(nearestObj).position = np;
                Score = Score + 5;
                ScoreLabel.text = ScoreLabel.text.Substring(0, 6) + Score.ToString();
                PlayerPrefs.SetInt("canShoot", PlayerPrefs.GetInt("canShoot")-1);
				if (PlayerPrefs.GetInt("canShoot")==0)
				{
					sung.renderer.enabled = false;
                	aim.transform.renderer.enabled = false;
				}
            }
        }
		else 
			aim.transform.renderer.enabled = false;
	}
}
