using UnityEngine;
using System.Collections;

namespace Game
{
	public class Unit : Actor
	{
        internal Vector3 velocity = new Vector3();
        internal Quaternion RotationDriveMode = new Quaternion();

        public Unit(OpposingSide side) : base(side)
        {
        }

		// Use this for initialization
		void Start ()
		{
			SetColorByOpposingSide();
		}
	
		// Update is called once per frame
		void Update ()
		{
			
		}
        
        void OnTriggerExit(Collider other) {
			Destroy(this.gameObject);   
		}
	}
}