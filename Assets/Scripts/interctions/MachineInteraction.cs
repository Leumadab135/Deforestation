using UnityEngine;
using System;
namespace Deforestation.Interaction
{
    public enum MachineInteractionType
    {
        Door,
        Stairs,
        Machine
    }
    public class MachineInteraction : MonoBehaviour, IInteractable
    {
        #region Properties
        #endregion

        #region Fields
        [SerializeField] protected MachineInteractionType _type;
        [SerializeField] protected Transform _target;
        [SerializeField] protected InteractableInfo _interactableInfo;
        [SerializeField] private GameObject _outlineObject;

        #endregion

        #region Public Methods
        public InteractableInfo GetInfo()
        {
            _interactableInfo.Type = _type.ToString();
            return _interactableInfo;
        }

        public virtual void Interact()
        {
            if (_type == MachineInteractionType.Door)
            {
                //Move Door
                transform.position = _target.position;
            }
            if (_type == MachineInteractionType.Stairs)
            {
                //Teleport Player
                GameController.Instance.TeleportPlayer(_target.position);
            }
            if (_type == MachineInteractionType.Machine)
            {
                GameController.Instance.MachineMode(true);
            }
        }
        public void EnableOutline()
        {
            if (_outlineObject != null)
            {
                Vector3 outlineScale = new Vector3(1.05f, 1.05f, 1);
                _outlineObject.transform.localScale = outlineScale;
            }
        }

        public void DisableOutline()
        {
            if (_outlineObject != null)
            {
                if (_outlineObject != null)
                    _outlineObject.transform.localScale = Vector3.zero;
            }
        }

        #endregion
    }

}