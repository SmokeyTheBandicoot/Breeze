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
    Dim BlockBTN As New SFMLButton
    Dim BGBTN As New SFMLButton

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
        With BlockBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Blocks"
            .ForeColor = Drawing.Color.Black
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 32
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Location = New Point(0, YBlocks * BlockSize + 6)
            .Size = New Size(150, 50)
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\BTN1.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\BTN1.png"))
            .IDStr = "BlockBTN"
            EditorGUI.Controls.Add(BlockBTN)
        End With

        With BGBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Backgrounds"
            .ForeColor = Drawing.Color.Black
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 32
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Location = New Point(156, YBlocks * BlockSize + 6)
            .Size = New Size(150, 50)
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\BTN1.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\BTN1.png"))
            .IDStr = "BGBTN"
            EditorGUI.Controls.Add(BGBTN)
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
                            BGForm.show
                            BGForm.focus
                    End Select
                End If
            End If
        Next
    End Sub

    Sub EditorKeyDown(sender As Object, e As SFML.Window.KeyEventArgs)

    End Sub

    Sub EditorKeyup(sender As Object, e As SFML.Window.KeyEventArgs)

    End Sub
#End Region
End Module
