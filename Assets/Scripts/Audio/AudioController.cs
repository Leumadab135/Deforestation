using UnityEngine;
using DG.Tweening;

namespace Deforestation.Audio
{
    public class AudioController : MonoBehaviour
    {
        private float _fadeDuration = 0.3f;
        #region Fields
        [Header("FX")]
        [SerializeField] private AudioSource _steps;
        [SerializeField] private AudioSource _machineSteps;
        [SerializeField] private AudioSource _machineOn;
        [SerializeField] private AudioSource _machineOff;
        [SerializeField] private AudioSource _shoot;
        [SerializeField] private AudioSource _forest;
        private bool _isMoving;

        [Space(10)]

        [Header("Music")]
        [SerializeField] private AudioSource _musicMachine;
        [SerializeField] private AudioSource _musicHuman;
        #endregion

        #region Properties
        #endregion

        #region Unity Callbacks	
        private void Awake()
        {
            GameController.Instance.OnMachineModeChange += SetMachineMusicState;
            GameController.Instance.MachineController.OnMachineDriveChange += SetMachineDriveEffect;
            GameController.Instance.MachineController.WeaponController.OnMachineShoot += ShootFX;
        }

        private void Start()
        {
            _musicHuman.Play();
            _forest.Play();
        }


        private void Update()
        {
            _isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

            if (_isMoving)
            {
                if (GameController.Instance.MachineModeOn)
                    PlayMachineSteps();
                else
                    PlayNormalSteps();
            }
            else
            {
                StopAllSteps();
            }
        }

        private void PlayMachineSteps()
        {
            if (!_machineSteps.isPlaying)
            {
                _machineSteps.volume = 0.8f;
                _machineSteps.Play();
            }

            if (_steps.isPlaying)
                _steps.Stop();
        }

        private void PlayNormalSteps()
        {
            if (!_steps.isPlaying)
                _steps.Play();

            if (_machineSteps.isPlaying)
                FadeOutMachineSteps();
        }

        private void StopAllSteps()
        {
            if (_steps.isPlaying)
                _steps.Stop();

            if (_machineSteps.isPlaying)
                FadeOutMachineSteps();
        }

        private void FadeOutMachineSteps()
        {
            _machineSteps.DOFade(0f, _fadeDuration).OnComplete(() =>
            {
                _machineSteps.Stop();
                _machineSteps.volume = 0.7f;
            });
        }

        #endregion

        #region Private Methods
        private void SetMachineMusicState(bool machineMode)
        {
            if (machineMode)
            {
                _musicHuman.DOFade(0, 3);
                _forest.DOFade(0, 0.2f);
                _musicMachine.DOFade(0.6f, 1);
                _musicMachine.Play();
            }
            else
            {
                _musicHuman.DOFade(0.1f, 3);
                _forest.DOFade(0.2f, 0.1f);
                _musicMachine.DOFade(0, 3);
            }
        }

        private void SetMachineDriveEffect(bool startDriving)
        {
            if (startDriving)
                _machineOn.Play();
            else
                _machineOff.Play();

        }
        private void ShootFX()
        {
            _shoot.Play();
        }
        #endregion

        #region Public Methods
        #endregion

    }

}