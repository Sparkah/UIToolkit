using UnityEngine;

namespace UI.RobotInstantiationPanel
{
    public class SceneObjectsInstantiator : MonoBehaviour
    {
        public GameObject SceneObject { get; private set; } 
        public GameObject[] Robots;

        public void CreateOrDestroyObject(GameObject objectToCreate)
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
    }
}