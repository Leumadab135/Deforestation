using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Deforestation.Network
{

    public class NetworkController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private UINetwork _ui;
         // Start is called before the first frame update
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        // Update is called once per frame
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinOrCreateRoom("FPSRoom", new RoomOptions { MaxPlayers = 5 }, null);
        }
        public override void OnJoinedRoom()
        {
            PhotonNetwork.Instantiate("PlayerMultiplayer", new Vector3(584, 651, 1389f), Quaternion.identity);
            PhotonNetwork.Instantiate("TheMachineMultiplayer", new Vector3(600, 653, 1389f), Quaternion.identity);
            _ui.LoadingComplete();
        }
    }

}