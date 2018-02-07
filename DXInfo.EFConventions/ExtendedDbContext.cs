using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DXInfo.EFConventions.Reflection;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DXInfo.EFConventions
{
  public abstract class ExtendedDbContext : DbContext
  {
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      // call derived class to add conventions
      AddConventions();
      // now process conventions
      ProcessAddedConventions(modelBuilder);
    }

    /// <summary>
    /// Force implementation via astract class
    /// </summary>
    protected abstract void AddConventions();

    // conventsions saved here
    private List<IConvention> conventions = new List<IConvention>();

    //reflrecion data about DbContext, its sets, properties and attributes
    private static Dictionary<string, List<DbSetMetadata>> dbSetMetadata =
        new Dictionary<string, List<DbSetMetadata>>();

    private static object locker = new object();

    /// <summary>
    /// Add one convention
    /// </summary>
    /// <param name="convention">Convention to add</param>
    protected void AddConvention(IConvention convention)
    {
      conventions.Add(convention);
    }

    /// <summary>
    /// Process conventions
    /// </summary>
    /// <param name="modelBuilder">Model builder</param>
    protected virtual void ProcessAddedConventions(DbModelBuilder modelBuilder)
    {
      if (conventions.Count > 0)
      {
        // poulate reflection data
        PopulateSetMetadata();
        // run through all global added conventions
        conventions.ForEach(convention =>
        {
          if (convention is IGlobalConvention)
          {
            ProcessGlobalConvention(modelBuilder, convention as IGlobalConvention);
          }
        });
        // run through attribute based conventions
        conventions.ForEach(convention =>
        {
          if (convention is IAttributeConvention)
          {
            ProcessAttributeBasedConvention(modelBuilder, convention as IAttributeConvention);
          }
        });
      }
    }


    /// <summary>
    /// Process global conventions
    /// </summary>
    /// <param name="modelBuilder">Model builder</param>
    /// <param name="convention">One global convention to process</param>
    private void ProcessGlobalConvention(DbModelBuilder modelBuilder, IGlobalConvention convention)
    {
      var setMetadata = dbSetMetadata[this.GetType().AssemblyQualifiedName];
      // run through DbSets in current context
      setMetadata.ForEach(set =>
      {
        //run through properties in each DbSet<T> for class of type T
        set.DbSetItemProperties.ToList().ForEach(prop =>
        {
          // get type of property that matches current convention
          List<Type> targetTypes = GetMatchingTypeForConfiguration(convention.PropertyConfigurationType);
          // make sure this type matched property type
          if (targetTypes.Contains(prop.PropertyInfo.PropertyType))
          {
            // Get entity method in ModuleBuilder
            // we are trying to get to the point of expressing the following
            //modelBuilder.Entity<Person>().Property(a => a.Name).IsMaxLength() for example
            var setMethod = modelBuilder.GetType()
                .GetMethod("Entity", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            // one we have Entity method, we have to add generic parameters to get to Entity<T>
            var genericSetMethod = setMethod.MakeGenericMethod(new Type[] { set.ItemType });
            // Get an instance of EntityTypeConfiguration<T>
            var entityInstance = genericSetMethod.Invoke(modelBuilder, null);

            //Get methods of EntityTypeConfiguration<T>
            var propertyAccessors = entityInstance.GetType().GetMethods(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();

            // we are looking for Property method that returns PropertyConfiguration
            // that is used in current convention
            // we are looking for Property method that returns PropertyConfiguration
            // that is used in current convention
            bool isNullableProperty = false;
            // check for nullable property
            if (prop.PropertyInfo.PropertyType.IsGenericType)
            {
              var arguments = prop.PropertyInfo.PropertyType.GetGenericArguments();
              if (arguments.Length == 1 && prop.PropertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
              {
                isNullableProperty = true;
              }
            }
            MethodInfo propertyMethod = null;
            propertyAccessors.Where(oneProperty =>
                  oneProperty.ReturnType == convention.PropertyConfigurationType).ToList().ForEach(one =>
                  {
                    if (isNullableProperty)
                    {
                      // nullable property will have generic type
                      if (one.GetParameters()[0].ParameterType.GetGenericArguments()[0].GetGenericArguments()[1].IsGenericType)
                      {
                        propertyMethod = one;
                      }
                    }
                    else
                    {
                      // non nullable property is non-generic type
                      if (!one.GetParameters()[0].ParameterType.GetGenericArguments()[0].GetGenericArguments()[1].IsGenericType)
                      {
                        propertyMethod = one;
                      }
                    }
                  });

            //Get method handle in order to build the expression 
            // example: (a => a.Name)
            var expressionGetMethod = GetPropertyExpressionMethodHandle();

            //Create lamda expression by making expression method that takes two generic parameters
            // one for class, the other for property type
            var genericExpressionMethod = expressionGetMethod
                .MakeGenericMethod(new Type[] { prop.PropertyInfo.DeclaringType, prop.PropertyInfo.PropertyType });

            //FInally, get lamda expression it self
            // example: (a => a.Name)
            var propertyExpression = genericExpressionMethod.Invoke(null, new object[] { prop.PropertyInfo });

            //Not get an instance of PrimitivePropertyConfiguration by infoking EntityTypeConfiguration<T>'s 
            // Property() method
            var config = propertyMethod
                .Invoke(entityInstance, new object[] { propertyExpression }) as PrimitivePropertyConfiguration;

            //Finally, pass this configuration and attribute into the convention
            convention.ApplyConfiguration(prop.PropertyInfo, config);
          }
        });
      });
    }

    /// <summary>
    /// Determine what property type should be used for a specific convention
    /// </summary>
    /// <param name="propertyConfigurationType">
    /// Type of PrimitivePropertyConfiguration to process
    /// </param>
    /// <returns>
    /// Property types that should be used with current convention
    /// </returns>
    private List<Type> GetMatchingTypeForConfiguration(Type propertyConfigurationType)
    {
      List<Type> returnValue = new List<Type>();
      if (propertyConfigurationType == typeof(DecimalPropertyConfiguration))
      {
        returnValue.Add(typeof(decimal));
        returnValue.Add(typeof(decimal?));
      }
      if (propertyConfigurationType == typeof(StringPropertyConfiguration))
      {
        returnValue.Add(typeof(string));
      }
      if (propertyConfigurationType == typeof(DateTimePropertyConfiguration))
      {
        returnValue.Add(typeof(DateTime));
        returnValue.Add(typeof(DateTime?));
      }
      if (propertyConfigurationType == typeof(BinaryPropertyConfiguration))
      {
        returnValue.Add(typeof(byte[]));
      }
      else
      {
        returnValue.Add(typeof(object));
      }
      return returnValue;
    }


    /// <summary>
    /// Process attribute based conventions
    /// </summary>
    /// <param name="modelBuilder">Model builder</param>
    /// <param name="convention">One attribute convention to process</param>
    private void ProcessAttributeBasedConvention(DbModelBuilder modelBuilder, IAttributeConvention convention)
    {
      var setMetadata = dbSetMetadata[this.GetType().AssemblyQualifiedName];
      // run through DbSets in current context
      setMetadata.ForEach(set =>
      {
        //run through properties in each DbSet<T> for class of type T
        set.DbSetItemProperties.ToList().ForEach(prop =>
        {
          // get attribute that matches convention
          var data = prop.DbSetItemAttributes
              .Where(attr => attr.Attribute.GetType() == convention.AttributeType).FirstOrDefault();

          // this class's property has the attribute
          if (data != null)
          {
            // Get entity method in ModuleBuilder
            // we are trying to get to the point of expressing the following
            //modelBuilder.Entity<Person>().Property(a => a.Name).IsMaxLength() for example
            var setMethod = modelBuilder.GetType()
                .GetMethod("Entity", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            // one we have Entity method, we have to add generic parameters to get to Entity<T>
            var genericSetMethod = setMethod.MakeGenericMethod(new Type[] { set.ItemType });
            // Get an instance of EntityTypeConfiguration<T>
            var entityInstance = genericSetMethod.Invoke(modelBuilder, null);

            //Get methods of EntityTypeConfiguration<T>
            var propertyAccessors = entityInstance.GetType().GetMethods(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();

            // we are looking for Property method that returns PropertyConfiguration
            // that is used in current convention
            bool isNullableProperty = false;
            // check for nullable property
            if (prop.PropertyInfo.PropertyType.IsGenericType)
            {
              var arguments = prop.PropertyInfo.PropertyType.GetGenericArguments();
              if (arguments.Length == 1 && prop.PropertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
              {
                isNullableProperty = true;
              }
            }
            MethodInfo propertyMethod = null;
            propertyAccessors.Where(oneProperty =>
                  oneProperty.ReturnType == convention.PropertyConfigurationType).ToList().ForEach(one =>
                  {
                    if (isNullableProperty)
                    {
                      // nullable property will have generic type
                      if (one.GetParameters()[0].ParameterType.GetGenericArguments()[0].GetGenericArguments()[1].IsGenericType)
                      {
                        propertyMethod = one;
                      }
                    }
                    else
                    {
                      // non nullable property is non-generic type
                      if (!one.GetParameters()[0].ParameterType.GetGenericArguments()[0].GetGenericArguments()[1].IsGenericType)
                      {
                        propertyMethod = one;
                      }
                    }
                  });


            //Get method handle in order to build the expression 
            // example: (a => a.Name)
            var expressionGetMethod = GetPropertyExpressionMethodHandle();

            //Create lamda expression by making expression method that takes two generic parameters
            // one for class, the other for property type
            var genericExpressionMethod = expressionGetMethod
                .MakeGenericMethod(new Type[] { prop.PropertyInfo.DeclaringType, prop.PropertyInfo.PropertyType });

            //FInally, get lamda expression it self
            // example: (a => a.Name)
            var propertyExpression = genericExpressionMethod.Invoke(null, new object[] { prop.PropertyInfo });

            //Not get an instance of PrimitivePropertyConfiguration by infoking EntityTypeConfiguration<T>'s 
            // Property() method
            var config = propertyMethod
                .Invoke(entityInstance, new object[] { propertyExpression }) as PrimitivePropertyConfiguration;

            //Finally, pass this configuration and attribute into the convention
            convention.ApplyConfiguration(prop.PropertyInfo, config, data.Attribute);
          }

        });
      });
    }

    /// <summary>
    /// Locate member info handle for GetPropertyExpression method by iterating through
    /// class hierarchy
    /// </summary>
    /// <returns>MemberInfo handle for GetPropertyExpression method</returns>
    private MethodInfo GetPropertyExpressionMethodHandle()
    {
      MethodInfo returnValue = null;
      Type currentType = this.GetType();
      while (returnValue == null)
      {
        returnValue = currentType
                        .GetMethod("GetPropertyExpression",
                        BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Static);
        if (returnValue == null)
        {
          currentType = currentType.BaseType;
          if (currentType == null)
          {
            break;
          }
        }
      }
      return returnValue;
    }

    /// <summary>
    /// Create Expression that can access property on a class.  You would typically write it as 
    /// (p=>p.Name)
    /// In our case we are using Expression to build the same expression
    /// </summary>
    /// <typeparam name="TClass">Class type that is owning the property in question</typeparam>
    /// <typeparam name="TProperty">Property type</typeparam>
    /// <param name="property">PropertyInfo object for property in question</param>
    /// <returns>Expression that returns the property, such as (p=>p.Name)</returns>
    private static Expression<Func<TClass, TProperty>> GetPropertyExpression<TClass, TProperty>(PropertyInfo property)
    {
      //  Create {p=> portion of the Epxression in example (p=>p.Name)
      var objectExpression = Expression.Parameter(property.DeclaringType, "param");
      // create property expression - .Name for example
      var propertyExpression = Expression.Property(objectExpression, property);
      //Create lambda expression from two parts
      var returnValue = Expression.Lambda<Func<TClass, TProperty>>(propertyExpression, objectExpression);
      return returnValue;
    }

    /// <summary>
    /// RUn through DbContnxt sets and save reflection data in a dictionary
    /// </summary>
    private void PopulateSetMetadata()
    {
      if (!dbSetMetadata.ContainsKey(this.GetType().AssemblyQualifiedName))
      {
        lock (locker)
        {
          if (!dbSetMetadata.ContainsKey(this.GetType().AssemblyQualifiedName))
          {
            var props = this.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).ToList();
            List<DbSetMetadata> sets = new List<DbSetMetadata>();
            props.ForEach(one =>
            {
              //Filter out db sets
              if (one.PropertyType.IsGenericType &&
                  (one.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) ||
                  one.PropertyType.GetGenericTypeDefinition() == typeof(IDbSet<>)))
              {
                sets.Add(new DbSetMetadata(one.PropertyType.GetGenericArguments().First(), one));
              }
            });
            // add this context to diutionary
            dbSetMetadata.Add(this.GetType().AssemblyQualifiedName, sets);
          }
        }
      }
    }
  }
}
