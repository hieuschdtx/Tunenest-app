﻿using System.Reflection;

namespace tunenest.Infrastructure
{
    public static class AssemblyReference
    {
        public static readonly Assembly assembly = typeof(AssemblyReference).Assembly;
    }
}
