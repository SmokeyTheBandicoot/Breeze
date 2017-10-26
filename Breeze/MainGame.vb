Option Strict On
Option Explicit On

Imports System.Math
Imports System.Drawing
Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window
Imports NAudio.Wave
Imports System.Threading
Imports System.Windows.Forms
Imports GameShardsCore2
Imports GameShardsCore2.Physics.UnitsOfMeasurement
Imports GameShardsCore2.Geometry.Geometry2D
Imports GameShardsCoreSFML

'Imports TGUI

Module MainGame

    'Threading
    Dim GameLoopThread As Thread

    'Framerate
    Dim TimeSpan As Date
    Dim FPS As Double

    'Engine
    Dim ItemID As Integer
    Dim IsCont As Boolean = False
    Public Pict As New PictureBox


    Public Height As Double = 0
    Public Frict As Double = 0.9999
    Public Accel As New Vector2f
    Public Speed As New Vector2f


    'Should not be needed, except for cool things
    Dim mass As New Mass(1, MassUnit.Gram)

    'Will be adjusted, should be 9.8
    Dim Gravity As New Acceleration(0.2, AccelerationUnit.MeterPerSquareSecond)

    '(In newtons)
    Dim BreezeForce As New Force(2, ForceUnit.Newton)

    Public MinY As Integer = 0
    Public MaxY As Integer = 646

    Public XLoc As Integer = 500

    Public XFriction As Single = 0.98
    Public YFriction As Single = 0.98

    Public Env As FlightEnvironment = FlightEnvironment.Normal
    Public Wind As WeatherWind = WeatherWind.LandBreeze

    Public Enum FlightEnvironment As Byte
        Normal = 20
        Underwater = 10
        Moon = 5
    End Enum

    Public Enum WeatherWind As Integer
        LandBreeze = 55 'Weakest, Arid (Right) 35
        SeaBreeze = 15 'Weakest, Humid (Left) 35
        LightBreeze = 20 'Weak, almost non-existant, Clean 50
        MountainBreeze = 25 'Weak-Normal, Cold, Arid 75
        ValleyBreeze = 30 'Weak-Normal, Warm, Humid 75
        ModerateBreeze = 35 'Normal, Humid 150
        ModerateWind = 40 'Normal-Strong, Clean 200
        StrongWind = 45 'Strong, Cold, Clean 250
        TempestWind = 50 'Stronger, Cold, Humid 350
        WirlWind = 55 'Strongest, Cold 500
    End Enum

    'Rendering Engine
    Dim BackScroll As Single = 0

    'Level
    Dim level As New Level

    'GamePlay
    Dim Score As Long = 0
    Dim Flasks As Byte = 0
    Dim Parfumes As Byte = 0
    Dim HeartFragments As Integer = 0

    'Graphic Elements
    Dim Font As SFML.Graphics.Font

    Public Items As New List(Of IItemTicker)
    Public Particles As New List(Of IEntityTicker)
    Public MovableObjs As New List(Of IMovableObject)
    Public Player As New Player

    'Light Engine (Behind the scenes calculations)
    Dim BaseVariationFactor As Integer = 10 '(percent)
    Dim BaseVariationInterval As Integer = 100 '(seconds/100, half cycle)
    '"Dark" engine
    Dim Darkness As New RenderTexture(window.Size.X, window.Size.Y)
    Dim DarknessSprite As New Sprite(Darkness.Texture)
    Public LightText As New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\Ambient\Light.png")
    Public LightSprite As New Sprite(LightText)
    Public LightShade As New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\Ambient\LightShade.png")

    Dim LightDirection As SByte = 1

    Dim HUDTopLeft As New Text

    'GUI elements
    'MainGame
    Dim WithEvents LevelEditorBTN As New SFMLButton
    Dim WithEvents CloseBTN As New SFMLButton
    Dim WithEvents OptionsBTN As New SFMLButton

    Public Sub DoMainGame()

        'Start calculating FPS
        TimeSpan = Now


        'Update HUDs
        HUDTopLeft.DisplayedString = String.Format("Time: {1}{0}Position: X: {2}; Y:{3}{0}Speed: X: {4}; Y: {5}{0}Acceleration: X: {6}; Y: {7}{0}Rotation: {8}{0}Current State: {9}{0}FPS: {10}{0}LightScale: {11}", vbNewLine, Now.ToString, Pict.Left, Pict.Top, XSpeed, YSpeed, XAcceleration, YAcceleration, Player.Sprite.Rotation.ToString, BaseVariationInterval, FPS.ToString, LightSprite.Scale.ToString)
        HUDTopLeft.CharacterSize = 24
        HUDTopLeft.Color = SFML.Graphics.Color.White
        HUDTopLeft.Position = New Vector2f(0, 0)

        If Not IsPaused Then

            'Execute Physics
            GameLoop()

            'Update Player position
            Player.Location = Pict.Location
            Player.Sprite.Position = New Vector2f(Pict.Left, Pict.Top)
            Player.Sprite.Rotation = 48 * CSng(Atan(YSpeed / XSpeed))
            Player.Sprite.Color = New SFML.Graphics.Color(255, 255, 255)

            'Update CheckPoints

            'Update Items
            For x = 0 To Items.Count - 1
                Items(x).Tick() 'Player.Sprite.GetGlobalBounds)
                If Not Items(x).STRItemType = IItemTicker.ItemType.Light Then
                    Items(x).Location = New Point(CInt(Items(x).Location.X - XSpeed), CInt(Items(x).Location.Y - YSpeed))
                End If
            Next

            'Update Particles
            For y = 0 To Particles.Count - 1
                Particles(y).Tick()
                'Particles(y).Location = New Point(CInt(Particles(y).Location.X - XSpeed), CInt(Particles(y).Location.Y - YSpeed))
                Particles(y).Location = New Point(CInt(Particles(y).Location.X - XSpeed), CInt(Particles(y).Location.Y))
            Next

            'Update Blocks and backgrounds (IMovableObject)
            For x = 0 To MovableObjs.Count - 1
                MovableObjs(x).Tick()
                MovableObjs(x).Location = New Point(CInt(MovableObjs(x).Location.X - XSpeed), MovableObjs(x).Location.Y)
            Next

            'Update Background objects

            'Update Background
            Select Case level.BackGround.HScroll
                Case Background.HorizontalScrollMode.Fixed
                Case Background.HorizontalScrollMode.Repeated
                    level.BackGround.BGImage.Position = New Vector2f(level.BackGround.BGImage.Position.X - (XSpeed / level.BackGround.ScrollSpeedX), 0)
                    If level.BackGround.BGImage.Position.X + level.BackGround.BGImage.Texture.Size.X < 0 Then
                        level.BackGround.BGImage.Position = New Vector2f(0, level.BackGround.BGImage.Position.Y)
                    End If
                Case Background.HorizontalScrollMode.Stretched

            End Select



        End If

        'Draw everything

        'Draw Background
        Select Case level.BackGround.HScroll
            Case Background.HorizontalScrollMode.Fixed
            Case Background.HorizontalScrollMode.Repeated
                'If Background.Right < window.Size.X Then
                '    For x = 0 To Background.GetHowManyRepeated(window,level)
                '        Background.BGImage.Position = New Vector2f(x * Background.BGImage.Texture.Size.X - BackScroll, Background.BGImage.Position.Y)
                window.Draw(level.BackGround.BGImage)
                level.BackGround.BGImage.Position = New Vector2f(level.BackGround.BGImage.Position.X + level.BackGround.BGImage.Texture.Size.X, level.BackGround.BGImage.Position.Y)
                window.Draw(level.BackGround.BGImage)
                level.BackGround.BGImage.Position = New Vector2f(level.BackGround.BGImage.Position.X + level.BackGround.BGImage.Texture.Size.X, level.BackGround.BGImage.Position.Y)
                window.Draw(level.BackGround.BGImage)
                level.BackGround.BGImage.Position = New Vector2f(level.BackGround.BGImage.Position.X - 2 * level.BackGround.BGImage.Texture.Size.X, level.BackGround.BGImage.Position.Y)

                'End If
            Case Background.HorizontalScrollMode.Stretched

                'Case Else


        End Select


        'Draw Blocks and backgrounds (IMovableObject)
        For x = 0 To MovableObjs.Count - 1
            MovableObjs(x).Draw(window)
        Next

        'Draw Items (IItemTicker)
        For x = 0 To Items.Count - 1
            Items(x).Draw(window)
        Next

        ''Draw Lights
        'Darkness.Clear(level.Darkness)

        'Draw player
        window.Draw(Player.Sprite)

        'Draw Particles (IEntityTicker)
        For x = 0 To Particles.Count - 1
            Particles(x).Draw(window)
        Next

        For x = 0 To Items.Count - 1
            If Items(x).STRItemType = IItemTicker.ItemType.Light Then
                'Darkness.Draw(DirectCast(Items(x), Light).Sprite, New RenderStates(BlendMode.Multiply))
                'LightSprite.Position = New Vector2f((Pict.Left + Pict.Width), CSng(Pict.Top + Pict.Height / 2))
                LightSprite.Scale = New Vector2f(CSng((DirectCast(Items(x), Light).Size.Width / LightText.Size.X) + (Sin(BaseVariationInterval / 100 * PI)) * DirectCast(Items(x), Light).Size.Width * DirectCast(Items(x), Light).Variation / (100 * 100)), CSng((DirectCast(Items(x), Light).Size.Width / LightText.Size.Y) + (Sin(BaseVariationInterval / 100 * PI) * DirectCast(Items(x), Light).Size.Width * DirectCast(Items(x), Light).Variation / (100 * 100))))
                LightSprite.Position = DirectCast(Items(x), Light).Sprite.Position 'New Vector2f(DirectCast(Items(x), Light).Sprite.Position.X, DirectCast(Items(x), Light).Sprite.Position.Y)
                'LightSprite.Color = DirectCast(Items(x), Light).SpriteColor
                Darkness.Draw(LightSprite, New RenderStates(BlendMode.Multiply))
                'MsgBox(LightSprite.Position.ToString)
            End If
        Next
        'Darkness.Display()

        'Draw Darkness
        'window.Draw(DarknessSprite)

        'Draw GUI
        MainGameGUI.Draw(window)

        'Draw HUDs
        window.Draw(HUDTopLeft)

        'Regulate TPS/FPS
        'Thread.CurrentThread.Sleep(CInt(GameShardsCore.Base.Math.Operators.ValueContained(1000 / 50 - (Now - TimeSpan).TotalMilliseconds, Double.PositiveInfinity, 0, True)))

        'Finish calculating FPS
        FPS = 1000 / (Now - TimeSpan).TotalMilliseconds
    End Sub

    Public Sub GameLoop()
        'Do While IsExit = False

        Application.DoEvents()

        If IsPaused Then

        Else
            'Start calculating framerate
            TimeSpan = Now

            'Calculate Light Span Cycle
            BaseVariationInterval += LightDirection
            If BaseVariationInterval > 100 Then
                LightDirection = -1
            ElseIf BaseVariationInterval < 0 Then
                LightDirection = 1
            End If

            'Do physics

            'PHYSICS V3.0
            Height = -Player.Bounds.Top





            'PHYSICS V1.0
            'YSpeed += Gravity
            'YSpeed += YAcceleration
            'XSpeed = CSng(XSpeed + (Wind / 100))
            'XSpeed += XAcceleration

            'XSpeed *= Friction
            'YSpeed *= Friction


            'PHYSICS V2.0
            'XAcceleration = CSng((BreezeForce.Value - XFriction * XSpeed) / mass.Value)
            'YAcceleration = CSng(YFriction * YSpeed - mass.Value * Gravity.Value)

            'XSpeed += XAcceleration
            'YSpeed += YAcceleration
            'angle = CSng(Atan(YSpeed / XSpeed))




            'PHYSICS V2.0
            'xfriction: f(x) = (x + 0.4) * log (x + 0.4) + 0.16
            'yfriction: f(x) = (|4 - x| + 0.4) * log ((|4 - (x)| + 0.4)) + 0.16
            'Where x is (Angle * AngleCoefficient, a value contained between 0 and 4)

            'PHYSICS V2.1
            'XFriction = CSng(Abs((angle * AngleCoeff) + 0.4) * Log(Abs((angle * AngleCoeff) + 0.4)) + 0.16 + XMinSpeed)
            'YFriction = CSng((4 - Abs((angle * AngleCoeff)) + 0.4) * Log(Abs((4 - (angle * AngleCoeff)) + 0.4)) + 0.16)



            'Blocks.left -= xspeed
            'Pict.Left = CInt(Pict.Left + XSpeed)
            BackScroll += XSpeed / 25
            Pict.Top = CInt(Pict.Top + YSpeed)

            If Pict.Top > window.Size.Y - Pict.Height Then
                Pict.Top = CInt(window.Size.Y - Pict.Height)
                YSpeed *= -1
            ElseIf Pict.Top < 0 Then
                Pict.Top = 0
                YSpeed *= -1
            End If

            If Pict.Left > window.Size.X - Pict.Width Then
                Pict.Left = CInt(window.Size.X - Pict.Width)
                XSpeed *= -1
            ElseIf Pict.Left < 0 Then
                Pict.Left = 0
                XSpeed *= -1
            End If

            'Do light calculations
            'If Items.Count > 0 Then
            '    For x = 0 To Items.Count - 1
            '        If Items(x).Item = Item.ItemType.Light Then
            '            Dim r As New IntRect(CInt(DirectCast(Items(x), Light).Location.X), CInt(DirectCast(Items(x), Light).Location.Y), CInt(DirectCast(Items(x), Light).AoE), CInt(DirectCast(Items(x), Light).AoE))
            '            If Player.TextureRect.Intersects(r) Then
            '                Player.Color = (DirectCast(Items(x), Light).Color)
            '            End If
            '        End If
            '    Next
            'End If
        End If
    End Sub

