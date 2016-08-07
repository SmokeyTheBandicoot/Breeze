Imports System.Math
Imports System.Drawing
Imports SFML.Graphics
Imports SFML.System
Imports SFML.Window
Imports NAudio.Wave
Imports System.Threading
Imports System.Windows.Forms
Imports GameShardsCoreSFML

Module MainBackbone

    'Main Window
    Public WithEvents window As RenderWindow

    'GUIs
    Public MainGameGUI As New GUI
    Public OptionGUI As New GUI
    Public EditorGUI As New GUI

    'Core
    Public GGeom As New GameShardsCore.Base.Geometry.Geometry
    Public GUtils As New Utils

    'Resources
    Public ResourceFold As String = "GameShardsCore\Resources\Fonts"

    'GameStates
    Public CurrentState As GameStates

    'Audio
    'Sound
    Public RdMusic As New AudioFileReader("C:\GameShardsSoftware\Resources\Music\TerraSwoopForceTheme.mp3")
    Public WaMusic As New WaveOut
    Public MusicIsMuted As Boolean = False
    'Music
    Public RdSound As AudioFileReader
    Public WaSound As New WaveOut
    Public SoundIsMuted As Boolean = False

    'Font
    Public GlobalFont As SFML.Graphics.Font = New SFML.Graphics.Font("crash-a-like.ttf")

    Sub Main()
        'CurrentState = GameStates.
        PreInit()
        Initialize()
        PostInitMainMenu()
        GUILoadMainMenu()
        PostInitMainGame()
        GUILoadMainGame()
        PostInitLevelEditor()
        GUILoadLevelEditor()
        PostInitLevelSelect()
        GUILoadLevelSelect()


        'Graphics thread
        While Window.IsOpen

            'Common
            'Do events
            Window.DispatchEvents()

            'Clear window
            Window.Clear()

            'Do the things to do
            If CurrentState.Name.ToUpper = "MAINGAME" Then
                DoMainGame()
            ElseIf CurrentState.Name.ToUpper = "LEVELEDITOR" Then
                DoEditor()
            ElseIf CurrentState.Name.ToUpper = "MAINMENU" Then
                DoMainMenu()
            ElseIf CurrentState.Name.ToUpper = "LEVELSELECT" Then
                DoLevelSelect()
            End If

            'Invalidate the window
            Window.Display()

        End While
    End Sub


    ''' <summary>
    ''' Initializes the window and the elements
    ''' </summary>
    Public Sub PreInit()

        Console.WriteLine("Starting Pre-Initialization")
        window = New RenderWindow(VideoMode.DesktopMode, "Breeze", Styles.None)

        window.SetFramerateLimit(60)
        Console.WriteLine("Created a new RenderWindow with MAXFPS set to 60")
        window.RequestFocus()
        window.SetActive()
        Console.WriteLine("Focus Request Sent")
        Console.WriteLine("Pre-Initialization finished!")
    End Sub

    Public Sub Initialize()
        'Do here the things to do when the application starts
        WaMusic.Init(RdMusic)
        WaMusic.Play()

        CurrentState = GameStates.MainMenu

    End Sub

    Sub WindowClick(ByVal sender As Object, e As MouseButtonEventArgs) Handles window.MouseButtonPressed
        Select Case True
            Case CurrentState.Name.ToUpper = "MAINGAME"
                MainGameWindowClick(sender, e)
            Case CurrentState.Name.ToUpper = "LEVELEDITOR"
                EditorWindowClick(sender, e)
            Case CurrentState.Name.ToUpper = "MAINMENU"
                MainMenuWindowClick(sender, e)
            Case CurrentState.Name.ToUpper = "LEVELSELECT"
                LevelSelectWindowClick(sender, e)
        End Select
    End Sub

    Sub WindowClosed(ByVal sender As Object, ByVal e As EventArgs) Handles window.Closed
        window.Close()
        'Dim window = CType(sender, RenderWindow)
    End Sub

    Sub WindowKeyDown(ByVal sender As Object, e As SFML.Window.KeyEventArgs) Handles window.KeyPressed

        Select Case True
            Case CurrentState.Name.ToUpper = "MAINGAME"
                MainGameKeyDown(sender, e)
            Case CurrentState.Name.ToUpper = "LEVELEDITOR"
                EditorKeyDown(sender, e)
            Case CurrentState.Name.ToUpper = "MAINMENU"
            Case CurrentState.Name.ToUpper = "LEVELSELECT"

        End Select
    End Sub

    Sub WindowKeyUp(ByVal sender As Object, e As SFML.Window.KeyEventArgs) Handles window.KeyReleased
        Select Case True
            Case CurrentState.Name.ToUpper = "MAINGAME"
                MainGameKeyUp(sender, e)
            Case CurrentState.Name.ToUpper = "LEVELEDITOR"
                EditorKeyup(sender, e)
            Case CurrentState.Name.ToUpper = "MAINMENU"
            Case CurrentState.Name.ToUpper = "LEVELSELECT"
        End Select
    End Sub

    Sub WindowMouseMove(sender As Object, e As MouseMoveEventArgs) Handles window.MouseMoved
        Select Case True
            Case CurrentState.Name.ToUpper = "MAINGAME"

            Case CurrentState.Name.ToUpper = "LEVELEDITOR"

            Case CurrentState.Name.ToUpper = "MAINMENU"
                MainMenuMouseMoved(sender, e)
            Case CurrentState.Name.ToUpper = "LEVELSELECT"
                LevelSelectMouseMoved(sender, e)
        End Select
    End Sub

End Module
