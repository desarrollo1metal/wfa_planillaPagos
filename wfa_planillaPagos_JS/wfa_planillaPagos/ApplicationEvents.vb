Imports log4net
Imports log4net.Config
Public Module ApplicationEvents
    Private ReadOnly log As ILog = LogManager.GetLogger(GetType(ApplicationEvents))

    Public Sub InitializeLogging()
        XmlConfigurator.Configure()
        log.Info("Aplicación iniciada - Logging configurado correctamente")
    End Sub
End Module
