using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace System
{
    public static class ObjectExtensions
    {
        #region -- SetProperty && GetProperty --
        /// <summary>
        /// 通过反射的方式设置对象的属性
        /// 通常在该属性的 setter 为 private 时使用
        /// 通常在单元测试时使用，提高单元测试的灵活性，在产品代码中不建议使用此属性
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static T SetProperty<T>(this T instance, string propertyName, object value)
        {
            var pi = GetPropertyAccess(instance, propertyName);
            
            if (!pi.CanWrite)
                throw new Exception($"The property '{propertyName}' on the instance of type '{instance.GetType()}' does not have a setter.");
            
            if (value != null && !value.GetType().IsAssignableFrom(pi.PropertyType))
                value = To(value, pi.PropertyType);
            
            pi.SetValue(instance, value, new object[0]);
            
            return instance;
        }

        /// <summary>
        /// 通过反射的方式设置对象的属性
        /// 通常在该属性的 setter 为 private 时使用
        /// 通常在单元测试时使用，提高单元测试的灵活性，在产品代码中不建议使用此属性
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        public static T SetProperty<T, TProperty>(this T instance, Expression<Func<T, TProperty>> propertyExpression,
            TProperty value)
        {
            var pi = instance.GetPropertyAccess(propertyExpression);
            
            pi.SetValue(instance, value, new object[0]);
            
            return instance;
        }

        public static object GetProperty<T>(this T instance, string propertyName)
        {
            var pi = GetPropertyAccess(instance, propertyName);
            
            if (!pi.CanRead)
                throw new Exception($"The property '{propertyName}' on the instance of type '{instance.GetType()}' does not have a getter.");

            return pi.GetValue(instance, new object[0]);
        }
        
        public static TProperty GetProperty<T, TProperty>(this T instance, Expression<Func<T, TProperty>> propertyExpression)
        {
            var pi = instance.GetPropertyAccess(propertyExpression);
            
            var value = pi.GetValue(instance, new object[0]);
            
            return (TProperty) value;
        }
        #endregion
        
        #region -- GetPropertyAccess --
        public static PropertyInfo GetPropertyAccess<T, TProperty>(this T instance,
            Expression<Func<T, TProperty>> propertyExpression)
        {
            return propertyExpression.GetPropertyAccess();
        }

        public static PropertyInfo GetPropertyAccess<T>(this T instance, string propertyName)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            var instanceType = instance.GetType();
            var pi = instanceType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            
            if (pi == null)
                throw new Exception($"No property '{propertyName}' found on the instance of type '{instanceType}'.");

            return pi;
        }
        #endregion
        
        #region -- To --
        /// <summary> 
        /// 将值转换为目标类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="destinationType">要将值转换为的类型</param>
        /// <returns>转换后的值</returns>
        public static object To(this object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }
        
        /// <summary>
        /// 将值转换为目标类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="destinationType">要将值转换为的类型</param>
        /// <param name="culture">Culture</param>
        /// <returns>转换后的值</returns>
        public static object To(this object value, Type destinationType, CultureInfo culture)
        {
            if (value == null) 
                return null;

            var sourceType = value.GetType();

            var destinationConverter = TypeDescriptor.GetConverter(destinationType);
            if (destinationConverter.CanConvertFrom(value.GetType()))
                return destinationConverter.ConvertFrom(null, culture, value);

            var sourceConverter = TypeDescriptor.GetConverter(sourceType);
            if (sourceConverter.CanConvertTo(destinationType))
                return sourceConverter.ConvertTo(null, culture, value, destinationType);

            if (destinationType.IsEnum && value is int)
                return Enum.ToObject(destinationType, (int)value);

            if (!destinationType.IsInstanceOfType(value))
                return Convert.ChangeType(value, destinationType, culture);

            return value;
        }
        #endregion
    }
}