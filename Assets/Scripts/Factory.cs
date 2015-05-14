using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Game
{
    public class Factory : Actor
    {
        private const int TIME_TO_SPAWN = 2;

        public GameObject unit;
        public Transform spawnTransform;

        private List<GameObject> units;
        private float currentTimeToSpawn;
        private Vector3 unitPosition;

		//public properties
		public List<GameObject> Units
		{
			get { return units;}
		}

		public Factory() {}

		public Factory(OpposingSide side) : base(side) {}

        // Use this for initialization
		internal override void Start ()
        {
			base.Start();
			health = 5;
            SetColorByOpposingSide ();
        }
    
        // Update is called once per frame
        internal override void Update ()
        {
			base.Update();
            currentTimeToSpawn += Time.deltaTime;
            if (currentTimeToSpawn >= TIME_TO_SPAWN)
            {
                SpawnUnit();
                currentTimeToSpawn = 0;
            }
        }

        private void SpawnUnit() {
            if (units == null)
            {
                units = new List<GameObject>();
            }

            GameObject newUnit = (GameObject) Instantiate(unit, spawnTransform.position, spawnTransform.rotation);
            units.Add(newUnit);
			GameController.allUnits[opposingSide].Add(newUnit);
           
			Unit unitScript = newUnit.gameObject.GetComponent<Unit>();
            if (unitScript != null)
            {
                unitScript.SetOpposingSide(opposingSide);
				unitScript.parentFactory = this;
            }
            
			Rigidbody rigidBody = newUnit.gameObject.GetComponent<Rigidbody>();
            if (rigidBody != null)
            {
                rigidBody.AddForce(Vector3.right * 100);
            }
        }

		protected override void Die()
		{
			GameController.allFactories[opposingSide].Remove(this.gameObject);
			base.Die();
		}

    }
}
