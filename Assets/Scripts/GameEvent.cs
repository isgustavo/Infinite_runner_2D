using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ODT.Scriptable
{
    [CreateAssetMenu(menuName = "Event/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (listener != null)
            {
                listeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (listener != null)
            {
                listeners.Remove(listener);
            }
        }
    }
}