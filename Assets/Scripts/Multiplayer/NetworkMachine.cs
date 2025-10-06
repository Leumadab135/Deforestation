using Deforestation;
using Deforestation.Interaction;
using Deforestation.Machine;
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

    public class NetworkMachine : MonoBehaviourPun
    {
        [SerializeField] private MachineController _machine;
        [SerializeField] private Transform _machineFollow;
        private NetworkGameController _gameController;

        // Start is called before the first frame update
        void Start()
        {
            _gameController = FindObjectOfType<NetworkGameController>(true);
            if (photonView.IsMine)
            {
                _gameController.InitializeMachine(_machine, _machineFollow);
                _gameController.gameObject.SetActive(true);
                //_3dAvatar.SetActive(false);
            }
            else
            {
                //---
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
