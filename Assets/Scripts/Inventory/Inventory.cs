using UnityEngine;
using System;
using System.Collections.Generic;
using Deforestation.Audio;

namespace Deforestation.Recolectables
{
	public class Inventory : MonoBehaviour
	{
		#region Properties

		public Dictionary<RecolectableType, int> InventoryStack = new Dictionary<RecolectableType, int>();
		public Action OnInventoryUpdated;
        #endregion

        #region Fields
        [SerializeField] private AudioSource _collect;
        #endregion

        #region Unity Callbacks
        #endregion

        #region Public Methods
        public void AddRecolectable(RecolectableType type, int count)
		{
			_collect.Play();
			if (InventoryStack.ContainsKey(type))
				InventoryStack[type] += count;
			else
				InventoryStack.Add(type, count);
			OnInventoryUpdated?.Invoke();			

		}
		public bool UseResource (RecolectableType type, int count = 1)
		{
			if (HasResource(type, count))
			{
				InventoryStack[type] -= count;
				OnInventoryUpdated?.Invoke();
				return true;
			}

			return false;
		}

		public bool HasResource(RecolectableType type, int count = 1)
		{
			if (InventoryStack.ContainsKey(type) && InventoryStack[type] >= count)
				return true;

			return false;
		}
		#endregion

		#region Private Methods
		#endregion
	}
}
