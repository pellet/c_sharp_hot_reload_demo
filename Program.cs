using System;
using System.Reflection.Metadata;
using System.Reactive.Linq;

[assembly: MetadataUpdateHandler(typeof(HotReloadManager))]

internal static class Program
{
    static void Main()
    {
        Console.ReadLine();
    }
}

internal static class HotReloadManager
{
    public static void ClearCache(Type[]? types)
    {
        Console.WriteLine("ClearCache");
    }

    public static void UpdateApplication(Type[]? types)
    {
        // Re-render the list of properties
        Console.WriteLine("UpdateApplication");
        //@@bp
        Observable
            .Range(start: 0, count: 20)
            .Aggregate((x, y) => x+y)
            .Subscribe(
                onNext: Console.WriteLine);
    }
}
