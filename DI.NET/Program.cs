// See https://aka.ms/new-console-template for more information

using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace DI.NET;

public interface IOpenGeneric<T> {

}

public class Concrete1 : IOpenGeneric<int> {

}

public class Concrete2 : IOpenGeneric<string> {

}

public struct BatchRegistrar {

    private readonly IServiceCollection _sc;
    private readonly Type[]             _targetTypes;
    private  ServiceLifetime    _lifetime;
    private          object?            _serviceKey;

    public BatchRegistrar(IServiceCollection sc, params Type[] targetTypes) {
        _sc               = sc;
        _targetTypes = targetTypes;
    }

    public BatchRegistrar WithKey(object serviceKey) {
        _serviceKey = serviceKey;
        return this;
    }

    public BatchRegistrar AsTransient() {
        _lifetime = ServiceLifetime.Transient;
        return this;
    }

    public BatchRegistrar AsScoped() {
        _lifetime = ServiceLifetime.Scoped;
        return this;
    }

    public BatchRegistrar AsSingleton() {
        _lifetime = ServiceLifetime.Singleton;
        return this;
    }

    public IServiceCollection AddClosedTypesOf(Type openGenericType) {
        foreach(var t in _targetTypes)
            if (t.IsClosedTypeOf(openGenericType)) {
                var iiiii = t.GetInterfaces().Where(i => i.GetGenericTypeDefinition() == openGenericType);
                
            }
    }

}

public static class Extensions {

    public static IEnumerable<Type> GetAssignableFromTypes(this Type type) {
        var interfaces = type.GetInterfaces();
        var parents    = GetInheritanceHierarchy(type);
        return interfaces.Concat(parents);
    }

    public static IEnumerable<Type> GetInheritanceHierarchy(this Type type) {
        var   list = new List<Type>();
        var t    = type;
        do {
            list.Add(t);
            t = t.BaseType;
        } while (t != null);
        return list;
    }

    // The credits completely goes for Jon Skeet at https://stackoverflow.com/a/7889272  
    public static Type[] GetLoadableTypes(this Assembly assembly) {
        // TODO: Argument validation
        try {
            return assembly.GetTypes();
        } catch (ReflectionTypeLoadException e) {
            return e.Types.Where(t => t != null).ToArray()!;
        }
    }

    public static Type[] GetLoadableTypes(this IEnumerable<Assembly> assemblies) {
        return assemblies.SelectMany(GetLoadableTypes).ToArray();
    }
    
    public static BatchRegistrar ScanAssemblies(this IServiceCollection sc, params Assembly[] assemblies) {
        return new BatchRegistrar(sc, assemblies.GetLoadableTypes());
    }

    public static BatchRegistrar ScanAssemblies(this IServiceCollection sc, params Type[] markerTypes) {
        return new BatchRegistrar(sc, markerTypes.Select(t => t.Assembly).GetLoadableTypes());
    }

    public static BatchRegistrar ScanTypes(this IServiceCollection sc, params Type[] targetTypes) {
        return new BatchRegistrar(sc, targetTypes);
    }

    public static bool IsClosedTypeOf(this Type type, Type openGenericType) {
        return type is { IsGenericType: true, ContainsGenericParameters: false } &&
               type.GetGenericTypeDefinition() == openGenericType;
    }

}

public static class Program {

    public static void Main() {
        var sc = new ServiceCollection();

        sc.AddTransient(typeof(IOpenGeneric<int>), typeof(Concrete1));
        sc.AddTransient(typeof(IOpenGeneric<string>), typeof(Concrete2));

        var sp = sc.BuildServiceProvider();

        var c1 = sp.GetService<IOpenGeneric<int>>();
        var c2 = sp.GetService<IOpenGeneric<string>>();

        Console.ReadLine();
    }

}