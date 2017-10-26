Option Explicit On
Option Strict On

Imports System.Drawing
Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window
Imports GameShardsCore2.Geometry.Geometry2D
Imports GameShardsCoreSFML
Imports System.Windows.Forms

Module InGameOptions

    Dim WithEvents ResumeBTN As New SFMLButton
    Dim WithEvents SoundBTN As New SFMLButton
    Dim WithEvents MusicBTN As New SFMLButton
    Dim WithEvents QuitBTN As New SFMLButton
    Dim WithEvents SoundTB As New SFMLTrackbar
    Dim WithEvents MusicTB As New SFMLTrackbar

    'GUI Position
    Dim SqSize As Size

    Sub DoInGameOptions()

        'window.Clear(New SFML.Graphics.Color(0, 0, 0, 0))

        'IsPaused = True
        InGameOptionsGUI.Draw(window)
    End Sub

    Sub PostInitInGameOptions()
        SqSize = New Size(500, 500)
    End Sub

    Sub GUILoadInGameOptions()
        Console.WriteLine("Loading InGameOptions GUI...")


        With ResumeBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Resume"
            .IDStr = "RESUME"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 40
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(500, 50)
            .Location = New Point(CInt(window.Size.X \ 2) - 250, CInt(window.Size.Y \ 2 - 75))
            '.AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512.png"))
            .SpriteToggled = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512T.png"))
            InGameOptionsGUI.Controls.Add(ResumeBTN)
        End With

        'With SoundBTN
        '    .TextAlign = ContentAlignment.MiddleCenter
        '    .Text = "Music:"
        '    .IDStr = "MUSIC"
        '    .ForeColor = Drawing.Color.Blue
        '    .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
        '    .SFMLFontSize = 32
        '    .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
        '    .Toggleable = True
        '    .ToggleChangesSprite = False
        '    .ToggleChangesColor = True
        '    .Size = New Size(300, 50)
        '    .Location = New Point(CInt(window.Size.X \ 2), CInt(window.Size.Y - 260))
        '    '.AutoPadding = True
        '    .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
        '    .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
        '    .SpriteNormal = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512.png"))
        '    .SpriteToggled = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512T.png"))
        '    InGameOptionsGUI.Controls.Add(SoundBTN)
        'End With

        'With MusicBTN
        '    .TextAlign = ContentAlignment.MiddleCenter
        '    .Text = "Sound: "
        '    .IDStr = "SOUND"
        '    .ForeColor = Drawing.Color.Blue
        '    .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
        '    .SFMLFontSize = 32
        '    .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
        '    .Toggleable = True
        '    .ToggleChangesSprite = False
        '    .ToggleChangesColor = True
        '    .Size = New Size(410, 50)
        '    .Location = New Point(CInt(window.Size.X \ 2 - SqSize.Width \ 2), CInt(window.Size.Y - 140))
        '    '.AutoPadding = True
        '    .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
        '    .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
        '    .SpriteNormal = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512.png"))
        '    .SpriteToggled = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512T.png"))
        '    InGameOptionsGUI.Controls.Add(MusicBTN)
        'End With

        With QuitBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Quit"
            .IDStr = "QUIT"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 32
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(500, 50)
            .Location = New Point(CInt(window.Size.X \ 2) - 250, CInt(window.Size.Y \ 2) + 75)
            '.AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512.png"))
            .SpriteToggled = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512T.png"))
            InGameOptionsGUI.Controls.Add(QuitBTN)
        End With

        With SoundTB
            .BorderColor = New SFML.Graphics.Color(0, 0, 0)
            .ContentBackColor = New SFML.Graphics.Color(23, 165, 232)
            .Size = New Size(400, 50)
            .Location = New Point(CInt(window.Size.X \ 2) - 250, CInt(window.Size.Y \ 2))
            .Maximum = 100
            .Minimum = 0
            My.Settings.Reload()
            .Value = My.Settings.SoundVolume
            My.Settings.Save()
            .SFMLFont = KeyFont
            .SFMLFontSize = 16
            .Orientation = Orientation.Horizontal
            .TickStyle = TickStyle.TopLeft
            .TickFrequency = 10
            InGameOptionsGUI.Controls.Add(SoundTB)
        End With


        Console.WriteLine("Finished Loading LevelSelect GUI!")
    End Sub

#Region "Handles"
    Sub InGameOptionsWindowClick(sender As Object, e As MouseButtonEventArgs)
        For x = 0 To InGameOptionsGUI.Controls.Count - 1
            InGameOptionsGUI.Controls(x).CheckClick(New Point(e.X, e.Y))
            If TypeOf InGameOptionsGUI.Controls(x) Is SFMLButton Then
                If CheckIfRectangleIntersectsPoint(DirectCast(InGameOptionsGUI.Controls(x), SFMLButton).Bounds, New Point(e.X, e.Y)) Then
                    Select Case DirectCast(InGameOptionsGUI.Controls(x), SFMLButton).IDStr.ToUpper
                        Case "RESUME"
                            CurrentState = GameStates.MainGame
                        Case "QUIT"
                            CurrentState = GameStates.MainMenu
                    End Select
                End If
            End If
        Next
    End Sub

    Sub InGameOptionsMouseMoved(sender As Object, e As MouseMoveEventArgs)

        For x = 0 To InGameOptionsGUI.Controls.Count - 1
            InGameOptionsGUI.Controls(x).CheckHover(New Point(e.X, e.Y))
        Next

        'For x = 0 To InGameOptionsGUI.Controls.Count - 1

        '    'Button click
        '    If TypeOf InGameOptionsGUI.Controls(x) Is SFMLButton Then
        '        If CheckIfRectangleIntersectsPoint(DirectCast(InGameOptionsGUI.Controls(x), SFMLButton).Bounds, New Point(e.X, e.Y)) Then
        '            DirectCast(InGameOptionsGUI.Controls(x), SFMLButton).IsToggled = True
        '        Else
        '            DirectCast(InGameOptionsGUI.Controls(x), SFMLButton).IsToggled = False
        '        End If

        '        'Trackbar click
        '    ElseIf TypeOf InGameOptionsGUI.Controls(x) Is SFMLTrackbar Then
        '        If CheckIfRectangleIntersectsPoint(DirectCast(InGameOptionsGUI.Controls(x), SFMLTrackbar).Bounds, New Point(e.X, e.Y)) Then
        '            DirectCast(InGameOptionsGUI.Controls(x), SFMLButton). = True
        '        End If
        '    End If
        'Next
    End Sub

    Sub InGameOptionsClickUp(sender As Object, e As MouseButtonEventArgs)
        For x = 0 To InGameOptionsGUI.Controls.Count - 1
            InGameOptionsGUI.Controls(x).CheckClickUp(New Point(e.X, e.Y))

            ''Additional External Click Checks
            'If GGeom.CheckIfRectangleIntersectsPoint(New Rectangle(EditorGUI.Controls(x).location.X, EditorGUI.Controls(x).location.Y, EditorGUI.Controls(x).size.Width, EditorGUI.Controls(x).size.Height), New Point(e.X, e.Y)) Then
            '    If TypeOf EditorGUI.Controls(x) Is SFMLTrackbar Then
            '        DirectCast(EditorGUI.Controls(x), SFMLTrackbar).SetClick(False)

            '    End If
            'End If
        Next
    End Sub
#End Region

End Module

