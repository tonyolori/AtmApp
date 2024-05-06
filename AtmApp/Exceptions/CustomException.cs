using System;

public class MyCustomException : Exception
{
    // Constructors
    public MyCustomException() : base() { }

    public MyCustomException(string message) : base(message) { }

    public MyCustomException(string message, Exception innerException)
        : base(message, innerException) { }

    // You can also add any additional properties or methods as needed
}
