using UnityEngine;
using System.Collections.Generic;

namespace Game
{
	public class GameController : MonoBehaviour
	{
		public static GameObject enemFact;

		public GameObject factory;
		public GameObject enemyFactory;

		private Quaternion zeroQuat = new Quaternion();

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (Input.GetButtonDown("Fire1"))
			{
				Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				position.y = 0;
				GameObject currentFactory = (GameObject) Instantiate(factory, position, zeroQuat);
				currentFactory.GetComponent<Factory>().SetOpposingSide(OpposingSide.Ally);
			}

		}
		
	}
}