using UnityEngine;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using Altimit.Reflection;
using System.Reflection;
using System.Runtime.InteropServices;
using Altimit.Serialization;

namespace Altimit.UI
{
    public static partial class A
    {

        public static List<KeyValuePair<object, object>> Bindings = new List<KeyValuePair<object, object>>();

        static A ()
        {
        }

        public static bool TryBind(this GameObject go, Type compType, Type propType, object compExp, object prop, object propExp)
        {
            Type delType = typeof(Func<,>).MakeGenericType(compType, typeof(object));
            Type expType = typeof(Expression<>).MakeGenericType(delType);

            var method = typeof(A).GetMethods().Where(x => x.Name == "TryBind" &&
                x.ContainsGenericParameters &&
                x.GetParameters().Length == 4 &&// !x.GetParameters()[1].ParameterType.Equals(typeof(object)) &&
                !x.GetParameters()[2].ParameterType.Equals(propType)).SingleOrDefault();

            MethodInfo genericMethod = method.MakeGenericMethod(compType, propType);
            var parameters = new[] { go, compExp, prop, propExp };
            var result = genericMethod.Invoke(null, parameters);

            return (bool)result;
        }
      
        public static GameObject Bind(this GameObject go, IBound value)
        {
            foreach (var binding in Bindings)
            {
                if (go.TryBind((Type)binding.Key, binding.Value, value))
                    break;
            }
            return go;
        }

        public static bool TryBind(this GameObject go, Type compType, object compExp, IBound value)
        {
            Type delType = typeof(Func<,>).MakeGenericType(compType, typeof(object));
            Type expType = typeof(Expression<>).MakeGenericType(delType);

            var method = typeof(A).GetMethods().Where(x => x.Name == "TryBind" &&
                x.ContainsGenericParameters &&
                x.GetParameters().Length == 3 &&
                x.GetParameters()[2].ParameterType.Equals(typeof(IBound))).SingleOrDefault();

            MethodInfo genericMethod = method.MakeGenericMethod(compType);
            var parameters = new[] { go, compExp, value };
            var result = genericMethod.Invoke(null, parameters);

            return (bool)result;
        }
      
        public static GameObject BindList<T>(this GameObject go, Func<T, GameObject> childGO, Action<Action<object>> callback)
        {
            go.AddOrGet<ListBinder>().Init(typeof(List<T>), ConvertFunc(childGO), callback);
            return go;
        }

        public static GameObject BindList<T>(this GameObject go, Func<T, GameObject> childGO, Func<T, bool> isBindable = null)
        {
            go.AddOrGet<ListBinder>().Init(typeof(List<T>), ConvertFunc(childGO), isBindable != null ? ConvertFunc(isBindable) : null);
            return go;
        }

        public static GameObject Bind<T>(this GameObject go)
        {
            Binder binder;
            return go.Bind<T>(out binder);
        }

        public static GameObject Bind<T>(this GameObject go, out Binder binder)
        {
            if (typeof(T).IsGenericType && (typeof(T).GetGenericTypeDefinition() == typeof(List<>) ||
                typeof(T).GetGenericTypeDefinition() == typeof(BoundList<>)))
            {
                binder = go.AddOrGet<ListBinder>();
            }
            else if (typeof(T).Namespace != "System")
            {
                binder = go.AddOrGet<ClassBinder>();
            } else
            {
                binder = go.AddOrGet<VarBinder>();
            }
            binder.Init(typeof(T));
            return go;
        }

        public static GameObject BindChild<T>(this GameObject go)
        {
            go.AddOrGet<ChildBinder>().Init(typeof(T));
            return go;
        }

        //Bind function instead of two properties using gameObject
        public static GameObject Bind<Prop>(this GameObject go, Action<GameObject, Prop> getAction, Action<GameObject,Prop> setAction)
        {
            return go.BindGet(getAction).Bind(setAction);
        }

        public static Action<GameObject, object> ConvertAction<Prop>(Action<GameObject, Prop> action)
        {
            return new Action<GameObject, object>((x, y) => { action(x, (Prop)y); });
        }

        public static Func<object, D> ConvertFunc<T, D>(Func<T, D> func)
        {
            return new Func<object, D>((x) => { return func((T)x); });
        }

        public static GameObject BindGet<Prop>(this GameObject go, Action<GameObject, Prop> action)
        {
            return go.BindGet(ConvertAction<Prop>(action));
        }
        
        public static GameObject BindChild<Prop>(this GameObject go, Action<GameObject, Prop> action)
        {
            return go.BindChild<Prop>().Bind(ConvertAction<Prop>(action));
        }

        public static GameObject Bind<Prop>(this GameObject go, Action<GameObject, Prop> action)
        {
            return go.Bind<Prop>().Bind(ConvertAction<Prop>(action));
        }

        public static GameObject Bind(this GameObject go, Action<GameObject, object> action)
        {
            return go.Get<Binder>(x => x.BindSet(
                action)
            );
        }

