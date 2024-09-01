using UnityEngine;

namespace _9_Pong_ANN
{
	public class ServeBall : MonoBehaviour {

		public GameObject ball;

		public bool backWall = false;
		public ANN.Brain b;

		void OnCollisionEnter2D(Collision2D col)
		{
			if(col.gameObject.tag == "ball" && backWall)
			{
				b.numMissed += 1;
				ball.GetComponent<MoveBall>().ResetBall();
			}
		}

		// Use this for initialization
		void Start () {

		}
	
		// Update is called once per frame
		void Update () {

		}
	}
}
