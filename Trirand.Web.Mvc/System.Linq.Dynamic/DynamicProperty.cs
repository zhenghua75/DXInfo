namespace System.Linq.Dynamic
{
    using System;

    public class DynamicProperty
    {
        private string name;
        private System.Type type;

        public DynamicProperty(string name, System.Type type)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            this.name = name;
            this.type = type;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public System.Type Type
        {
            get
            {
                return this.type;
            }
        }
    }
}

