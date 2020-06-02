using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGAC.General
{
    public static class GeneralMethods
    {
        public static void CloseApp()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        public static T GetInstance<T>(string objectName) where T : Behaviour
        {
            T[] sceneObjects = GameObject.FindObjectsOfType<T>();
            if (sceneObjects == null || sceneObjects.Length <= 0)
                return GetNewObjectInstance<T>(objectName);
            else
                return GetActiveObjectInstance<T>(sceneObjects);
        }

        public static int GetLayer(LayerMask layerMask)
        {
            int layerNumber = 0;
            int layer = layerMask.value;
            while (layer > 0)
            {
                layer = layer >> 1;
                layerNumber++;
            }
            return layerNumber - 1;
        }
        public static T[] GetComponentsInChildren<T>(Transform parent) where T : Component
        {
            List<T> components = new List<T>();
            int children = 0;
            if(parent!=null)
                children = parent.childCount;
            for (int child = 0; child < children; ++child)
            {
                Transform Child = parent.GetChild(child);
                T component = Child.GetComponent<T>();
                if (component != null)
                    components.Add(component);
            }
            return components.ToArray();
        }

        #region PRIVATE METHODS
        private static T GetNewObjectInstance<T>(string name) where T : Behaviour
        {
            return new GameObject(name).AddComponent<T>();
        }
        private static T GetActiveObjectInstance<T>(T[] objects) where T : Behaviour
        {
            T activeObject = null;
            for (int obj = 0; obj < objects.Length; ++obj)
            {
                if (activeObject != null)
                    objects[obj].enabled = false;
                else if (objects[obj].enabled)
                    activeObject = objects[obj];
            }

            if (activeObject == null)
            {
                objects[0].enabled = true;
                activeObject = objects[0];
            }
            return activeObject;
        }
        #endregion
    }
}