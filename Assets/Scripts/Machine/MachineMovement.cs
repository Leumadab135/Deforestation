using Deforestation.Dinosaurus;
using Deforestation.Recolectables;
using UnityEngine;
namespace Deforestation.Machine
{
    public class MachineMovement : MonoBehaviour
    {
        #region Fields
        [SerializeField] private float _speedForce = 50;
        [SerializeField] private float _speedRotation = 15;
        [SerializeField] private float _jumpForce = 10000;
        [SerializeField] private bool _canJump;
        private Rigidbody _rb;
        private Vector3 _movementDirection;
        private Inventory _inventory => GameController.Instance.Inventory;

        [Header("Energy")]
        [SerializeField] private float energyDecayRate = 20f;
        public float energyTimer = 0f;
        #endregion

        #region Properties
        #endregion

        #region Unity Callbacks	
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_inventory.HasResource(RecolectableType.HyperCrystal))
            {
                //Movement
                _movementDirection = new Vector3(Input.GetAxis("Vertical"), 0, 0);
                transform.Rotate(Vector3.up * _speedRotation * Time.deltaTime * Input.GetAxis("Horizontal"));
                Debug.DrawRay(transform.position, transform.InverseTransformDirection(_movementDirection.normalized) * _speedForce);

                //Energy
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    energyTimer += Time.deltaTime;
                    if (energyTimer >= energyDecayRate)
                        _inventory.UseResource(RecolectableType.HyperCrystal);
                }

            }
            else
            {
                GameController.Instance.MachineController.StopMoving();
                energyTimer = 0f;
            }

            if (Input.GetKeyUp(KeyCode.Space) && _inventory.HasResource(RecolectableType.BoostCrystal) && _canJump)
            {
                _rb.AddForce(Vector3.up * _jumpForce);
                _inventory.UseResource(RecolectableType.BoostCrystal);
            }

            CheckGround();
        }

        private void FixedUpdate()
        {
            _rb.AddRelativeForce(_movementDirection.normalized * _speedForce, ForceMode.Impulse);
        }

        void CheckGround()
        {
            RaycastHit hit;
            float maxDistance = 4f;
            float force = 100000;
            Vector3 direction = -transform.up;

            // Dibuja el rayo en el editor
            Debug.DrawRay(transform.position, direction * maxDistance, Color.red);

            // Calcula la máscara de la capa correctamente
            int layerMask = 1 << LayerMask.NameToLayer("Terrain");

            // Lanza un rayo hacia abajo desde la posición del objeto
            if (!Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask))
            {
                _canJump = false;
                _rb.AddRelativeForce(direction * force);
            }
            else
                _canJump = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Tree")
            {
                int index = other.GetComponent<Tree>().Index;
                GameController.Instance.TerrainController.DestroyTree(index, other.transform.position);
            }


        }
        private void OnCollisionEnter(Collision collision)
        {
            //Hacemos daño por contacto a los Stegasaurus
            HealthSystem target = collision.gameObject.GetComponent<HealthSystem>();
            if (target != null)
            {
                if (target.tag == "Player")
                    return;
                else
                    target.TakeDamage(10);
            }
        }

        #endregion

        #region Private Methods
        #endregion

        #region Public Methods

        #endregion

    }

}
