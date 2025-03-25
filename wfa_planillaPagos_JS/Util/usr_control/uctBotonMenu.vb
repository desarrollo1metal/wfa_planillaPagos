Imports System.Windows.Forms
Imports System.Drawing

Public Class uctBotonMenu

    ' Eventos
    Public Event evt_btnMenuClick As eventHandler

    'Atributos
    Private o_propBoton As Button
    Private i_tipoMenu As Integer
    Private s_funcion As String
    Private i_menuPadre As String
    Private s_nomMenu As String
    Private i_id As Integer
    Private i_nivel As Integer
    Private s_clase As String
    Private s_proyecto As String
    Private s_form As String
    Private s_configSis As String

    ' Propiedades
    Public Property TipoMenu() As Integer
        Get
            Return i_tipoMenu
        End Get
        Set(ByVal value As Integer)
            i_tipoMenu = value
            sub_asigPropiedadesBoton()
        End Set
    End Property

    Public Property Funcion() As String
        Get
            Return s_funcion
        End Get
        Set(ByVal value As String)
            s_funcion = value
            lblTextoBtn.Tag = s_funcion
        End Set
    End Property

    Public Property MenuPadre() As String
        Get
            Return i_menuPadre
        End Get
        Set(ByVal value As String)
            i_menuPadre = value
        End Set
    End Property

    Public Property NombreMenu() As String
        Get
            Return s_nomMenu
        End Get
        Set(ByVal value As String)
            s_nomMenu = value
            lblTextoBtn.Text = s_nomMenu
        End Set
    End Property

    Public Property Id() As Integer
        Get
            Return i_id
        End Get
        Set(ByVal value As Integer)
            i_id = value
        End Set
    End Property

    Public Property Nivel() As Integer
        Get
            Return i_nivel
        End Get
        Set(ByVal value As Integer)
            i_nivel = value
            sub_ubicarTextoBoton()
        End Set
    End Property

    Public Property Clase() As String
        Get
            Return s_clase
        End Get
        Set(ByVal value As String)
            s_clase = value
        End Set
    End Property

    Public Property Proyecto() As String
        Get
            Return s_proyecto
        End Get
        Set(ByVal value As String)
            s_proyecto = value
        End Set
    End Property

    Public Property EsFormulario() As String
        Get
            Return s_form
        End Get
        Set(ByVal value As String)
            s_form = value
        End Set
    End Property

    Public Property configSis() As String
        Get
            Return s_configSis
        End Get
        Set(ByVal value As String)
            s_configSis = value
        End Set
    End Property

    ' Se asigna las propiedades al menu de acuerdo a la entidad recibida
    Private Sub sub_asigPropiedadesBoton()
        Try

            ' Se asigna el color de fondo de acuerdo al tipo de menu
            If TipoMenu = enm_tipoMenu.Titulo Then
                Me.BackColor = Drawing.Color.White
            ElseIf TipoMenu = enm_tipoMenu.Funcion Then
                Me.BackColor = Drawing.Color.LightGray
            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    ' Se ubica la etiqueta de acuerdo al nivel del menu
    Private Sub sub_ubicarTextoBoton()
        Try

            ' Se verifica el nivel del menu
            lblTextoBtn.Location = New Point(lblTextoBtn.Location.X + 10 * Nivel, lblTextoBtn.Location.Y)

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    ' Se alterna el color al ubicar el mouse sobre el control
    Private Sub sub_alternarColorFondo(pb_sobreControl As Boolean)
        Try

            ' Se indica el color de acuerdo al evento realizado
            If pb_sobreControl = True Then
                Me.BackColor = Color.Orange
            Else

                ' Se asigna el color de fondo de acuerdo al tipo de menu
                If TipoMenu = enm_tipoMenu.Titulo Then
                    Me.BackColor = Drawing.Color.White
                ElseIf TipoMenu = enm_tipoMenu.Funcion Then
                    Me.BackColor = Drawing.Color.LightGray
                End If

            End If

        Catch ex As Exception
            MsgBox(System.Reflection.Assembly.GetExecutingAssembly.GetName.Name & "." & Me.GetType.Name.ToString & "." & System.Reflection.MethodInfo.GetCurrentMethod.Name & ": " & ex.Message)
        End Try
    End Sub

    ' Eventos 
    ' - Click
    Private Sub uctBotonMenu_Click(sender As Object, e As System.EventArgs) Handles Me.Click
        RaiseEvent evt_btnMenuClick(Me, e)
    End Sub

    Private Sub lblTextoBtn_Click(sender As Object, e As System.EventArgs) Handles lblTextoBtn.Click
        RaiseEvent evt_btnMenuClick(Me, e)
    End Sub

    Private Sub uctBotonMenu_MouseEnter(sender As Object, e As System.EventArgs) Handles Me.MouseEnter
        sub_alternarColorFondo(True)
    End Sub

    ' - Mouse
    'Private Sub uctBotonMenu_MouseHover(sender As Object, e As System.EventArgs) Handles Me.MouseHover
    '    sub_alternarColorFondo(True)
    'End Sub

    Private Sub uctBotonMenu_MouseLeave(sender As Object, e As System.EventArgs) Handles Me.MouseLeave
        sub_alternarColorFondo(False)
    End Sub

    Private Sub lblTextoBtn_MouseEnter(sender As Object, e As System.EventArgs) Handles lblTextoBtn.MouseEnter
        sub_alternarColorFondo(True)
    End Sub

    'Private Sub lblTextoBtn_MouseHover(sender As Object, e As System.EventArgs) Handles lblTextoBtn.MouseHover
    '    sub_alternarColorFondo(True)
    'End Sub

    Private Sub lblTextoBtn_MouseLeave(sender As Object, e As System.EventArgs) Handles lblTextoBtn.MouseLeave
        sub_alternarColorFondo(False)
    End Sub
End Class
