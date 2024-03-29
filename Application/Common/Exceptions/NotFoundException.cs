﻿using System;
namespace Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" key:({key}) was not found.")
        {
        }
    }
}
