using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class BoundList : BoundList<object>
{

}

public class ListChangedEventArgs : EventArgs
{
    public int index;
    public object item;
    public ListChangedEventArgs(int index, object item)
    {
        this.index = index;
        this.item = item;
    }
}


public delegate void ItemAddedEventHandler(object source, ListChangedEventArgs e);
public delegate void ItemRemovedEventHandler(object source, ListChangedEventArgs e);
public delegate void ListChangedEventHandler(object source, ListChangedEventArgs e);
public delegate void ListClearedEventHandler(object source, EventArgs e);

public interface IBoundList
{
    event ListChangedEventHandler ListChanged;
    event ItemRemovedEventHandler ItemRemoved;
    event ItemAddedEventHandler ItemAdded;
    event ListClearedEventHandler ListCleared;
}

public class BoundList<T> : IList<T>, IBoundList
{
    private IList<T> internalList;

    /// <summary>
    /// Fired whenever list item has been changed, added or removed or when list has been cleared
    /// </summary>
    public event ListChangedEventHandler ListChanged;
    /// <summary>
    /// Fired when list item has been removed from the list
    /// </summary>
    public event ItemRemovedEventHandler ItemRemoved;
    /// <summary>
    /// Fired when item has been added to the list
    /// </summary>
    public event ItemAddedEventHandler ItemAdded;
    /// <summary>
    /// Fired when list is cleared
    /// </summary>
    public event ListClearedEventHandler ListCleared;

    public BoundList()
    {
        internalList = new List<T>();
    }

    public BoundList(IList<T> list)
    {
        internalList = list;
    }

    public BoundList(IEnumerable<T> collection)
    {
        internalList = new List<T>(collection);
    }

    protected virtual void OnItemAdded(ListChangedEventArgs e)
    {
        if (ItemAdded != null)
            ItemAdded(this, e);
    }

    protected virtual void OnItemRemoved(ListChangedEventArgs e)
    {
        if (ItemRemoved != null)
            ItemRemoved(this, e);
    }

    protected virtual void OnListChanged(ListChangedEventArgs e)
    {
        if (ListChanged != null)
            ListChanged(this, e);
    }

    protected virtual void OnListCleared(EventArgs e)
    {
        if (ListCleared != null)
            ListCleared(this, e);
    }

    public int IndexOf(T item)
    {
        return internalList.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        internalList.Insert(index, item);
        OnListChanged(new ListChangedEventArgs(index, item));
    }

    public void RemoveAt(int index)
    {
        T item = internalList[index];
        internalList.Remove(item);
        OnListChanged(new ListChangedEventArgs(index, item));
        OnItemRemoved(new ListChangedEventArgs(index, item));
    }

    public T this[int index]
    {
        get { return internalList[index]; }
        set
        {
            internalList[index] = value;
            OnListChanged(new ListChangedEventArgs(index, value));
        }
    }

    public void Add(T item)
    {
        internalList.Add(item);
        OnListChanged(new ListChangedEventArgs(internalList.IndexOf(item), item));
        OnItemAdded(new ListChangedEventArgs(internalList.IndexOf(item), item));
    }

    public void AddRange(IEnumerable<T> list)
    {
        foreach (var e in list)
        {
            Add(e);
        }
    }

    public void Clear()
    {
        internalList.Clear();
        OnListCleared(new EventArgs());
    }

    public bool Contains(T item)
    {
        return internalList.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        internalList.CopyTo(array, arrayIndex);
    }

    public int Count
    {
        get { return internalList.Count; }
    }

    public bool IsReadOnly
    {
        get { return IsReadOnly; }
    }

    public bool Remove(T item)
    {
        lock (this)
        {
            int index = internalList.IndexOf(item);
            if (internalList.Remove(item))
            {
                OnListChanged(new ListChangedEventArgs(index, item));
                OnItemRemoved(new ListChangedEventArgs(index, item));
                return true;
            }
            else
                return false;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return internalList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)internalList).GetEnumerator();
    }
}

