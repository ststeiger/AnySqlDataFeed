
using System;
using System.Xml.Serialization;
using System.Collections.Generic;


namespace AnySqlDataFeed.XML
{

    // http://xmltocsharp.azurewebsites.net/
    public class TableData
    {


        [XmlRoot(ElementName = "properties", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
        public class MyProperties : IXmlSerializable
        {
            // public IB MyB { get; set; }
            // public System.Data.DataSet MyB;

            private System.Data.DataTable m_schema;
            private System.Data.DataRow m_data;

            public MyProperties()
            { }


            public MyProperties(System.Data.DataTable schema, System.Data.DataRow data)
            { 
                m_schema = schema;
                m_data = data;
            }


            public System.Xml.Schema.XmlSchema GetSchema()
            {
                throw new NotImplementedException();
            }

            public void ReadXml(System.Xml.XmlReader reader)
            {
                // deserialize other member attributes

                // SeekElement(reader, "MyB");
                // string typeName = reader.GetAttribute("Type");

                // Somehow need to the type based on the typename. From potentially 
                //an external assembly. Is it possible to use the extra types passed 
                //into an XMlSerializer Constructor???
                // Type bType = ???

                // Somehow then need to deserialize B's Members
                // Deserialize X
                // Deserialize Y
            }


            protected static bool IsDevelopment = StringComparer.OrdinalIgnoreCase.Equals(System.Environment.UserDomainName, "COR") || StringComparer.OrdinalIgnoreCase.Equals(Environment.MachineName, "HP15");

            public static void OptionallyDecryptPassword(ref string data, string columnName)
            {

                if (StringComparer.OrdinalIgnoreCase.Equals(columnName, "AD_Password") || StringComparer.OrdinalIgnoreCase.Equals(columnName, "BE_Passwort"))
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(data))
                        {
                            string data2 = Tools.Cryptography.DES.DeCrypt(data);
                            data = data2;
                        }
                    }
                    catch (Exception)
                    {
                        // Who cares
                    }

                }
            }


            public void WriteXml(System.Xml.XmlWriter writer)
            {
                try
                {


                    // serialize other members as attributes

                    foreach (System.Data.DataRow dr in m_schema.Rows)
                    {
                        string columnName = System.Convert.ToString(dr["column_name"]);
                        string entityType = System.Convert.ToString(dr["EntityType"]);
                        string data = null;

                        // 2014-11-26T12:30:53.967
                        if (object.ReferenceEquals(m_data.Table.Columns[columnName].DataType, typeof(DateTime)))
                        {
                            if (m_data[columnName] == System.DBNull.Value)
                                data = null;
                            else
                            {
                                System.DateTime dat = (System.DateTime)m_data[columnName];
                                data = dat.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", System.Globalization.CultureInfo.InvariantCulture);
                            }
                        }
                        else
                            data = System.Convert.ToString(m_data[columnName], System.Globalization.CultureInfo.InvariantCulture);

                        // LoLz
                        if (IsDevelopment)
                            OptionallyDecryptPassword(ref data, columnName);

                        writer.WriteStartElement("d:" + columnName);

                        if (!StringComparer.Ordinal.Equals(entityType, "Edm.String"))
                            writer.WriteAttributeString("m:type", entityType);

                        if (string.IsNullOrEmpty(data))
                            writer.WriteAttributeString("m:null", "true");

                        if (data != null)
                            writer.WriteValue(data);

                        writer.WriteEndElement();
                    }


                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    throw;
                }

                /*
                // <d:AD_UID m:type="Edm.Guid">6d12a79a-033d-4ca4-8e48-4a5eaa6f6aad</d:AD_UID>
                writer.WriteStartElement("d:" + "AD_UID");
                writer.WriteAttributeString("m:type", "Edm.Guid");
                writer.WriteValue(System.Guid.NewGuid().ToString());
                writer.WriteEndElement();

                //<d:AD_User>hbd_cafm</d:AD_User>
                writer.WriteStartElement("d:" + "AD_User");
                writer.WriteValue("hbd_cafm");
                writer.WriteEndElement();

                // <d:AD_Password>DrpC0u2ZJp0=</d:AD_Password>
                writer.WriteStartElement("d:" + "AD_Password");
                writer.WriteValue("DrpC0u2ZJp0=");
                writer.WriteEndElement();

                // <d:AD_Level m:type="Edm.Byte">1</d:AD_Level>
                writer.WriteStartElement("d:" + "AD_Level");
                writer.WriteAttributeString("m:type", "Edm.Byte");
                writer.WriteValue(1);
                writer.WriteEndElement();
                */
            }

