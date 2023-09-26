namespace Tests.CSharp.Homework3;

public class SingleInitializationSingleton
{
    public const int DefaultDelay = 3_000;
    private static readonly object Locker = new();

    private static volatile bool _isInitialized = false;

    private SingleInitializationSingleton(int delay = DefaultDelay)
    {
        Delay = delay;
        // imitation of complex initialization logic
        Thread.Sleep(delay);
    }

    public int Delay { get; }

    private static SingleInitializationSingleton _instance;

    public static SingleInitializationSingleton Instance
    {
        get
        {
            lock (Locker)
            {
                if (!_isInitialized)
                    lock (Locker)
                    {
                        _instance = new SingleInitializationSingleton();
                        _isInitialized = true;
                    }
                return _instance;
            }
        }
    }

    internal static void Reset()
    {
        lock (Locker)
        {
            _instance = new SingleInitializationSingleton();
            _isInitialized = false;
        }
    }

    public static void Initialize(int delay)
    {
        lock (Locker)
        {
            if (_isInitialized) throw new InvalidOperationException("Попытка двойной инициализации");
            lock (Locker)
            {
                _instance = new SingleInitializationSingleton(delay); 
                _isInitialized = true;
            }
        }
    }
}