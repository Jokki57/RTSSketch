using UnityEngine;
using System.Collections.Generic;
using Game;

namespace Game
{
    public class Factory : Actor
    {
        private const int TIME_TO_SPAWN = 5;

        public GameObject unit;
        public Transform spawnTransform;

        private List<GameObject> units;
        private float currentTimeToSpawn;
        private Vector3 unitPosition;

		public Factory() {}

		public Factory(OpposingSide side) : base(side) {}

        // Use this for initialization
        void Start ()
        {
            SetColorByOpposingSide ();
        }
    
        // Update is called once per frame
        void Update ()
        {
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
            Unit unitScript = newUnit.gameObject.GetComponent<Unit>();
            if (unitScript != null)
            {
                unitScript.SetOpposingSide(opposingSide);
            }
            Rigidbody rigidBody = newUnit.gameObject.GetComponent<Rigidbody>();
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector3(-10, 0, 0);
            }
        }

    }
}
