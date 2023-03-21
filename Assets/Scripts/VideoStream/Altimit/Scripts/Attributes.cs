using System;

namespace Altimit.Serialization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class KeyConstructorAttribute : Attribute
    {
        public int[] IDs;

        public KeyConstructorAttribute(params int[] ids)
        {
            this.IDs = ids;
        }
    }
}