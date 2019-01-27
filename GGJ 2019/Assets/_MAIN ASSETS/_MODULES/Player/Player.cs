﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities.Inspector;

using UnityStandardAssets.Characters.FirstPerson;

using Semantic;
using CustomInputs;

namespace Game
{
	public class Player : MonoBehaviour 
	{
		#region ATTRIBUTES

		[Header("SEMANTIC")]
		[ReadOnly] [SerializeField] private SemanticFields currentSemanticField = SemanticFields.Calle;

		[Header("REFERENCES")]
		[SerializeField] private FirstPersonController firstPersonController;

		private Vector3 originalPosition;
		private Vector3 originalRotation;

		#endregion

		#region INITIALIZATION

		void Awake()
		{
			InputManager.DoubleTapEvent += SaveSemanticFieldEvent;

			originalPosition = transform.position;
			originalRotation = transform.eulerAngles;
		}

		void OnDestroy()
		{
			InputManager.DoubleTapEvent -= SaveSemanticFieldEvent;
		}

		#endregion

		#region BEHAVIOURS

		private void SaveSemanticFieldEvent()
		{
			Debug.Log (currentSemanticField + " liked!");

			Quests.QuestManager.AddSemanticData(currentSemanticField, 1);
		}

		public void EnableMovement()
		{
			firstPersonController.enabled = true;
		}

		public void DisableMovement()
		{
			firstPersonController.enabled = false;
		}

		public void ResetPosition()
		{
			transform.position = originalPosition;
			transform.eulerAngles = originalRotation;
		}

		#endregion

		#region COLLISION_BEHAVIOURS

		void OnTriggerEnter(Collider collider)
		{
			if (collider.tag == "Semantic Field")
			{
				SemanticField semanticField = collider.gameObject.GetComponentInChildren<SemanticField>();

				currentSemanticField = semanticField.GetSemanticField;
			}
		}

		void OnTriggerExit(Collider collider)
		{
			if (collider.tag == "Semantic Field")
			{
				SemanticField semanticField = collider.gameObject.GetComponentInChildren<SemanticField>();

				if (currentSemanticField == semanticField.GetSemanticField)
					currentSemanticField = SemanticFields.Calle;
			}
		}

		#endregion
	}
}