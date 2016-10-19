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

    Property Sprite As Sprite

    Property SpriteColor As SFML.Graphics.Color

    Sub Tick()

    Sub Draw(ByRef w As RenderWindow)


End Interface

