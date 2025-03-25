Imports System.IO
Imports Util

Public Class frmComp

    ' Variables del formulario
    Public s_comp As String = ""

#Region "Eventos"

    Private Sub frmComp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sub_iniForm()
    End Sub

    Private Sub btnConectar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConectar.Click
        sub_obtComp()
    End Sub

    Private Sub btnHabil_Click(sender As System.Object, e As System.EventArgs) Handles btnHabil.Click
        sub_habilitarUsr()
    End Sub

#End Region

#Region "Ini_form"

    Private Sub sub_iniForm()
        Try

            ' Se obtiene las compañias
            sub_obtCompanias()

            ' Se obtiene el usuario conectado
            sub_obtUsuarioCon()

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub sub_obtCompanias()
        Try

            ' Se obtiene el directorio donde se encuentran los archivos XML de configuracion de SAP Business One Client
            Dim ls_ruta As String = My.Application.Info.DirectoryPath
            Dim lo_directory As New DirectoryInfo(ls_ruta & "\bds\")

            ' Se obtiene los archivos del directorio 
            Dim lo_files As FileInfo() = lo_directory.GetFiles

            ' Se recorre los archivos del directorio
            For Each lo_file As FileInfo In lo_files

                ' Se verifica el nombre del archivo
                If lo_file.Name.ToLower = "bds.txt" Then

                    ' Se obtiene el control desde el formulario
                    Dim lo_cboBds As System.Windows.Forms.ComboBox = cboComp

                    ' Se lee el archivo
                    Using sr As New IO.StreamReader(lo_file.FullName)
                        While Not sr.EndOfStream
                            lo_cboBds.Items.Add(sr.ReadLine)
                        End While
                    End Using

                End If

            Next

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_obtComp()
        Try

            ' Se verifica si se seleccionó algún valor
            If cboComp.Text = Nothing Then
                MsgBox("Debe seleccionar una compañia")
                Exit Sub
            End If

            If cboComp.Text.Trim = "" Then
                MsgBox("Debe seleccionar una compañia")
                Exit Sub
            End If

            ' Se obtiene la selección 
            s_comp = Mid(cboComp.Text, 1, 3)

            ' Se verifica si el usuario ingresado es igual al de la sesión actual
            s_sysUsr = txtUsuario.Text
            s_sysPass = txtContrasena.Text

            ' Se cierra el formulario
            Me.Close()

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_obtUsuarioCon()
        Try

            ' Se obtiene el usuario de windows conectado a la compañia
            Dim ls_winUsr As String = str_obtWinUsr()

            ' Se asigna el usuario de windows a la caja de texto
            txtUsuario.Text = ls_winUsr

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    Private Sub sub_habilitarUsr()
        Try

            ' Se habilita o deshabilita los controles para el usuario y contraseña
            If txtUsuario.Enabled = True Then
                txtUsuario.Enabled = False
            End If
            If txtContrasena.Enabled = True Then
                txtContrasena.Enabled = False
            End If

            If txtUsuario.Enabled = False Then
                txtUsuario.Enabled = True
            End If
            If txtContrasena.Enabled = False Then
                txtContrasena.Enabled = True
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

#End Region

End Class