

Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Diagnostics


Namespace Tools.XML


    ' http://www.switchonthecode.com/tutorials/csharp-tutorial-xml-serialization
    ' http://www.codeproject.com/KB/XML/xml_serializationasp.aspx
    Public Class Serialization


        Public Shared Sub SerializeToXml(Of T)(ThisTypeInstance As T, strFileNameAndPath As String)
            SerializeToXml(Of T)(ThisTypeInstance, New System.IO.StreamWriter(strFileNameAndPath))
        End Sub ' SerializeToXml


        Public NotInheritable Class StringWriterWithEncoding
            Inherits System.IO.StringWriter
            Private ReadOnly m_encoding As System.Text.Encoding


            Public Sub New(sb As System.Text.StringBuilder)
                MyBase.New(sb)
                Me.m_encoding = System.Text.Encoding.Unicode
            End Sub


            Public Sub New(encoding As System.Text.Encoding)
                Me.m_encoding = encoding
            End Sub

            Public Sub New(sb As System.Text.StringBuilder, encoding As System.Text.Encoding)
                MyBase.New(sb)
                Me.m_encoding = encoding
            End Sub

            Public Overrides ReadOnly Property Encoding() As System.Text.Encoding
                Get
                    Return m_encoding
                End Get
            End Property
        End Class



        Public Shared Function SerializeToXml(Of T)(ThisTypeInstance As T) As String
            Dim sb As New System.Text.StringBuilder()
            Dim strReturnValue As String = Nothing

            'SerializeToXml<T>(ThisTypeInstance, new System.IO.StringWriter(sb));
            SerializeToXml(Of T)(ThisTypeInstance, New StringWriterWithEncoding(sb, System.Text.Encoding.UTF8))

            strReturnValue = sb.ToString()
            sb = Nothing

            Return strReturnValue
        End Function ' SerializeToXml


        Public Shared Function GetSerializerNamespaces(t As Type) As System.Xml.Serialization.XmlSerializerNamespaces
            Dim ns As New System.Xml.Serialization.XmlSerializerNamespaces()

            Dim attribs As Object() = t.GetCustomAttributes(GetType(System.Xml.Serialization.XmlRootAttribute), False)
            Dim hasXmlRootAttribute As Boolean = attribs.Length > 0
            If hasXmlRootAttribute Then
                Dim xa As System.Xml.Serialization.XmlRootAttribute = DirectCast(attribs(0), System.Xml.Serialization.XmlRootAttribute)
                Dim [nameSpace] As String = xa.[Namespace]
                ns.Add("", xa.[Namespace])
            Else
                ns.Add("", "")
            End If

            Return ns
        End Function


        Public Shared Sub SerializeToXml(Of T)(ThisTypeInstance As T, strm As System.IO.Stream)
            Dim ns As System.Xml.Serialization.XmlSerializerNamespaces = GetSerializerNamespaces(GetType(T))
            Dim serializer As New System.Xml.Serialization.XmlSerializer(GetType(T))
            serializer.Serialize(strm, ThisTypeInstance, ns)
            serializer = Nothing
        End Sub


        Public Shared Sub SerializeToXml(Of T)(ThisTypeInstance As T, tw As System.IO.TextWriter)
            If ThisTypeInstance Is Nothing Then
                Throw New NullReferenceException("ThisTypeInstance")
            End If

            Dim ns As System.Xml.Serialization.XmlSerializerNamespaces = GetSerializerNamespaces(ThisTypeInstance.GetType())
            Dim serializer As New System.Xml.Serialization.XmlSerializer(ThisTypeInstance.GetType())

            Using twTextWriter As System.IO.TextWriter = tw
                ' serializer.Serialize(twTextWriter, ThisTypeInstance)
                serializer.Serialize(twTextWriter, ThisTypeInstance, ns)

                twTextWriter.Close()
            End Using ' twTextWriter

            serializer = Nothing
        End Sub
        ' End Sub SerializeToXml

        Public Shared Function DeserializeXmlFromFile(Of T)(strFileNameAndPath As String) As T
            Dim tReturnValue As T = Nothing

            Using fstrm As New System.IO.FileStream(strFileNameAndPath, System.IO.FileMode.Open, System.IO.FileAccess.Read)
                tReturnValue = DeserializeXmlFromStream(Of T)(fstrm)
                fstrm.Close()
            End Using ' fstrm
            Return tReturnValue
        End Function '  DeserializeXmlFromFile

        Public Shared Function DeserializeXmlFromEmbeddedRessource(Of T)(strRessourceName As String) As T
            Dim tReturnValue As T = Nothing

            Dim ass As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()


            Using fstrm As System.IO.Stream = ass.GetManifestResourceStream(strRessourceName)
                tReturnValue = DeserializeXmlFromStream(Of T)(fstrm)
                fstrm.Close()
            End Using ' fstrm

            Return tReturnValue
        End Function ' DeserializeXmlFromEmbeddedRessource

        Public Shared Function DeserializeXmlFromString(Of T)(s As String) As T
            Dim tReturnValue As T = Nothing

            Using stream As New System.IO.MemoryStream()
                Using writer As New System.IO.StreamWriter(stream)
                    writer.Write(s)
                    writer.Flush()
                    stream.Position = 0

                    tReturnValue = DeserializeXmlFromStream(Of T)(stream)
                End Using ' writer
            End Using '  stream
            Return tReturnValue
        End Function ' DeserializeXmlFromString

        Public Shared Function DeserializeXmlFromStream(Of T)(strm As System.IO.Stream) As T
            Dim deserializer As New System.Xml.Serialization.XmlSerializer(GetType(T))
            Dim ThisType As T = Nothing

            Using srEncodingReader As New System.IO.StreamReader(strm, System.Text.Encoding.UTF8)
                ThisType = DirectCast(deserializer.Deserialize(srEncodingReader), T)
                srEncodingReader.Close()
            End Using ' srEncodingReader

            deserializer = Nothing

            Return ThisType
        End Function ' DeserializeXmlFromStream

#If notneeded Then

		Public Shared Sub SerializeToXML(Of T)(ThisTypeInstance As System.Collections.Generic.List(Of T), strConfigFileNameAndPath As String)
			Dim serializer As New System.Xml.Serialization.XmlSerializer(GetType(System.Collections.Generic.List(Of T)))

			Using textWriter As System.IO.TextWriter = New System.IO.StreamWriter(strConfigFileNameAndPath)
				serializer.Serialize(textWriter, ThisTypeInstance)
				textWriter.Close()
			End Using

			serializer = Nothing
		End Sub
		' SerializeToXML


		Public Shared Function DeserializeXmlFromFileAsList(Of T)(strFileNameAndPath As String) As System.Collections.Generic.List(Of T)
			Dim deserializer As New System.Xml.Serialization.XmlSerializer(GetType(System.Collections.Generic.List(Of T)))
			Dim ThisTypeList As System.Collections.Generic.List(Of T) = Nothing

			Using srEncodingReader As New System.IO.StreamReader(strFileNameAndPath, System.Text.Encoding.UTF8)
				ThisTypeList = DirectCast(deserializer.Deserialize(srEncodingReader), System.Collections.Generic.List(Of T))
				srEncodingReader.Close()
			End Using

			deserializer = Nothing

			Return ThisTypeList
		End Function
		' DeserializeXmlFromFileAsList

#End If

    End Class ' Serialization


End Namespace ' COR.Tools.XML
