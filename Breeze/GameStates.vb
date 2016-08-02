Public Class GameStates

    Private _Name As String
    Private _SubStates As List(Of String)

    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property

    Private Sub New(ByVal Name As String, SubStates As List(Of String))
        _Name = Name
    End Sub

    Public ReadOnly Property Loading() As GameStates
        Get
            Return New GameStates("Loading", {"PreInit", "Init", "PostInit"}.ToList)
        End Get
    End Property

    Public ReadOnly Property MainMenu() As GameStates
        Get
            Return New GameStates("MainMenu", {"SingleState"}.ToList)
        End Get
    End Property

    Public ReadOnly Property InGameOptions() As GameStates
        Get
            Return New GameStates("InGameOptions", {"SingleState"}.ToList)
        End Get
    End Property

    Public ReadOnly Property MainMenuOptions() As GameStates
        Get
            Return New GameStates("MainMenuOptions", {"SingleState"}.ToList)
        End Get
    End Property

    Public ReadOnly Property MainGame() As GameStates
        Get
            Return New GameStates("MainGame", {"Running", "UnFocused", "Paused", "ExitPending"}.ToList)
        End Get
    End Property

    Public ReadOnly Property Credits() As GameStates
        Get
            Return New GameStates("Credits", {"SingleState"}.ToList)
        End Get
    End Property
End Class
