using UnityEngine;
using System;
using Deforestation.Machine.Weapon;
using UnityEngine.UIElements.Experimental;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using UnityEditor;

namespace Deforestation.Machine
{
    [RequireComponent(typeof(HealthSystem))]
    public class MachineController : MonoBehaviour
    {
        #region Properties
        public HealthSystem HealthSystem => _health;
        public WeaponController WeaponController;
        public Action<bool> OnMachineDriveChange;
        private bool _onAnimation;

        #endregion

        #region Fields
        private HealthSystem _health;
        private MachineMovement _movement;
        private Animator _anim;

        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            _health = GetComponent<HealthSystem>();
            _movement = GetComponent<MachineMovement>();
            _anim = GetComponent<Animator>();

        }
        // Start is called before the first frame update
        void Start()
        {
            _movement.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            //TODO: Mover a Input System
            if (Input.GetKeyUp(KeyCode.Q) && GameController.Instance.MachineModeOn && !_onAnimation)
            {
                StopDriving();
                OnMachineDriveChange?.Invoke(false);

                AnimatorStateInfo stateInfo = _anim.GetCurrentAnimatorStateInfo(0); //For quicker leaving if machine is not moving

                if (stateInfo.IsName("Off_Pose"))
                    GameController.Instance.MachineMode(false);
                else
                    StartCoroutine(GetOutAfterAnimation());
            }
        }

        #endregion

        #region Public Methods
        public void StopDriving()
        {
            StopMoving();
            StartAnimation();
            StartCoroutine(CanGetOut());
        }

        public void StartDriving(bool machineMode)
        {
            enabled = machineMode;
            _movement.enabled = machineMode;
            _anim.SetTrigger("WakeUp");
            _anim.SetBool("Move", machineMode);
            OnMachineDriveChange?.Invoke(true);
            StartAnimation();
            StartCoroutine(CanGetOut());
        }

        public void StopMoving()
        {
            _movement.enabled = false;
            _anim.SetBool("Move", false);
        }

        IEnumerator GetOutAfterAnimation()
        {
            yield return new WaitForSeconds(7);
            GameController.Instance.MachineMode(false);
        }

        IEnumerator CanGetOut()
        {
            yield return new WaitForSeconds(7);
            StopAnimation();
        }
        #endregion
        #region Private Methods
        private void StartAnimation()
        {
            _onAnimation = true;
        }

        private void StopAnimation()
        {
            _onAnimation = false;
        }
        #endregion
    }

}