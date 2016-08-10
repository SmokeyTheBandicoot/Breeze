Public Class BGForm
    Private Sub BGForm_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Hide()
    End Sub
End Class