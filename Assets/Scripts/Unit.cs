using UnityEngine;
using System.Collections.Generic;
using System;

namespace Game
{
	public class Unit : Actor
	{
		internal static int speed = 2;

		protected float damage;

        internal Vector3 velocity = new Vector3();
        internal Quaternion RotationDriveMode = new Quaternion();
		internal GameObject target;
		internal List<ActorTypes> targetTypes;
		internal Factory parentFactory;

		private System.Random random;

		public float Damage { get { return damage; } }

        public Unit(OpposingSide side) : base(side)
        {
        }

		// Use this for initialization
		internal override void Start ()
		{
			damage = 0.05f;
			random = new System.Random();

			targetTypes = new List<ActorTypes>();
			targetTypes.Add((ActorTypes)random.Next(0, 2));
			base.Start();
			SetColorByOpposingSide();
			health = 2;
		}
	
		// Update is called once per frame
		internal override void Update ()
		{
			base.Update();
			if (target != null)
			{
				Vector3 direction = (target.transform.position - this.gameObject.transform.position).normalized;
				this.gameObject.GetComponent<Rigidbody>().velocity = direction * speed;
			}
		}
        
        void OnTriggerExit(Collider other) {
			if (other.tag == TagNames.BOUND)
			{
				if (parentFactory)
					parentFactory.Units.Remove(this.gameObject);
				GameController.allUnits[opposingSide].Remove(this.gameObject);
				Destroy(this.gameObject);
			}
		}

		internal void SetTargetType(ActorTypes type)
		{
			targetTypes.RemoveRange(0, targetTypes.Count);
			targetTypes.Add(type);
		}

		protected void OnCollisionStay(Collision collision)
		{
			Actor actor = collision.gameObject.GetComponent<Actor>();
			if (actor)
			{
				if (actor.opposingSide != this.opposingSide && targetTypes.IndexOf(actor.Type) != -1) {
					actor.SetDamage(damage);
				}
			}
		}

		protected override void Die()
		{
			if (parentFactory != null)
				parentFactory.Units.Remove(this.gameObject);
			GameController.allUnits[opposingSide].Remove(this.gameObject);
			base.Die();
		}
	}
}