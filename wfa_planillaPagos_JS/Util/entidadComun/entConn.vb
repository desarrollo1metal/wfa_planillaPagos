Imports System.IO

Public Class entConn

    Private s_dbServ As String
    Private s_dbUser As String
    Private s_dbPass As String
    Private s_SAPComp As String
    Private s_SAPUser As String
    Private s_SAPPass As String
    Private s_ipServ As String
    Private s_company As String
    Private s_svrUsr As String
    Private s_svrPwd As String

    Public Property SAPPass() As String
        Get
            Return s_SAPPass
        End Get
        Set(ByVal value As String)
            s_SAPPass = value
        End Set
    End Property

    Public Property SAPUser() As String
        Get
            Return s_SAPUser
        End Get
        Set(ByVal value As String)
            s_SAPUser = value
        End Set
    End Property

    Public Property SAPComp() As String
        Get
            Return s_SAPComp
        End Get
        Set(ByVal value As String)
            s_SAPComp = value
        End Set
    End Property

    Public Property dbPass() As String
        Get
            Return s_dbPass
        End Get
        Set(ByVal value As String)
            s_dbPass = value
        End Set
    End Property

    Public Property dbUser() As String
        Get
            Return s_dbUser
        End Get
        Set(ByVal value As String)
            s_dbUser = value
        End Set
    End Property

    Public Property dbServ() As String
        Get
            Return s_dbServ
        End Get
        Set(ByVal value As String)
            s_dbServ = value
        End Set
    End Property

    Public Property ipServ() As String
        Get
            Return s_ipServ
        End Get
        Set(ByVal value As String)
            s_ipServ = value
        End Set
    End Property

    Public Property company() As String
        Get
            Return s_company
        End Get
        Set(ByVal value As String)
            s_company = value
        End Set
    End Property

    Public Property ServerUser() As String
        Get
            Return s_svrUsr
        End Get
        Set(ByVal value As String)
            s_svrUsr = value
        End Set
    End Property

    Public Property ServerPassword() As String
        Get
            Return s_svrPwd
        End Get
        Set(ByVal value As String)
            s_svrPwd = value
        End Set
    End Property

    Public Function str_obtDatosConn() As String

        ' Se declara una variab le para las etiquetas
        Dim ls_etq As String = ""

        ' Se declara una variab le para las etiquetas de error
        Dim ls_etqErr As String = ""

        Try

            ' Se obtiene la ruta de la aplicacion
            Dim ls_ruta As String = My.Application.Info.DirectoryPath

            ' Se declara una lista para las lineas del archivo de texto
            Dim lo_lines As New List(Of String)

            ' Se obtiene la ruta del archivo
            ls_ruta = ls_ruta & "\conn\" & s_company.Replace(" ", "") & "\conn.txt"
            'ls_ruta = ls_ruta & "\conn\conn.txt"

            ' Se obtiene el objeto StreamReader y se completa la lista de lineas
            Using lo_sr As New StreamReader(ls_ruta)
                While Not lo_sr.EndOfStream
                    lo_lines.Add(lo_sr.ReadLine)
                End While
            End Using

            ' Se declara una variable para indicar si hubo alguna etiqueta que no contiene ningun valor
            Dim lb_ind As Boolean = False ' False indica que no falta ningun valor

            ' Se recorre el listado de lineas
            For Each ls_line As String In lo_lines

                ' Se verifica el tipo de información que contiene la linea
                ls_etq = "<dbserv>"
                If ls_line.ToLower.Contains(ls_etq) Then

                    ' Se declara una variable para el valor de la etiqueta
                    Dim ls_info As String = ""

                    ' Se obtiene el valor de la etiqueta
                    ls_info = Mid(ls_line, ls_line.ToLower.IndexOf("<dbserv>") + "<dbserv>".Length + 1, ls_line.ToLower.IndexOf("</dbserv>") - "</dbserv>".Length + 1)

                    ' Se asigna el valor de la etiqueta
                    s_dbServ = ls_info

                    ' Se verifica si se obtuvo el valor para la etiqueta
                    If s_dbServ.Trim = "" Then
                        lb_ind = True
                        ls_etqErr = ls_etq
                    End If

                End If

                ls_etq = "<dbuser>"
                If ls_line.ToLower.Contains(ls_etq) Then

                    ' Se declara una variable para el valor de la etiqueta
                    Dim ls_info As String = ""

                    ' Se obtiene el valor de la etiqueta
                    ls_info = Mid(ls_line, ls_line.ToLower.IndexOf("<dbuser>") + "<dbuser>".Length + 1, ls_line.ToLower.IndexOf("</dbuser>") - "</dbuser>".Length + 1)

                    ' Se asigna el valor de la etiqueta
                    s_dbUser = ls_info

                    ' Se verifica si se obtuvo el valor para la etiqueta
                    If s_dbUser.Trim = "" Then
                        lb_ind = True
                        ls_etqErr = ls_etq
                    End If

                End If

                ls_etq = "<dbpass>"
                If ls_line.ToLower.Contains(ls_etq) Then

                    ' Se declara una variable para el valor de la etiqueta
                    Dim ls_info As String = ""

                    ' Se obtiene el valor de la etiqueta
                    ls_info = Mid(ls_line, ls_line.ToLower.IndexOf("<dbpass>") + "<dbpass>".Length + 1, ls_line.ToLower.IndexOf("</dbpass>") - "</dbpass>".Length + 1)

                    ' Se asigna el valor de la etiqueta
                    s_dbPass = ls_info

                    ' Se verifica si se obtuvo el valor para la etiqueta
                    If s_dbPass.Trim = "" Then
                        lb_ind = True
                        ls_etqErr = ls_etq
                    End If

                End If

                ls_etq = "<sapcomp>"
                If ls_line.ToLower.Contains(ls_etq) Then

                    ' Se declara una variable para el valor de la etiqueta
                    Dim ls_info As String = ""

                    ' Se obtiene el valor de la etiqueta
                    ls_info = Mid(ls_line, ls_line.ToLower.IndexOf("<sapcomp>") + "<sapcomp>".Length + 1, ls_line.ToLower.IndexOf("</sapcomp>") - "</sapcomp>".Length + 1)

                    ' Se asigna el valor de la etiqueta
                    s_SAPComp = ls_info

                    ' Se verifica si se obtuvo el valor para la etiqueta
                    If s_SAPComp.Trim = "" Then
                        lb_ind = True
                        ls_etqErr = ls_etq
                    End If

                End If

                ls_etq = "<sapuser>"
                If ls_line.ToLower.Contains(ls_etq) Then

                    ' Se declara una variable para el valor de la etiqueta
                    Dim ls_info As String = ""

                    ' Se obtiene el valor de la etiqueta
                    ls_info = Mid(ls_line, ls_line.ToLower.IndexOf("<sapuser>") + "<sapuser>".Length + 1, ls_line.ToLower.IndexOf("</sapuser>") - "</sapuser>".Length + 1)

                    ' Se asigna el valor de la etiqueta
                    s_SAPUser = ls_info

                    ' Se verifica si se obtuvo el valor para la etiqueta
                    If s_SAPUser.Trim = "" Then
                        lb_ind = True
                        ls_etqErr = ls_etq
                    End If

                End If

                ls_etq = "<sappass>"
                If ls_line.ToLower.Contains(ls_etq) Then

                    ' Se declara una variable para el valor de la etiqueta
                    Dim ls_info As String = ""

                    ' Se obtiene el valor de la etiqueta
                    ls_info = Mid(ls_line, ls_line.ToLower.IndexOf("<sappass>") + "<sappass>".Length + 1, ls_line.ToLower.IndexOf("</sappass>") - "</sappass>".Length + 1)

                    ' Se asigna el valor de la etiqueta
                    s_SAPPass = ls_info

                    ' Se verifica si se obtuvo el valor para la etiqueta
                    If s_SAPPass.Trim = "" Then
                        lb_ind = True
                        ls_etqErr = ls_etq
                    End If

                End If

                ls_etq = "<ipserv>"
                If ls_line.ToLower.Contains(ls_etq) Then

                    ' Se declara una variable para el valor de la etiqueta
                    Dim ls_info As String = ""

                    ' Se obtiene el valor de la etiqueta
                    ls_info = Mid(ls_line, ls_line.ToLower.IndexOf("<ipserv>") + "<ipserv>".Length + 1, ls_line.ToLower.IndexOf("</ipserv>") - "</ipserv>".Length + 1)

                    ' Se asigna el valor de la etiqueta
                    s_ipServ = ls_info

                    ' Se verifica si se obtuvo el valor para la etiqueta
                    If s_ipServ.Trim = "" Then
                        lb_ind = True
                        ls_etqErr = ls_etq
                    End If

                End If

                ls_etq = "<svrusr>"
                If ls_line.ToLower.Contains(ls_etq) Then

                    ' Se declara una variable para el valor de la etiqueta
                    Dim ls_info As String = ""

                    ' Se obtiene el valor de la etiqueta
                    ls_info = Mid(ls_line, ls_line.ToLower.IndexOf("<svrusr>") + "<svrusr>".Length + 1, ls_line.ToLower.IndexOf("</svrusr>") - "</svrusr>".Length + 1)

                    ' Se asigna el valor de la etiqueta
                    s_svrUsr = ls_info

                    ' Se verifica si se obtuvo el valor para la etiqueta
                    If s_svrUsr.Trim = "" Then
                        lb_ind = True
                        ls_etqErr = ls_etq
                    End If

                End If

                ls_etq = "<svrpwd>"
                If ls_line.ToLower.Contains(ls_etq) Then

                    ' Se declara una variable para el valor de la etiqueta
                    Dim ls_info As String = ""

                    ' Se obtiene el valor de la etiqueta
                    ls_info = Mid(ls_line, ls_line.ToLower.IndexOf("<svrpwd>") + "<svrpwd>".Length + 1, ls_line.ToLower.IndexOf("</svrpwd>") - "</svrpwd>".Length + 1)

                    ' Se asigna el valor de la etiqueta
                    s_svrPwd = ls_info

                    ' Se verifica si se obtuvo el valor para la etiqueta
                    If s_svrPwd.Trim = "" Then
                        lb_ind = True
                        ls_etqErr = ls_etq
                    End If

                End If

            Next

            ' Se retorna el resultado
            If lb_ind = True Then
                Return "No se obtuvo uno de los valores de la conexion. (" & ls_etq & ")"
            Else
                Return ""
            End If

        Catch ex As Exception
            Return "Etiqueta " & ls_etq & " :" & ex.Message
        End Try
    End Function

End Class
