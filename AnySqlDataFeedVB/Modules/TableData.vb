
Imports System.Xml.Serialization
Imports System.Collections.Generic
Imports System.Xml
Imports System.Xml.Schema


Namespace AnySqlDataFeed.XML


    ' http://xmltocsharp.azurewebsites.net/
    Public Class TableData


        <XmlRoot(ElementName:="properties", [Namespace]:="http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")>
        Public Class MyProperties
            Implements IXmlSerializable
            ' public IB MyB { get; set; }
            ' public System.Data.DataSet MyB;

            Private m_schema As System.Data.DataTable
            Private m_data As System.Data.DataRow
            Private m_XmlEndcoder As MyXmlEncoder = New MyXmlEncoder()

            Public Sub New()
                Me.m_XmlEndcoder = New MyXmlEncoder()
            End Sub


            Public Sub New(schema As System.Data.DataTable, data As System.Data.DataRow)
                Me.m_schema = schema
                Me.m_data = data
                Me.m_XmlEndcoder = New MyXmlEncoder()
            End Sub


            Private Function GetSchema() As XmlSchema Implements IXmlSerializable.GetSchema
                Throw New NotImplementedException()
            End Function


            Private Sub ReadXml(reader As XmlReader) Implements IXmlSerializable.ReadXml
                ' deserialize other member attributes

                ' SeekElement(reader, "MyB");
                ' string typeName = reader.GetAttribute("Type");

                ' Somehow need to the type based on the typename. From potentially 
                'an external assembly. Is it possible to use the extra types passed 
                'into an XMlSerializer Constructor???
                ' Type bType = ???

                ' Somehow then need to deserialize B's Members
                ' Deserialize X
                ' Deserialize Y
            End Sub


            Protected Shared IsDevelopment As Boolean = StringComparer.OrdinalIgnoreCase.Equals(System.Environment.UserDomainName, "COR")

            Public Shared Sub OptionallyDecryptPassword(ByRef data As String, columnName As String)
                If StringComparer.OrdinalIgnoreCase.Equals(columnName, "AD_Password") OrElse StringComparer.OrdinalIgnoreCase.Equals(columnName, "BE_Passwort") Then
                    Try
                        If Not String.IsNullOrEmpty(data) Then
                            Dim data2 As String = Tools.Cryptography.DES.DeCrypt(data)
                            data = data2
                        End If
                        ' Who cares
                    Catch generatedExceptionName As Exception

                    End Try
                End If
            End Sub





            Public Sub WriteXml(writer As System.Xml.XmlWriter) Implements IXmlSerializable.WriteXml
                Try
                    For Each dr As System.Data.DataRow In m_schema.Rows
                        Dim columnName As String = System.Convert.ToString(dr("column_name"))
                        Dim entityType As String = System.Convert.ToString(dr("EntityType"))
                        Dim data As String = Nothing

                        ' 2014-11-26T12:30:53.967
                        If Object.ReferenceEquals(m_data.Table.Columns(columnName).DataType, GetType(DateTime)) Then
                            If m_data(columnName) IsNot System.DBNull.Value Then
                                Dim dat As System.DateTime = DirectCast(m_data(columnName), System.DateTime)
                                data = dat.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", System.Globalization.CultureInfo.InvariantCulture)
                            End If
                        ElseIf Object.ReferenceEquals(m_data.Table.Columns(columnName).DataType, GetType(Byte())) Then
                            If m_data(columnName) IsNot System.DBNull.Value Then
                                Dim dat As Byte() = DirectCast(m_data(columnName), Byte())

                                If dat IsNot Nothing Then
                                    data = System.Convert.ToBase64String(dat)
                                End If

                            End If
                        Else
                            If m_data(columnName) IsNot System.DBNull.Value Then
                                data = System.Convert.ToString(m_data(columnName), System.Globalization.CultureInfo.InvariantCulture)
                            End If
                        End If

                        ' LoLz
                        If IsDevelopment Then
                            OptionallyDecryptPassword(data, columnName)
                        End If

                        writer.WriteStartElement(Convert.ToString("d:") & columnName)

                        If Not StringComparer.Ordinal.Equals(entityType, "Edm.String") Then
                            writer.WriteAttributeString("m:type", entityType)
                        End If

                        If String.IsNullOrEmpty(data) Then
                            writer.WriteAttributeString("m:null", "true")
                        End If

                        If data IsNot Nothing Then
                            'writer.WriteValue(data)

                            'If StringComparer.OrdinalIgnoreCase.Equals(columnName, "KU_Bemerkung") Then
                            '    System.Console.WriteLine(data)
                            'End If

                            'Dim str As String = System.Security.SecurityElement.Escape(data)
                            Dim str As String = m_XmlEndcoder.XmlEscape(data)
                            writer.WriteRaw(str)
                        End If

                        writer.WriteEndElement()
                    Next dr
                Catch ex As Exception
                    System.Console.WriteLine(ex.Message)
                    Throw
                End Try


                '' <d:AD_UID m:type="Edm.Guid">6d12a79a-033d-4ca4-8e48-4a5eaa6f6aad</d:AD_UID>
                'writer.WriteStartElement("d:" + "AD_UID")
                'writer.WriteAttributeString("m:type", "Edm.Guid")
                'writer.WriteValue(System.Guid.NewGuid().ToString())
                'writer.WriteEndElement()

                ''<d:AD_User>hbd_cafm</d:AD_User>
                'writer.WriteStartElement("d:" + "AD_User")
                'writer.WriteValue("hbd_cafm")
                'writer.WriteEndElement()

                '' <d:AD_Password>DrpC0u2ZJp0=</d:AD_Password>
                'writer.WriteStartElement("d:" + "AD_Password")
                'writer.WriteValue("DrpC0u2ZJp0=")
                'writer.WriteEndElement()

                '' <d:AD_Level m:type="Edm.Byte">1</d:AD_Level>
                'writer.WriteStartElement("d:" + "AD_Level")
                'writer.WriteAttributeString("m:type", "Edm.Byte")
                'writer.WriteValue(1)
                'writer.WriteEndElement()
            End Sub


            ' http://stackoverflow.com/questions/1132494/string-escape-into-xml/22958657#22958657
            Public Class MyXmlEncoder
                Private m_doc As System.Xml.XmlDocument
                Private m_node As System.Xml.XmlNode

                Public Sub New()
                    m_doc = New System.Xml.XmlDocument()
                    m_node = m_doc.CreateElement("root")
                End Sub


                Public Function XmlEscape(unescaped As String) As String
                    m_node.InnerText = unescaped

                    Dim sanitizedXmlEncodedString As String = m_node.InnerXml.Replace("&#xB;", "")
                    Return sanitizedXmlEncodedString
                End Function


                ' Does not work with Excel...
                Public Shared Function SpecialXmlEscape(input As String) As String
                    'string content = System.Xml.XmlConvert.EncodeName("\t");
                    'string content = System.Security.SecurityElement.Escape("\t");
                    'string strDelimiter = System.Web.HttpUtility.HtmlEncode("\t"); // XmlEscape("\t"); //XmlDecode("&#09;");
                    'strDelimiter = XmlUnescape("&#59;");
                    'Console.WriteLine(strDelimiter);
                    'Console.WriteLine(string.Format("&#{0};", (int)';'));
                    'Console.WriteLine(System.Text.Encoding.ASCII.HeaderName);
                    'Console.WriteLine(System.Text.Encoding.UTF8.HeaderName);


                    Dim strXmlText As String = ""

                    If String.IsNullOrEmpty(input) Then
                        Return input
                    End If


                    Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

                    For i As Integer = 0 To input.Length - 1
                        'sb.AppendFormat("&#{0};", CInt(input(i)))
                        sb.AppendFormat("&#{0};", AscW(input(i)))
                    Next

                    strXmlText = sb.ToString()
                    sb.Length = 0
                    sb = Nothing

                    Return strXmlText
                End Function ' SpecialXmlEscape

            End Class
            



            Private Sub SeekElement(reader As System.Xml.XmlReader, elementName As String)
                ReaderToNextNode(reader)
                While reader.Name <> elementName
                    ReaderToNextNode(reader)
                End While
            End Sub

            Private Sub ReaderToNextNode(reader As System.Xml.XmlReader)
                reader.Read()
                While reader.NodeType = System.Xml.XmlNodeType.Whitespace
                    reader.Read()
                End While
            End Sub


        End Class



        <XmlRoot(ElementName:="title", [Namespace]:="http://www.w3.org/2005/Atom")>
        Public Class Title
            <XmlAttribute(AttributeName:="type")>
            Public Property Type() As String
                Get
                    Return m_Type
                End Get
                Set
                    m_Type = Value
                End Set
            End Property
            Private m_Type As String

            <XmlText>
            Public Property Text() As String
                Get
                    Return m_Text
                End Get
                Set
                    m_Text = Value
                End Set
            End Property
            Private m_Text As String
        End Class

        <XmlRoot(ElementName:="link", [Namespace]:="http://www.w3.org/2005/Atom")>
        Public Class Link
            <XmlAttribute(AttributeName:="rel")>
            Public Property Rel() As String
                Get
                    Return m_Rel
                End Get
                Set
                    m_Rel = Value
                End Set
            End Property
            Private m_Rel As String

            <XmlAttribute(AttributeName:="title")>
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

        <XmlRoot(ElementName:="category", [Namespace]:="http://www.w3.org/2005/Atom")>
        Public Class Category
            <XmlAttribute(AttributeName:="term")>
            Public Property Term() As String
                Get
                    Return m_Term
                End Get
                Set
                    m_Term = Value
                End Set
            End Property
            Private m_Term As String

            <XmlAttribute(AttributeName:="scheme")>
            Public Property Scheme() As String
                Get
                    Return m_Scheme
                End Get
                Set
                    m_Scheme = Value
                End Set
            End Property
            Private m_Scheme As String
        End Class

        <XmlRoot(ElementName:="author", [Namespace]:="http://www.w3.org/2005/Atom")>
        Public Class Author
            <XmlElement(ElementName:="name", [Namespace]:="http://www.w3.org/2005/Atom")>
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

        '
        '        [XmlRoot(ElementName = "AD_UID", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        '        public class AD_UID
        '        {
        '            [XmlAttribute(AttributeName = "type", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
        '            public string Type { get; set; }
        '            [XmlText]
        '            public string Text { get; set; }
        '        }
        '
        '        [XmlRoot(ElementName = "AD_Level", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        '        public class AD_Level
        '        {
        '            [XmlAttribute(AttributeName = "type", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
        '            public string Type { get; set; }
        '            [XmlText]
        '            public string Text { get; set; }
        '        }
        '        


        <XmlRoot(ElementName:="properties", [Namespace]:="http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")>
        Public Class Properties
            ' [XmlElement(ElementName = "AD_UID", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            ' public AD_UID AD_UID { get; set; }

            <XmlElement(ElementName:="AD_User", [Namespace]:="http://schemas.microsoft.com/ado/2007/08/dataservices")>
            Public Property AD_User() As String
                Get
                    Return m_AD_User
                End Get
                Set
                    m_AD_User = Value
                End Set
            End Property
            Private m_AD_User As String

            <XmlElement(ElementName:="AD_Password", [Namespace]:="http://schemas.microsoft.com/ado/2007/08/dataservices")>
            Public Property AD_Password() As String
                Get
                    Return m_AD_Password
                End Get
                Set
                    m_AD_Password = Value
                End Set
            End Property
            Private m_AD_Password As String

            ' [XmlElement(ElementName = "AD_Level", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            ' public AD_Level AD_Level { get; set; }
        End Class

        <XmlRoot(ElementName:="content", [Namespace]:="http://www.w3.org/2005/Atom")>
        Public Class Content
            'public Properties Properties { get; set; }
            <XmlElement(ElementName:="properties", [Namespace]:="http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")>
            Public Property Properties() As MyProperties
                Get
                    Return m_Properties
                End Get
                Set
                    m_Properties = Value
                End Set
            End Property
            Private m_Properties As MyProperties


            <XmlAttribute(AttributeName:="type")>
            Public Property Type() As String
                Get
                    Return m_Type
                End Get
                Set
                    m_Type = Value
                End Set
            End Property
            Private m_Type As String
        End Class

        <XmlRoot(ElementName:="entry", [Namespace]:="http://www.w3.org/2005/Atom")>
        Public Class Entry
            <XmlElement(ElementName:="id", [Namespace]:="http://www.w3.org/2005/Atom")>
            Public Property Id() As String
                Get
                    Return m_Id
                End Get
                Set
                    m_Id = Value
                End Set
            End Property
            Private m_Id As String

            <XmlElement(ElementName:="category", [Namespace]:="http://www.w3.org/2005/Atom")>
            Public Property Category() As Category
                Get
                    Return m_Category
                End Get
                Set
                    m_Category = Value
                End Set
            End Property
            Private m_Category As Category

            <XmlElement(ElementName:="link", [Namespace]:="http://www.w3.org/2005/Atom")>
            Public Property Link() As Link
                Get
                    Return m_Link
                End Get
                Set
                    m_Link = Value
                End Set
            End Property
            Private m_Link As Link

            <XmlElement(ElementName:="title", [Namespace]:="http://www.w3.org/2005/Atom")>
            Public Property Title() As String
                Get
                    Return m_Title
                End Get
                Set
                    m_Title = Value
                End Set
            End Property
            Private m_Title As String

            <XmlElement(ElementName:="updated", [Namespace]:="http://www.w3.org/2005/Atom")>
            Public Property Updated() As String
                Get
                    Return m_Updated
                End Get
                Set
                    m_Updated = Value
                End Set
            End Property
            Private m_Updated As String

            <XmlElement(ElementName:="author", [Namespace]:="http://www.w3.org/2005/Atom")>
            Public Property Author() As Author
                Get
                    Return m_Author
                End Get
                Set
                    m_Author = Value
                End Set
            End Property
            Private m_Author As Author

            <XmlElement(ElementName:="content", [Namespace]:="http://www.w3.org/2005/Atom")>
            Public Property Content() As Content
                Get
                    Return m_Content
                End Get
                Set
                    m_Content = Value
                End Set
            End Property
            Private m_Content As Content
        End Class

        <XmlRoot(ElementName:="feed", [Namespace]:="http://www.w3.org/2005/Atom")>
        Public Class Feed
            <XmlElement(ElementName:="id", [Namespace]:="http://www.w3.org/2005/Atom")>
            Public Property Id() As String
                Get
                    Return m_Id
                End Get
                Set
                    m_Id = Value
                End Set
            End Property
            Private m_Id As String

            <XmlElement(ElementName:="title", [Namespace]:="http://www.w3.org/2005/Atom")>
            Public Property Title() As Title
                Get
                    Return m_Title
                End Get
                Set
                    m_Title = Value
                End Set
            End Property
            Private m_Title As Title

            <XmlElement(ElementName:="updated", [Namespace]:="http://www.w3.org/2005/Atom")>
            Public Property Updated() As String
                Get
                    Return m_Updated
                End Get
                Set
                    m_Updated = Value
                End Set
            End Property
            Private m_Updated As String

            <XmlElement(ElementName:="link", [Namespace]:="http://www.w3.org/2005/Atom")>
            Public Property Link() As Link
                Get
                    Return m_Link
                End Get
                Set
                    m_Link = Value
                End Set
            End Property
            Private m_Link As Link

            <XmlElement(ElementName:="entry", [Namespace]:="http://www.w3.org/2005/Atom")>
            Public Property Entry() As List(Of Entry)
                Get
                    Return m_Entry
                End Get
                Set
                    m_Entry = Value
                End Set
            End Property
            Private m_Entry As List(Of Entry)

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

            <XmlAttribute(AttributeName:="d", [Namespace]:="http://www.w3.org/2000/xmlns/")>
            Public Property D() As String
                Get
                    Return m_D
                End Get
                Set
                    m_D = Value
                End Set
            End Property
            Private m_D As String

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

            <XmlAttribute(AttributeName:="base", [Namespace]:="http://www.w3.org/XML/1998/namespace")>
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
