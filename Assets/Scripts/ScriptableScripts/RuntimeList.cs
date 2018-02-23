using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeList<T> : ScriptableObject {
    
    public List<T> Items = new List<T>();
    [SerializeField]
    private bool isResetableOnLoad;

    public GameEvent ItemAdded;
    public GameEvent ItemRemoved;

    protected void OnEnable()
    {
        if(isResetableOnLoad)
        {
            Items = new List<T>();
        }
    }

    public virtual void Add (T t)
    {
        if(!Items.Contains(t)) {
            Items.Add(t);
            if(ItemAdded != null)
            {
                ItemAdded.Raise();
            }
        }
    }

    public virtual void Remove(T t)
    {
        if (Items.Contains(t))
        {
            Items.Remove(t);
            if (ItemRemoved != null)
            {
                ItemRemoved.Raise();
            }
        }
    }

}
