using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data.Tests
{
    class SampleData
    {
        public const string River = "River";
        public const string Willis = "Willis";

        public const string Brandon = "Brandon";
        public const string Fields = "Fields";

        public const string Jon = "Jon";
        public const string Doe = "Doe";

        static public User CreateRiverWillis() => new User(River, Willis);
        static public User CreateBrandonFields() => new User(Brandon, Fields);
        static public User CreateJonDoe() => new User(Jon, Doe);
    }
}
