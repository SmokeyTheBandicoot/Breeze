Imports System.Math
Imports System.Drawing
Imports System.Drawing.drawing2d
Imports SFML.Audio
Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window
Imports GameShardsCore
Imports GameShardsCore.BaseGameShardsCoreBETA
Imports NAudio.Wave
Imports System.Windows.forms
Imports System.Windows
Imports System.Threading

Module Module1



    Dim WithEvents window As RenderWindow

    'Core
    Dim GBase As New BaseGameShardsCoreBETA

    'Base
    Dim IsExit As Boolean = False
    Dim IsPaused As Boolean = False
    Dim ShowGrid As Boolean = False

    'Audio
    'Sound
    Dim RdMusic As New AudioFileReader("C:\GameShardsSoftware\Resources\Music\TerraSwoopForceTheme.mp3")
    Dim WaMusic As New WaveOut
    Dim MusicIsMuted As Boolean = False
    'Music
    Dim RdSound As AudioFileReader
    Dim WaSound As New WaveOut
    Dim SoundIsMuted As Boolean = False

    'Threading
    Dim GameLoopThread As Thread

    'Framerate
    Dim TimeSpan As Date
    Dim FPS As Double
    Dim IdealFPS As Integer = 30

    'Temp graphics
    'Dim g As Graphics

    'Engine
    Dim IsCont As Boolean = False
    Dim Pict As New PictureBox
    Dim angle As Single = 0
    Dim XSpeed As Single = 0
    Dim YSpeed As Single = 0
    Dim XAcceleration As Single = 0
    Dim YAcceleration As Single = 0
    Dim Gravity As Single = 0.2 'Breeze lows gravity
    Dim BreezeSpeed As Single = 0.1
    Public AngleMax As Single = 90 - 0.001
    Public MinY As Integer = 0
    Public MaxY As Integer = 646
    Public XLoc As Integer = 500
    Public Friction As Single = 0.98
    Public Env As FlightEnvironment = FlightEnvironment.Normal
    Public Wind As WeatherWind = WeatherWind.LandBreeze

    Public Enum FlightEnvironment As Byte
        Normal
        Underwater
        Moon
    End Enum

    Public Enum WeatherWind As Integer
        LandBreeze = 35 'Weakest, Arid (Right)
        SeaBreeze = 35 'Weakest, Humid (Left)
        LightBreeze = 50 'Weak, almost non-existant, Clean
        MountainBreeze = 75 'Weak-Normal, Cold, Arid
        ValleyBreeze = 75 'Weak-Normal, Warm, Humid
        ModerateBreeze = 150 'Normal, Humid
        ModerateWind = 200 'Normal-Strong, Clean
        StrongWind = 250 'Strong, Cold, Clean
        TempestWind = 350 'Stronger, Cold, Humid
        WirlWind = 500 'Strongest, Cold
    End Enum

    'Rendering Engine
    Dim BackScroll As Single = 0

    'Level
    Dim Blocks(,) As Block

    'GamePlay
    Dim Score As Long = 0
    Dim Coins As Byte = 0
    Dim Lives As Byte = 3
    Dim HP As Byte = 100

    'Graphic Elements
    Dim Player As New Sprite(New Texture("C:\\GameShardsSoftware\Resources\Sprites\Bankruptcy\[Bankruptcy]Bankruptcy.png"))




    Sub Main()
        'Console.WriteLine("Starting Sub Main")

        window = New RenderWindow(New VideoMode(800, 600), "Breeze")
        Player = New Sprite(New Texture("C:\\GameShardsSoftware\Resources\Sprites\Bankruptcy\[Bankruptcy]Bankruptcy.png"))


        'Dim Texture As New Texture("C:\\GameShardsCore\")

        Initialize()

        'Graphics thread
        While window.IsOpen

            window.Clear()
            window.DispatchEvents()
            GameLoop()
            window.Draw(Player)
            window.Display()

            Player = New Sprite(New Texture("C:\\GameShardsSoftware\Resources\Sprites\Bankruptcy\[Bankruptcy]Bankruptcy.png"), New IntRect(Pict.Left, Pict.Top, 20, 20))
        End While



    End Sub

    Public Sub GameLoop()
        'Do While IsExit = False

        Application.DoEvents()

            If IsPaused Then

            Else
                'Start calculating framerate
                TimeSpan = Now

                'Rendering
                'g.Clear(Drawing.Color.White)

                ''Show grid or not
                'If ShowGrid Then
                '    For x = 0 To 800 Step 20
                '        For y = 0 To 600 Step 20
                '            'g.DrawLine(Pens.Black, New Point(CInt(x), CInt(y)), New Point(CInt(x), CInt(y + 20)))
                '            'g.DrawLine(Pens.Black, New Point(CInt(x), CInt(y)), New Point(CInt(x + 20), CInt(y)))
                '            g.DrawRectangle(Pens.Gray, CInt(x), CInt(y), 20, 20)
                '        Next
                '    Next
                'End If

                'If MusicIsMuted Then
                '    WaMusic.Volume = 0
                'Else
                '    WaMusic.Volume = CSng(TrackBar1.Value / 100)
                'End If

                'If SoundIsMuted Then
                '    WaSound.Volume = 0
                'Else
                '    WaSound.Volume = CSng(TrackBar2.Value / 100)
                'End If

                YSpeed += Gravity
                YSpeed += YAcceleration
                XSpeed = CSng(XSpeed + (Wind / 100))
                XSpeed += XAcceleration

                XSpeed *= Friction
                YSpeed *= Friction

                'Blocks.left -= xspeed
                'Pict.Left = CInt(Pict.Left + XSpeed)
                BackScroll += XSpeed / 25
                Pict.Top = CInt(Pict.Top + YSpeed)

                If Pict.Top > window.Size.Y - Pict.Height Then
                    Pict.Top = window.Size.Y - Pict.Height
                    YSpeed *= -1
                ElseIf Pict.Top < 0 Then
                    Pict.Top = 0
                    YSpeed *= -1
                End If

                If Pict.Left > window.Size.X - Pict.Width Then
                    Pict.Left = window.Size.X - Pict.Width
                    XSpeed *= -1
                ElseIf Pict.Left < 0 Then
                    Pict.Left = 0
                    XSpeed *= -1
                End If

                'Finish calculatingf FPS
                'If Not CheckBox1.Checked Then
                '    Thread.Sleep(GBase.SmallestPositiveValueINT(CInt(1000 / IdealFPS - (Now - TimeSpan).TotalMilliseconds)))
                'End If
                'FPS = Round((1 / (Now - TimeSpan).TotalMilliseconds) * 1000, 2)
            End If

        'Loop
    End Sub

    Public Sub Initialize()
        'Do here the things to do when the application starts
        WaMusic.Init(RdMusic)
        WaMusic.Play()

        'GameLoopThread = New Thread(AddressOf GameLoop)
        'GameLoopThread.Priority = ThreadPriority.Highest
        'GameLoopThread.IsBackground = True
        'GameLoopThread.Start()
        'GameLoop()

        'YLocation will be replaced by level's startpoint
        Pict.Location = New Point(XLoc, 30)
        Pict.Size = New Size(20, 20)
    End Sub

    Sub App_Closed(ByVal sender As Object, ByVal e As EventArgs) Handles window.Closed
        Dim window = CType(sender, RenderWindow)
        window.Close()
    End Sub
End Module
