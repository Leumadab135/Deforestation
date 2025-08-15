using UnityEngine;
using System.Collections.Generic;

namespace Deforestation
{
    public class TreeTerrainController : MonoBehaviour
    {
        #region Properties
        public TreeInstance[] Trees => _runtimeTrees;
        #endregion

        #region Fields
        [SerializeField] private Tree _treeDetectionPrefab;
        [SerializeField] private Tree _treePrefab;

        private readonly List<Tree> _treeDetectors = new();

        private TreeInstance[] _originalTrees;   // copia intacta
        private TreeInstance[] _runtimeTrees;    // sobre este trabajamos

        private Terrain _terrain;
        #endregion

        #region Unity Callbacks
        private void Start()
        {
            _terrain = Terrain.activeTerrain;

            // Copia profunda para conservar el estado original
            _originalTrees = (TreeInstance[])_terrain.terrainData.treeInstances.Clone();
            _runtimeTrees = (TreeInstance[])_originalTrees.Clone();

            InitializeTrees();
        }

        private void OnDestroy()
        {
            // Restauramos el asset a su estado original
            _terrain.terrainData.treeInstances = _originalTrees;
            _terrain.Flush();
        }
        #endregion

        #region Public Methods
        public Vector3 TreeToWorldPosition(TreeInstance tree)
        {
            return Vector3.Scale(tree.position, _terrain.terrainData.size) +
                   _terrain.transform.position;
        }

        public GameObject DestroyTree(int index, Vector3 worldPos)
        {
            Tree fallen = Instantiate(_treePrefab, worldPos, Quaternion.identity);

            if (index >= 0 && index < _treeDetectors.Count)
            {
                Destroy(_treeDetectors[index].gameObject);
                _treeDetectors.RemoveAt(index);
            }

            RemoveTreeFromTerrain(index);
            UpdateDetectorIndices(index);

            return fallen.gameObject;
        }
        #endregion

        #region Private Methods
        private void InitializeTrees()
        {
            for (int i = 0; i < _runtimeTrees.Length; i++)
            {
                Vector3 pos = TreeToWorldPosition(_runtimeTrees[i]);
                Tree detector = Instantiate(_treeDetectionPrefab, pos, Quaternion.identity);
                detector.transform.parent = transform;
                detector.Index = i;
                _treeDetectors.Add(detector);
            }
        }

        private void RemoveTreeFromTerrain(int index)
        {
            List<TreeInstance> list = new(_runtimeTrees);
            list.RemoveAt(index);
            _runtimeTrees = list.ToArray();

            _terrain.Flush();
            _terrain.terrainData.treeInstances = _runtimeTrees;
        }

        private void UpdateDetectorIndices(int start)
        {
            for (int i = start; i < _treeDetectors.Count; i++)
                _treeDetectors[i].Index = i;
        }
        #endregion
    }
}


