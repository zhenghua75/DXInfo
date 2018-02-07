namespace System.Linq.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Signature : IEquatable<System.Linq.Dynamic.Signature>
    {
        public int hashCode;
        public DynamicProperty[] properties;

        public Signature(IEnumerable<DynamicProperty> properties)
        {
            this.properties = properties.ToArray<DynamicProperty>();
            this.hashCode = 0;
            foreach (DynamicProperty property in properties)
            {
                this.hashCode ^= property.Name.GetHashCode() ^ property.Type.GetHashCode();
            }
        }

        public bool Equals(System.Linq.Dynamic.Signature other)
        {
            if (this.properties.Length != other.properties.Length)
            {
                return false;
            }
            for (int i = 0; i < this.properties.Length; i++)
            {
                if ((this.properties[i].Name != other.properties[i].Name) || (this.properties[i].Type != other.properties[i].Type))
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return ((obj is System.Linq.Dynamic.Signature) && this.Equals((System.Linq.Dynamic.Signature) obj));
        }

        public override int GetHashCode()
        {
            return this.hashCode;
        }
    }
}

