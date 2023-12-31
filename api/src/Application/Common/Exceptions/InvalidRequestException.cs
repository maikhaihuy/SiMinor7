﻿namespace SiMinor7.Application.Common.Exceptions;

public class InvalidRequestException : Exception
{
    public InvalidRequestException()
    {
    }

    public InvalidRequestException(string message)
        : base(message)
    {
    }

    public InvalidRequestException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}