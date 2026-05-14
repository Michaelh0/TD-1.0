using System.Linq;
using UnityEngine;

public static class GameObjectExtensions
{
    public static GameObject[] GetGameObjects(this GameObject self) => GetGameObjects<Transform>(self);
    public static GameObject[] GetGameObjects<T>(this GameObject self) where T : Component
    {
        return self.GetComponentsInChildren<T>()
            .Where(component => component.gameObject != self)
            .Select(component => component.gameObject)
            .ToArray();
        // return (from component in self.GetComponentsInChildren<T>()
        //         where component.gameObject != self
        //         select component.gameObject).ToArray();
    }
}