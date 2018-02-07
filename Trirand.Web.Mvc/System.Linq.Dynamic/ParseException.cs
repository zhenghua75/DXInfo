namespace System.Linq.Dynamic
{
    using System;

    public sealed class ParseException : Exception
    {
        private int position;

        public ParseException(string message, int position) : base(message)
        {
            this.position = position;
        }

        public override string ToString()
        {
            return string.Format("{0} (at index {1})", this.Message, this.position);
        }

        public int Position
        {
            get
            {
                return this.position;
            }
        }
    }
}

