using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BO
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

    [Serializable]
    public class BattaryExeption : Exception, ISerializable
    {
        public BattaryExeption() : base() { }
        public BattaryExeption(string message) : base(message) { }
        public BattaryExeption(string message, Exception inner) : base(message, inner) { }
        protected BattaryExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class ContradictoryDataExeption : Exception, ISerializable
    {
        public ContradictoryDataExeption() : base() { }
        public ContradictoryDataExeption(string message) : base(message) { }
        public ContradictoryDataExeption(string message, Exception inner) : base(message, inner) { }
        protected ContradictoryDataExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }


}