#Region "Loading"
    Public Sub PostInitMainGame()

        ''Configure Darkness
        'Darkness.Clear(New SFML.Graphics.Color(0, 0, 0, 255))
        'Darkness.Draw(LightSprite, New RenderStates(BlendMode.Multiply))
        'Darkness.Display()

        'Configure Lighting
        LightSprite.Origin = New Vector2f(LightSprite.Origin.X + LightSprite.GetGlobalBounds.Width / 2, LightSprite.Origin.Y + LightSprite.GetGlobalBounds.Height / 2)
        LightSprite.Scale = New Vector2f(CSng((level.PlayerLightIntensity / LightSprite.Texture.Size.X) + Sin(BaseVariationInterval / 1000)), CSng((level.PlayerLightIntensity / LightSprite.Texture.Size.Y) + Sin(BaseVariationInterval / 1000)))

        ''Configure Light
        'PLight.Radius = Pict.Width * 2
        'PLight.Origin = New Vector2f(PLight.Origin.X + PLight.Radius, PLight.Origin.Y + PLight.Radius)
        'PLight.FillColor = New SFML.Graphics.Color(255, 255, 255, 0)

        'Configure HUD
        HUDTopLeft.Font = New SFML.Graphics.Font("crash-a-like.ttf")
        HUDTopLeft.CharacterSize = 20

        'YLocation will be replaced by level's startpoint
        Player.Sprite = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\Characters\PaperPlane.png"))
        Pict.Location = New Point(XLoc, 30)
        Pict.Size = New Size(20, 20)

        'Load the correct level - should go in tandem with MainMenu
        level = LoadLevel("No path yet")
        level = New Level
        level.Darkness = New SFML.Graphics.Color(0, 0, 0, 248)
        level.PlayerLightIntensity = 300
        level.BackGround = New Background(New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\Levels\Meadows\background2-13.gif")))
        level.BackGround.HScroll = Background.HorizontalScrollMode.Repeated
        level.BackGround.ScrollSpeedX = 10
        level.BackGround.BGImage.Position = New Vector2f(0, 0)
        level.Width = 20000



        For x = 0 To 200
            Dim b As New Block
            b.Hardness = 255
            b.Sprite = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\Levels\Meadows\block-29.gif"))
            b.XEspr = "0"
            b.YEspr = "0"
            b.Size = New Size(32, 32)
            b.Location = New Point(0 + x * 32, 400)
            b.Sprite.Position = New Vector2f(x * 32, 400)


            MovableObjs.Add(b)
        Next

        'Load the items from the level

        'Lights have to be loaded last, because they are attached to preiously loaded objects
        'Dim p1 As New Particle(3, 17, 0, New Point(800, 100), New Size(50, 50), 100, New Texture("C:\GameShardsSoftware\Resources\Sprites\Breeze\Aqua.png"), New SFML.Graphics.Color(255, 255, 255), 3, 0, -1, New Point(0, 0))
        Dim p1 As New Particle(3, 17, 0, New Point(800, 100), New Size(50, 50), 100, Nothing, New SFML.Graphics.Color(255, 0, 0, 128), 3, 0, -1, New Point(0, 0))
        Particles.Add(p1)

        Dim l2 As New Light(2, Nothing, Nothing, Nothing, CInt(level.PlayerLightIntensity * 2), 0, New SFML.Graphics.Color(255, 0, 0, 128), New Point(0, 0))
        l2.SetAttachedObj(DirectCast(Particles(0), IEntity))
        Items.Add(l2)

        'Dim l As New Light(1, Nothing, Nothing, Nothing, level.PlayerLightIntensity * 4, 0, level.PlayerLightColor, New Point(40, 10))
        Dim l As New Light(1, Nothing, Nothing, Nothing, level.PlayerLightIntensity * 2, 0, New SFML.Graphics.Color(0, 0, 255, 128), New Point(40, 10))
        l.SetAttachedObj(DirectCast(Player, IEntity))
        Items.Add(l)

        'level.BackGround.BGImage.Color = New SFML.Graphics.Color(level.BackGround.BGImage.Color.R, level.BackGround.BGImage.Color.G, level.BackGround.BGImage.Color.B, 128)

    End Sub

    Sub GUILoadMainGame()
        Console.WriteLine("Loading Main Game GUI...")

        With LevelEditorBTN
            .TextAlign = ContentAlignment.MiddleLeft
            .Text = "LevelEditor"
            .IDStr = "LEVELEDITOR"
            .ForeColor = Drawing.Color.Blue
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 48
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Location = New Point(0, CInt(window.Size.Y - 50))
            .Size = New Size(900, 50)
            '.AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512.png"))
            .SpriteToggled = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512T.png"))

            MainGameGUI.Controls.Add(LevelEditorBTN)
        End With

        With CloseBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "X"
            .IDStr = "CLOSE"
            .ForeColor = Drawing.Color.Black
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 48
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Location = New Point(CInt(window.Size.X - 50), 0)
            .Size = New Size(50, 50)
            '.AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512.png"))
            .SpriteToggled = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512T.png"))

            MainGameGUI.Controls.Add(CloseBTN)
        End With

        With OptionsBTN
            .TextAlign = ContentAlignment.MiddleCenter
            .Text = "Options"
            .IDStr = "OPTIONS"
            .ForeColor = Drawing.Color.Black
            .SFMLFont = New SFML.Graphics.Font("crash-a-like.ttf")
            .SFMLFontSize = 32
            .Toggleable = True
            .ToggleChangesSprite = False
            .ToggleChangesColor = True
            .Location = New Point(CInt(window.Size.X - 250), 0)
            .Size = New Size(150, 50)
            '.AutoPadding = True
            .ColorNormal = New SFML.Graphics.Color(255, 255, 255, 255)
            .ColorToggled = New SFML.Graphics.Color(200, 200, 200, 200)
            .SpriteNormal = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512.png"))
            .SpriteToggled = New Sprite(New Texture("F:\Backup\GameShardsSoftware\GameShardsBreeze\Resources\Sprites\GUI\BTN512T.png"))

            MainGameGUI.Controls.Add(OptionsBTN)
        End With

        Console.WriteLine("Successfully Loaded ""LevelEditorButton""")
        Console.WriteLine("Main GameGUI loaded successfully!")
    End Sub
#End Region

#Region "Handles"

    Sub MainGameKeyDown(sender As Object, e As Keys)
        Select Case True

            Case e = Keys.W Or e = Keys.Up


            Case e = Keys.S Or e = Keys.Down
                Select Case Env
                    Case FlightEnvironment.Normal
                        YAcceleration = 0.2
                    Case FlightEnvironment.Underwater
                        YAcceleration = 0.1
                    Case FlightEnvironment.Moon
                        YAcceleration = 0.05
                End Select

            Case e = Keys.D Or e = Keys.Right
                angle -= 0.2F

            Case e = Keys.A Or e = Keys.Left
                angle += 0.2F
                'Select Case Env
                '    Case FlightEnvironment.Normal
                '        XAcceleration = CSng(BreezeSpeed - 0.1)
                '    Case FlightEnvironment.Underwater
                '        XAcceleration = CSng(BreezeSpeed - 0.05)
                '    Case FlightEnvironment.Moon
                '        XAcceleration = CSng(BreezeSpeed - 0.025)
                'End Select
                XAcceleration = CSng(-Wind / 100) 'CSng(Wind / 100 - (Wind / 200))
        End Select
    End Sub

    Sub MainGameKeyUp(ByVal sender As Object, e As Keys)

        'Select Case Env
        '    Case FlightEnvironment.Normal
        '        Gravity = 0.2
        '    Case FlightEnvironment.Underwater
        '        Gravity = 0.1
        '    Case FlightEnvironment.Moon
        '        Gravity = 0.05
        'End Select

        'Select Case True
        '    Case e = Keys.Right Or e = Keys.Left
        '        XAcceleration = BreezeSpeed
        'End Select


        'XAcceleration = 0
        'YAcceleration = 0
    End Sub

    Sub MainGameMouseMoved(sender As Object, e As MouseMoveEventArgs)
        For x = 0 To MainGameGUI.Controls.Count - 1
            If TypeOf MainGameGUI.Controls(x) Is SFMLButton Then
                If CheckIfRectangleIntersectsPoint(DirectCast(MainGameGUI.Controls(x), SFMLButton).Bounds, New Point(e.X, e.Y)) Then
                    DirectCast(MainGameGUI.Controls(x), SFMLButton).IsToggled = True
                Else
                    DirectCast(MainGameGUI.Controls(x), SFMLButton).IsToggled = False
                End If
            End If
        Next
    End Sub


    Sub MainGameWindowClick(sender As Object, e As MouseButtonEventArgs)
        For x = 0 To MainGameGUI.Controls.Count - 1
            If TypeOf MainGameGUI.Controls(x) Is SFMLButton Then
                If CheckIfRectangleIntersectsPoint(DirectCast(MainGameGUI.Controls(x), SFMLButton).Bounds, New Point(e.X, e.Y)) Then
                    CareerSelected = False
                    Select Case DirectCast(MainGameGUI.Controls(x), SFMLButton).IDStr.ToUpper
                        Case "LEVELEDITOR"
                            CurrentState = GameStates.LevelEditor
                        Case "CLOSE"
                            CurrentState = GameStates.MainMenu
                        Case "OPTIONS"
                            CurrentState = GameStates.InGameOptions
                        Case ""
                    End Select

                End If
            End If
        Next
    End Sub

#End Region
End Module
