Imports System.Drawing
Imports System.Windows.Forms
Public Class BlocksForm
    Dim ResFold As String = "C:\\GameShardsSoftware\Resources\Sprites\Breeze\Blocks\Cave\"

    Dim SelPic As String

    Dim Pict As PictureBox(,)

    Private Sub BlocksForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Tries to load all the blocks

        ReDim Pict(0, 12)
        For x = 0 To 12
            Pict(0, x) = New PictureBox
            Pict(0, x).Name = x.ToString
            Pict(0, x).BackgroundImage = Image.FromFile(ResFold + "Cave" + (x + 1).ToString + ".png")
            Pict(0, x).Size = New Size(32, 32)
            Pict(0, x).BorderStyle = BorderStyle.FixedSingle
            AddHandler Pict(0, x).Click, AddressOf PictClick
            TabControl1.TabPages(0).Controls.Add(Pict(0, x))
        Next

        Pict(0, 0).Location = New Point(8, 6)
        Pict(0, 1).Location = New Point(46, 6)
        Pict(0, 2).Location = New Point(84, 6)
        Pict(0, 3).Location = New Point(8, 44)
        Pict(0, 4).Location = New Point(46, 44)
        Pict(0, 5).Location = New Point(84, 44)
        Pict(0, 6).Location = New Point(8, 82)
        Pict(0, 7).Location = New Point(46, 82)
        Pict(0, 8).Location = New Point(84, 82)
        Pict(0, 9).Location = New Point(154, 6)
        Pict(0, 10).Location = New Point(192, 6)
        Pict(0, 11).Location = New Point(154, 44)
        Pict(0, 12).Location = New Point(192, 44)

        'PictureBox1.BackgroundImage = Image.FromFile(ResFold + "Cave1.png")
        'PictureBox2.BackgroundImage = Image.FromFile(ResFold + "Cave2.png")
        'PictureBox3.BackgroundImage = Image.FromFile(ResFold + "Cave3.png")
        'PictureBox4.BackgroundImage = Image.FromFile(ResFold + "Cave4.png")
        'PictureBox5.BackgroundImage = Image.FromFile(ResFold + "Cave5.png")
        'PictureBox6.BackgroundImage = Image.FromFile(ResFold + "Cave6.png")
        'PictureBox7.BackgroundImage = Image.FromFile(ResFold + "Cave7.png")
        'PictureBox8.BackgroundImage = Image.FromFile(ResFold + "Cave8.png")
        'PictureBox9.BackgroundImage = Image.FromFile(ResFold + "Cave9.png")
        'PictureBox10.BackgroundImage = Image.FromFile(ResFold + "Cave10.png")
        'PictureBox11.BackgroundImage = Image.FromFile(ResFold + "Cave11.png")
        'PictureBox12.BackgroundImage = Image.FromFile(ResFold + "Cave12.png")
        'PictureBox13.BackgroundImage = Image.FromFile(ResFold + "Cave13.png")

        'Try
        'For x = 1 To 13
        'If TypeOf TabControl1.TabPages(0).Controls(x - 1) Is Button AndAlso TabControl1.TabPages(0).Controls(x - 1).Name = "Picturebox" + x.ToString Then
        'PictureBox1.BackgroundImage = Image.FromFile(ResFold + "Cave" + x.ToString + ".png")
        'End If
        'Next
        'Catch ex As Exception

        'End Try
    End Sub

    Public Sub PictClick(sender As Object, e As EventArgs)
        For x = 0 To 12
            Pict(0, x).BorderStyle = BorderStyle.FixedSingle
        Next
        DirectCast(sender, PictureBox).BorderStyle = BorderStyle.Fixed3D
        SelPic = DirectCast(sender, PictureBox).ImageLocation
    End Sub

    Private Sub BlocksForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub
End Class