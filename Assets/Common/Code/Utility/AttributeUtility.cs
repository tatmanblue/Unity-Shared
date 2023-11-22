using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TatmanGames.Common.Utility
{
    public class AttributeUtility
    {
        /// <summary>
        /// looks up all types in AppDomain.CurrentDomain of Type attribute.
        /// </summary>
        /// <param name="attribute">Expected to be an attribute derived type</param>
        /// <returns></returns>
        public static List<Type> FindByAttributeType(Type attribute)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> found = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    // Check if the type has the AchievementLogicAttribute applied
                    if (type.GetCustomAttributes(attribute, true).Length > 0)
                    {
                        found.Add(type);
                    }
                }
            }            
            
            return found;
        }

        /// <summary>
        /// For all implementations found to have Type attribute applied
        /// create an instance, returning list of all found and allocated
        /// </summary>
        /// <param name="attribute"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> AllocateAll<T>(Type attribute)
        {
            List<T> instances = new List<T>();
            List<Type> found = AttributeUtility.FindByAttributeType(attribute);

            foreach (Type impl in found)
            {
                T allocated = (T) Activator.CreateInstance(impl);
                // if this fails to create an instance, what should we do?
                instances.Add(allocated);
            }
            
            return instances;
        }
    }
    
}