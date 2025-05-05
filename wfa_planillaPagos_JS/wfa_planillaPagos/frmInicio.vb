Imports cl_Negocio
Imports Util
Imports log4net
Public Class frmInicio
    Inherits frmComun



    Private Shared ReadOnly log As ILog = LogManager.GetLogger(GetType(frmInicio))


#Region "Inicializacion"

    ' Se asigna el objeto de negocio asociado al formulario
    Overrides Sub sub_iniObjNegocio()
        Try
            log4net.Config.XmlConfigurator.Configure()

            log.Info("Iniciando Planilla Cobranza ...")
            ' Se asigna el objeto de negocio asociado a este formulario
            o_objNegocio = New classInicio(Me)

        Catch ex As Exception

            log.Error("Error en inicio , " & ex.Message.ToString())

            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

    Private Sub frmInicio_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class