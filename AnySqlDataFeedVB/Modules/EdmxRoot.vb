
Imports System.Xml.Serialization
Imports System.Collections.Generic


' http://xmltocsharp.azurewebsites.net/
' http://jsonclassgenerator.codeplex.com/
Namespace AnySqlDataFeed.XML


    <XmlRoot(ElementName:="PropertyRef", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class PropertyRef


        <XmlAttribute(AttributeName:="Name")>
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set
                m_Name = Value
            End Set
        End Property

        Private m_Name As String
    End Class


    <XmlRoot(ElementName:="Key", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class Key


        <XmlElement(ElementName:="PropertyRef", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property PropertyRef() As List(Of PropertyRef)
            Get
                Return m_PropertyRef
            End Get
            Set
                m_PropertyRef = Value
            End Set
        End Property

        Private m_PropertyRef As List(Of PropertyRef)
    End Class



    <XmlRoot(ElementName:="Property", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class [Property]


        <XmlAttribute(AttributeName:="Name")>
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set
                m_Name = Value
            End Set
        End Property


        Private m_Name As String

        <XmlAttribute(AttributeName:="Type")>
        Public Property Type() As String
            Get
                Return m_Type
            End Get
            Set
                m_Type = Value
            End Set
        End Property


        Private m_Type As String

        <XmlAttribute(AttributeName:="Nullable")>
        Public Property Nullable() As String
            Get
                Return m_Nullable
            End Get
            Set
                m_Nullable = Value
            End Set
        End Property


        Private m_Nullable As String

        <XmlAttribute(AttributeName:="MaxLength")>
        Public Property MaxLength() As String
            Get
                Return m_MaxLength
            End Get
            Set
                m_MaxLength = Value
            End Set
        End Property


        Private m_MaxLength As String

        <XmlAttribute(AttributeName:="FixedLength")>
        Public Property FixedLength() As String
            Get
                Return m_FixedLength
            End Get
            Set
                m_FixedLength = Value
            End Set
        End Property


        Private m_FixedLength As String

        <XmlAttribute(AttributeName:="Unicode")>
        Public Property Unicode() As String
            Get
                Return m_Unicode
            End Get
            Set
                m_Unicode = Value
            End Set
        End Property


        Private m_Unicode As String

        <XmlAttribute(AttributeName:="Precision")>
        Public Property Precision() As String
            Get
                Return m_Precision
            End Get
            Set
                m_Precision = Value
            End Set
        End Property


        Private m_Precision As String

        <XmlAttribute(AttributeName:="p6", [Namespace]:="http://www.w3.org/2000/xmlns/")>
        Public Property P6() As String
            Get
                Return m_P6
            End Get
            Set
                m_P6 = Value
            End Set
        End Property


        Private m_P6 As String

        <XmlAttribute(AttributeName:="StoreGeneratedPattern", [Namespace]:="http://schemas.microsoft.com/ado/2009/02/edm/annotation")>
        Public Property StoreGeneratedPattern() As String
            Get
                Return m_StoreGeneratedPattern
            End Get
            Set
                m_StoreGeneratedPattern = Value
            End Set
        End Property


        Private m_StoreGeneratedPattern As String

        <XmlAttribute(AttributeName:="Scale")>
        Public Property Scale() As String
            Get
                Return m_Scale
            End Get
            Set
                m_Scale = Value
            End Set
        End Property

        Private m_Scale As String
    End Class



    <XmlRoot(ElementName:="EntityType", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class EntityType


        <XmlElement(ElementName:="Key", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property Key() As Key
            Get
                Return m_Key
            End Get
            Set
                m_Key = Value
            End Set
        End Property


        Private m_Key As Key

        <XmlElement(ElementName:="Property", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property [Property]() As List(Of [Property])
            Get
                Return m_Property
            End Get
            Set
                m_Property = Value
            End Set
        End Property


        Private m_Property As List(Of [Property])

        <XmlAttribute(AttributeName:="Name")>
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set
                m_Name = Value
            End Set
        End Property


        Private m_Name As String

        <XmlElement(ElementName:="NavigationProperty", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property NavigationProperty() As List(Of NavigationProperty)
            Get
                Return m_NavigationProperty
            End Get
            Set
                m_NavigationProperty = Value
            End Set
        End Property

        Private m_NavigationProperty As List(Of NavigationProperty)
    End Class



    <XmlRoot(ElementName:="NavigationProperty", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class NavigationProperty
        <XmlAttribute(AttributeName:="Name")>
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set
                m_Name = Value
            End Set
        End Property


        Private m_Name As String

        <XmlAttribute(AttributeName:="Relationship")>
        Public Property Relationship() As String
            Get
                Return m_Relationship
            End Get
            Set
                m_Relationship = Value
            End Set
        End Property


        Private m_Relationship As String

        <XmlAttribute(AttributeName:="ToRole")>
        Public Property ToRole() As String
            Get
                Return m_ToRole
            End Get
            Set
                m_ToRole = Value
            End Set
        End Property


        Private m_ToRole As String

        <XmlAttribute(AttributeName:="FromRole")>
        Public Property FromRole() As String
            Get
                Return m_FromRole
            End Get
            Set
                m_FromRole = Value
            End Set
        End Property

        Private m_FromRole As String
    End Class



    <XmlRoot(ElementName:="End", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class [End]


        <XmlAttribute(AttributeName:="Type")>
        Public Property Type() As String
            Get
                Return m_Type
            End Get
            Set
                m_Type = Value
            End Set
        End Property


        Private m_Type As String

        <XmlAttribute(AttributeName:="Role")>
        Public Property Role() As String
            Get
                Return m_Role
            End Get
            Set
                m_Role = Value
            End Set
        End Property


        Private m_Role As String

        <XmlAttribute(AttributeName:="Multiplicity")>
        Public Property Multiplicity() As String
            Get
                Return m_Multiplicity
            End Get
            Set
                m_Multiplicity = Value
            End Set
        End Property


        Private m_Multiplicity As String


        <XmlElement(ElementName:="OnDelete", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property OnDelete() As OnDelete
            Get
                Return m_OnDelete
            End Get
            Set
                m_OnDelete = Value
            End Set
        End Property

        Private m_OnDelete As OnDelete

        <XmlAttribute(AttributeName:="EntitySet")>
        Public Property EntitySet() As String
            Get
                Return m_EntitySet
            End Get
            Set
                m_EntitySet = Value
            End Set
        End Property

        Private m_EntitySet As String
    End Class

    <XmlRoot(ElementName:="Principal", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class Principal


        <XmlElement(ElementName:="PropertyRef", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property PropertyRef() As PropertyRef
            Get
                Return m_PropertyRef
            End Get
            Set
                m_PropertyRef = Value
            End Set
        End Property


        Private m_PropertyRef As PropertyRef

        <XmlAttribute(AttributeName:="Role")>
        Public Property Role() As String
            Get
                Return m_Role
            End Get
            Set
                m_Role = Value
            End Set
        End Property

        Private m_Role As String
    End Class

    <XmlRoot(ElementName:="Dependent", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class Dependent


        <XmlElement(ElementName:="PropertyRef", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property PropertyRef() As PropertyRef
            Get
                Return m_PropertyRef
            End Get
            Set
                m_PropertyRef = Value
            End Set
        End Property


        Private m_PropertyRef As PropertyRef

        <XmlAttribute(AttributeName:="Role")>
        Public Property Role() As String
            Get
                Return m_Role
            End Get
            Set
                m_Role = Value
            End Set
        End Property

        Private m_Role As String
    End Class


    <XmlRoot(ElementName:="ReferentialConstraint", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class ReferentialConstraint


        <XmlElement(ElementName:="Principal", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property Principal() As Principal
            Get
                Return m_Principal
            End Get
            Set
                m_Principal = Value
            End Set
        End Property


        Private m_Principal As Principal

        <XmlElement(ElementName:="Dependent", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property Dependent() As Dependent
            Get
                Return m_Dependent
            End Get
            Set
                m_Dependent = Value
            End Set
        End Property

        Private m_Dependent As Dependent
    End Class

    <XmlRoot(ElementName:="Association", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class Association


        <XmlElement(ElementName:="End", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property [End]() As List(Of [End])
            Get
                Return m_End
            End Get
            Set
                m_End = Value
            End Set
        End Property


        Private m_End As List(Of [End])

        <XmlElement(ElementName:="ReferentialConstraint", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property ReferentialConstraint() As ReferentialConstraint
            Get
                Return m_ReferentialConstraint
            End Get
            Set
                m_ReferentialConstraint = Value
            End Set
        End Property


        Private m_ReferentialConstraint As ReferentialConstraint

        <XmlAttribute(AttributeName:="Name")>
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set
                m_Name = Value
            End Set
        End Property
        Private m_Name As String
    End Class



    <XmlRoot(ElementName:="OnDelete", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class OnDelete


        <XmlAttribute(AttributeName:="Action")>
        Public Property Action() As String
            Get
                Return m_Action
            End Get
            Set
                m_Action = Value
            End Set
        End Property

        Private m_Action As String
    End Class

    <XmlRoot(ElementName:="Schema", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class Schema


        <XmlElement(ElementName:="EntityType", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property EntityType() As List(Of EntityType)
            Get
                Return m_EntityType
            End Get
            Set
                m_EntityType = Value
            End Set
        End Property


        Private m_EntityType As List(Of EntityType)

        <XmlElement(ElementName:="Association", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property Association() As List(Of Association)
            Get
                Return m_Association
            End Get
            Set
                m_Association = Value
            End Set
        End Property


        Private m_Association As List(Of Association)

        <XmlAttribute(AttributeName:="xmlns")>
        Public Property Xmlns() As String
            Get
                Return m_Xmlns
            End Get
            Set
                m_Xmlns = Value
            End Set
        End Property


        Private m_Xmlns As String

        <XmlAttribute(AttributeName:="Namespace")>
        Public Property [Namespace]() As String
            Get
                Return m_Namespace
            End Get
            Set
                m_Namespace = Value
            End Set
        End Property


        Private m_Namespace As String

        <XmlElement(ElementName:="EntityContainer", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property EntityContainer() As EntityContainer
            Get
                Return m_EntityContainer
            End Get
            Set
                m_EntityContainer = Value
            End Set
        End Property

        Private m_EntityContainer As EntityContainer
    End Class

    <XmlRoot(ElementName:="EntitySet", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class EntitySet


        <XmlAttribute(AttributeName:="Name")>
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set
                m_Name = Value
            End Set
        End Property


        Private m_Name As String


        <XmlAttribute(AttributeName:="EntityType")>
        Public Property EntityType() As String
            Get
                Return m_EntityType
            End Get
            Set
                m_EntityType = Value
            End Set
        End Property

        Private m_EntityType As String
    End Class


    <XmlRoot(ElementName:="AssociationSet", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class AssociationSet


        <XmlElement(ElementName:="End", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property [End]() As List(Of [End])
            Get
                Return m_End
            End Get
            Set
                m_End = Value
            End Set
        End Property

        Private m_End As List(Of [End])


        <XmlAttribute(AttributeName:="Name")>
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set
                m_Name = Value
            End Set
        End Property


        Private m_Name As String

        <XmlAttribute(AttributeName:="Association")>
        Public Property Association() As String
            Get
                Return m_Association
            End Get
            Set
                m_Association = Value
            End Set
        End Property

        Private m_Association As String
    End Class


    <XmlRoot(ElementName:="EntityContainer", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
    Public Class EntityContainer

        <XmlElement(ElementName:="EntitySet", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property EntitySet() As List(Of EntitySet)
            Get
                Return m_EntitySet
            End Get
            Set
                m_EntitySet = Value
            End Set
        End Property


        Private m_EntitySet As List(Of EntitySet)

        <XmlElement(ElementName:="AssociationSet", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property AssociationSet() As List(Of AssociationSet)
            Get
                Return m_AssociationSet
            End Get
            Set
                m_AssociationSet = Value
            End Set
        End Property


        Private m_AssociationSet As List(Of AssociationSet)

        <XmlAttribute(AttributeName:="p6", [Namespace]:="http://www.w3.org/2000/xmlns/")>
        Public Property P6() As String
            Get
                Return m_P6
            End Get
            Set
                m_P6 = Value
            End Set
        End Property


        Private m_P6 As String

        <XmlAttribute(AttributeName:="Name")>
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set
                m_Name = Value
            End Set
        End Property

        Private m_Name As String


        <XmlAttribute(AttributeName:="IsDefaultEntityContainer", [Namespace]:="http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")>
        Public Property IsDefaultEntityContainer() As String
            Get
                Return m_IsDefaultEntityContainer
            End Get
            Set
                m_IsDefaultEntityContainer = Value
            End Set
        End Property



        Private m_IsDefaultEntityContainer As String

        <XmlAttribute(AttributeName:="LazyLoadingEnabled", [Namespace]:="http://schemas.microsoft.com/ado/2009/02/edm/annotation")>
        Public Property LazyLoadingEnabled() As String
            Get
                Return m_LazyLoadingEnabled
            End Get
            Set
                m_LazyLoadingEnabled = Value
            End Set
        End Property

        Private m_LazyLoadingEnabled As String
    End Class


    <XmlRoot(ElementName:="DataServices", [Namespace]:="http://schemas.microsoft.com/ado/2007/06/edmx")>
    Public Class DataServices


        <XmlElement(ElementName:="Schema", [Namespace]:="http://schemas.microsoft.com/ado/2008/09/edm")>
        Public Property Schema() As List(Of Schema)
            Get
                Return m_Schema
            End Get
            Set
                m_Schema = Value
            End Set
        End Property


        Private m_Schema As List(Of Schema)


        <XmlAttribute(AttributeName:="m", [Namespace]:="http://www.w3.org/2000/xmlns/")>
        Public Property M() As String
            Get
                Return m_M
            End Get
            Set
                m_M = Value
            End Set
        End Property


        Private m_M As String

        <XmlAttribute(AttributeName:="DataServiceVersion", [Namespace]:="http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")>
        Public Property DataServiceVersion() As String
            Get
                Return m_DataServiceVersion
            End Get
            Set
                m_DataServiceVersion = Value
            End Set
        End Property


        Private m_DataServiceVersion As String

        <XmlAttribute(AttributeName:="MaxDataServiceVersion", [Namespace]:="http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")>
        Public Property MaxDataServiceVersion() As String
            Get
                Return m_MaxDataServiceVersion
            End Get
            Set
                m_MaxDataServiceVersion = Value
            End Set
        End Property


        Private m_MaxDataServiceVersion As String

    End Class


    <XmlRoot(ElementName:="Edmx", [Namespace]:="http://schemas.microsoft.com/ado/2007/06/edmx")>
    Public Class EdmxRoot


        <XmlElement(ElementName:="DataServices", [Namespace]:="http://schemas.microsoft.com/ado/2007/06/edmx")>
        Public Property DataServices() As DataServices
            Get
                Return m_DataServices
            End Get
            Set
                m_DataServices = Value
            End Set
        End Property


        Private m_DataServices As DataServices

        <XmlAttribute(AttributeName:="edmx", [Namespace]:="http://www.w3.org/2000/xmlns/")>
        Public Property Edmx() As String
            Get
                Return m_Edmx
            End Get
            Set
                m_Edmx = Value
            End Set
        End Property


        Private m_Edmx As String

        <XmlAttribute(AttributeName:="Version")>
        Public Property Version() As String
            Get
                Return m_Version
            End Get
            Set
                m_Version = Value
            End Set
        End Property

        Private m_Version As String

    End Class

End Namespace
