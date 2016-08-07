Imports System.Math
Imports System.Drawing
Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window
Imports NAudio.Wave
Imports System.Threading
Imports System.Windows.Forms
Imports GameShardsCoreSFML
Module MainMenu

    'Extend GUI
    Public CareerSelected As Boolean = False

    'Background
    Dim Background As New Sprite(New Texture("C:\Program Files (x86)\SMBX141\GFXPack\NSMB\NSMBWii\Backgrounds\New Super Mario Bros. Wii Custom Backgrounds\background2-19.gif"))

    'GUI location
    Dim UnitX As Integer
    Dim UnitY As Integer

    'GUI
    Dim MainMenuGUI As New GUI
    Dim MainMenuNewLoadCancelGUI As New GUI

    Dim CareerBTN As New SFMLButton
    Dim LevelEditorBTN As New SFMLButton
    Dim FreePlayBTN As New SFMLButton
    Dim ArcadeBTN As New SFMLButton
    Dim TimeTrialBTN As New SFMLButton
    Dim OptionsBTN As New SFMLButton
    Dim CreditsBTN As New SFMLButton
    Dim ExitBTN As New SFMLButton
    Dim TitleLBL As New SFMLLabel
    Dim DownTextLBL As New SFMLLabel
    Dim FBBTN As New SFMLButton
    Dim TWBTN As New SFMLButton
    Dim GHBTN As New SFMLButton
    Dim EMBTN As New SFMLButton
    Dim NewBTN As New SFMLButton
    Dim LoadBTN As New SFMLButton
    Dim CancelBTN As New SFMLButton

    Sub DoMainMenu()

        window.Draw(Background)
        MainMenuGUI.Draw(window)
        If CareerSelected Then
            MainMenuNewLoadCancelGUI.Draw(window)
        End If

    End Sub

    Sub PostInitMainMenu()
        UnitX = CInt(window.Size.X / 3.5)
        UnitY = CInt(window.Size.Y / 80)

        Background.Scale = New Vector2f(CSng(window.Size.X / Background.Texture.Size.X), CSng(window.Size.Y / Background.Texture.Size.Y))
    End Sub

    Sub GUILoadMainMenu()
        Console.WriteLine("Loading Main Menu GUI...")
        With CareerBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Career"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 48
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 30 * UnitY)
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))
            MainMenuGUI.Controls.Add(CareerBTN)
        End With

        With LevelEditorBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Level Editor"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 48
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 36 * UnitY)
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))
            MainMenuGUI.Controls.Add(LevelEditorBTN)
        End With

        With FreePlayBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Free Play"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 48
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 42 * UnitY)
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))
            MainMenuGUI.Controls.Add(FreePlayBTN)
        End With

        With TimeTrialBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Time Trials"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 48
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 48 * UnitY)
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))
            MainMenuGUI.Controls.Add(TimeTrialBTN)
        End With

        With ArcadeBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Arcade"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 48
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 54 * UnitY)
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))
            MainMenuGUI.Controls.Add(ArcadeBTN)
        End With

        With OptionsBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Options"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 48
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 60 * UnitY)
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))
            MainMenuGUI.Controls.Add(OptionsBTN)
        End With

        With CreditsBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Credits"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 48
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 66 * UnitY)
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))
            MainMenuGUI.Controls.Add(CreditsBTN)
        End With

        With ExitBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Quit"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 48
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 - .Size.Width \ 2), 72 * UnitY)
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))
            MainMenuGUI.Controls.Add(ExitBTN)
        End With

        With NewBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "New"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 48
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX \ 2, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 + CareerBTN.Size.Width \ 2 + UnitX \ 30), 30 * UnitY)
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))
            MainMenuNewLoadCancelGUI.Controls.Add(NewBTN)
        End With

        With LoadBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Load"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 48
            .Font = New Drawing.Font("crash-a-like", .SFMLFontSize)
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Size = New Size(UnitX \ 2, 5 * UnitY)
            .Location = New Point(CInt(window.Size.X \ 2 + CareerBTN.Size.Width \ 2 + UnitX \ 30), 36 * UnitY)
            .AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayout.png"))
            .SpriteToggled = New Sprite(New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\MainLayoutToggled.png"))
            MainMenuNewLoadCancelGUI.Controls.Add(LoadBTN)
        End With
        Console.WriteLine("Loaded Main Menu GUI!")
    End Sub

    Sub MainMenuWindowClick(sender As Object, e As MouseButtonEventArgs)
        For x = 0 To MainMenuGUI.Controls.Count - 1
            If TypeOf MainMenuGUI.Controls(x) Is SFMLButton Then
                If GGeom.CheckIfRectangleIntersectsPoint(DirectCast(MainMenuGUI.Controls(x), SFMLButton).Bounds, New Point(e.X, e.Y)) Then
                    CareerSelected = False
                    Select Case DirectCast(MainMenuGUI.Controls(x), SFMLButton).Text.ToUpper
                        Case "QUIT"
                            window.Close()
                        Case "CAREER"
                            CareerSelected = True

                        Case ""
                    End Select

                End If
            End If
        Next

        For x = 0 To MainMenuNewLoadCancelGUI.Controls.Count - 1
            If TypeOf MainMenuNewLoadCancelGUI.Controls(x) Is SFMLButton Then
                If GGeom.CheckIfRectangleIntersectsPoint(DirectCast(MainMenuNewLoadCancelGUI.Controls(x), SFMLButton).Bounds, New Point(e.X, e.Y)) Then
                    CareerSelected = False
                    Select Case DirectCast(MainMenuNewLoadCancelGUI.Controls(x), SFMLButton).Text.ToUpper
                        Case "NEW"
                            CurrentState = GameStates.MainGame
                        Case "LOAD"
                            CurrentState = GameStates.LevelSelect
                        Case "CANCEL"
                            CareerSelected = False
                    End Select

                End If
            End If
        Next
    End Sub

    Sub MainMenuMouseMoved(sender As Object, e As MouseMoveEventArgs)
        For x = 0 To MainMenuGUI.Controls.Count - 1
            If TypeOf MainMenuGUI.Controls(x) Is SFMLButton Then
                If GGeom.CheckIfRectangleIntersectsPoint(DirectCast(MainMenuGUI.Controls(x), SFMLButton).Bounds, New Point(e.X, e.Y)) Then
                    DirectCast(MainMenuGUI.Controls(x), SFMLButton).IsToggled = True
                Else
                    DirectCast(MainMenuGUI.Controls(x), SFMLButton).IsToggled = False
                End If
            End If
        Next

        For x = 0 To MainMenuNewLoadCancelGUI.Controls.Count - 1
            If TypeOf MainMenuNewLoadCancelGUI.Controls(x) Is SFMLButton Then
                If GGeom.CheckIfRectangleIntersectsPoint(DirectCast(MainMenuNewLoadCancelGUI.Controls(x), SFMLButton).Bounds, New Point(e.X, e.Y)) Then
                    DirectCast(MainMenuNewLoadCancelGUI.Controls(x), SFMLButton).IsToggled = True
                Else
                    DirectCast(MainMenuNewLoadCancelGUI.Controls(x), SFMLButton).IsToggled = False
                End If
            End If
        Next
    End Sub

End Module
