using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Altimit
{
    public static class HierarchyExtensions
    {
        public static bool InheritsFrom(this GameObject source, GameObject target)
        {
            return source.transform.InheritsFrom(target.transform);
        }

        public static bool InheritsFrom(this Transform source, Transform target)
        {
            var parent = source;
            while (parent.parent != null)
            {
                parent = parent.parent;
                if (target == parent)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<Transform> GetParents(this Transform transform, bool includeInactive = true)
        {
            var parents = new List<Transform>();
            var parent = transform;

            if (!parent.gameObject.activeInHierarchy)
                return parents;

            while (parent.parent != null)
            {
                parent = parent.parent;
                parents.Add(parent);
            }
            return parents;
        }
    }
}