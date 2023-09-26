using Tests.RunLogic.Attributes;

namespace Tests.CSharp.Homework3;

public class SingleInitializationSingletonTests
{
    [Homework(Homeworks.HomeWork3)]
    public void DefaultInitialization_ReturnsSingleInstance()
    {
        SingleInitializationSingleton.Reset();
        SingleInitializationSingleton? i1 = null;
        var elapsed = StopWatcher.Stopwatch(() => { i1 = SingleInitializationSingleton.Instance; });

        var i2 = SingleInitializationSingleton.Instance;
        Assert.Equal(i2, i1);

        Assert.True(elapsed.TotalMilliseconds >= i2.Delay);
    }

    [Homework(Homeworks.HomeWork3)]
    public void CustomInitialization_ReturnsSingleInstance()
    {
        //Выкидывало ошибку двойной инициализации, если не вызвать Reset
        SingleInitializationSingleton.Reset();
        var delay = 5_000;
        var elapsed = StopWatcher.Stopwatch(() =>
        {
            //Инициализация проходила вне таймера, поэтому первый Assert.True всегда падал
            SingleInitializationSingleton.Initialize(delay);
            var i = SingleInitializationSingleton.Instance;
            Assert.Equal(i, SingleInitializationSingleton.Instance);
        });

        Assert.True(elapsed.TotalMilliseconds > SingleInitializationSingleton.DefaultDelay);
        Assert.True(elapsed.TotalMilliseconds >= delay);
    }

    [Homework(Homeworks.HomeWork3)]
    public void DoubleInitializationAttemptThrowsException()
    {
        SingleInitializationSingleton.Reset();
        SingleInitializationSingleton.Initialize(2);
        Assert.Throws<InvalidOperationException>(() => { SingleInitializationSingleton.Initialize(3); });
    }
}