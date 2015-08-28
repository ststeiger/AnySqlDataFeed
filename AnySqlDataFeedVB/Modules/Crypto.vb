

Namespace Tools.Cryptography


    Public Class AES
        Protected Shared strKey As String = "1b55ec1d96f637aa7b73c31765a12c2c8fb8b9f6ae8b14396475a20ed1a83dac"
        Protected Shared strIV As String = "d4e3381cdd39ddb70f85e96d11b667e5"


        Public Shared Function GetKey() As String
            Return strKey
        End Function ' GetKey


        Public Shared Function GetIV() As String
            Return strIV
        End Function ' GetIV


        Public Shared Sub SetKey(ByRef strInputKey As String)
            strKey = strInputKey
        End Sub ' SetKey 


        Public Shared Sub SetIV(ByRef strInputIV As String)
            strIV = strInputIV
        End Sub ' SetIV


        Public Shared Function GenerateKey() As String
            Dim objRijndael As New System.Security.Cryptography.RijndaelManaged()

            objRijndael.GenerateKey()
            objRijndael.GenerateIV()

            Dim bIV As Byte() = objRijndael.IV
            Dim bKey As Byte() = objRijndael.Key
            objRijndael.Clear()

            Return Convert.ToString((Convert.ToString("IV: ") & ByteArrayToHexString(bIV)) + System.Environment.NewLine + "Key: ") & ByteArrayToHexString(bKey)
        End Function ' GenerateKey


        Public Shared Function Encrypt(strPlainText As String) As String
            'Dim roundtrip As String
            'Dim encASCII As New System.Text.ASCIIEncoding()
            Dim enc As System.Text.Encoding = System.Text.Encoding.UTF8
            Dim objRijndael As New System.Security.Cryptography.RijndaelManaged()
            'Dim fromEncrypt() As Byte
            Dim baCipherTextBuffer As Byte() = Nothing
            Dim baPlainTextBuffer As Byte() = Nothing
            Dim baEncryptionKey As Byte() = Nothing
            Dim baInitializationVector As Byte() = Nothing

            'Create a new key and initialization vector.
            'objRijndael.GenerateKey()
            'objRijndael.GenerateIV()
            objRijndael.Key = HexStringToByteArray(strKey)
            objRijndael.IV = HexStringToByteArray(strIV)


            'Get the key and initialization vector.
            baEncryptionKey = objRijndael.Key
            baInitializationVector = objRijndael.IV
            'strKey = ByteArrayToHexString(baEncryptionKey)
            'strIV = ByteArrayToHexString(baInitializationVector)

            'Get an encryptor.
            Dim ifaceAESencryptor As System.Security.Cryptography.ICryptoTransform = objRijndael.CreateEncryptor(baEncryptionKey, baInitializationVector)

            'Encrypt the data.
            Dim msEncrypt As New System.IO.MemoryStream()
            Dim csEncrypt As New System.Security.Cryptography.CryptoStream(msEncrypt, ifaceAESencryptor, System.Security.Cryptography.CryptoStreamMode.Write)

            'Convert the data to a byte array.
            baPlainTextBuffer = enc.GetBytes(strPlainText)

            'Write all data to the crypto stream and flush it.
            csEncrypt.Write(baPlainTextBuffer, 0, baPlainTextBuffer.Length)
            csEncrypt.FlushFinalBlock()

            'Get encrypted array of bytes.
            baCipherTextBuffer = msEncrypt.ToArray()

            Return ByteArrayToHexString(baCipherTextBuffer)
        End Function ' Encrypt


        Public Shared Function DeCrypt(strEncryptedInput As String) As String
            Dim strReturnValue As String = Nothing

            If String.IsNullOrEmpty(strEncryptedInput) Then
                Throw New System.ArgumentNullException("strEncryptedInput", "strEncryptedInput may not be string.Empty or NULL, because these are invid values.")
            End If

            ' Dim encASCII As New System.Text.ASCIIEncoding()
            Dim enc As System.Text.Encoding = System.Text.Encoding.UTF8

            Dim objRijndael As New System.Security.Cryptography.RijndaelManaged()

            Dim baCipherTextBuffer As Byte() = HexStringToByteArray(strEncryptedInput)


            Dim baDecryptionKey As Byte() = HexStringToByteArray(strKey)
            Dim baInitializationVector As Byte() = HexStringToByteArray(strIV)

            ' This is where the message would be transmitted to a recipient
            ' who already knows your secret key. Optionally, you can
            ' also encrypt your secret key using a public key algorithm
            ' and pass it to the mesage recipient along with the RijnDael
            ' encrypted message.            
            'Get a decryptor that uses the same key and IV as the encryptor.
            Dim ifaceAESdecryptor As System.Security.Cryptography.ICryptoTransform = objRijndael.CreateDecryptor(baDecryptionKey, baInitializationVector)

            'Now decrypt the previously encrypted message using the decryptor
            ' obtained in the above step.
            Dim msDecrypt As New System.IO.MemoryStream(baCipherTextBuffer)
            Dim csDecrypt As New System.Security.Cryptography.CryptoStream(msDecrypt, ifaceAESdecryptor, System.Security.Cryptography.CryptoStreamMode.Read)

            'Dim baPlainTextBuffer() As Byte
            'baPlainTextBuffer = New Byte(baCipherTextBuffer.Length) {}
            Dim baPlainTextBuffer As Byte() = New Byte(baCipherTextBuffer.Length) {}

            'Read the data out of the crypto stream.
            csDecrypt.Read(baPlainTextBuffer, 0, baPlainTextBuffer.Length)

            'Convert the byte array back into a string.
            strReturnValue = enc.GetString(baPlainTextBuffer)
            If Not String.IsNullOrEmpty(strReturnValue) Then
                strReturnValue = strReturnValue.Trim(ControlChars.NullChar)
            End If

            Return strReturnValue
        End Function ' DeCrypt


        ' VB.NET to convert a byte array into a hex string
        Public Shared Function ByteArrayToHexString(arrInput As Byte()) As String
            Dim strOutput As New System.Text.StringBuilder(arrInput.Length)

            For i As Integer = 0 To arrInput.Length - 1
                strOutput.Append(arrInput(i).ToString("X2"))
            Next

            Return strOutput.ToString().ToLower()
        End Function ' ByteArrayToHexString


        Public Shared Function HexStringToByteArray(strHexString As String) As Byte()
            Dim iNumberOfChars As Integer = strHexString.Length
            Dim baBuffer As Byte() = New Byte(iNumberOfChars / 2 - 1) {}
            For i As Integer = 0 To iNumberOfChars - 1 Step 2
                baBuffer(i / 2) = System.Convert.ToByte(strHexString.Substring(i, 2), 16)
            Next
            Return baBuffer
        End Function ' HexStringToByteArray


    End Class ' AES 



    Public Class DES


        Protected Shared strSymmetricKey As String = "z67f3GHhdga78g3gZUIT(6/&ns289hsB_5Tzu6"
        'Protected Shared strSymmetricKey As String = "Als symmetrischer Key kann irgendein Text verwendet werden. äöü'"

        ' http://www.codeproject.com/KB/aspnet/ASPNET_20_Webconfig.aspx
        ' http://www.codeproject.com/KB/database/Connection_Strings.aspx
        Public Shared Function DeCrypt(SourceText As String) As String
            Dim strReturnValue As String = ""

            If String.IsNullOrEmpty(SourceText) Then
                Return strReturnValue
            End If ' (string.IsNullOrEmpty(SourceText)) 

            Using Des As New System.Security.Cryptography.TripleDESCryptoServiceProvider()

                Using HashMD5 As New System.Security.Cryptography.MD5CryptoServiceProvider()
                    Des.Key = HashMD5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strSymmetricKey))
                    Des.Mode = System.Security.Cryptography.CipherMode.ECB

                    Dim desdencrypt As System.Security.Cryptography.ICryptoTransform = Des.CreateDecryptor()
                    Dim buff As Byte() = System.Convert.FromBase64String(SourceText)
                    strReturnValue = System.Text.Encoding.UTF8.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length))
                End Using ' HashMD5

            End Using ' Des

            Return strReturnValue
        End Function ' DeCrypt


        Public Shared Function Crypt(SourceText As String) As String
            Dim strReturnValue As String = ""

            Using Des As New System.Security.Cryptography.TripleDESCryptoServiceProvider()

                Using HashMD5 As New System.Security.Cryptography.MD5CryptoServiceProvider()
                    Des.Key = HashMD5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strSymmetricKey))
                    Des.Mode = System.Security.Cryptography.CipherMode.ECB
                    Dim desdencrypt As System.Security.Cryptography.ICryptoTransform = Des.CreateEncryptor()
                    Dim buff As Byte() = System.Text.Encoding.UTF8.GetBytes(SourceText)

                    strReturnValue = System.Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length))
                End Using ' HashMD5
            End Using ' Des

            Return strReturnValue
        End Function ' Crypt

        Public Shared Function GenerateKey() As String
            Dim objDESprovider As New System.Security.Cryptography.TripleDESCryptoServiceProvider()

            objDESprovider.GenerateKey()
            objDESprovider.GenerateIV()
            Dim bIV As Byte() = objDESprovider.IV
            Dim bKey As Byte() = objDESprovider.Key

            Return Convert.ToString((Convert.ToString("IV: ") & AES.ByteArrayToHexString(bIV)) + System.Environment.NewLine + "Key: ") & AES.ByteArrayToHexString(bKey)
        End Function ' GenerateKey


        Public Shared Function GenerateHash(SourceText As String) As String
            Dim strReturnValue As String = ""
            Dim ByteSourceText As Byte() = System.Text.Encoding.UTF8.GetBytes(SourceText)

            Using Md5 As New System.Security.Cryptography.MD5CryptoServiceProvider()
                Dim ByteHash As Byte() = Md5.ComputeHash(ByteSourceText)
                strReturnValue = System.Convert.ToBase64String(ByteHash)
                ByteHash = Nothing
            End Using ' Md5

            Return strReturnValue
        End Function ' GenerateHash


    End Class ' DES


End Namespace ' COR.Tools.Cryptography
