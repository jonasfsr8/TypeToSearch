﻿namespace TypeToSearch.Domain.Exceptions
{
    public class ResourceAlreadyExistsException : Exception
    {
        public ResourceAlreadyExistsException(string message) : base(message) { }
    }
}
