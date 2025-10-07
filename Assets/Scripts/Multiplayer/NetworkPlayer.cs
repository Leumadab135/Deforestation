using Cinemachine;
using Deforestation;
using Deforestation.Interaction;
using Deforestation.Network;
using Deforestation.Recolectables;
using Photon.Pun;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Deforestation.Network
{

    public class NetworkPlayer : MonoBehaviourPun
    {
        [SerializeField] private HealthSystem _health;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InteractionSystem _interactions;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private FirstPersonController _fps;
        [SerializeField] private StarterAssetsInputs _inputs;
        [SerializeField] private PlayerInput _inputsPlayer;
        [SerializeField] private GameObject _3dAvatar;
        [SerializeField] private Transform _playerFollow;
        private NetworkGameController _gameController;

        // Start is called before the first frame update
        void Start()
        {
            _gameController = FindObjectOfType<NetworkGameController>(true);
            if (photonView.IsMine)
            {
                _gameController.InitializeMe(_health,_controller,_inventory,_interactions, _playerFollow);
                CinemachineVirtualCamera vc = FindFirstObjectByType<CinemachineVirtualCamera>();
                vc.Follow = _playerFollow;
                _3dAvatar.SetActive(false);
            }
            else
            {
                Destroy(_health);
                Destroy(_inventory);
                Destroy(_interactions);
                Destroy(_controller);
                Destroy(_fps);
                Destroy(_inputs);
                Destroy(_inputsPlayer);
                _3dAvatar.SetActive(true);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
