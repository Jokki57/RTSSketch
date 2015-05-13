using UnityEngine;
using System.Collections.Generic;
using Game;

namespace Game
{
    public class Factory : Actor
    {
        private const int TIME_TO_SPAWN = 10;

        public GameObject unit;

        private List<GameObject> units;
        private float currentTimeToSpawn;
        private Vector3 unitPosition;

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
            GameObject newUnit = (GameObject) Instantiate(unit, this.gameObject.transform.position, this.gameObject.transform.rotation);
            units.Add(newUnit);
            Rigidbody rigidBody = newUnit.gameObject.GetComponent<Rigidbody>();
            if (rigidBody != null)
            {
                rigidBody.AddForce(new Vector3(-5,0,0));
            }
        }

    }
}