        public static GameObject BindGet(this GameObject go, Action<GameObject, object> action)
        {
            return go.Get<Binder>(x => x.BindGet(
                action)
            );
        }

        //Bind function instead of two properties using component
        public static GameObject Bind<Comp, Prop>(this GameObject go, Action<Comp, Prop> getAction, Action<Comp, Prop> setAction) where Comp : Component where Prop : new()
        {
            go.Bind<Prop>(
                (x, y) => { getAction(x.AddOrGet<Comp>(), y); },
                (x, y) => { setAction(x.AddOrGet<Comp>(), y); }
                );
            return go;
        }
       
        /// <summary>
        /// Search for a method by name and parameter types.  
        /// Unlike GetMethod(), does 'loose' matching on generic
        /// parameter types, and searches base interfaces.
        /// </summary>
        /// <exception cref="AmbiguousMatchException"/>
        public static MethodInfo GetMethodExt(this Type thisType,
                                                string name,
                                                params Type[] parameterTypes)
        {
            return GetMethodExt(thisType,
                                name,
                                BindingFlags.Instance
                                | BindingFlags.Static
                                | BindingFlags.Public
                                | BindingFlags.NonPublic
                                | BindingFlags.FlattenHierarchy,
                                parameterTypes);
        }

        /// <summary>
        /// Search for a method by name, parameter types, and binding flags.  
        /// Unlike GetMethod(), does 'loose' matching on generic
        /// parameter types, and searches base interfaces.
        /// </summary>
        /// <exception cref="AmbiguousMatchException"/>
        public static MethodInfo GetMethodExt(this Type thisType,
                                                string name,
                                                BindingFlags bindingFlags,
                                                params Type[] parameterTypes)
        {
            MethodInfo matchingMethod = null;

            // Check all methods with the specified name, including in base classes
            GetMethodExt(ref matchingMethod, thisType, name, bindingFlags, parameterTypes);

            // If we're searching an interface, we have to manually search base interfaces
            if (matchingMethod == null && thisType.IsInterface)
            {
                foreach (Type interfaceType in thisType.GetInterfaces())
                    GetMethodExt(ref matchingMethod,
                                 interfaceType,
                                 name,
                                 bindingFlags,
                                 parameterTypes);
            }

            return matchingMethod;
        }

        private static void GetMethodExt(ref MethodInfo matchingMethod,
                                            Type type,
                                            string name,
                                            BindingFlags bindingFlags,
                                            params Type[] parameterTypes)
        {
            // Check all methods with the specified name, including in base classes
            foreach (MethodInfo methodInfo in type.GetMember(name,
                                                             MemberTypes.Method,
                                                             bindingFlags))
            {
                // Check that the parameter counts and types match, 
                // with 'loose' matching on generic parameters
                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                if (parameterInfos.Length == parameterTypes.Length)
                {
                    int i = 0;
                    for (; i < parameterInfos.Length; ++i)
                    {
                        if (!parameterInfos[i].ParameterType
                                              .IsSimilarType(parameterTypes[i]))
                            break;
                    }
                    if (i == parameterInfos.Length)
                    {
                        if (matchingMethod == null)
                            matchingMethod = methodInfo;
                        else
                            throw new AmbiguousMatchException(
                                   "More than one matching method found!");
                    }
                }
            }
        }

        /// <summary>
        /// Special type used to match any generic parameter type in GetMethodExt().
        /// </summary>
        public class T
        { }

        /// <summary>
        /// Determines if the two types are either identical, or are both generic 
        /// parameters or generic types with generic parameters in the same
        ///  locations (generic parameters match any other generic paramter,
        /// but NOT concrete types).
        /// </summary>
        private static bool IsSimilarType(this Type thisType, Type type)
        {
            // Ignore any 'ref' types
            if (thisType.IsByRef)
                thisType = thisType.GetElementType();
            if (type.IsByRef)
                type = type.GetElementType();

            // Handle array types
            if (thisType.IsArray && type.IsArray)
                return thisType.GetElementType().IsSimilarType(type.GetElementType());

            // If the types are identical, or they're both generic parameters 
            // or the special 'T' type, treat as a match
            if (thisType == type || ((thisType.IsGenericParameter || thisType == typeof(T))
                                 && (type.IsGenericParameter || type == typeof(T))))
                return true;

            // Handle any generic arguments
            if (thisType.IsGenericType && type.IsGenericType)
            {
                Type[] thisArguments = thisType.GetGenericArguments();
                Type[] arguments = type.GetGenericArguments();
                if (thisArguments.Length == arguments.Length)
                {
                    for (int i = 0; i < thisArguments.Length; ++i)
                    {
                        if (!thisArguments[i].IsSimilarType(arguments[i]))
                            return false;
                    }
                    return true;
                }
            }

            return false;
        }
    }
}