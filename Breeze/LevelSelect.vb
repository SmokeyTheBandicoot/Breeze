Imports System.Math
Imports System.Drawing
Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window
Imports NAudio.Wave
Imports System.Threading
Imports System.Windows.Forms
Imports GameShardsCoreSFML
Module LevelSelect

    'Num of buttons
    Dim BTNX As Byte = 6
    Dim BTNY As Byte = 6

    'Background
    Dim Background As New Background(New Sprite(New Texture("C:\Program Files (x86)\SMBX141\GFXPack\NSMB\NSMBWii\Backgrounds\New Super Mario Bros. Wii Custom Backgrounds\background2-19.gif")))

    'GUI
    Dim LevelSelectGUI As New GUI

    Dim Levels As New List(Of String)
    Dim BTNs(,) As SFMLButton
    Dim BackBTN As New SFMLButton
    Dim TutorialBTN As New SFMLButton
    Dim FinallvlBTN As New SFMLButton

    'GUI Position
    Dim SqSize As Size

    Sub DoLevelSelect()
        window.Draw(Background.BGImage)
        LevelSelectGUI.Draw(window)
    End Sub

    Sub PostInitLevelSelect()
        SqSize = New Size(50 * BTNX + 10 * BTNX, 50 * BTNY + 10 * BTNY)

        Background.BGImage.Scale = New Vector2f(CSng(window.Size.X / Background.BGImage.Texture.Size.X), CSng(window.Size.Y / Background.BGImage.Texture.Size.Y))
    End Sub

    Sub GUILoadLevelSelect()
        Console.WriteLine("Loading LevelSelect GUI...")
        For x = 0 To BTNX
            For y = 0 To BTNX
                Dim b As New SFMLButton
                With b
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Text = (x + 1 + (BTNX + 1) * y).ToString
                    .ForeColor = Drawing.Color.Blue
                    .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
                    .SFMLFontSize = 48
                    .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
                    .Toggleable = True
                    .ToggleChangesSprite = False
                    .ToggleChangesColor = True
                    .Size = New Size(50, 50)
                    .Location = New Point(CInt(window.Size.X \ 2 - SqSize.Width \ 2 + 50 * x + 10 * x), CInt(window.Size.Y - 200 - SqSize.Height + 50 * y + 10 * y))
                    .AutoPadding = True
                    .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
                    .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
                    .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
                    .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))
                    LevelSelectGUI.Controls.Add(b)
                End With
            Next
        Next

        With BackBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "> Back"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 40
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(110, 50)
            .Location = New Point(CInt(window.Size.X \ 2 - SqSize.Width \ 2), CInt(window.Size.Y - 260 - SqSize.Height))
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))
            LevelSelectGUI.Controls.Add(BackBTN)
        End With

        With TutorialBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Tutorial (0)"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 32
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(290, 50)
            .Location = New Point(CInt(window.Size.X \ 2 - SqSize.Width \ 2 + 120), CInt(window.Size.Y - 260 - SqSize.Height))
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))
            LevelSelectGUI.Controls.Add(TutorialBTN)
        End With

        With FinallvlBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Final Boss"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 32
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(410, 50)
            .Location = New Point(CInt(window.Size.X \ 2 - SqSize.Width \ 2), CInt(window.Size.Y - 140))
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))
            LevelSelectGUI.Controls.Add(FinallvlBTN)
        End With
        Console.WriteLine("Finished Loading LevelSelect GUI!")
    End Sub

    Sub LevelSelectWindowClick(sender As Object, e As MouseButtonEventArgs)
        For x = 0 To LevelSelectGUI.Controls.Count - 1
            If TypeOf LevelSelectGUI.Controls(x) Is SFMLButton Then
                If GGeom.CheckIfRectangleIntersectsPoint(DirectCast(LevelSelectGUI.Controls(x), SFMLButton).Bounds, New Point(e.X, e.Y)) Then
                    Select Case LevelSelectGUI.Controls(x).Text.ToUpper
                        Case "> BACK"
                            CurrentState = GameStates.MainMenu
                    End Select
                End If
            End If
        Next
    End Sub

    Sub LevelSelectMouseMoved(sender As Object, e As MouseMoveEventArgs)
        For x = 0 To LevelSelectGUI.Controls.Count - 1
            If TypeOf LevelSelectGUI.Controls(x) Is SFMLButton Then
                If GGeom.CheckIfRectangleIntersectsPoint(DirectCast(LevelSelectGUI.Controls(x), SFMLButton).Bounds, New Point(e.X, e.Y)) Then
                    DirectCast(LevelSelectGUI.Controls(x), SFMLButton).IsToggled = True
                Else
                    DirectCast(LevelSelectGUI.Controls(x), SFMLButton).IsToggled = False
                End If
            End If
        Next
    End Sub

End Module
