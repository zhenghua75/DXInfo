using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.Reflection;
using System.CodeDom.Compiler;
using System.IO;

namespace DXInfo.CodeGenerate
{
    public class GenerateBase
    {
        public CodeCompileUnit MyUnit { get; set; }
        public CodeTypeDeclaration MyClass { get; set; }
        public string ClassName { get; set; }
        public string generatePath { get; set; }
        public GenerateBase(CodeNamespaceImport[] lcnmSpace, string nmSpace, string className, CodeTypeReference[] lBaseClass, string generatePath)
        {
            this.ClassName = className;
            this.MyUnit = new CodeCompileUnit();
            CodeNamespace cnmSpace = new CodeNamespace(nmSpace);
            cnmSpace.Imports.AddRange(lcnmSpace);
            this.MyClass = new CodeTypeDeclaration(className);
            this.MyClass.IsClass = true;            
            this.MyClass.TypeAttributes = TypeAttributes.Public;

            

            this.MyClass.BaseTypes.AddRange(lBaseClass);
            cnmSpace.Types.Add(this.MyClass);
            this.MyUnit.Namespaces.Add(cnmSpace);
            this.generatePath = generatePath;
        }
        public virtual void AddField(string filedName, string typeName)
        {
            CodeMemberField widthValueField = new CodeMemberField();
            widthValueField.Attributes = MemberAttributes.Private;
            widthValueField.Name = filedName;
            widthValueField.Type = new CodeTypeReference(typeName);
            this.MyClass.Members.Add(widthValueField);
        }
        public virtual void AddProperty(string propertyName, string typeName)
        {
            CodeMemberProperty property = new CodeMemberProperty();
            property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            property.Name = propertyName;
            property.HasGet = true;
            property.HasSet = true;
            property.Type = new CodeTypeReference(typeName);
            this.MyClass.Members.Add(property);
        }
        public virtual void AddMethod()
        {
            // Declaring a ToString method
            CodeMemberMethod toStringMethod = new CodeMemberMethod();
            toStringMethod.Attributes =
                MemberAttributes.Public | MemberAttributes.Override;
            toStringMethod.Name = "ToString";
            toStringMethod.ReturnType =
                new CodeTypeReference(typeof(System.String));

            CodeFieldReferenceExpression widthReference =
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "Width");
            CodeFieldReferenceExpression heightReference =
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "Height");
            CodeFieldReferenceExpression areaReference =
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "Area");

            // Declaring a return statement for method ToString.
            CodeMethodReturnStatement returnStatement =
                new CodeMethodReturnStatement();

            // This statement returns a string representation of the width,
            // height, and area.
            string formattedOutput = "The object:" + Environment.NewLine +
                " width = {0}," + Environment.NewLine +
                " height = {1}," + Environment.NewLine +
                " area = {2}";
            returnStatement.Expression =
                new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression("System.String"), "Format",
                new CodePrimitiveExpression(formattedOutput),
                widthReference, heightReference, areaReference);
            toStringMethod.Statements.Add(returnStatement);
            this.MyClass.Members.Add(toStringMethod);
        }
        public virtual void AddConstructor()
        {
            // Declare the constructor
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;

            //// Add parameters.
            //constructor.Parameters.Add(new CodeParameterDeclarationExpression(
            //    typeof(System.Double), "width"));
            //constructor.Parameters.Add(new CodeParameterDeclarationExpression(
            //    typeof(System.Double), "height"));

            //// Add field initialization logic
            //CodeFieldReferenceExpression widthReference =
            //    new CodeFieldReferenceExpression(
            //    new CodeThisReferenceExpression(), "widthValue");
            //constructor.Statements.Add(new CodeAssignStatement(widthReference,
            //    new CodeArgumentReferenceExpression("width")));
            //CodeFieldReferenceExpression heightReference =
            //    new CodeFieldReferenceExpression(
            //    new CodeThisReferenceExpression(), "heightValue");
            //constructor.Statements.Add(new CodeAssignStatement(heightReference,
            //    new CodeArgumentReferenceExpression("height")));
            this.MyClass.Members.Add(constructor);
        }
        public virtual void AddEntryPoint()
        {
            CodeEntryPointMethod start = new CodeEntryPointMethod();
            CodeObjectCreateExpression objectCreate =
                new CodeObjectCreateExpression(
                new CodeTypeReference("CodeDOMCreatedClass"),
                new CodePrimitiveExpression(5.3),
                new CodePrimitiveExpression(6.9));

            // Add the statement:
            // "CodeDOMCreatedClass testClass = 
            //     new CodeDOMCreatedClass(5.3, 6.9);"
            start.Statements.Add(new CodeVariableDeclarationStatement(
                new CodeTypeReference("CodeDOMCreatedClass"), "testClass",
                objectCreate));

            // Creat the expression:
            // "testClass.ToString()"
            CodeMethodInvokeExpression toStringInvoke =
                new CodeMethodInvokeExpression(
                new CodeVariableReferenceExpression("testClass"), "ToString");

            // Add a System.Console.WriteLine statement with the previous 
            // expression as a parameter.
            start.Statements.Add(new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression("System.Console"),
                "WriteLine", toStringInvoke));
            this.MyClass.Members.Add(start);
        }
        public void GenerateCSharpCode()
        {
            string fileName = this.ClassName + ".cs";
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            if (!Directory.Exists(this.generatePath))
            {
                Directory.CreateDirectory(this.generatePath);
            }
            using (StreamWriter sourceWriter = new StreamWriter(Path.Combine(this.generatePath, fileName)))
            {
                provider.GenerateCodeFromCompileUnit(
                    this.MyUnit, sourceWriter, options);
            }
        }
    }
}
