Public Class Functions
    Public Function Encrypt(ByVal sender As Object, ByVal data As String, ByVal type As String, ByVal phrase As String, ByVal salt As String, Optional ByVal iterations As Integer = 2) As String
        Encrypt = Nothing
        Dim passPhrase As String = phrase
        Dim saltValue As String = salt
        Dim hashAlgorithm As String = type
        Dim passwordIterations As Integer = iterations
        Dim initVector As String = "@sYGD54g83HRyhv7"
        Dim keySize As Integer = 256
        Try
            Dim initVectorBytes As Byte() = Text.Encoding.ASCII.GetBytes(initVector)
            Dim saltValueBytes As Byte() = Text.Encoding.ASCII.GetBytes(saltValue)
            Dim password As New Security.Cryptography.PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)
            Dim keyBytes As Byte() = password.GetBytes(keySize \ 8)
            Dim symmetricKey As New Security.Cryptography.RijndaelManaged()
            symmetricKey.Mode = Security.Cryptography.CipherMode.CBC
            Dim plainTextBytes As Byte() = Text.Encoding.UTF8.GetBytes(data)
            Dim encryptor As Security.Cryptography.ICryptoTransform = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)
            Dim memoryStream As New IO.MemoryStream()
            Dim cryptoStream As New Security.Cryptography.CryptoStream(memoryStream, encryptor, Security.Cryptography.CryptoStreamMode.Write)
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
            cryptoStream.FlushFinalBlock()
            Dim cipherTextBytes As Byte() = memoryStream.ToArray()
            memoryStream.Close()
            cryptoStream.Close()
            Dim cipherText As String = Convert.ToBase64String(cipherTextBytes)
            Encrypt = cipherText
        Catch ex As Exception
            MsgBox("Caught Exception" & vbNewLine & vbCrLf & "At: " & Me.ToString & vbCrLf & "Sender: " & sender.ToString & vbCrLf & "Message: " & ex.Message)
            Return "Unable to encrypt data!"
        End Try
    End Function
    Public Function Decrypt(ByVal sender As Object, ByVal data As String, ByVal type As String, ByVal phrase As String, ByVal salt As String, Optional ByVal iterations As Integer = 2) As String
        Decrypt = Nothing
        Dim passPhrase As String = phrase
        Dim saltValue As String = salt
        Dim hashAlgorithm As String = type
        Dim passwordIterations As Integer = iterations
        Dim initVector As String = "@sYGD54g83HRyhv7"
        Dim keySize As Integer = 256
        Try
            Dim initVectorBytes As Byte() = Text.Encoding.ASCII.GetBytes(initVector)
            Dim saltValueBytes As Byte() = Text.Encoding.ASCII.GetBytes(saltValue)
            Dim cipherTextBytes As Byte() = Convert.FromBase64String(data)
            Dim password As New Security.Cryptography.PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations)
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
