using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace DXInfo.CodeGenerate
{
    public class DbContextGenerate : GenerateBase
    {
        public string ConnectiongStringName { get; set; }
        public DbContextGenerate(CodeNamespaceImport[] lcnmSpace, string nmSpace, string className, CodeTypeReference[] lBaseClass, string generatePath)
            : base(lcnmSpace, nmSpace, className + "DbContext", lBaseClass, generatePath)
        {
            this.ConnectiongStringName = className;
        }
        public override void AddField(string propertyName, string filedName)
        {
            CodeMemberField field = new CodeMemberField();
            field.Attributes = MemberAttributes.Private;
            field.Name = filedName;
            field.Type = new CodeTypeReference("DbSet<" + propertyName + ">");
            this.MyClass.Members.Add(field);
        }
        public override void AddProperty(string propertyName,string fieldName)
        {
            //CodeMemberField property = new CodeMemberField();
            //property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            //property.Name = propertyName;
            ////property.HasGet = false;
            ////property.HasSet = false;
            //property.Type = new CodeTypeReference("DbSet<" + propertyName + ">");
   //         property.GetStatements.Add(new CodeMethodReturnStatement(
   //             new CodeVariableReferenceExpression(fieldName)));
   //         property.SetStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression(fieldName), new
   //CodeVariableReferenceExpression("value")));

            CodeSnippetTypeMember property = new CodeSnippetTypeMember();
            property.Text = "        public DbSet<" + propertyName + "> " + propertyName + " { get; set; }";
            //_type.Members.Add(_snippet);

            this.MyClass.Members.Add(property);
        }
        public override void AddConstructor()
        {
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;

            constructor.BaseConstructorArgs.Add(new CodeSnippetExpression("nameOrConnectionString:\"" + ConnectiongStringName + "\""));
            this.MyClass.Members.Add(constructor);
        }

        public void AddMethod(CodeStatementCollection csc)
        {
            CodeMemberMethod modelCreating = new CodeMemberMethod();
            modelCreating.Attributes =
                MemberAttributes.Family | MemberAttributes.Override;
            modelCreating.Name = "OnModelCreating";
            modelCreating.Parameters.Add(new CodeParameterDeclarationExpression("DbModelBuilder", "modelBuilder"));

            CodeMethodInvokeExpression m1 = new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(),
    "OnModelCreating", new CodeVariableReferenceExpression("modelBuilder"));

            CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("modelBuilder.Conventions"),
    "Remove<PluralizingTableNameConvention>");
            

            //CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("modelBuilder.Entity<InventoryCategory>().Property(o => o.Name)"), "IsUnicode");

            modelCreating.Statements.Add(m1);
            modelCreating.Statements.Add(m2);

            modelCreating.Statements.AddRange(csc);
            this.MyClass.Members.Add(modelCreating);
        }
    }
}
