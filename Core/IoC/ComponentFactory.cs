namespace SignalRKit.Core.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StructureMap;

    public static class ComponentFactory
    {
        public static IContainer Container { get; private set; }

        /// <summary>
        /// Initialize ComponentFactory with a new StructureMap container
        /// </summary>
        public static void Initialize()
        {
            Container = new Container(x =>
            {
                x.Scan(s =>
                {
                    s.Assembly("SignalRKit.Core");
                    s.WithDefaultConventions();
                    s.LookForRegistries();
                });
            });
        }

        public static Object TryGetInstance(Type type)
        {
            return Container.TryGetInstance(type);
        }

        public static T GetInstance<T>()
        {
            return Container.GetInstance<T>();
        }

        public static Object GetInstance(Type t)
        {
            return Container.GetInstance(t);
        }

        public static IList<Object> GetAllInstances(Type type)
        {
            var list = Container.GetAllInstances(type);
            return list.Cast<Object>().ToList();
        }

        public static void Configure<TClass>()
        {
            Container.Configure(x =>
            {
                x.For<TClass>().Use<TClass>();
            });
        }
    }
}