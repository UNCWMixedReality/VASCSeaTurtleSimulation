using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using Altimit.UI;

namespace Altimit.Serialization
{
    [ExecuteInEditMode]
    public class HierarchyObserver : MonoBehaviour
    {
        public static bool DebugMessages = false;
        private static bool IncludeInactive = true;

        public Action<Transform> onAddChild;
        public Action<Transform> onRemoveChild;

        public Action<Transform> onAddSomeChild;
        public Action<Transform> onRemoveSomeChild;
        
        bool hasInitialized = false;

        public List<Transform> parents = new List<Transform>();
        public List<Transform> children = new List<Transform>();

        private int instanceID = 0;
        bool isDestroying = false;
        // Use this for initialization

        void Start()
        {
            if (instanceID != GetInstanceID())
            {
                hasInitialized = false;
                parents = new List<Transform>();
                children = new List<Transform>();
            }

            if (!hasInitialized)
                Init();
        }

        public void Init(int initDepth = 0)
        {
            TryAddChildObservers(initDepth);
            OnTransformParentChanged(initDepth);

            hasInitialized = true;
            instanceID = GetInstanceID();
        }

        private void OnEnable()
        {
            if (!hasInitialized)
                return;

            Init();
            //transform.parent?.gameObject.Get<HierarchyObserver>(x => x.AddChild(gameObject));
        }

        public bool wasChange = false;

        public void OnTransformParentChanged()
        {
            // OnTransformParentChanged(0);
            wasChange = true;
        }

        public void Update()
        {
            if (wasChange)
            {
                OnTransformParentChanged(0);
            }
        }

        public void OnTransformParentChanged(int initDepth)
        {
            //oldParent?.gameObject.Get<HierarchyObserver>(x => x.RemoveChild(gameObject));
            //transform.parent?.gameObject.Get<HierarchyObserver>(x => x.AddChild(gameObject));
            var newParents = isDestroying ? new List<Transform>() : transform.GetParents(IncludeInactive);
            var oldParents = parents;

            var newParentsAdded = new List<Transform>();
            var oldParentsRemoved = new List<Transform>();

            bool isNewParent = newParents.Count > 0;
            bool isOldParent = oldParents.Count > 0;

            if ((isOldParent ? oldParents[0] : null) != (isNewParent ? newParents[0] : null))
            {
                if (isOldParent)
                {
                    //is our old first parent one of our new parents
                    for (int i = 0; i < newParents.Count; i++)
                    {
                        if (newParents[i] == oldParents[0])
                        {
                            //if so, traverse back down the tree and call AddChild on those parents--these are our actual new parents, everything else is the same
                            for (int j = i-1; j >= 0; j--)
                            {
                                if (DebugMessages)
                                    Debug.LogFormat("Adding parent named {0} to {1}.", newParents[j].gameObject.name, gameObject.name);
                                newParentsAdded.Add(newParents[j]);
                            }
                            break;
                        }
                    }
                }
                else
                {
                    if (DebugMessages)
                        Debug.LogFormat("Adding {0} new parents to {1}.", oldParents.Count, gameObject.name);
                    newParentsAdded.AddRange(newParents);
                }

                if (isNewParent)
                {
                    //is our new first parent one of our old parents
                    for (int i = 0; i < oldParents.Count; i++)
                    {
                        if (oldParents[i] == newParents[0])
                        {
                            //if so, traverse back down the tree and call RemoveChild on those parents--these are our actual old parents, everything else is the same
                            for (int j = i-1; j >= 0; j--)
                            {
                                if (DebugMessages)
                                    Debug.LogFormat("Removing parent named {0} from {1}.", oldParents[j].gameObject.name, gameObject.name);
                                oldParentsRemoved.Add(oldParents[j]);
                            }
                            break;
                        }
                    }
                }
                else
                {
                    if (DebugMessages)
                        Debug.LogFormat("Removing {0} old parents from {1}.", oldParents.Count, gameObject.name);
                    oldParentsRemoved.AddRange(oldParents);
                }

                if (initDepth <= 1)
                {
                    foreach (var child in GetSelfAndAllChildren())
                    {
                        foreach (var parent in newParentsAdded)
                        {
                            if (DebugMessages)
                                Debug.LogFormat("Added some child {0} to some parent {1}.", child.name, parent.name);
                            parent.gameObject.Get<HierarchyObserver>(x => x.AddSomeChild(child));
                        }
                        foreach (var parent in oldParentsRemoved)
                        {
                            if (DebugMessages)
                                Debug.LogFormat("Removing some child {0} from some parent {1}.", child.name, parent.name);
                            parent.gameObject.Get<HierarchyObserver>(x => x.RemoveSomeChild(child));
                        }
                    }
                }
            }

            if (isOldParent)
                oldParents[0].gameObject.Get<HierarchyObserver>(x => x.RemoveChild(transform));

            if (isNewParent)
                newParents[0].gameObject.Get<HierarchyObserver>(x => x.AddChild(transform));

            parents = newParents;
            wasChange = false;
        }

