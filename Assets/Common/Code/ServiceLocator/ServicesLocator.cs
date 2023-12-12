using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TatmanGames.Common.ServiceLocator
{
    /// <summary>
    /// Standard service locator.  To use and reference the global service locator
    /// use the type GlobalServicesLocator
    /// </summary>
    public class ServicesLocator
    {
        private Dictionary<string, object> services = new Dictionary<string, object>();
        
        /// <summary>
        /// TODO: should T be typed to class or a base type
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ServiceLocatorException"></exception>
        public void AddService<T>(T instance)
        {
            string name = GetServiceName<T>();
            if (true == services.ContainsKey(name))
                throw new ServiceLocatorException($"{name} exists");

            services.Add(name, instance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        public void AddReplaceService<T>(T instance)
        {
            string name = GetServiceName<T>();
            if (true == services.ContainsKey(name))
            {
                services[name] = instance;
                return;
            }
            
            services.Add(name, instance);
        }

        /// <summary>
        /// TODO: should T be typed to class or a base type
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ServiceLocatorException"></exception>
        public T GetServiceByName<T>(string name)
        {
            if (false == services.ContainsKey(name))
                throw new ServiceLocatorException($"{name} isn't found");

            return (T) services[name];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            string name = GetServiceName<T>();
            return GetServiceByName<T>(name);
        }

        /// <summary>
        /// GetService with exception handler trap.  Will return null if
        /// service is not found
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T TryGetService<T>()
        {
            try
            {
                return GetService<T>();
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        public List<string> ListAllServices()
        {
            return new List<string>(services.Keys.ToArray());
        }

        private string GetServiceName<T>()
        {
            return typeof(T).FullName;
        }
    }
}