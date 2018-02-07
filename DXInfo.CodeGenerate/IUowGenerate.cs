using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace DXInfo.CodeGenerate
{
    public class IUowGenerate:GenerateBase
    {
        public string ConnectiongStringName { get; set; }
        public IUowGenerate(CodeNamespaceImport[] lcnmSpace, string nmSpace, string className, CodeTypeReference[] lBaseClass, string generatePath)
            : base(lcnmSpace, nmSpace, "I"+className + "Uow", lBaseClass, generatePath)
        {
            this.ConnectiongStringName = className;
            this.MyClass.IsInterface = true;
        }

        public void AddProperty(string propertyName)
        {
            CodeMemberProperty property = new CodeMemberProperty();
            property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            property.Name = propertyName;
            property.HasGet = true;
            //property.HasSet = true;
            property.Type = new CodeTypeReference("IRepository<" + propertyName + ">");
            this.MyClass.Members.Add(property);
        }
    }
}
