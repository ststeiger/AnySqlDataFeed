
Imports System.Xml.Serialization
Imports System.Collections.Generic


Namespace AnySqlDataFeed.XML


    ' http://xmltocsharp.azurewebsites.net/
    Public Class TableList

        Const AppNamespace As String = "http://www.w3.org/2007/app"
        Const AtomNamespace As String = "http://www.w3.org/2005/Atom"

        Const AtomXmlns As String = "http://www.w3.org/2000/xmlns/"
        Const BaseXmlns As String = "http://www.w3.org/XML/1998/namespace"


        <XmlRoot(ElementName:="collection", [Namespace]:=AppNamespace)>
        Public Class Collection
            <XmlElement(ElementName:="title", [Namespace]:=AtomNamespace)>
            Public Property Title() As String
                Get
                    Return m_Title
                End Get
                Set
                    m_Title = Value
                End Set
            End Property
            Private m_Title As String

            <XmlAttribute(AttributeName:="href")>
            Public Property Href() As String
                Get
                    Return m_Href
                End Get
                Set
                    m_Href = Value
                End Set
            End Property
            Private m_Href As String
        End Class


        <XmlRoot(ElementName:="workspace", [Namespace]:=AppNamespace)>
        Public Class Workspace
            <XmlElement(ElementName:="title", [Namespace]:=AtomNamespace)>
            Public Property Title() As String
                Get
                    Return m_Title
                End Get
                Set
                    m_Title = Value
                End Set
            End Property
            Private m_Title As String

            <XmlElement(ElementName:="collection", [Namespace]:=AppNamespace)>
            Public Property Collection() As List(Of Collection)
                Get
                    Return m_Collection
                End Get
                Set
                    m_Collection = Value
                End Set
            End Property
            Private m_Collection As List(Of Collection)
        End Class


        <XmlRoot(ElementName:="service", [Namespace]:=AppNamespace)>
        Public Class Service
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

            <XmlAttribute(AttributeName:="atom", [Namespace]:=AtomXmlns)>
            Public Property Atom() As String
                Get
                    Return m_Atom
                End Get
                Set
                    m_Atom = Value
                End Set
            End Property
            Private m_Atom As String

            <XmlElement(ElementName:="workspace", [Namespace]:=AppNamespace)>
            Public Property Workspace() As Workspace
                Get
                    Return m_Workspace
                End Get
                Set
                    m_Workspace = Value
                End Set
            End Property
            Private m_Workspace As Workspace

            <XmlAttribute(AttributeName:="base", [Namespace]:=BaseXmlns)>
            Public Property Base() As String
                Get
                    Return m_Base
                End Get
                Set
                    m_Base = Value
                End Set
            End Property
            Private m_Base As String
        End Class


    End Class


End Namespace
