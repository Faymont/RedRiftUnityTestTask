using CodeBase.Card;
using UnityEngine;

namespace CodeBase.Logic
{
	public class PlayZoneTrigger : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			var card = other.GetComponent<CardObject>();
			if (card != null)
			{
				card.CanPlay = true;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			var card = other.GetComponent<CardObject>();
			if (card != null)
			{
				card.CanPlay = false;
			}
		}
	}
}