using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace DXInfo.CodeGenerate
{
    public class UowGenerate:GenerateBase
    {
        public string ConnectiongStringName { get; set; }
        public UowGenerate(CodeNamespaceImport[] lcnmSpace, string nmSpace, string className, CodeTypeReference[] lBaseClass, string generatePath)
            : base(lcnmSpace, nmSpace, className + "Uow", lBaseClass, generatePath)
        {
            this.ConnectiongStringName = className;
            //this.MyClass.IsInterface = true;
        }
        public override void AddConstructor()
        {
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;
            constructor.Parameters.Add(new CodeParameterDeclarationExpression("IRepositoryProvider","repositoryProvider"));
            constructor.BaseConstructorArgs.Add(new CodeSnippetExpression("repositoryProvider"));
            this.MyClass.Members.Add(constructor);
        }
        public void AddProperty(string propertyName)
        {
            CodeMemberProperty property = new CodeMemberProperty();
            property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            property.Name = propertyName;
            property.HasGet = true;
            property.Type = new CodeTypeReference("IRepository<" + propertyName + ">");
            property.GetStatements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(null,"GetStandardRepo<"+propertyName+">"))));
            this.MyClass.Members.Add(property);
        }
    }
}
