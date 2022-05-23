using System;
using System.Reflection;
using UnityObject = UnityEngine.Object;
using UnityEngine;

namespace Altimit.Reflection
{
	[Serializable]
	public class UnityVariable : UnityMember
	{
		private enum SourceType
		{
			Unknown,
			Field,
			Property
		}

		private SourceType sourceType = SourceType.Unknown;

		/// <summary>
		/// The underlying reflected field, or null if the variable is a property.
		/// </summary>
		public FieldInfo fieldInfo { get; private set; }

		/// <summary>
		/// The underlying property field, or null if the variable is a field.
		/// </summary>
		public PropertyInfo propertyInfo { get; private set; }

		#region Constructors

		public UnityVariable() { }
		public UnityVariable(string name) : base(name) { }
		public UnityVariable(string name, UnityObject target) : base(name, target) { }
		public UnityVariable(string component, string name) : base(component, name) { }
		public UnityVariable(string component, string name, UnityObject target) : base(component, name, target) { }

		#endregion

		/// <inheritdoc />
		public override void Reflect()
		{
			EnsureAssigned();
			EnsureTargeted();

			fieldInfo = null;
			propertyInfo = null;
			sourceType = SourceType.Unknown;

			var memberInfo = UnityMemberHelper.ReflectVariable(reflectionTarget, name);
			fieldInfo = memberInfo as FieldInfo;
			propertyInfo = memberInfo as PropertyInfo;

			if (fieldInfo != null)
			{
				sourceType = SourceType.Field;
			}
			else if (propertyInfo != null)
			{
				sourceType = SourceType.Property;
			}

			isReflected = true;
		}

		/// <summary>
		/// Retrieves the value of the variable.
		/// </summary>
		public object Get()
		{
			EnsureReflected();

            switch (sourceType)
			{
				case SourceType.Field: return fieldInfo.GetValue(reflectionTarget);
				case SourceType.Property: return propertyInfo.GetValue(reflectionTarget, null);
				default: throw new UnityReflectionException();
			}

          
        }

        /// <summary>
        /// Retrieves the value of the variable casted to the specified type.
        /// </summary>
        public T Get<T>()
		{
			return (T)Get();
		}

		/// <summary>
		/// Assigns a new value to the variable.
		/// </summary>
		public void Set(object value)
		{
            //provides a conversion from enum to int
            if (value != null && value.GetType().IsEnum && type == typeof(int))
            {
                Array values = Enum.GetValues(value.GetType());
                int num = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    if (values.GetValue(i) == value)
                    {
                        num = i;
                    }
                }
                value = num;
            }

            EnsureReflected();
            /*
            if (propertyInfo != null)
            {
                Debug.Log(propertyInfo.Name);
                Debug.Log(reflectionTarget.GetType());
            }
            */
            switch (sourceType)
			{
				case SourceType.Field: fieldInfo.SetValue(reflectionTarget, value); break;
				case SourceType.Property:
                    if (propertyInfo.GetGetMethod() == null) break;
                    propertyInfo.SetValue(reflectionTarget, value, null); break;
				default: throw new UnityReflectionException();
			}
          

        }

        /// <summary>
        /// The type of the reflected field or property.
        /// </summary>
        Type type
		{
			get
			{
				EnsureReflected();

				switch (sourceType)
				{
					case SourceType.Field: return fieldInfo.FieldType;
					case SourceType.Property: return propertyInfo.PropertyType;
					default: throw new UnityReflectionException();
				}
			}
		}

        public Type GetVariableType()
        {
            return type;
        }

		public override bool Corresponds(UnityMember other)
		{
			return other is UnityVariable && base.Corresponds(other);
		}

        object oldValue;
        public virtual bool GetChanged()
        {
            object value = this.Get();
            bool hasChanged = (value != oldValue);
            return hasChanged;
        }

        public virtual void SetChanged()
        {
            oldValue = this.Get();
        }
    }
}