        /*
        private void Initialize()
        {
            IsRootInitializer = (transform.parent == null || !transform.parent.gameObject.Has<HierarchyObserver>());
            
        }

        bool IsChildOfRootInitializer()
        {
            if (transform.parent != null)
            {
                var observer = transform.parent.gameObject.Get<HierarchyObserver>();
                if (observer != null && observer.IsRootInitializer)
                    return true;
            }
            return false;
        }*/

        private void OnDisable()
        {
            if (!IncludeInactive)
                OnTransformParentChanged();
        }

        void OnDestroy()
        {
            isDestroying = true;
            //transform.parent = null;
            OnTransformParentChanged(0);
        }

        private void OnTransformChildrenChanged()
        {
            TryAddChildObservers();
        }

        List<Transform> GetSelfAndAllChildren()
        {
            var selfAndChildren = new List<Transform>();
            selfAndChildren.AddRange(GetAllChildren());
            selfAndChildren.Add(transform);
            return selfAndChildren;
        }

        public List<Transform> GetAllChildren()
        {
            var allChildren = new List<Transform>();
            foreach (var child in children)
            {
                allChildren.Add(child);
                child.gameObject.Get<HierarchyObserver>(x => allChildren.AddRange(x.GetAllChildren()));
            }
            return allChildren;
        }

        bool TryAddChildObservers(int initDepth = 0)
        {
            bool addedObservers = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                if (!child.Has<HierarchyObserver>())
                {
                    var childObserver = child.AddOrGet<HierarchyObserver>();
                    childObserver.Init(initDepth+1);

                    addedObservers = true;
                }
            }
            return addedObservers;
        }

        public void AddChild(Transform child)
        {
            children.Add(child);

            if (onAddChild != null)
                onAddChild(child);
        }

        public void RemoveChild(Transform child)
        {
            children.Remove(child);

            if (onRemoveChild != null)
                onRemoveChild(child);
        }

        public void AddSomeChild(Transform child)
        {

            if (onAddSomeChild != null)
                onAddSomeChild(child);
        }

        void RemoveSomeChild(Transform child)
        {
            if (onRemoveSomeChild != null)
                onRemoveSomeChild(child);
        }
        /*
        private void OnDrawGizmos()
        {
            var hideFlags = HideFlags.None;
            if (parents.Count > 0)
                hideFlags = HideFlags.HideInInspector;

            this.hideFlags = hideFlags;
        }*/
    }

    public static class HieararchyExtensions
    {
        public static void SetParentImmediate(this Transform source, Transform p, bool worldPositionStays = true)
        {
            source.SetParent(p, worldPositionStays);
            var observer = source.gameObject.Get<HierarchyObserver>();
            if (observer != null)
            {
                observer.OnTransformParentChanged(0);
            }
        }
    }
}