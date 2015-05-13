using UnityEngine;
using System;

namespace Game
{
	public class Actor : MonoBehaviour
	{
		public OpposingSide opposingSide;

		public Actor ()
		{
		}

		public Actor(OpposingSide side)
		{
			opposingSide = side;
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
	}
}

