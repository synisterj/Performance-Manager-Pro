Public Class Functions
    Public Function Encrypt(ByVal sender As Object, ByVal data As String, ByVal hashtype As String, ByVal phrase As String, ByVal salt As String, Optional ByVal inivec As String = "@sYGD54g83HRyhv7", Optional ByVal iterations As Integer = 2, Optional ByVal keysize As Integer = 256) As String
        Encrypt = Nothing
        Dim PassPhrase As String = phrase
        Dim SaltValue As String = salt
        Dim HashAlgorithm As String = hashtype
        Dim PasswordIterations As Integer = iterations
        Dim InitVector As String = inivec
        Dim InitKeySize As Integer = keysize
        Try
            Dim InitVectorBytes As Byte() = Text.Encoding.ASCII.GetBytes(InitVector)
            Dim SaltValueBytes As Byte() = Text.Encoding.ASCII.GetBytes(Salt)
            Dim Password As New Security.Cryptography.PasswordDeriveBytes(PassPhrase, SaltValueBytes, HashAlgorithm, PasswordIterations)
            Dim KeyBytes As Byte() = Password.GetBytes(KeySize \ 8)
            Dim SymmetricKey As New Security.Cryptography.RijndaelManaged()
            SymmetricKey.Mode = Security.Cryptography.CipherMode.CBC
            Dim PlainTextBytes As Byte() = Text.Encoding.UTF8.GetBytes(data)
            Dim Encryptor As Security.Cryptography.ICryptoTransform = SymmetricKey.CreateEncryptor(KeyBytes, InitVectorBytes)
            Dim MyMemoryStream As New IO.MemoryStream()
            Dim MyCryptoStream As New Security.Cryptography.CryptoStream(MyMemoryStream, Encryptor, Security.Cryptography.CryptoStreamMode.Write)
            MyCryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length)
            MyCryptoStream.FlushFinalBlock()
            Dim CipherTextBytes As Byte() = MyMemoryStream.ToArray()
            MyMemoryStream.Close()
            MyCryptoStream.Close()
            Dim CipherText As String = Convert.ToBase64String(CipherTextBytes)
            Encrypt = CipherText
        Catch Ex As Exception
            MsgBox("Caught Exception" & vbNewLine & vbCrLf & "At: " & Me.ToString & vbCrLf & "Sender: " & sender.ToString & vbCrLf & "Message: " & Ex.Message)
            Return "Unable to encrypt data!"
        End Try
    End Function
    Public Function Decrypt(ByVal sender As Object, ByVal data As String, ByVal type As String, ByVal phrase As String, ByVal salt As String, Optional ByVal inivec As string = "@sYGD54g83HRyhv7", Optional ByVal iterations As Integer = 2) As String
        Decrypt = Nothing
        Dim passPhrase As String = phrase
        Dim saltValue As String = salt
        Dim hashAlgorithm As String = type
        Dim passwordIterations As Integer = iterations
        Dim initVector As String = inivec
        Dim keySize As Integer = keysize
        Try
            Dim InitVectorBytes As Byte() = Text.Encoding.ASCII.GetBytes(InitVector)
            Dim SaltValueBytes As Byte() = Text.Encoding.ASCII.GetBytes(SaltValue)
            Dim CipherTextBytes As Byte() = Convert.FromBase64String(data)
            Dim Password As New Security.Cryptography.PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)
            Dim keyBytes As Byte() = password.GetBytes(keySize \ 8)
            Dim symmetricKey As New Security.Cryptography.RijndaelManaged()
            symmetricKey.Mode = Security.Cryptography.CipherMode.CBC
            Dim decryptor As Security.Cryptography.ICryptoTransform = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)
            Dim memoryStream As New IO.MemoryStream(cipherTextBytes)
            Dim cryptoStream As New Security.Cryptography.CryptoStream(memoryStream, decryptor, Security.Cryptography.CryptoStreamMode.Read)
            Dim plainTextBytes As Byte() = New Byte(cipherTextBytes.Length - 1) {}
            Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)
            memoryStream.Close()
            cryptoStream.Close()
            Dim plainText As String = Text.Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)
            Decrypt = plainText
        Catch ex As Exception
            MsgBox("Caught Exception" & vbNewLine & vbCrLf & "At: " & Me.ToString & vbCrLf & "Sender: " & sender.ToString & vbCrLf & "Message: " & ex.Message)
            Return "Unable to decrypt data!"
        End Try
    End Function
    Public Function TimeNow(ByVal sender As Object)
        Try
            TimeNow = Nothing
            Dim T As DateTime
            T = Now.ToLongTimeString
            TimeNow = T
        Catch ex As Exception
            MsgBox("Caught Exception" & vbNewLine & vbCrLf & "At: " & Me.ToString & vbCrLf & "Sender: " & sender.ToString & vbCrLf & "Message: " & ex.Message)
            Return "Unable to decrypt data!"
        End Try
    End Function
    Public Function DateNow(ByVal sender As Object) As String
        Try
            DateNow = Nothing
            Dim D As New DateTime
            D = Now.ToLongDateString
            DateNow = D
        Catch ex As Exception
            MsgBox("Caught Exception" & vbNewLine & vbCrLf & "At: " & Me.ToString & vbCrLf & "Sender: " & sender.ToString & vbCrLf & "Message: " & ex.Message)
            Return "Unable to decrypt data!"
        End Try
    End Function
End Class