            private void SeekElement(System.Xml.XmlReader reader, string elementName)
            {
                ReaderToNextNode(reader);
                while (reader.Name != elementName)
                {
                    ReaderToNextNode(reader);
                }
            }

            private void ReaderToNextNode(System.Xml.XmlReader reader)
            {
                reader.Read();
                while (reader.NodeType == System.Xml.XmlNodeType.Whitespace)
                {
                    reader.Read();
                }
            }

        }



        [XmlRoot(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
        public class Title
        {
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
        public class Link
        {
            [XmlAttribute(AttributeName = "rel")]
            public string Rel { get; set; }

            [XmlAttribute(AttributeName = "title")]
            public string Title { get; set; }

            [XmlAttribute(AttributeName = "href")]
            public string Href { get; set; }
        }

        [XmlRoot(ElementName = "category", Namespace = "http://www.w3.org/2005/Atom")]
        public class Category
        {
            [XmlAttribute(AttributeName = "term")]
            public string Term { get; set; }

            [XmlAttribute(AttributeName = "scheme")]
            public string Scheme { get; set; }
        }

        [XmlRoot(ElementName = "author", Namespace = "http://www.w3.org/2005/Atom")]
        public class Author
        {
            [XmlElement(ElementName = "name", Namespace = "http://www.w3.org/2005/Atom")]
            public string Name { get; set; }
        }

        /*
        [XmlRoot(ElementName = "AD_UID", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public class AD_UID
        {
            [XmlAttribute(AttributeName = "type", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string Type { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "AD_Level", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public class AD_Level
        {
            [XmlAttribute(AttributeName = "type", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string Type { get; set; }
            [XmlText]
            public string Text { get; set; }
        }
        */

        [XmlRoot(ElementName = "properties", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
        public class Properties
        {
            // [XmlElement(ElementName = "AD_UID", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            // public AD_UID AD_UID { get; set; }

            [XmlElement(ElementName = "AD_User", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public string AD_User { get; set; }

            [XmlElement(ElementName = "AD_Password", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public string AD_Password { get; set; }

            // [XmlElement(ElementName = "AD_Level", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            // public AD_Level AD_Level { get; set; }
        }

        [XmlRoot(ElementName = "content", Namespace = "http://www.w3.org/2005/Atom")]
        public class Content
        {
            [XmlElement(ElementName = "properties", Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            //public Properties Properties { get; set; }
            public MyProperties Properties { get; set; }


            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
        }

        [XmlRoot(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
        public class Entry
        {
            [XmlElement(ElementName = "id", Namespace = "http://www.w3.org/2005/Atom")]
            public string Id { get; set; }

            [XmlElement(ElementName = "category", Namespace = "http://www.w3.org/2005/Atom")]
            public Category Category { get; set; }

            [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
            public Link Link { get; set; }

            [XmlElement(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
            public string Title { get; set; }

            [XmlElement(ElementName = "updated", Namespace = "http://www.w3.org/2005/Atom")]
            public string Updated { get; set; }

            [XmlElement(ElementName = "author", Namespace = "http://www.w3.org/2005/Atom")]
            public Author Author { get; set; }

            [XmlElement(ElementName = "content", Namespace = "http://www.w3.org/2005/Atom")]
            public Content Content { get; set; }
        }

        [XmlRoot(ElementName = "feed", Namespace = "http://www.w3.org/2005/Atom")]
        public class Feed
        {
            [XmlElement(ElementName = "id", Namespace = "http://www.w3.org/2005/Atom")]
            public string Id { get; set; }

            [XmlElement(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
            public Title Title { get; set; }

            [XmlElement(ElementName = "updated", Namespace = "http://www.w3.org/2005/Atom")]
            public string Updated { get; set; }

            [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
            public Link Link { get; set; }

            [XmlElement(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
            public List<Entry> Entry { get; set; }

            [XmlAttribute(AttributeName = "xmlns")]
            public string Xmlns { get; set; }

            [XmlAttribute(AttributeName = "d", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string D { get; set; }

            [XmlAttribute(AttributeName = "m", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string M { get; set; }

            [XmlAttribute(AttributeName = "base", Namespace = "http://www.w3.org/XML/1998/namespace")]
            public string Base { get; set; }
        }


    }


}
