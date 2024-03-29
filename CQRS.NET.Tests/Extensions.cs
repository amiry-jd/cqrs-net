using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace CQRS.NET.Autofac {

    public static class Extensions {

        public static void AddRange<T>(this ICollection<T> collection, [NotNull] IEnumerable<T> range) {
            if (range == null) throw new ArgumentNullException(nameof(range));

            foreach (var item in range) collection.Add(item);
        }

        public static IServiceCollection RegisterCqrs(
            this IServiceCollection builder,
            IEnumerable<Assembly>   assemblies,
            Action<CqrsOptions>?    optionBuilder = null) {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            if (assemblies == null || !assemblies.Any())
                throw new ArgumentNullException(nameof(CqrsOptions.AssembliesLoader));

            var asses = assemblies.ToArray();

            var options = new CqrsOptions();
            optionBuilder?.Invoke(options);

            builder.AddTransient<IServiceProvider, DefaultExtendedServiceProvider>();
            builder.AddTransient<IExtendedServiceProvider, DefaultExtendedServiceProvider>();


            builder.AddTransient<IHandlerExecutor, DefaultHandlerExecutor>();

            builder.AddTransient(typeof(IQueryHandler<,>), typeof(GeneralStrategicQueryHandler<,>));

            var assTypes = assemblies.SelectMany(ass => ass.GetTypes()).ToList();

            foreach (var at in assTypes) {
                if (at is { IsGenericType: true, ContainsGenericParameters: false } &&
                    at.GetGenericTypeDefinition() == typeof(IStrategicQueryHandler<,>))
                    builder.AddKeyedTransient(typeof(IQueryHandler<,>), at.FullName, at);
                else
                    builder.AddTransient(typeof(IQueryHandler<,>), at);
            }

            /*


            builder.RegisterAssemblyTypes(asses)
                   .Where(t => !t.IsClosedTypeOf(typeof(IStrategicQueryHandler<,>)))
                   .AsClosedTypesOf(typeof(IQueryHandler<,>));

            builder.RegisterAssemblyTypes(asses)
                   .Where(t => t.IsClosedTypeOf(typeof(IStrategicQueryHandler<,>)))
                   .AsClosedTypesOf(typeof(IQueryHandler<,>), t => t.FullName);

            builder.RegisterAssemblyTypes(asses)
                   .AsClosedTypesOf(typeof(IStrategyDetector<>));

            builder.RegisterAssemblyTypes(asses)
                   .AsClosedTypesOf(typeof(IValidator<>))
                   .AsClosedTypesOf(typeof(IValidator<>), t => t.FullName);

            builder.RegisterGenericDecorator(typeof(QueryValidatorDecorator<,>), typeof(IQueryHandler<,>), dc => {
                var implType = dc.ImplementationType;

                if (implType.GetCustomAttribute(typeof(SkipValidationAttribute)) != null) return false;

                if (!(implType.GetCustomAttribute(typeof(SkipDecoratorAttribute)) is SkipDecoratorAttribute attr))
                    return true;

                return attr.DecoratorTypes.All(t => t != typeof(QueryValidatorDecorator<,>));
            });

            foreach (var dec in options.QuerySide.GenericDecorators) {
                builder.RegisterGenericDecorator(dec.DecoratorType, dec.ServiceType, dc => {
                    var implType = dc.ImplementationType;

                    if (!(implType.GetCustomAttribute(typeof(SkipDecoratorAttribute)) is SkipDecoratorAttribute attr))
                        return true;

                    return attr.DecoratorTypes.All(t => t != dec.DecoratorType)
                     && dec.Condition == null || dec.Condition!.Invoke(dec.DecoratorType, dec.ServiceType, implType);
                });
            }

            */

            return builder;
        }

    }

}