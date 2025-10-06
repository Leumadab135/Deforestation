using Deforestation.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Deforestation.Network
{

    public class UINetwork : MonoBehaviour
    {
        [SerializeField] private GameObject _connectingPanel;
        [SerializeField] private UIGameController _uIGameController;

        public void LoadingComplete()
        {
            _connectingPanel.SetActive(false);
            _uIGameController.enabled = true;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}