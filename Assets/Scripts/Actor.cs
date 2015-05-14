using UnityEngine;
using UnityEngine.UI;
using System;
using UnityStandardAssets.Effects;

namespace Game
{
	public abstract class Actor : MonoBehaviour
	{
		public OpposingSide opposingSide;
		public GameObject explosion;
		public Text text;

		protected float health;
		protected ActorTypes type;		

		public float Health { get { return health; } }
		public ActorTypes Type { get { return type; } }


		public Actor ()
		{
		}

		public Actor(OpposingSide side)
		{
			opposingSide = side;
		}

		internal virtual void Start()
		{
			health = 1;
			if (this is Factory)
				type = ActorTypes.Factory;
			else if (this is Unit)
				type = ActorTypes.Unit;
			SetTag();
		}

		internal virtual void Update()
		{
			text.text = health.ToString();
		}

        public void SetOpposingSide(OpposingSide side)
        {
            opposingSide = side;
            SetColorByOpposingSide();
			SetTag();
        }

		protected void SetTag()
		{
			switch (opposingSide)
			{
				case OpposingSide.Ally:
					if (this is Factory)
						this.gameObject.tag = TagNames.ALLY_FACTORY;
					else if (this is Unit)
						this.gameObject.tag = TagNames.ALLY_UNIT;
					break;
					
				case OpposingSide.Enemy:
					if (this is Factory)
						this.gameObject.tag = TagNames.ENEMY_FACTORY;
					else if (this is Unit)
						this.gameObject.tag = TagNames.ENEMY_UNIT;
					break;
			}
		}

		protected void SetColorByOpposingSide ()
		{
			MeshRenderer meshRenderer;
			meshRenderer = this.gameObject.GetComponent<MeshRenderer> ();
			if (meshRenderer != null) {
				if (opposingSide == OpposingSide.Enemy) {
					meshRenderer.materials [0].color = new Color (0.69f, 0.25f, 0.17f);
				} else if (opposingSide == OpposingSide.Ally) {
					meshRenderer.materials [0].color = new Color (0.17f, 0.69f, 0.47f);
		    	}
			}
		}

		public virtual void SetDamage(float damage)
		{
			health -= damage;
			if (health <= 0)
			{
				health = 0;
				Die();
			}
		}

		protected virtual void Die()
		{
			//this.gameObject.GetComponent<MeshRenderer>().enabled = false;
			GameObject exp = (GameObject)Instantiate(explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
			exp.GetComponent<ExplosionPhysicsForce>().explosionForce = 1;
			Destroy(exp, 3);
			Destroy(this.gameObject);

		}
	}
}

