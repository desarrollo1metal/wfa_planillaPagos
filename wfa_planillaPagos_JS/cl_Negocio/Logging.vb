Imports log4net
Imports log4net.Config

Public Module Logging
    Private ReadOnly _initialized As Boolean = False
    Private ReadOnly _lockObj As New Object()

    Public Sub Initialize()
        If Not _initialized Then
            SyncLock _lockObj
                If Not _initialized Then
                    XmlConfigurator.Configure()
                    Logger.Info("Logging inicializado")
                End If
            End SyncLock
        End If
    End Sub

    Public Function GetLogger(Optional caller As Type = Nothing) As ILog
        If caller Is Nothing Then
            caller = New StackTrace().GetFrame(1).GetMethod().DeclaringType
        End If
        Return LogManager.GetLogger(caller)
    End Function

    Public ReadOnly Property Logger As ILog
        Get
            Return GetLogger()
        End Get
    End Property
End Module