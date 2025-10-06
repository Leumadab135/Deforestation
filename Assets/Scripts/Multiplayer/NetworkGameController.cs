using Deforestation;
using Deforestation.Interaction;
using Deforestation.Machine;
using Deforestation.Recolectables;
using Photon.Pun;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Deforestation.Network
{

    public class NetworkGameController : GameController
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void InitializeMe(HealthSystem health, CharacterController player, Inventory inventory, InteractionSystem interaction, Transform playerFollow)
        {
            _playerFollow = playerFollow;
            _player = player;
            _inventory = inventory;
            _interactionSystem = interaction;
            _playerHealth = health;
        }

        public void InitializeMachine (MachineController machine, Transform follow)
        {
            _machine = machine;
            _machineFollow = follow;
        }
    }
}
