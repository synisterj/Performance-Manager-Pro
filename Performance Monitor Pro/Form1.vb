

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim TimeNowFunction As New Common.Functions
            Dim TimeNow As String = TimeNowFunction.DateNow(sender)
            'MsgBox(TimeNow)

            Dim EncryptFunction As New Common.Functions
            Dim DataToEncrypt As String = "some data to encrypt"
            Dim EncryptedData As String = EncryptFunction.Encrypt(sender, DataToEncrypt, "md5", "phrase", "salt")
            'MsgBox(EncryptedData)

            Dim ErrorFunction As New ErrorHandler.Err(sender, 1000, True)
            ErrorFunction.ErrorMessage(sender, 1000, True)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
