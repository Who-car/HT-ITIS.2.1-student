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
    
    private static Lazy<SingleInitializationSingleton> _instance;

    public static SingleInitializationSingleton Instance => _instance.Value;
    
    internal static void Reset()
    {
        _instance = new Lazy<SingleInitializationSingleton>
            (() => new SingleInitializationSingleton(), true);
        _isInitialized = false;
    }
    
    public static void Initialize(int delay)
    {
        if (_isInitialized) throw new InvalidOperationException("Попытка двойной инициализации"); 
        
        _instance = new Lazy<SingleInitializationSingleton>
            (() => new SingleInitializationSingleton(delay), true); 
        _isInitialized = true;
    }
}