Imports System.Math
Imports System.Drawing
Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window
Imports NAudio.Wave
Imports System.Threading
Imports System.Windows.Forms
Imports GameShardsCoreSFML

Module LevelEditor
    'Editor
    Public ShowGrid As Boolean = True
    Public XBlocks As Integer = 25
    Public YBlocks As Integer = 19
    Public BlockSize As Integer = 32

    'Rendering
    Public Backrect As New RectangleShape(New Vector2f(window.Size.X, window.Size.Y))
    Public GridRect As New RectangleShape(New Vector2f(XBlocks * BlockSize, YBlocks * BlockSize))
    Public GridColor As New SFML.Graphics.Color
    Public GridOutlineColor As New SFML.Graphics.Color


    'Grid
    Public r As New RectangleShape
    Dim GridOffSetX As Integer = 6
    Dim GridOffSetY As Integer = 6

    'GUI
    'Dim BlockBTN As New SFMLButton
    'Dim BGBTN As New SFMLButton

    Dim NameTB As New SFMLTextbox
    Dim WithEvents FakeCHK As New SFMLCheckbox
    Dim KB As New SFMLKeyboard(New SFML.Graphics.Font("arial.ttf"))

    'Windows
    Dim BlocksForm As New BlocksForm
    Dim BGForm As New BGForm

    Sub DoEditor()

        'Draw BackColor
        window.Draw(Backrect)

        'Draw Backrect

        'Draw Background

        'Draw Background objects

        'Draw Blocks

        'Draw Items

        'Draw Player

        'Draw Grid
        If ShowGrid Then
            DrawGrid()
        End If

        'Draw GUI
        EditorGUI.Draw(window)

    End Sub

    Public Sub CC(sender As Object, e As EventArgs) Handles FakeCHK.CheckedChanged
        MsgBox(sender.ToString)
    End Sub

    Sub DrawGrid()
        For x = 0 To (XBlocks - 1) * BlockSize Step BlockSize
            For y = 0 To (YBlocks - 1) * BlockSize Step BlockSize
                r.OutlineThickness = 1
                r.Position = New Vector2f(x + GridOffSetX, y + GridOffSetY)
                r.Size = New Vector2f(BlockSize, BlockSize)
                r.FillColor = GridColor
                r.OutlineColor = GridOutlineColor
                window.Draw(r)
            Next
        Next

    End Sub

    Sub PostInitLevelEditor()
        BlocksForm = New BlocksForm
        BlocksForm.StartPosition = FormStartPosition.CenterScreen

        Backrect.FillColor = SFML.Graphics.Color.White
        Backrect.FillColor = SFML.Graphics.Color.Black

        GridOutlineColor = SFML.Graphics.Color.White
        GridColor = SFML.Graphics.Color.Transparent

    End Sub

    Public Sub GUILoadLevelEditor()

        With NameTB
            .TextAlign = HorizontalAlignment.Left
            .Text = "LevelName"
            .ForeColor = Drawing.Color.White
            .SFMLFont = New SFML.Graphics.Font("arial.ttf")
            .SFMLFontSize = 32
            .Location = New Point(400, 150)
            .Size = New Size(300, 30)
            .IDStr = "BGBTN"
            .MaxLength = 15
            EditorGUI.Controls.Add(NameTB)
        End With

        'KB = New SFMLKeyboard(New Vector2f(0, 650), New Vector2f(522, 172))
        With KB
            '.KeyPadding = 3
            .SetKeys(New Vector2f(0, 660), New Vector2f(522, 172), 0, .SFMLFont)
            .ForeColor = Drawing.Color.Black
            .Font = New Drawing.Font("arial", 15)
            '.SFMLFont = New SFML.Graphics.Font("arial.ttf")
            '.SizeWH = New Size(522, 172)
            '.Location = New Point(0, 700)
            NameTB.BoundKeyboard = KB
            .BoundToTextbox = True
            EditorGUI.Controls.Add(KB)
        End With

        With FakeCHK
            .CheckSpriteNormal = New Sprite(New Texture("C:\\GameShardsSoftware\Resources\Sprites\Breeze\CheckMark.png"))
            .CheckSpriteUnchecked = New Sprite(New Texture("C:\\GameShardsSoftware\Resources\Sprites\Breeze\CheckMark1.png"))
            .Checked = True
            .Location = New Point(500, 500)
            .ForeColor = Drawing.Color.Purple
            .SFMLFont = KeyFont
            .SFMLFontSize = 24
            .TextAlign = ContentAlignment.MiddleLeft
            .Text = "Is Fake?"
            .Autoscale = True
            .BoxSize = New Size(24, 24)
            .BorderColornormal = SFML.Graphics.Color.White
            .BorderColorHover = New SFML.Graphics.Color(0, 0, 128)
            .CycleIndeterminate = True
            EditorGUI.Controls.Add(FakeCHK)
        End With
    End Sub



#Region "Handles"
    Sub EditorWindowClick(sender As Object, e As MouseButtonEventArgs)

        For x = 0 To EditorGUI.Controls.Count - 1

            If TypeOf EditorGUI.Controls(x) Is SFMLButton Then
                Dim b As New SFMLButton
                b = DirectCast(EditorGUI.Controls(x), SFMLButton)
                If GGeom.CheckIfRectangleIntersectsPoint(b.Bounds, New Point(e.X, e.Y)) Then
                    Select Case DirectCast(EditorGUI.Controls(x), SFMLButton).IDStr
                        Case "BlockBTN"
                            BlocksForm.Show()
                            BlocksForm.Focus()
                        Case "BGBTN"
                            BGForm.Show()
                            BGForm.Focus()
                    End Select
                End If

            ElseIf TypeOf EditorGUI.Controls(x) Is SFMLTextbox Then
                DirectCast(EditorGUI.Controls(x), SFMLTextbox).SetActive(New Point(e.X, e.Y))

            ElseIf TypeOf EditorGUI.Controls(x) Is SFMLKeyboard Then
                If NameTB.IsActive Then
                    DirectCast(EditorGUI.Controls(x), SFMLKeyboard).SetKeyPressed(New Point(e.X, e.Y), NameTB.Text)
                End If

            ElseIf TypeOf EditorGUI.Controls(x) Is SFMLCheckbox Then
                DirectCast(EditorGUI.Controls(x), SFMLCheckbox).ChangeCheckedState(New Point(e.X, e.Y))
            End If

        Next
    End Sub

    Sub LevelEditorMouseMoved(sender As Object, e As MouseMoveEventArgs)
        For x = 0 To EditorGUI.Controls.Count - 1
            If TypeOf EditorGUI.Controls(x) Is SFMLKeyboard Then
                DirectCast(EditorGUI.Controls(x), SFMLKeyboard).SetKeyToggled(New Point(e.X, e.Y))


            ElseIf TypeOf EditorGUI.controls(x) Is SFMLCheckbox Then
                DirectCast(EditorGUI.Controls(x), SFMLCheckbox).CheckHover(New Point(e.X, e.Y))
            End If
        Next
    End Sub

    Sub EditorKeyDown(sender As Object, e As SFML.Window.KeyEventArgs)
        For x = 0 To EditorGUI.Controls.Count - 1
            If TypeOf EditorGUI.Controls(x) Is SFMLTextbox Then
                TextboxUtils.UpdateTextboxes(EditorGUI, e.Code.ToString)
            End If
        Next
    End Sub

    Sub EditorKeyup(sender As Object, e As SFML.Window.KeyEventArgs)

    End Sub
#End Region
End Module
