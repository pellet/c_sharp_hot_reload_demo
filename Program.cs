using System;
using System.Reactive.Disposables;
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
    static CompositeDisposable disposables = new();
    
    public static void ClearCache(Type[]? types)
    {
        Console.WriteLine("ClearCache");
        disposables.Dispose();
        disposables = new CompositeDisposable();
    }

    public static void UpdateApplication(Type[]? types)
    {
        // Re-render the list of properties
        Console.WriteLine("UpdateApplication");
        //@@bp
        // disposables.Add(
        //     Observable
        //         .Interval(TimeSpan.FromMilliseconds(1500))
        //         .Scan((x, y) => x+y)
        //         .Subscribe(
        //             onNext: Console.WriteLine));

        Observable
            .Defer(
                () =>
                {
                    var meh = new Meh
                    {
                        BoilerPlateProperty = "original value"
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
            .Select(
                _ => new Meh { BoilerPlateProperty = "blah", BoilerPlateProperty2 = ""})
            .Subscribe();
    }

    record Meh
    {
        public string? BoilerPlateProperty { init; get; }
        public string BoilerPlateProperty2 { init; get; }

        public override string ToString()
        {
            return $"{this.BoilerPlateProperty}, {this.BoilerPlateProperty2}";
        }
    }
}
