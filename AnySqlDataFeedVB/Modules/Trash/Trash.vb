
Public Class Trash


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


End Class
