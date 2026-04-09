using VMF.NET.Runtime;
using Tutorial08.VmfModel;

namespace Tutorial08;

public class CustomBehavior : IDelegatedBehavior<IObjectWithCustomBehavior>
{
    private IObjectWithCustomBehavior _caller = null!;

    public void SetCaller(IObjectWithCustomBehavior caller)
    {
        _caller = caller;
    }

    /// <summary>
    /// Delegated behavior. It is called whenever caller.ComputeSum() is called.
    /// This method computes and returns the sum of property 'A' and 'B'.
    /// </summary>
    public int ComputeSum()
    {
        return _caller.A + _caller.B;
    }

    public void OnObjectWithCustomBehaviorInstantiated()
    {
        Console.WriteLine("object instantiated");
    }
}
