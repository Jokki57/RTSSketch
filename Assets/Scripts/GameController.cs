using UnityEngine;
using System.Collections.Generic;

namespace Game
{
	public class GameController : MonoBehaviour
	{
		public static Dictionary<OpposingSide, List<GameObject>> allFactories = new Dictionary<OpposingSide, List<GameObject>>();
		public static Dictionary<OpposingSide, List<GameObject>> allUnits = new Dictionary<OpposingSide, List<GameObject>>();

		public GameObject factory;

		private Quaternion spawnQuaternion = new Quaternion();

		// Use this for initialization
		void Start ()
		{
			allUnits.Add(OpposingSide.Ally, new List<GameObject>());
			allUnits.Add(OpposingSide.Enemy, new List<GameObject>());
			allFactories.Add(OpposingSide.Ally, new List<GameObject>());
			allFactories.Add(OpposingSide.Enemy, new List<GameObject>());

			GameObject[] objects;
			objects = GameObject.FindGameObjectsWithTag(TagNames.ALLY_FACTORY);
			if (objects.Length > 0)
				allFactories [OpposingSide.Ally].AddRange(objects);
			objects = GameObject.FindGameObjectsWithTag(TagNames.ALLY_UNIT);
			if (objects.Length > 0)
				allUnits [OpposingSide.Ally].AddRange(objects);
			objects = GameObject.FindGameObjectsWithTag(TagNames.ENEMY_FACTORY);
			if (objects.Length > 0)
				allFactories [OpposingSide.Enemy].AddRange(objects);
			objects = GameObject.FindGameObjectsWithTag(TagNames.ENEMY_UNIT);
			if (objects.Length > 0)
				allUnits [OpposingSide.Enemy].AddRange(objects);
		}
	
		// Update is called once per frame
		void Update ()
		{
			//checking user input
			if (Input.GetButtonDown("Fire1"))
			{
				Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				position.y = 0;
				GameObject currentFactory = (GameObject)Instantiate(factory, position, spawnQuaternion);
				currentFactory.GetComponent<Factory>().SetOpposingSide(OpposingSide.Ally);
				allFactories [OpposingSide.Ally].Add(this.gameObject);
			} 
			else if (Input.GetButtonDown("Fire2"))
			{
				Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				position.y = 0;
				GameObject currentFactory = (GameObject)Instantiate(factory, position, spawnQuaternion);
				currentFactory.GetComponent<Factory>().SetOpposingSide(OpposingSide.Enemy);
				allFactories [OpposingSide.Enemy].Add(this.gameObject);
			}


			List<GameObject> allyUnits = allUnits[OpposingSide.Ally];
			List<GameObject> enemyUnits = allUnits[OpposingSide.Enemy];
			Unit unitComp;
			string targetTag = "";
			GameObject target;
			GameObject[] targets;

			//set targets for ally units
			//ai, al, ei, el -- ally iterator, ally length, enemy iterator, enemy length
			for (int allyIterator = 0, allyLength = allyUnits.Count; allyIterator < allyLength; allyIterator++)
			{
				try
				{
					unitComp = allyUnits[allyIterator].GetComponent<Unit>();
					if (unitComp.targetTypes == null)
						break;
					if (unitComp.targetTypes.IndexOf(ActorTypes.Factory) != -1)
						targetTag = TagNames.ENEMY_FACTORY;
					else if (unitComp.targetTypes.IndexOf(ActorTypes.Unit) != -1)
						targetTag = TagNames.ENEMY_UNIT;

					targets = GameObject.FindGameObjectsWithTag(targetTag);
					float offset1 = float.MaxValue;
					float offset2 = float.MaxValue;
					target = null;

					if (targets.Length > 0) {
						//using sqrMagnitude using for performance
						offset1 = Vector3.SqrMagnitude(targets[0].transform.position - allyUnits[allyIterator].transform.position);
						target = targets[0];
					}
					for (int enemyIterator = 1, enemyLength = targets.Length; enemyIterator < enemyLength; enemyIterator++)
					{
						offset2 = Vector3.SqrMagnitude(targets[enemyIterator].transform.position - allyUnits[allyIterator].transform.position);
						if (offset2 < offset1)
						{
							offset1 = offset2;
							target = targets[enemyIterator];
						}
					}

					allyUnits[allyIterator].GetComponent<Unit>().target = target;
				} 
				catch (UnityException exc)
				{
					Debug.LogError(exc.Message);
				}
			}

			//set targets for enemy units
			for (int enemyIterator = 0, enemyLength = enemyUnits.Count; enemyIterator < enemyLength; enemyIterator++)
			{
				try
				{
					unitComp = enemyUnits[enemyIterator].GetComponent<Unit>();
					if (unitComp.targetTypes == null)
						break;
					if (unitComp.targetTypes.IndexOf(ActorTypes.Factory) != -1)
						targetTag = TagNames.ALLY_FACTORY;
					else if (unitComp.targetTypes.IndexOf(ActorTypes.Unit) != -1)
						targetTag = TagNames.ALLY_UNIT;
					
					targets = GameObject.FindGameObjectsWithTag(targetTag);
					float offset1 = float.MaxValue;
					float offset2 = float.MaxValue;
					target = null;

					if (targets.Length > 0) {
						//using sqrMagnitude using for performance
						offset1 = Vector3.SqrMagnitude(targets[0].transform.position - enemyUnits[enemyIterator].transform.position);
						target = targets[0];
					}
					for (int allyIterator = 1, allyLength = targets.Length; allyIterator < allyLength; allyIterator++)
					{
						offset2 = Vector3.SqrMagnitude(targets[allyIterator].transform.position - enemyUnits[enemyIterator].transform.position);
						if (offset2 < offset1)
						{
							offset1 = offset2;
							target = targets[allyIterator];
						}
					}

					enemyUnits[enemyIterator].GetComponent<Unit>().target = target;
				} 
				catch (UnityException exc)
				{
					Debug.Log(exc.Message);
				}
			}

		}
		
	}
}