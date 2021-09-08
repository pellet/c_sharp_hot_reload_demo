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
            .Scan((x, y) => x+y)
            .Subscribe(
                onNext: Console.WriteLine);

        
        Observable
            .Defer(
                () =>
                {
                    var meh = new Meh
                    {
                        BoilerPlateProperty = "meh"
                    };
                    return Observable.Return(meh);
                })
            .Do(Console.WriteLine)
            .Select(
                meh =>
                {
                    var meh2 = meh with { BoilerPlateProperty2 = "new value" };
                    return (meh, meh2);
                })
            .Do(t => Console.WriteLine(t))
            .Subscribe();
    }

    record Meh
    {
        public string BoilerPlateProperty { init; get; }
        public string BoilerPlateProperty2 { init; get; }

        public override string ToString()
        {
            return $"{BoilerPlateProperty}, {BoilerPlateProperty2}";
        }
    }
}
