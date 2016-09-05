# Nancy.IPLock
IP Locking for Nancy to restrict/allow access from certain IP addresses

## How to integrate it in to Nancy

In your `Bootstrapper` class, add the following

```csharp
protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
{
    // ...

    IPLock.Enable(pipelines, new IPLockConfiguration
    {
        IPValidator = new MyIPValidator() // or container.Resolve<IIPValidator>()
    });

    // ...
}
```


## Implementing the IP Validator

You will need to create a class that implements the `IIPValidator` interface.

```csharp
public class MyIPValidator : IIPValidator
{
    public bool IsValid(string remoteAddress)
    {
        return remoteAddress == "1.2.3.4";
    }
}
```

## Copyright

Copyright © 2016 Steven Atkinson and contributors

## License

Nancy.IPLock is licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to LICENSE for more information.
