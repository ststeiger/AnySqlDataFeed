
Imports System.Collections.Generic
Imports System.Text
Imports AnySqlDataFeedVB.AnySqlDataFeed.XML


Namespace AnySqlDataFeed.Feed


    Public Class TableDataTest


        ' https://msdn.microsoft.com/en-us/library/cc716729(v=vs.110).aspx
        ' https://www.devart.com/dotconnect/oracle/docs/DataTypeMapping.html
        ' https://msdn.microsoft.com/en-us/library/ms131092.aspx
        Public Shared Sub MapDotNetTypes(t As System.Type)
            Dim dict As New System.Collections.Generic.Dictionary(Of System.Type, String)()

            dict.Add(GetType(System.Guid), "Edm.Guid")
            dict.Add(GetType(String), "Edm.String")
            dict.Add(GetType(Byte), "Edm.Byte")
            'dict.Add(typeof(bool), "Edm.Byte"); // TinyInt
            dict.Add(GetType(Boolean), "Edm.Boolean")
            dict.Add(GetType(Integer), "Edm.Int32")
            dict.Add(GetType(Long), "Edm.Int64")
            dict.Add(GetType(DateTime), "Edm.DateTime")
            dict.Add(GetType(Single), "Edm.Double")
            '  .NET Single - SqlReal
            dict.Add(GetType(Double), "Edm.Double")
            ' .NET Double - SqlFloat
            dict.Add(GetType(Byte()), "Edm.Binary")
            dict.Add(GetType(Decimal), "Edm.Decimal")
            ' <Property Name="AP_Price" Type="Edm.Decimal" Precision="18" Scale="2"/>
        End Sub


        Public Shared Sub MapSqlServerTypes(t As System.Type)
            Dim dict As New System.Collections.Generic.Dictionary(Of System.Data.SqlDbType, String)()

            ' System.Data.SqlDbType dbt = System.Data.SqlDbType.;

            dict.Add(System.Data.SqlDbType.UniqueIdentifier, "Edm.Guid")
            dict.Add(System.Data.SqlDbType.NVarChar, "Edm.String")
            dict.Add(System.Data.SqlDbType.NText, "Edm.String")
            dict.Add(System.Data.SqlDbType.VarChar, "Edm.String")
            dict.Add(System.Data.SqlDbType.Text, "Edm.String")

            dict.Add(System.Data.SqlDbType.Bit, "Edm.Boolean")
            dict.Add(System.Data.SqlDbType.TinyInt, "Edm.Byte")
            'dict.Add(typeof(bool), "Edm.Byte"); // TinyInt
            dict.Add(System.Data.SqlDbType.Int, "Edm.Int32")
            dict.Add(System.Data.SqlDbType.BigInt, "Edm.Int64")

            dict.Add(System.Data.SqlDbType.Float, "Edm.Double")
            dict.Add(System.Data.SqlDbType.Real, "Edm.Double")
            dict.Add(System.Data.SqlDbType.[Decimal], "Edm.Decimal")
            ' <Property Name="AP_Price" Type="Edm.Decimal" Precision="18" Scale="2"/>
            dict.Add(System.Data.SqlDbType.SmallDateTime, "Edm.DateTime")
            dict.Add(System.Data.SqlDbType.DateTime, "Edm.DateTime")
            dict.Add(System.Data.SqlDbType.DateTime2, "Edm.DateTime")

            dict.Add(System.Data.SqlDbType.VarBinary, "Edm.Binary")
            dict.Add(System.Data.SqlDbType.Image, "Edm.Binary")
        End Sub


        Public Class PrimaryKeyInfo
            Public ColumnName As String
            Public DotNetType As System.Type
        End Class


        Public Shared Function GetPrimaryKeyInfo(tableName As String, dtTableData As System.Data.DataTable) As PrimaryKeyInfo()
            Dim pkl As New List(Of PrimaryKeyInfo)()

            Dim strSQL As String = vbCr & vbLf & "SELECT  " & vbCr & vbLf & vbTab & " --iskcu.constraint_name" & vbCr & vbLf & vbTab & "--,iskcu.table_schema" & vbCr & vbLf & vbTab & "--,iskcu.table_name" & vbTab & vbCr & vbLf & vbTab & " iskcu.column_name" & vbCr & vbLf & vbTab & "--,isc.ordinal_position" & vbCr & vbLf & vbTab & "--,isc.data_type" & vbCr & vbLf & vbTab & "-- ,*" & vbCr & vbLf & "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS istc " & vbCr & vbLf & vbCr & vbLf & "LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS iskcu " & vbCr & vbLf & vbTab & "ON iskcu.TABLE_SCHEMA = istc.TABLE_SCHEMA" & vbCr & vbLf & vbTab & "AND iskcu.TABLE_NAME = istc.TABLE_NAME" & vbCr & vbLf & vbTab & "AND iskcu.CONSTRAINT_NAME = istc.CONSTRAINT_NAME" & vbCr & vbLf & vbTab & vbCr & vbLf & "LEFT JOIN INFORMATION_SCHEMA.COLUMNS AS isc " & vbCr & vbLf & vbTab & "ON isc.TABLE_SCHEMA = iskcu.TABLE_SCHEMA" & vbCr & vbLf & vbTab & "AND isc.TABLE_NAME = iskcu.TABLE_NAME" & vbCr & vbLf & vbTab & "AND isc.COLUMN_NAME = iskcu.COLUMN_NAME" & vbCr & vbLf & vbTab & vbCr & vbLf & "WHERE (1=1) " & vbCr & vbLf & "AND istc.CONSTRAINT_TYPE = 'PRIMARY KEY' " & vbCr & vbLf & "AND istc.TABLE_NAME LIKE '" + tableName.Replace("'", "''") + "' " & vbCr & vbLf & "AND istc.table_schema NOT IN ('pg_catalog', 'information_schema') " & vbCr & vbLf & vbCr & vbLf & "ORDER BY isc.ORDINAL_POSITION " & vbCr & vbLf

            Using dtPkInfo As System.Data.DataTable = SQL.GetDataTable(strSQL)
                For Each dr As System.Data.DataRow In dtPkInfo.Rows
                    Dim columnName As String = System.Convert.ToString(dr("column_name"))

                    pkl.Add(New PrimaryKeyInfo() With {.ColumnName = columnName, .DotNetType = dtTableData.Columns(columnName).DataType})
                Next dr
            End Using ' dt

            Return pkl.ToArray()
        End Function


        ' datetime'1900-01-01T00%3A00%3A00'
        Public Shared Function CreatePkTemplate(tableName As String, pki As PrimaryKeyInfo()) As String
            If pki.Length = 0 Then
                Throw New NotImplementedException("Case no PK not studied...")
            End If


            If pki.Length = 1 Then
                If Object.ReferenceEquals(pki(0).DotNetType, GetType(System.Guid)) Then
                    Return tableName & Convert.ToString("(guid'{@value0}')")
                End If
                ' guid
                If Object.ReferenceEquals(pki(0).DotNetType, GetType(Integer)) Then
                    Return tableName & Convert.ToString("({@value0})")
                End If
                ' int
                If Object.ReferenceEquals(pki(0).DotNetType, GetType(Long)) Then
                    Return tableName & Convert.ToString("({@value0})")
                End If
                ' long
                If Object.ReferenceEquals(pki(0).DotNetType, GetType(String)) Then
                    Return tableName & Convert.ToString("('{@value0}')")
                End If
                ' string
                If Object.ReferenceEquals(pki(0).DotNetType, GetType(System.DateTime)) Then
                    Return tableName & Convert.ToString("(datetime'{@value0}')")
                End If
                ' string
                Throw New NotImplementedException("CreatePkTemplate for type " + pki(0).DotNetType.Name + " not implemented")
            End If


            Dim str As String = tableName & Convert.ToString("(")
            ' <link rel="edit" title="T_SYS_VisualisierungStichtag" href="T_SYS_VisualisierungStichtag(VIS_DWG='%3F',VIS_Stichtag=datetime'1900-01-01T00%3A00%3A00')" />


            For i As Integer = 0 To pki.Length - 1
                If Object.ReferenceEquals(pki(i).DotNetType, GetType(System.Guid)) Then
                    str += pki(i).ColumnName + "=" + "guid'{@value" + i.ToString() + "}'"
                End If
                ' guid
                If Object.ReferenceEquals(pki(i).DotNetType, GetType(String)) Then
                    str += pki(i).ColumnName + "=" + "'{@value" + i.ToString() + "}'"
                End If
                ' string
                If Object.ReferenceEquals(pki(i).DotNetType, GetType(Integer)) Then
                    str += pki(i).ColumnName + "=" + "{@value" + i.ToString() + "}"
                End If
                ' int
                If Object.ReferenceEquals(pki(i).DotNetType, GetType(Long)) Then
                    str += pki(i).ColumnName + "=" + "{@value" + i.ToString() + "}"
                End If
                ' long
                If Object.ReferenceEquals(pki(i).DotNetType, GetType(System.DateTime)) Then
                    str += pki(i).ColumnName + "=" + "datetime'{@value" + i.ToString() + "}'"
                End If
                ' string
                If i < pki.Length - 1 Then
                    str += ","
                End If
            Next
            str += ")"

            ' return tableName + "T_Benutzer({@value})"; // string
            ' <link rel="edit" title="T_SYS_VisualisierungStichtag" href="T_SYS_VisualisierungStichtag(VIS_DWG='%3F',VIS_Stichtag=datetime'1900-01-01T00%3A00%3A00')" />
            Return str
        End Function


        Public Shared Function GetColumnInfo(table_name As String) As System.Data.DataTable
            Dim strSQL As String = vbCr & vbLf & "-- DECLARE @__in_table_name nvarchar(128)" & vbCr & vbLf & "-- SET @__in_table_name = N'___AllTypes' " & vbCr & vbLf & vbCr & vbLf & vbCr & vbLf & "SELECT " & vbCr & vbLf & vbTab & "--  table_schema" & vbCr & vbLf & vbTab & " --table_name," & vbCr & vbLf & vbTab & " column_name" & vbCr & vbLf & vbTab & "-- ,ordinal_position" & vbCr & vbLf & vbTab & "-- ,column_default" & vbCr & vbLf & vbTab & "-- ,is_nullable" & vbCr & vbLf & vbTab & ",data_type" & vbCr & vbLf & vbTab & vbCr & vbLf & vbTab & ",CASE WHEN is_nullable = 'YES'" & vbCr & vbLf & vbTab & vbTab & "THEN NULL " & vbCr & vbLf & vbTab & vbTab & "ELSE 'false' " & vbCr & vbLf & vbTab & "END AS EntityNullable" & vbCr & vbLf & vbTab & vbCr & vbLf & vbTab & ",CASE " & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE IN ('bigint', 'bigserial') THEN 'Edm.Int64'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE = 'binary' THEN 'Edm.Binary'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE IN ('bit', 'boolean') THEN 'Edm.Boolean'" & vbCr & vbLf & vbTab & vbTab & "WHEN data_type IN ('char', 'nchar','varchar', 'nvarchar','text', 'ntext', 'character varying', 'national character varying') THEN 'Edm.String' " & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE = 'date' THEN 'Edm.DateTime'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE = 'smalldatetime' THEN 'Edm.DateTime'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE IN ('datetime') THEN 'Edm.DateTime'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE IN ('datetime2', 'timestamp without time zone') THEN 'Edm.DateTime'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE IN ('datetimeoffset', 'timestamp with time zone') THEN 'Edm.DateTimeOffset'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE = 'decimal' THEN 'Edm.Decimal'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE IN ('float', 'double', 'double precision') THEN 'Edm.Double'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE = 'image' THEN 'Edm.Binary'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE IN ('int', 'integer', 'serial') THEN 'Edm.Int32'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE = 'money' THEN 'Edm.Decimal'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE = 'numeric' THEN 'Edm.Decimal'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE = 'real' THEN 'Edm.Single'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE = 'smallint' THEN 'Edm.Int16'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE = 'smallmoney' THEN 'Edm.Decimal'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE IN ('time', 'time with timezone', 'time without timezone') THEN 'Edm.Time'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE = 'timestamp' THEN 'Edm.Binary'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE = 'tinyint' THEN 'Edm.Byte'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE IN ('uniqueidentifier', 'uuid') THEN 'Edm.Guid'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE IN ('varbinary', 'bytea') THEN 'Edm.Binary'" & vbCr & vbLf & vbTab & vbTab & "WHEN DATA_TYPE = 'xml' THEN 'Edm.String'" & vbCr & vbLf & vbTab & vbTab & vbCr & vbLf & vbTab & vbTab & "ELSE 'n.def.'" & vbCr & vbLf & vbTab & "END AS EntityType " & vbCr & vbLf & vbTab & vbCr & vbLf & vbTab & ",CASE " & vbCr & vbLf & vbTab & vbTab & "WHEN data_type IN ('image', 'xml') THEN 'Max' " & vbCr & vbLf & vbTab & vbTab & "WHEN data_type IN ('char', 'nchar','varchar', 'nvarchar','text', 'ntext', 'character varying', 'national character varying')" & vbCr & vbLf & vbTab & vbTab & vbTab & " AND (character_maximum_length > 100000 OR character_maximum_length < 0 OR DATA_TYPE IN ('text', 'ntext'))" & vbCr & vbLf & vbTab & vbTab & vbTab & vbTab & "THEN 'Max' " & vbCr & vbLf & vbTab & vbTab & "WHEN data_type IN ('char', 'nchar','varchar', 'nvarchar','text', 'ntext', 'character varying', 'national character varying')" & vbCr & vbLf & vbTab & vbTab & vbTab & "THEN CAST(character_maximum_length AS varchar(30)) " & vbCr & vbLf & vbTab & vbTab & "WHEN data_type IN ('binary', 'varbinary', 'bytea')" & vbCr & vbLf & vbTab & vbTab & vbTab & "THEN " & vbCr & vbLf & vbTab & vbTab & vbTab & vbTab & "CASE " & vbCr & vbLf & vbTab & vbTab & vbTab & vbTab & vbTab & "WHEN character_octet_length < 0 THEN 'Max' " & vbCr & vbLf & vbTab & vbTab & vbTab & vbTab & vbTab & "ELSE CAST(character_octet_length AS varchar(30)) " & vbCr & vbLf & vbTab & vbTab & vbTab & vbTab & "END " & vbCr & vbLf & vbTab & vbTab & "ELSE NULL " & vbCr & vbLf & vbTab & "END AS EntityMaxLength" & vbCr & vbLf & vbTab & vbCr & vbLf & vbTab & "," & vbCr & vbLf & vbTab & "CASE " & vbCr & vbLf & vbTab & vbTab & "WHEN data_type IN ('image', 'xml') THEN 'false' " & vbCr & vbLf & vbTab & vbTab & "WHEN data_type IN ('varchar', 'nvarchar','text', 'ntext', 'varbinary', 'bytea', 'xml', 'character varying', 'national character varying') THEN 'false' " & vbCr & vbLf & vbTab & vbTab & "WHEN data_type IN ('char', 'nchar', 'binary') THEN 'true' " & vbCr & vbLf & vbTab & vbTab & "ELSE NULL " & vbCr & vbLf & vbTab & "END AS EntityFixedLength" & vbCr & vbLf & vbTab & vbCr & vbLf & vbTab & ",CASE " & vbCr & vbLf & vbTab & vbTab & vbCr & vbLf & vbTab & vbTab & "WHEN EXISTS(SELECT * FROM information_schema.tables WHERE table_schema = 'pg_catalog') AND data_type IN ('character varying', 'national character varying', 'text') THEN 'true' -- PostGre uses UTF-8 always" & vbCr & vbLf & vbTab & vbTab & "WHEN data_type IN ('nchar', 'nvarchar', 'ntext', 'xml') THEN 'true' " & vbCr & vbLf & vbTab & vbTab & "WHEN data_type IN ('char', 'varchar', 'text') THEN 'false'" & vbCr & vbLf & vbTab & vbTab & "ELSE NULL " & vbCr & vbLf & vbTab & "END AS EntityUnicode " & vbCr & vbLf & vbTab & vbCr & vbLf & vbTab & "," & vbCr & vbLf & vbTab & "CASE " & vbCr & vbLf & vbTab & vbTab & "WHEN data_type IN ('bigint', 'biginteger', 'bigserial', 'int', 'integer', 'serial', 'smallint', 'tinyint') THEN NULL " & vbCr & vbLf & vbTab & vbTab & "WHEN datetime_precision IS NOT NULL THEN datetime_precision " & vbCr & vbLf & vbTab & vbTab & "ELSE numeric_precision " & vbCr & vbLf & vbTab & "END AS EntityPrecision " & vbCr & vbLf & vbTab & vbCr & vbLf & vbTab & "," & vbCr & vbLf & vbTab & "CASE " & vbCr & vbLf & vbTab & vbTab & "WHEN data_type IN ('bigint', 'biginteger', 'bigseial', 'int', 'integer', 'serial', 'smallint', 'tinyint') THEN NULL " & vbCr & vbLf & vbTab & vbTab & "ELSE numeric_scale " & vbCr & vbLf & vbTab & "END AS EntityScale" & vbCr & vbLf & vbTab & vbCr & vbLf & vbTab & "-- ,character_maximum_length" & vbCr & vbLf & vbTab & "-- ,character_octet_length" & vbCr & vbLf & vbTab & "-- ,numeric_precision" & vbCr & vbLf & vbTab & "-- ,numeric_precision_radix" & vbCr & vbLf & vbCr & vbLf & vbTab & "-- ,character_set_catalog" & vbCr & vbLf & vbTab & "-- ,character_set_schema" & vbCr & vbLf & vbTab & "-- ,character_set_name" & vbCr & vbLf & vbTab & "-- ,collation_catalog" & vbCr & vbLf & vbTab & "-- ,collation_schema" & vbCr & vbLf & vbTab & "-- ,collation_name" & vbCr & vbLf & vbTab & "-- ,*" & vbCr & vbLf & "FROM INFORMATION_SCHEMA.COLUMNS " & vbCr & vbLf & vbCr & vbLf & "WHERE (1=1) " & vbCr & vbLf & "-- AND TABLE_NAME = @__in_table_name " & vbCr & vbLf & "--AND TABLE_NAME = 't_blogpost' " & vbCr & vbLf & "AND TABLE_NAME = '" + table_name.Replace("'", "''") + "' " & vbCr & vbLf & vbCr & vbLf & "AND table_name NOT IN ('sysdiagrams', 'dtproperties') " & vbCr & vbLf & "AND table_schema NOT IN ('pg_catalog', 'information_schema') " & vbCr & vbLf & vbCr & vbLf & "ORDER BY ORDINAL_POSITION " & vbCr & vbLf

            Return SQL.GetDataTable(strSQL)
        End Function


        Public Shared Sub Test(table_name As String, tw As System.IO.TextWriter)
            '
            '            var set = new System.Xml.XmlWriterSettings();
            '            set.Indent = true;
            '
            '            using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(@"D:\employees.xml", set))
            '            {
            '                writer.WriteStartDocument();
            '                writer.WriteStartElement("Employees"); // <-- Important root element
            '
            '                writer.WriteAttributeString("localname", "value");
            '                writer.WriteValue("Foobar");
            '
            '                writer.WriteEndElement();              // <-- Closes it
            '                writer.WriteEndDocument();
            '            }
            '            



            ' table_name = "T_Admin";


            Const format As String = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"

            ' If you're using a debugger this will rightfully throw an exception
            ' with .NET 3.5 SP1 because 'z' is for local time only; however, the exception
            ' asks me to use the 'Z' specifier for UTC times, but it doesn't exist, so it
            ' just spits out 'Z' as a literal.
            ' string updatetime = "2015-08-26T15:29:19Z";
            Dim updatetime As String = System.DateTime.UtcNow.ToString(format, System.Globalization.CultureInfo.InvariantCulture)


            Dim feed As New TableData.Feed()

            Dim url As System.Uri = System.Web.HttpContext.Current.Request.Url
            Dim baseURL As String = url.Scheme + "://" + url.Authority + (System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath + "/").Replace("//", "/") + "ajax/ExcelDataFeed.ashx/"


            ' feed.Base = "http://localhost:5570/ExcelDataFeed.svc/";
            feed.Base = baseURL

            If Environment.OSVersion.Platform <> PlatformID.Unix Then
                feed.Xmlns = "http://www.w3.org/2005/Atom"
            End If

            feed.D = "http://schemas.microsoft.com/ado/2007/08/dataservices"
            feed.M = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata"

            ' feed.Id = "http://localhost:5570/ExcelDataFeed.svc/T_Admin";
            feed.Id = baseURL & table_name




            feed.Title = New TableData.Title()
            feed.Title.Type = "text"
            feed.Title.Text = table_name

            feed.Updated = updatetime

            feed.Link = New TableData.Link()
            feed.Link.Rel = "self"
            feed.Link.Title = table_name
            feed.Link.Href = table_name

            feed.Entry = New List(Of TableData.Entry)()

            Using dtColumnInfo As System.Data.DataTable = GetColumnInfo(table_name)

                Using dtTableData As System.Data.DataTable = SQL.GetDataTable(Convert.ToString("SELECT * FROM ") & table_name)

                    Dim pki As PrimaryKeyInfo() = GetPrimaryKeyInfo(table_name, dtTableData)
                    Dim pkTemplate As String = CreatePkTemplate(table_name, pki)

                    Dim cnt As Integer = dtTableData.Rows.Count
                    For i As Integer = 0 To cnt - 1

                        Dim thisPKvalue As String = pkTemplate

                        For j As Integer = 0 To pki.Length - 1
                            Dim str As String = System.Convert.ToString(dtTableData.Rows(i)(pki(j).ColumnName))
                            thisPKvalue = thisPKvalue.Replace("{@value" + j.ToString() + "}", str)
                        Next


                        Dim ent As New TableData.Entry()

                        ' ent.Id = "http://localhost:5570/ExcelDataFeed.svc/T_Admin(guid'6d12a79a-033d-4ca4-8e48-4a5eaa6f6aad')";
                        ent.Id = baseURL & thisPKvalue

                        ent.Category = New TableData.Category()
                        ent.Category.Term = Convert.ToString("COR_Basic_DemoModel.") & table_name
                        ent.Category.Scheme = "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme"

                        ent.Link = New TableData.Link()
                        ent.Link.Rel = "edit"
                        ent.Link.Title = table_name

                        ent.Link.Href = thisPKvalue
                        ent.Updated = updatetime
                        ent.Title = ""

                        ent.Author = New TableData.Author()
                        ent.Author.Name = ""

                        ent.Content = New TableData.Content()
                        ent.Content.Type = "application/xml"


                        'ent.Content.Properties = new TableData.Properties();
                        ent.Content.Properties = New TableData.MyProperties(dtColumnInfo, dtTableData.Rows(i))

                        feed.Entry.Add(ent)
                    Next


                    '
                    '                    // Serialze here ? 
                    '                    var x = feed;
                    '                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(x.GetType());
                    '
                    '                    using (System.IO.Stream strm = new System.IO.FileStream(@"D:\lolz.txt", System.IO.FileMode.Create))
                    '                        serializer.Serialize(strm, x);
                    '                     

                    Tools.XML.Serialization.SerializeToXml(feed, tw)

                End Using
            End Using
            ' End Using dtColumnInfo 
            ' Service x = new Service();

        End Sub

    End Class


    <System.Xml.Serialization.XmlRoot(ElementName:="service", [Namespace]:="AppNamespace")>
    Public Class Service
        <System.Xml.Serialization.XmlAttribute(AttributeName:="xmlns")>
        Public Property Xmlns() As String
            Get
                Return m_Xmlns
            End Get
            Set
                m_Xmlns = Value
            End Set
        End Property
        Private m_Xmlns As String

        <System.Xml.Serialization.XmlAttribute(AttributeName:="atom", [Namespace]:="AtomXmlns")>
        Public Property Atom() As String
            Get
                Return m_Atom
            End Get
            Set
                m_Atom = Value
            End Set
        End Property
        Private m_Atom As String

        <System.Xml.Serialization.XmlAttribute(AttributeName:="base", [Namespace]:="BaseXmlns")>
        Public Property Base() As String
            Get
                Return m_Base
            End Get
            Set
                m_Base = Value
            End Set
        End Property
        Private m_Base As String

        <System.Xml.Serialization.XmlElement>
        Public aaaa As XML.TableData.MyProperties = New TableData.MyProperties()
    End Class


End Namespace
