using UnityEngine;

namespace UI.RobotInstantiationPanel
{
    public class SceneObjectsInstantiator : MonoBehaviour
    {
        private MenuScript _menuScript;
        public GameObject SceneObject;

        public void ConstructMenuScript(MenuScript menuScript)
        {
            _menuScript = menuScript;
        }

        private void CreateObject(GameObject objectToCreate)
        {
            if (SceneObject == null)
            {
                SceneObject = Instantiate(objectToCreate);
            }
            else
            {
                DestroyObject(SceneObject);
            }
        }

        private void DestroyObject(GameObject objectToDestroy)
        {
            Destroy(objectToDestroy);
            SceneObject = null;
        }

        #region Positioner

        [SerializeField] private Transform _positionerTransform;
        private bool _positionerToggle = false;
        private GameObject _positionerObject;

        public void CreateDestroyPositioner()
        {
            CreateObject(_positionerTransform.gameObject);
        }

        #endregion


        #region Painter

        [SerializeField] private Transform _painterTransform;
        private bool _painterToggle = false;
        private GameObject _painterObject;

        public void CreateDestroyPainter()
        {
            CreateObject(_painterTransform.gameObject);
        }

        #endregion
    }
}