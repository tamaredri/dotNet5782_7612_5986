using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class InvalidInputExeption : Exception, ISerializable
    {
        public InvalidInputExeption() : base() { }
        public InvalidInputExeption(string message) : base(message) { }
        public InvalidInputExeption(string message, Exception inner) : base(message, inner) { }
        protected InvalidInputExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class AlreadyExistExeption : Exception, ISerializable
    {
        public AlreadyExistExeption() : base() { }
        public AlreadyExistExeption(string message) : base(message) { }
        public AlreadyExistExeption(string message, Exception inner) : base(message, inner) { }
        protected AlreadyExistExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class DoesntExistExeption : Exception, ISerializable
    {
        public DoesntExistExeption() : base() { }
        public DoesntExistExeption(string message) : base(message) { }
        public DoesntExistExeption(string message, Exception inner) : base(message, inner) { }
        protected DoesntExistExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
