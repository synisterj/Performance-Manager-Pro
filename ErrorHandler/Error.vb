Public Class Err
    Private E1000 As New ErrorStructure
    Public ErrorForm As System.Windows.Forms.Form
    Public Structure ErrorStructure
        Public ErrorId As Integer
        Public ErrorStyle As MsgBoxStyle
        Public ErrorTitle As String
        Public ErrorHeader As String
        Public ErrorHandler As String
        Public ErrorMessage As String
        Public ErrorSender As String
        Public ErrorFooter As String
    End Structure
    Private ErrList As New List(Of ErrorStructure)
    Public Sub PopulateErrorList(ByVal sender As Object)
        Dim SenderSplit As String() = sender.ToString.Split(",")
        E1000.ErrorId = 1000
        E1000.ErrorStyle = MsgBoxStyle.Critical
        E1000.ErrorTitle = "Performance Monitor Pro"
        E1000.ErrorHeader = "Caught Exception" & vbNewLine & vbCrLf
        E1000.ErrorMessage = "Message :  Index Out of Range" & vbCrLf
        E1000.ErrorSender = "   Sender :  " & SenderSplit(0) & vbCrLf
        E1000.ErrorHandler = "  Handler :  " & Me.ToString & vbNewLine & vbCrLf
        E1000.ErrorFooter = "For more information about this error visit: http://www.intelifunctions.com/error/" & E1000.ErrorId & ".html"
        ErrList.Add(E1000)
    End Sub
    Private Sub DisplayError(sender As Object, id As Integer, logerror As Boolean)
        Dim ErrorId As Integer = id - 1000
        Dim MessageLabel As System.Windows.Forms.Label
        ErrorForm = New System.Windows.Forms.Form
        ErrorForm.Name = "ErrorForm"
        ErrorForm.Text = ErrList.Item(ErrorId).ErrorTitle
        ErrorForm.Size = New System.Drawing.Point(460, 145)
        ErrorForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        MessageLabel = New System.Windows.Forms.Label
        MessageLabel.Name = "MessageLabel"
        MessageLabel.AutoSize = True
        MessageLabel.Text = ErrorMessage(sender, id, logerror)
        ErrorForm.Controls.Add(MessageLabel)
        ErrorForm.ShowDialog()
    End Sub
    Public Sub New(ByVal sender As Object, ByVal id As Integer, Optional ByVal logerror As Boolean = False)
        PopulateErrorList(sender)
        DisplayError(sender, id, logerror)
    End Sub
    Public Function ErrorMessage(ByVal sender As Object, ByVal id As Integer, Optional ByVal logerror As Boolean = False) As String
        ErrorMessage = Nothing
        If logerror Then WriteLog(sender.ToString, 1000)
        Dim ErrorId As Integer = id - 1000
        Dim RetErr As String = ErrList.Item(ErrorId).ErrorHeader & ErrList.Item(0).ErrorMessage & ErrList.Item(ErrorId).ErrorSender & ErrList.Item(ErrorId).ErrorHandler & ErrList.Item(ErrorId).ErrorFooter
        ErrorMessage = RetErr
    End Function
    Public Const DEBUGERR As Boolean = False
    Private Const LOGFILE As String = "errors.log"
    Private Const LOGTITLE As String = "Log Entry "
    Private Const LOGHEADER As String = "{0} {1}"
    Private Const LOGBODY_MESSAGE As String = "| Message: {0}"
    Private Const LOGBODY_SENDER As String = "| Sender: {0}"
    Private Const LOGFOOTER As String = "-------------------------------"
    Private Shared Sub PrepareLogForWriting(ByVal msg As String, ByVal sender As String, ByVal w As System.IO.TextWriter)
        w.Write(LOGTITLE)
        w.WriteLine(LOGHEADER, DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString())
        w.WriteLine(LOGBODY_MESSAGE, msg)
        w.WriteLine(LOGBODY_SENDER, sender)
        w.WriteLine(LOGFOOTER)
    End Sub
    Private Sub WriteLog(ByVal sender As String, ByVal msg As String)
        Using writer As System.IO.StreamWriter = System.IO.File.AppendText(LOGFILE)
            PrepareLogForWriting(msg, sender, writer)
        End Using
    End Sub
    Public Sub ShowDebugOutput(debug)
        If debug Then
            Using reader As System.IO.StreamReader = System.IO.File.OpenText(LOGFILE)
                DumpLog(reader)
            End Using
        End If
    End Sub
    Private Shared Sub DumpLog(reader As System.IO.StreamReader)
        Dim Line As String
        Line = reader.ReadLine()
        While Not (Line Is Nothing)
            Debug.WriteLine(Line)
            Line = reader.ReadLine()
        End While
    End Sub
End Class