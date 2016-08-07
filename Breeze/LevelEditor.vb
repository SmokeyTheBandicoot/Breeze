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

    'Grid
    Public r As New RectangleShape

    'GUI
    Dim BlockBTN As New SFMLButton

    'Windows
    Dim BlocksForm As New BlocksForm

    Sub DoEditor()
        If ShowGrid Then
            DrawGrid()
        End If
    End Sub

    Sub DrawGrid()
        For x = 0 To 800 Step 32
            For y = 0 To 600 Step 32
                r.FillColor = SFML.Graphics.Color.Black
                r.OutlineColor = SFML.Graphics.Color.White
                r.OutlineThickness = 1
                r.Position = New Vector2f(x, y)
                r.Size = New Vector2f(32, 32)
                window.Draw(r)
            Next
        Next
    End Sub

    Sub PostInitLevelEditor()
        'Doesn't have to do anything yet
    End Sub

    Public Sub GUILoadLevelEditor()
        BlockBTN.TextAlign = ContentAlignment.MiddleLeft
        BlockBTN.Text = "Blocks"
        BlockBTN.ForeColor = Drawing.Color.Blue
        BlockBTN.SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
        BlockBTN.SFMLFontSize = 48
        BlockBTN.Toggleable = True
        BlockBTN.ToggleChangesSprite = False
        BlockBTN.ToggleChangesColor = True
        BlockBTN.Location = New Point(0, 0)
        BlockBTN.Size = New Size(100, 50)
        BlockBTN.AutoPadding = True
        BlockBTN.ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
        BlockBTN.ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
        BlockBTN.SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
        BlockBTN.SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))

        EditorGUI.Controls.Add(BlockBTN)
    End Sub



#Region "Handles"
    Sub EditorWindowClick(sender As Object, e As MouseButtonEventArgs)
        For x = 0 To EditorGUI.Controls.Count - 1
            If TypeOf EditorGUI.Controls(x) Is SFMLButton Then
                Dim b As New SFMLButton
                b = DirectCast(EditorGUI.Controls(x), SFMLButton)
                If GGeom.CheckIfRectangleIntersectsPoint(b.Bounds, New Point(e.X, e.Y)) Then
                    EditorButtonClick(b)
                Else
                    CurrentState = GameStates.MainGame
                End If
            End If
        Next
    End Sub

    Public Sub EditorButtonClick(ByVal sender As SFMLButton)
        BlocksForm = New BlocksForm
        BlocksForm.StartPosition = FormStartPosition.CenterScreen
        BlocksForm.Show()
    End Sub

    Sub EditorKeyDown(sender As Object, e As SFML.Window.KeyEventArgs)

    End Sub

    Sub EditorKeyup(sender As Object, e As SFML.Window.KeyEventArgs)

    End Sub
#End Region
End Module
