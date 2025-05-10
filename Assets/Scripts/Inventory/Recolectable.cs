using UnityEngine;
using System;
using Deforestation.Interaction;

namespace Deforestation.Recolectables
{
	public enum RecolectableType
	{
		SuperCrystal,
		HyperCrystal,
	}
	public class Recolectable : MonoBehaviour, IInteractable
	{
		#region Properties
		[field: SerializeField] public int Count { get; set; }
		[field: SerializeField] public RecolectableType Type { get; set; }

		#endregion

		#region Fields
		[SerializeField] private InteractableInfo _interactableInfo;
        [SerializeField] private GameObject _outlineObject;

        #endregion

        #region Unity Callbacks
        // Start is called before the first frame update
        void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}
		#endregion

		#region Public Methods
		public InteractableInfo GetInfo()
		{
			_interactableInfo.Type = Type.ToString();
			return _interactableInfo;
		}

		public void Interact()
		{
			Destroy(gameObject);
		}
        public void EnableOutline()
        {
            if (_outlineObject != null)
                _outlineObject.transform.localScale = Vector3.one * 1.05f;
        }

        public void DisableOutline()
        {
            if (_outlineObject != null)
                _outlineObject.transform.localScale = Vector3.zero;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
