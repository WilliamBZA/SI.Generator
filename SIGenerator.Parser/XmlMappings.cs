using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGenerator.Parser
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class ClassDiagram
    {

        private ClassDiagramClass[] classField;

        private ClassDiagramFont fontField;

        private byte majorVersionField;

        private byte minorVersionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Class")]
        public ClassDiagramClass[] Class
        {
            get
            {
                return this.classField;
            }
            set
            {
                this.classField = value;
            }
        }

        /// <remarks/>
        public ClassDiagramFont Font
        {
            get
            {
                return this.fontField;
            }
            set
            {
                this.fontField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte MajorVersion
        {
            get
            {
                return this.majorVersionField;
            }
            set
            {
                this.majorVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte MinorVersion
        {
            get
            {
                return this.minorVersionField;
            }
            set
            {
                this.minorVersionField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ClassDiagramClass
    {

        private ClassDiagramClassPosition positionField;

        private ClassDiagramClassTypeIdentifier typeIdentifierField;

        private ClassDiagramClassShowAsAssociation showAsAssociationField;

        private string nameField;

        /// <remarks/>
        public ClassDiagramClassPosition Position
        {
            get
            {
                return this.positionField;
            }
            set
            {
                this.positionField = value;
            }
        }

        /// <remarks/>
        public ClassDiagramClassTypeIdentifier TypeIdentifier
        {
            get
            {
                return this.typeIdentifierField;
            }
            set
            {
                this.typeIdentifierField = value;
            }
        }

        /// <remarks/>
        public ClassDiagramClassShowAsAssociation ShowAsAssociation
        {
            get
            {
                return this.showAsAssociationField;
            }
            set
            {
                this.showAsAssociationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ClassDiagramClassPosition
    {

        private decimal xField;

        private decimal yField;

        private decimal widthField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal X
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ClassDiagramClassTypeIdentifier
    {

        private string hashCodeField;

        private string fileNameField;

        /// <remarks/>
        public string HashCode
        {
            get
            {
                return this.hashCodeField;
            }
            set
            {
                this.hashCodeField = value;
            }
        }

        /// <remarks/>
        public string FileName
        {
            get
            {
                return this.fileNameField;
            }
            set
            {
                this.fileNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ClassDiagramClassShowAsAssociation
    {

        private ClassDiagramClassShowAsAssociationProperty propertyField;

        /// <remarks/>
        public ClassDiagramClassShowAsAssociationProperty Property
        {
            get
            {
                return this.propertyField;
            }
            set
            {
                this.propertyField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ClassDiagramClassShowAsAssociationProperty
    {

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ClassDiagramFont
    {

        private string nameField;

        private byte sizeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
            }
        }
    }
}