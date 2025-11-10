using System;
using System.Collections.Generic;
using UnityEngine;

namespace DicePoker
{
    /// <summary>
    /// Lightweight service locator pattern implementation.
    /// Provides global access to services without tight coupling.
    /// </summary>
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        /// <summary>
        /// Registers a service instance for the given type.
        /// If service already exists, it will be replaced.
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <param name="service">Service instance</param>
        public static void Register<T>(T service) where T : class
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            Type type = typeof(T);
            _services[type] = service;
        }

        /// <summary>
        /// Gets a registered service of type T.
        /// Throws exception if service is not registered.
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>Service instance</returns>
        /// <exception cref="InvalidOperationException">Service not registered</exception>
        public static T Get<T>() where T : class
        {
            Type type = typeof(T);

            if (!_services.ContainsKey(type))
            {
                throw new InvalidOperationException(
                    $"Service of type {type.Name} is not registered. " +
                    $"Call ServiceLocator.Register<{type.Name}>() first.");
            }

            return _services[type] as T;
        }

        /// <summary>
        /// Tries to get a registered service.
        /// Returns false if service is not registered.
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <param name="service">Output service instance</param>
        /// <returns>True if service was found</returns>
        public static bool TryGet<T>(out T service) where T : class
        {
            Type type = typeof(T);

            if (_services.TryGetValue(type, out object serviceTemp))
            {
                service = serviceTemp as T;
                return true;
            }

            service = null;
            return false;
        }

        /// <summary>
        /// Checks if a service of type T is registered.
        /// </summary>
        /// <returns>True if registered</returns>
        public static bool IsRegistered<Type>() where Type : class
        {
            return _services.ContainsKey(typeof(Type));
        }

        /// <summary>
        /// Removes all registered services.
        /// Useful for testing and scene transitions.
        /// </summary>
        public static void Clear()
        {
            _services.Clear();
        }

        /// <summary>
        /// Unregisters a specific service type.
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        public static void Unregister<T>() where T : class
        {
            Type type = typeof(T);
            _services.Remove(type);
        }
    }
}
