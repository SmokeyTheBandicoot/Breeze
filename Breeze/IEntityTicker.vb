Imports System.Drawing
Imports SFML.Graphics

Public Interface IEntityTicker
    Inherits IEntity

    Enum EntityType As Byte
        Particle
    End Enum

    Property XSpeed As Single

    Property YSpeed As Single

    Property STREntityType As EntityType

    Property CShape As CircleShape

    Property Texture As Texture

    Property Color As SFML.Graphics.Color

    Sub Tick()

    Sub Draw(ByRef w As RenderWindow)


End Interface

