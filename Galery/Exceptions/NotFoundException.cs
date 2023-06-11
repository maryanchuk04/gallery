using System;
namespace Galery.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string key)
        : base($"Item not found with key {key}") { }
}

