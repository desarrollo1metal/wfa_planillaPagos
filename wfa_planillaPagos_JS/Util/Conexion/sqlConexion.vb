Imports System.Data.SqlClient
Imports Util
Public Class sqlConexion

    ' Se declara las variables globales
    Dim SBOCompany As SAPbobsCOM.Company

    Public Shared Function Conectar(ByVal ps_server As String, ByVal ps_dataBase As String, ByVal ps_user As String, ByVal ps_password As String) As String
        '' Return "Server=192.168.1.3; DataBase=Z_PRUEBAS_MIMSA; user id=sa; password=6m1$1$t3m@$"
        ' Return "Server=.; DataBase=Sbo_ComercialMendoza; user id=sa; password=25841867"
        'Return "Server=192.168.1.3; DataBase=Sbo_ComercialMendoza; user id=sa; password=6m1$1$t3m@$"
        Return "Server=" & ps_server & "; DataBase=" & ps_dataBase & "; user id=" & ps_user & "; password=" & ps_password
    End Function

    Public Function sbo_conectar(ByVal ps_SBOUsrName As String _
                           , ByVal ps_SBOPass As String) As SAPbobsCOM.Company
        Try

            'Se declara las variables del metodo
            Dim li_resultado As Integer = 0

            ' Se asigna las propiedades al objeto Company
            SBOCompany = New SAPbobsCOM.Company
            SBOCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2017
            SBOCompany.DbUserName = S_DbUserName
            SBOCompany.DbPassword = S_SQLPassword
            SBOCompany.Server = "192.168.1.2"
            SBOCompany.LicenseServer = "192.168.1.2:30000"
            SBOCompany.CompanyDB = S_CompanyDB
            SBOCompany.UserName = ps_SBOUsrName
            SBOCompany.Password = ps_SBOPass
            SBOCompany.UseTrusted = False

            ' Se conecta la compañia
            li_resultado = SBOCompany.Connect()

            If li_resultado = 0 Then
                ' Se muestra un mensaje de exito
                MsgBox("Conectado Correctamente")

                'Se retorna el objeto Company
                Return SBOCompany
            Else
                'MsgBox(ErrCode & " " & ErrMsg)
                MsgBox("Error de Autentificación " & li_resultado.ToString)
                Return Nothing
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function str_comprobarConexion(ByVal ps_SQLUsrName As String _
                                        , ByVal ps_SQLPass As String _
                                        , ByVal ps_DBServer As String _
                                        , ByVal ps_SBOCompany As String) As String
        Try

            ' Se realiza la conexion para comprobar los datos recibidos
            Dim cn As New SqlConnection(Conectar(ps_DBServer, ps_SBOCompany, ps_SQLUsrName, ps_SQLPass))

            ' Si no ocurrio ningun error, se almacenan los datos recibidos
            S_DbUserName = ps_SQLUsrName
            S_SQLPassword = ps_SQLPass
            S_CompanyDB = ps_SBOCompany
            S_Server = ps_DBServer

            ' Si no ocurrio ningun error, se retorna TRUE
            Return ""

        Catch ex As Exception
            Return "Error de Conexion: " & ex.Message
        End Try
    End Function


End Class
