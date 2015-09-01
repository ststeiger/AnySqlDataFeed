
using System;
using System.Xml.Serialization;
using System.Collections.Generic;


// http://xmltocsharp.azurewebsites.net/
// http://jsonclassgenerator.codeplex.com/
namespace AnySqlDataFeed.XML
{


    [XmlRoot(ElementName = "PropertyRef", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class PropertyRef
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }


    [XmlRoot(ElementName = "Key", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class Key
    {
        [XmlElement(ElementName = "PropertyRef", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public List<PropertyRef> PropertyRef { get; set; }
    }


    [XmlRoot(ElementName = "Property", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class Property
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "Nullable")]
        public string Nullable { get; set; }

        [XmlAttribute(AttributeName = "MaxLength")]
        public string MaxLength { get; set; }

        [XmlAttribute(AttributeName = "FixedLength")]
        public string FixedLength { get; set; }

        [XmlAttribute(AttributeName = "Unicode")]
        public string Unicode { get; set; }

        [XmlAttribute(AttributeName = "Precision")]
        public string Precision { get; set; }

        [XmlAttribute(AttributeName = "p6", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string P6 { get; set; }

        [XmlAttribute(AttributeName = "StoreGeneratedPattern", Namespace = "http://schemas.microsoft.com/ado/2009/02/edm/annotation")]
        public string StoreGeneratedPattern { get; set; }

        [XmlAttribute(AttributeName = "Scale")]
        public string Scale { get; set; }
    }


    [XmlRoot(ElementName = "EntityType", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class EntityType
    {
        [XmlElement(ElementName = "Key", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public Key Key { get; set; }

        [XmlElement(ElementName = "Property", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public List<Property> Property { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "NavigationProperty", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public List<NavigationProperty> NavigationProperty { get; set; }
    }


    [XmlRoot(ElementName = "NavigationProperty", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class NavigationProperty
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "Relationship")]
        public string Relationship { get; set; }

        [XmlAttribute(AttributeName = "ToRole")]
        public string ToRole { get; set; }

        [XmlAttribute(AttributeName = "FromRole")]
        public string FromRole { get; set; }
    }


    [XmlRoot(ElementName = "End", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class End
    {
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "Role")]
        public string Role { get; set; }

        [XmlAttribute(AttributeName = "Multiplicity")]
        public string Multiplicity { get; set; }

        [XmlElement(ElementName = "OnDelete", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public OnDelete OnDelete { get; set; }

        [XmlAttribute(AttributeName = "EntitySet")]
        public string EntitySet { get; set; }
    }


    [XmlRoot(ElementName = "Principal", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class Principal
    {
        [XmlElement(ElementName = "PropertyRef", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public PropertyRef PropertyRef { get; set; }

        [XmlAttribute(AttributeName = "Role")]
        public string Role { get; set; }
    }


    [XmlRoot(ElementName = "Dependent", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class Dependent
    {
        [XmlElement(ElementName = "PropertyRef", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public PropertyRef PropertyRef { get; set; }

        [XmlAttribute(AttributeName = "Role")]
        public string Role { get; set; }
    }


    [XmlRoot(ElementName = "ReferentialConstraint", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class ReferentialConstraint
    {
        [XmlElement(ElementName = "Principal", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public Principal Principal { get; set; }

        [XmlElement(ElementName = "Dependent", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public Dependent Dependent { get; set; }
    }


    [XmlRoot(ElementName = "Association", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class Association
    {
        [XmlElement(ElementName = "End", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public List<End> End { get; set; }

        [XmlElement(ElementName = "ReferentialConstraint", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public ReferentialConstraint ReferentialConstraint { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }


    [XmlRoot(ElementName = "OnDelete", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class OnDelete
    {
        [XmlAttribute(AttributeName = "Action")]
        public string Action { get; set; }
    }


    [XmlRoot(ElementName = "Schema", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class Schema
    {
        [XmlElement(ElementName = "EntityType", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public List<EntityType> EntityType { get; set; }

        [XmlElement(ElementName = "Association", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public List<Association> Association { get; set; }

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlAttribute(AttributeName = "Namespace")]
        public string Namespace { get; set; }

        [XmlElement(ElementName = "EntityContainer", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public EntityContainer EntityContainer { get; set; }
    }


    [XmlRoot(ElementName = "EntitySet", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class EntitySet
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "EntityType")]
        public string EntityType { get; set; }
    }


    [XmlRoot(ElementName = "AssociationSet", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class AssociationSet
    {
        [XmlElement(ElementName = "End", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public List<End> End { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "Association")]
        public string Association { get; set; }
    }


    [XmlRoot(ElementName = "EntityContainer", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
    public class EntityContainer
    {
        [XmlElement(ElementName = "EntitySet", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public List<EntitySet> EntitySet { get; set; }

        [XmlElement(ElementName = "AssociationSet", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public List<AssociationSet> AssociationSet { get; set; }

        [XmlAttribute(AttributeName = "p6", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string P6 { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "IsDefaultEntityContainer", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
        public string IsDefaultEntityContainer { get; set; }

        [XmlAttribute(AttributeName = "LazyLoadingEnabled", Namespace = "http://schemas.microsoft.com/ado/2009/02/edm/annotation")]
        public string LazyLoadingEnabled { get; set; }
    }


    [XmlRoot(ElementName = "DataServices", Namespace = "http://schemas.microsoft.com/ado/2007/06/edmx")]
    public class DataServices
    {

        [XmlAttribute(AttributeName = "m", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string M { get; set; }

        [XmlAttribute(AttributeName = "DataServiceVersion", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
        public string DataServiceVersion { get; set; }

        [XmlAttribute(AttributeName = "MaxDataServiceVersion", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
        public string MaxDataServiceVersion { get; set; }

        [XmlElement(ElementName = "Schema", Namespace = "http://schemas.microsoft.com/ado/2008/09/edm")]
        public List<Schema> Schema { get; set; }

    }


    [XmlRoot(ElementName = "Edmx", Namespace = "http://schemas.microsoft.com/ado/2007/06/edmx")]
    public class EdmxRoot
    {

        [XmlAttribute(AttributeName = "edmx", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Edmx { get; set; }


        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; }


        [XmlElement(ElementName = "DataServices", Namespace = "http://schemas.microsoft.com/ado/2007/06/edmx")]
        public DataServices DataServices { get; set; }

    }

}
