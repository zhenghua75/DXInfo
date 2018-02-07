using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using Microsoft.CSharp;
using System.Reflection;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DXInfo.CodeGenerate
{
    public class ModelGenerate:GenerateBase
    {
        public ModelGenerate(CodeNamespaceImport[] lcnmSpace, string nmSpace, string className, CodeTypeReference[] lBaseClass,string generatePath)
            :base(lcnmSpace,nmSpace,className,lBaseClass,generatePath)
        {
            //CodeTypeReference ctr = new CodeTypeReference("Serializable");
            CodeAttributeDeclaration cad = new CodeAttributeDeclaration("Serializable");
            CodeAttributeDeclaration cad1 = new CodeAttributeDeclaration("DataContract");
            CodeAttributeDeclarationCollection cadc = new CodeAttributeDeclarationCollection();
            cadc.Add(cad);
            cadc.Add(cad1);
            this.MyClass.CustomAttributes = cadc;
        }
        
        public void AddProperty(string propertyName, string fieldName, string typeName)
        {
            CodeMemberProperty property = new CodeMemberProperty();
            property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            CodeAttributeDeclaration cad = new CodeAttributeDeclaration("DataMember");
            CodeAttributeDeclarationCollection cadc = new CodeAttributeDeclarationCollection();
            cadc.Add(cad);
            property.CustomAttributes = cadc;
            property.Name = propertyName;
            property.HasGet = true;
            property.HasSet = true;
            property.Type = new CodeTypeReference(typeName);

            CodeThisReferenceExpression thisRef = new CodeThisReferenceExpression();

            property.GetStatements.Add(new CodeMethodReturnStatement(
                new CodeVariableReferenceExpression(fieldName)));


            CodeMethodInvokeExpression methodInvoke = new CodeMethodInvokeExpression(null,
    "OnPropertyChanged",
    new CodeExpression[] { new CodePrimitiveExpression(propertyName) });

            CodeConditionStatement conditionalStatement = new CodeConditionStatement();


            conditionalStatement.Condition = new CodeBinaryOperatorExpression(new
   CodeVariableReferenceExpression("value"), CodeBinaryOperatorType.IdentityInequality, new CodeVariableReferenceExpression(fieldName));

            conditionalStatement.TrueStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression(fieldName), new
   CodeVariableReferenceExpression("value")));
            conditionalStatement.TrueStatements.Add(methodInvoke);

            property.SetStatements.Add(conditionalStatement);
            this.MyClass.Members.Add(property);
        }
        
    } 
}
