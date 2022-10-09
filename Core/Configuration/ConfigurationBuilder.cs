using Core.UserZone;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Configuration
{
    public sealed class ConfigurationBuilder
    {
        private Dictionary<Type, Type> configuration = new Dictionary<Type, Type>();
        private List<ConfigurationModule> modules = new List<ConfigurationModule>();

        public ConfigurationBuilder Add<T, V>() where T : notnull where V : notnull
        {
            var parentType = typeof(T);
            var childType = typeof(V);

            if (configuration.ContainsKey(parentType))
                throw new ArgumentException($"Type {nameof(T)} is already set");

            if (!childType.IsAssignableTo(parentType))
                throw new ArgumentException($"Type {nameof(V)} isn't assignable to {nameof(T)}");

            configuration.Add(typeof(T), typeof(V));

            return this;
        }

        public ConfigurationBuilder AddModule<T>() where T : ConfigurationModule, new()
        {
            modules.Add(new T());
            return this;
        }

        public IConfiguration Build()
        {
            for(int i = 0; i < modules.Count; i++)
               modules[i].OnBuild(this);

            Dictionary<Type, object> objects = new Dictionary<Type, object>();

            foreach (var typePair in configuration)
            {
                if (objects.ContainsKey(typePair.Key))
                    continue;
                BuildObject(typePair.Key, objects);
            }

            return new Configuration(objects);
        }

        private void BuildObject(Type baseType, Dictionary<Type, object> objects)
        {
            var realType = configuration[baseType];

            var paramlessConstructor = realType.GetConstructor(Type.EmptyTypes);
            if (paramlessConstructor != null)
            {
                objects.Add(baseType, paramlessConstructor.Invoke(null));
                return;
            }

            ConstructorInfo? minimalConstructor = null;
            var constructors = realType.GetConstructors();
            int minParameterCount = int.MaxValue;
            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();

                bool configurationContainsAllParams = true;
                foreach (var parameter in parameters)
                {
                    if (!configuration.ContainsKey(parameter.ParameterType))
                    {
                        configurationContainsAllParams = false;
                        break;
                    }
                }

                if (!configurationContainsAllParams)
                    break;

                if (parameters.Length < minParameterCount)
                {
                    minParameterCount = parameters.Length;
                    minimalConstructor = constructor;
                }
            }

            if (minimalConstructor == null)
                throw new NotSupportedException(realType.Name);

            var minimalConstructorParameterTypes = minimalConstructor.GetParameters().Select(p => p.ParameterType).ToArray();
            foreach (var parameterType in minimalConstructorParameterTypes)
            {
               if(!objects.ContainsKey(parameterType))
                  objects.Add(parameterType, null);

               if (objects[parameterType] == null)
                  BuildObject(parameterType, objects);
            }

            objects[baseType] = minimalConstructor.Invoke(objects.Where(pair => minimalConstructorParameterTypes.Contains(pair.Key)).Select(pair => pair.Value).ToArray());
        }
    }
}