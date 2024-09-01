using UnityEngine;

namespace _10_Self_Balancing
{
	public class BallState : MonoBehaviour {

		public bool dropped = false;

		void OnCollisionEnter(Collision col)
		{
			if(col.gameObject.tag == "drop")
			{
				dropped = true;
			}
		}
	}
}
