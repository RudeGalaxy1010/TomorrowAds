using UnityEditor;
using UnityEngine;

namespace Tomorrow.Services
{
    public class MenuItemCreator
    {
        private const string MenuPath = "GameObject/Tomorrow Service";
        private const string ServiceName = "TomorrowService";
        private const string MultipleServicesExceptionMessage = "already has been added";

        [MenuItem(MenuPath, false, 0)]
        private static void CreateNewAsset(MenuCommand menuCommand)
        {
            if (Object.FindObjectOfType<TomorrowService>() != null)
            {
                Debug.LogError($"{ServiceName} {MultipleServicesExceptionMessage}");
                return;
            }

            GameObject service = new GameObject(ServiceName);
            GameObjectUtility.SetParentAndAlign(service, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(service, "Create " + service.name);
            Selection.activeObject = service;
            service.AddComponent<TomorrowService>();
        }
    }
}